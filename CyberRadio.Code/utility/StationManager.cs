using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AetherUtils.Core.Files;
using AetherUtils.Core.Structs;
using RadioExt_Helper.models;
using RadioExt_Helper.user_controls;

namespace RadioExt_Helper.utility
{
    /// <summary>
    /// This class is responsible for managing the radio stations. It provides methods for adding and removing stations as well as their station editors.
    /// Everything to do with stations is managed here.
    /// <para>This class cannot be instantiated. It is a singleton.</para>
    /// </summary>
    public class StationManager
    {
        #region Events
        /// <summary>
        /// Event that is raised when a station is added to the manager. Event data is the station ID.
        /// </summary>
        public event EventHandler<Guid>? StationAdded;

        /// <summary>
        /// Event that is raised when a station is removed from the manager. Event data is the station ID.
        /// </summary>
        public event EventHandler<Guid>? StationRemoved;

        /// <summary>
        /// Event that is raised when a station is updated in the manager. Event data is the station ID.
        /// </summary>
        public event EventHandler<Guid>? StationUpdated;

        /// <summary>
        /// Event that is raised when all stations are cleared from the manager.
        /// </summary>
        public event EventHandler? StationsCleared;

        /// <summary>
        /// Event that is raised when a station name is found to be a duplicate. Event data is a tuple with the station ID and the updated name.
        /// </summary>
        public event EventHandler<(Guid stationId, string updatedName)>? StationNameDuplicate;
        #endregion

        #region Static Members
        /// <summary>
        /// The singleton instance of the <see cref="StationManager"/> class.
        /// </summary>
        private static StationManager? _instance;

        /// <summary>
        /// The lock object used to synchronize access to the singleton instance.
        /// </summary>
        private static readonly object Lock = new();

        /// <summary>
        /// The format string for the station count.
        /// </summary>
        private static string StationCountFormat => GlobalData.Strings.GetString("EnabledStationsCount") ?? "Enabled Stations: {0} / {1}";
        #endregion

        #region Private, Readonly Members

        /// <summary>
        /// The dictionary of stations and editors managed by the manager. Each station has a unique ID which is used as the key.
        /// <para>Key: <c>Unique station ID</c></para>
        /// <para>Value: Pair with following:</para>
        ///     <list type="bullet">
        ///         <item>Key: <c>Trackable station</c></item>
        ///         <item>Value: <c>Station's editor control</c></item>
        ///     </list>
        /// </summary>
        private readonly Dictionary<Guid, Pair<TrackableObject<Station>, StationEditor>> _stations = new();

        /// <summary>
        /// The JSON object used to serialize and deserialize the metadata of a station.
        /// </summary>
        private readonly Json<MetaData> _metaDataJson = new();

        /// <summary>
        /// The JSON object used to serialize and deserialize the list of songs.
        /// </summary>
        private readonly Json<List<Song>> _songListJson = new();

        /// <summary>
        /// Dictionary of new station IDs and counts added to the manager during the current session.
        /// <para><c>Key:</c> Station ID</para>
        /// <para><c>Value:</c> Count of new stations added.</para>
        /// </summary>
        private readonly Dictionary<Guid, int> _newStations = new();

        /// <summary>
        /// A list of valid audio file extensions for song files.
        /// </summary>
        private readonly string?[] _validAudioExtensions =
            EnumHelper<ValidAudioFiles>.GetEnumDescriptions() as string[] ??
            EnumHelper<ValidAudioFiles>.GetEnumDescriptions().ToArray();

        #endregion

        #region Properties

        /// <summary>
        /// Get the singleton instance of the <see cref="StationManager"/> class.
        /// </summary>
        public static StationManager Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ??= new StationManager();
                }
            }
        }

        /// <summary>
        /// The current list of stations managed by the manager as a binding list.
        /// </summary>
        public BindingList<TrackableObject<Station>> StationsAsBindingList => _stations.Values.Select(pair => pair.Key).ToBindingList();

        /// <summary>
        /// The current list of stations managed by the manager as a list.
        /// </summary>
        public List<TrackableObject<Station>> StationsAsList => _stations.Values.Select(pair => pair.Key).ToList();

        /// <summary>
        /// Get a value indicating whether the station manager is empty (i.e., has no stations).
        /// </summary>
        public bool IsEmpty => _stations.Count == 0;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="StationManager"/> class.
        /// </summary>
        private StationManager() { }

        /// <summary>
        /// Loads stations from the specified directory into the manager, clearing any existing stations.
        /// </summary>
        /// <param name="directory">The directory to load stations from.</param>
        public void LoadStations(string directory)
        {
            ClearStations();
            foreach (var d in FileHelper.SafeEnumerateDirectories(directory))
                ProcessDirectory(d);
        }

        /// <summary>
        /// Adds a new station and editor to the manager.
        /// </summary>
        /// <param name="station">The station to add.</param>
        /// <returns>The <see cref="Guid"/> of the newly added station.</returns>
        public Guid AddStation(TrackableObject<Station> station, bool isNewStation = false)
        {
            lock (_stations)
            {
                if (_stations.ContainsKey(station.Id))
                {
                    _newStations[station.Id]++;
                    return station.Id;
                }

                var editor = new StationEditor(station);
                _stations[station.Id] = new Pair<TrackableObject<Station>, StationEditor>(station, editor);
                editor.StationUpdated += Editor_StationUpdated;

                CheckForDuplicateStations(station.Id, isNewStation);

                StationAdded?.Invoke(this, station.Id);
                return station.Id;
            }
        }

        private void Editor_StationUpdated(object? sender, EventArgs e)
        {
            if (sender is StationEditor editor)
                StationUpdated?.Invoke(this, editor.Station.Id);
        }

        /// <summary>
        /// Removes a station and it's editor from the manager.
        /// </summary>
        /// <param name="station">The station to remove.</param>
        public void RemoveStation(Guid stationId)
        {
            lock (_stations)
            {
                if (!_stations.TryGetValue(stationId, out var pair)) return;
                pair.Value.Dispose();
                _stations.Remove(stationId);
                StationRemoved?.Invoke(this, stationId);
            }
        }

        /// <summary>
        /// Gets the station count string, formatted with the enabled station count and localized.
        /// </summary>
        /// <returns>A formatted string showing enabled and disabled station counts: <c>Enabled Stations: {0} / {1}</c></returns>
        public string GetStationCount()
        {
            var enabledCount = _stations.Select(pair => pair.Value.Key.TrackedObject.MetaData.IsActive).Count(isActive => isActive);
            return string.Format(StationCountFormat, enabledCount, _stations.Count);
        }

        /// <summary>
        /// Clears all stations and their editors from the manager.
        /// </summary>
        public void ClearStations()
        {
            lock (_stations)
            {
                foreach (var pair in _stations.Values)
                    pair.Value.Dispose();
                _stations.Clear();
                StationsCleared?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Get the station and editor for the specified station ID.
        /// </summary>
        /// <param name="stationId">The ID of the station to get.</param>
        /// <returns>A pair where the key is the <see cref="TrackableObject{T}"/> and the value is the <see cref="StationEditor"/>; 
        /// or <c>null</c> if the <paramref name="stationId"/> did not exist in the manager.</returns>
        public Pair<TrackableObject<Station>, StationEditor>? GetStation(Guid? stationId)
        {
            if (stationId == null) return null;
            var id = (Guid)stationId;
            lock (_stations)
            {
                return _stations.TryGetValue(id, out var pair) ? pair : null;
            }
        }

        /// <summary>
        /// Add a new, blank station to the manager. The station will have the default metadata and no songs.
        /// The station will be added to the manager and the ID will be returned.
        /// </summary>
        /// <returns>The <see cref="Guid"/> of the newly added station.</returns>
        public Guid AddBlankStation()
        {
            var station = new Station()
            {
                MetaData =
                {
                    DisplayName = $"{GlobalData.Strings.GetString("NewStationListBoxEntry")}"
                }
            };
            var trackedStation = new TrackableObject<Station>(station);
            AddStation(trackedStation, true);

            return trackedStation.Id; //TODO: Check if this is correct. Add logic to create a new blank station and update new stations dictionary.
        }

        /// <summary>
        /// Stops all music players for all stations in the manager.
        /// </summary>
        public void StopAllMusicPlayers()
        {
            foreach (var editor in _stations.Values.Select(pair => pair.Value))
                editor.GetMusicPlayer().StopStream();
        }

        public void UpdateActiveStatus(Guid stationId, bool newStatus)
        {
            _stations[stationId].Key.TrackedObject.MetaData.IsActive = newStatus;
            StationUpdated?.Invoke(this, stationId);
        }

        public void CheckStatus(Guid stationId)
        {
            _stations[stationId].Key.CheckPendingSaveStatus();
        }

        /// <summary>
        /// Occurs when a station is updated.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="stationId">The <see cref="Guid"/> of the station that was updated.</param>
        public void OnStationUpdated(object sender, Guid stationId)
        {
            CheckForDuplicateStations(stationId, false);
            CheckStatus(stationId);
        }

        /// <summary>
        /// Checks for duplicate station names and gets an updated name if a duplicate is found.
        /// </summary>
        /// <param name="stationId">The <see cref="Guid"/> of the station to check against.</param>
        /// <param name="isNewStation">A value indicating whether the station is new and should not send events if a duplicate is found.</param>
        /// <returns>The updated station name or the original name if no duplicates are found.</returns>
        public string CheckForDuplicateStations(Guid stationId, bool isNewStation)
        {
            if (IsDuplicate(_stations[stationId].Key.TrackedObject.MetaData.DisplayName, out string updatedName))
            {
                lock (_stations)
                {
                    _stations[stationId].Key.TrackedObject.MetaData.DisplayName = updatedName;
                    if (!isNewStation)
                        StationNameDuplicate?.Invoke(this, (stationId, updatedName));
                }
                return updatedName;
            }

            return _stations[stationId].Key.TrackedObject.MetaData.DisplayName;
        }

        /// <summary>
        /// Translates all station editors in the manager to the current language.
        /// </summary>
        public void TranslateEditors()
        {
            foreach (var editor in _stations.Values.Select(pair => pair.Value))
                editor.Translate();
        }

        /// <summary>
        /// Get a dictionary of stations with a pair indicating if the station has missing songs and the count of missing songs.
        /// </summary>
        /// <returns>A dictionary where the key is the station's ID and the value is a pair containing whether the station is missing songs and the count of missing songs.</returns>
        public Dictionary<Guid, Pair<bool, int>> CheckForMissingSongs()
        {
            var missingSongs = new Dictionary<Guid, Pair<bool, int>>();
            foreach (var pair in _stations)
            {
                var station = pair.Value.Key.TrackedObject;
                var missingCount = station.Songs.Count(song => !FileHelper.DoesFileExist(song.FilePath, false));
                missingSongs[pair.Key] = new Pair<bool, int>(missingCount > 0, missingCount);
            }
            return missingSongs;
        }

        /// <summary>
        /// Get a dictionary of station ids with a value indicating if the station has pending saves.
        /// </summary>
        /// <returns>A dictionary where the key is the station's ID and the value indicates if the station is pending save.</returns>
        public Dictionary<Guid, bool> CheckPendingSave()
        {
            var pendingSave = new Dictionary<Guid, bool>();
            foreach (var pair in _stations)
            {
                pendingSave[pair.Key] = pair.Value.Key.IsPendingSave;
            }
            return pendingSave;
        }

        /// <summary>
        /// Checks for duplicate station names and updates the name if a duplicate is found.
        /// </summary>
        /// <param name="stationName">The name of the station to check.</param>
        /// <param name="updatedName">The updated name of the station; equal to the original name if no duplicates are found.</param>
        /// <returns><c>true</c> if duplicate stations are found; <c>false</c> otherwise.</returns>
        private bool IsDuplicate(string stationName, out string updatedName)
        {
            updatedName = stationName;
            if (!_stations.Values.Any(p => p.Key.TrackedObject.MetaData.DisplayName
                                        .Equals(stationName, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            int count = 1;
            while (_stations.Values.Any(p => p.Key.TrackedObject.MetaData.DisplayName
                                        .Equals($"{stationName} ({count})", StringComparison.OrdinalIgnoreCase)))
            {
                count++;
            }

            updatedName = $"{stationName} ({count})";
            return true;
        }

        /// <summary>
        /// Processes a directory by loading the metadata and songs from the files in the directory and adding them to the manager.
        /// </summary>
        /// <param name="directory">The directory to process.</param>
        private void ProcessDirectory(string directory)
        {
            var files = FileHelper.SafeEnumerateFiles(directory).ToList();
            var metadata = files.Where(file => file.EndsWith("metadata.json")).Select(_metaDataJson.LoadJson).FirstOrDefault();
            var songList = files.Where(file => file.EndsWith("songs.sgls")).Select(_songListJson.LoadJson).FirstOrDefault() ?? [];
            var songFiles = files.Where(file => _validAudioExtensions.Contains(Path.GetExtension(file).ToLower())).ToList();

            if (metadata == null) return;

            if (songList.Count == 0)
            {
                songFiles.ForEach(path =>
                {
                    var song = Song.FromFile(path);
                    if (song != null)
                        songList.Add(song);
                });
            }

            var station = new Station { MetaData = metadata, Songs = songList };
            var trackedStation = new TrackableObject<Station>(station);
            AddStation(trackedStation, true);
        }
    }
}