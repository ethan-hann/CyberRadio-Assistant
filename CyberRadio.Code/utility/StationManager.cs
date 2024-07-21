using System.ComponentModel;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
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
        private readonly Dictionary<Guid, Pair<TrackableObject<Station>, StationEditor>> _stations = [];

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
        private readonly Dictionary<Guid, int> _newStations = [];

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
        /// The current list of stations managed by the manager as a binding list. Auto-updates when stations are added or removed.
        /// </summary>
        public BindingList<TrackableObject<Station>> StationsAsBindingList { get; } = []; //_stations.Values.Select(pair => pair.Key).ToBindingList();

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
            try
            {
                ClearStations();
                foreach (var d in FileHelper.SafeEnumerateDirectories(directory))
                    ProcessDirectory(d);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error loading stations from directory.");
            }
        }

        /// <summary>
        /// Adds a new station and editor to the manager.
        /// </summary>
        /// <param name="station">The station to add.</param>
        /// <returns>The <see cref="Guid"/> of the newly added station.</returns>
        public Guid AddStation(TrackableObject<Station> station)
        {
            try
            {
                lock (_stations)
                {
                    CheckForDuplicateStation(station.Id);

                    if (_stations.ContainsKey(station.Id))
                    {
                        _newStations[station.Id]++;
                        return station.Id;
                    }

                    var editor = new StationEditor(station);
                    _stations[station.Id] = new Pair<TrackableObject<Station>, StationEditor>(station, editor);
                    editor.StationUpdated += Editor_StationUpdated;

                    StationsAsBindingList.Add(station);

                    StationAdded?.Invoke(this, station.Id);
                    return station.Id;
                }
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error adding station to manager.");
                return Guid.Empty;
            }
        }

        private void Editor_StationUpdated(object? sender, EventArgs e)
        {
            if (sender is StationEditor editor)
                StationUpdated?.Invoke(this, editor.Station.Id);
        }

        /// <summary>
        /// Removes a station, and it's editor from the manager.
        /// </summary>
        /// <param name="stationId">The station to remove, by ID.</param>
        public void RemoveStation(Guid stationId)
        {
            try
            {
                lock (_stations)
                {
                    if (!_stations.TryGetValue(stationId, out var pair)) return;
                    pair.Value.Dispose();
                    _stations.Remove(stationId);
                    StationsAsBindingList.Remove(StationsAsBindingList.First(s => s.Id == stationId));

                    StationRemoved?.Invoke(this, stationId);
                }
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error removing station from manager.");
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
            try
            {
                lock (_stations)
                {
                    foreach (var pair in _stations.Values)
                        pair.Value.Dispose();
                    _stations.Clear();
                    StationsAsBindingList.Clear();

                    StationsCleared?.Invoke(this, EventArgs.Empty);
                }
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error clearing stations from manager.");
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
            try
            {
                if (stationId == null) return null;
                var id = (Guid)stationId;
                lock (_stations)
                {
                    return _stations.TryGetValue(id, out var pair) ? pair : null;
                }
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error getting station from manager.");
                return null;
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
            AddStation(trackedStation);

            return trackedStation.Id; //TODO: Check if this is correct. Add logic to create a new blank station and update new stations' dictionary.
        }

        /// <summary>
        /// Stops all music players for all stations in the manager.
        /// </summary>
        public void StopAllMusicPlayers()
        {
            try
            {
                foreach (var editor in _stations.Values.Select(pair => pair.Value))
                    editor.GetMusicPlayer().StopStream();
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error stopping all music players.");
            }
        }

        /// <summary>
        /// Set the station's active status to the new status and raise the <see cref="StationUpdated"/> event.
        /// </summary>
        /// <param name="stationId">The ID of the station to change the status of.</param>
        /// <param name="newStatus">The new status of the station: <c>true</c> = enabled; <c>false</c> = disabled.</param>
        public void ChangeStationStatus(Guid stationId, bool newStatus)
        {
            try
            {
                if (!_stations.TryGetValue(stationId, out var pair)) return;

                pair.Key.TrackedObject.MetaData.IsActive = newStatus;
                StationsAsBindingList.First(s => s.Id == stationId).TrackedObject.MetaData.IsActive = newStatus;

                StationUpdated?.Invoke(this, stationId);
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error changing station status.");
            }
        }

        /// <summary>
        /// Check the pending save status of the station with the specified ID.
        /// </summary>
        /// <param name="stationId">The ID of the station to check.</param>
        /// <returns><c>true</c> if there are pending changes; <c>false</c> otherwise.</returns>
        public bool CheckStatus(Guid stationId)
        {
            try
            {
                return _stations[stationId].Key.CheckPendingSaveStatus() & StationsAsBindingList.First(s => s.Id == stationId).CheckPendingSaveStatus();
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error checking station status.");
                return false;
            }
        }

        /// <summary>
        /// Occurs when a station is updated.
        /// </summary>
        /// <param name="stationId">The <see cref="Guid"/> of the station that was updated.</param>
        public void OnStationUpdated(Guid stationId)
        {
            var newName = CheckForDuplicateStation(stationId);
            CheckStatus(stationId);

            _stations[stationId].Value.UpdateStationName(newName);
        }

        /// <summary>
        /// Checks for duplicate station names and gets an updated name if a duplicate is found.
        /// </summary>
        /// <param name="stationId">The ID of the station to check.</param>
        /// <returns>The updated station name.</returns>
        public string CheckForDuplicateStation(Guid stationId)
        {
            if (!_stations.TryGetValue(stationId, out var pair)) return string.Empty;

            var station = pair.Key.TrackedObject;
            var originalName = station.MetaData.DisplayName;
            var updatedName = originalName;
            var duplicateCount = 0;

            foreach (var existingStation in _stations.Values.Select(p => p.Key))
            {
                if (existingStation.Id == stationId) continue;

                if (!existingStation.TrackedObject.MetaData.DisplayName.Equals(updatedName,
                        StringComparison.OrdinalIgnoreCase)) continue;

                duplicateCount++;
                updatedName = $"{originalName} ({duplicateCount})";
            }

            if (duplicateCount <= 0) return updatedName;

            station.MetaData.DisplayName = updatedName;
            StationsAsBindingList.First(s => s.Id == stationId).TrackedObject.MetaData.DisplayName = updatedName;

            StationUpdated?.Invoke(this, stationId);
            StationNameDuplicate?.Invoke(this, (stationId, updatedName));

            return updatedName;
        }

        /// <summary>
        /// Translates all station editors in the manager to the current language.
        /// </summary>
        public void TranslateEditors()
        {
            try
            {
                foreach (var editor in _stations.Values.Select(pair => pair.Value))
                    editor.Translate();
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error translating station editors.");
            }
        }

        /// <summary>
        /// Get a dictionary of stations with a pair indicating if the station has missing songs and the count of missing songs.
        /// </summary>
        /// <returns>A dictionary where the key is the station's ID and the value is a pair containing whether the station is missing songs and the count of missing songs.</returns>
        public Dictionary<Guid, Pair<bool, int>> CheckForMissingSongs()
        {
            try
            {
                var missingSongs = new Dictionary<Guid, Pair<bool, int>>();
                foreach (var pair in _stations)
                {
                    var station = pair.Value.Key.TrackedObject;
                    var missingCount = station.Songs.Count(song => !FileHelper.DoesFileExist(song.FilePath, false));
                    missingSongs[pair.Key] = new Pair<bool, int>(missingCount > 0, missingCount);
                }
                return missingSongs;
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error checking for missing songs.");
                return new Dictionary<Guid, Pair<bool, int>>();
            }
        }

        /// <summary>
        /// Get a dictionary of station ids with a value indicating if the station has pending saves.
        /// </summary>
        /// <returns>A dictionary where the key is the station's ID and the value indicates if the station is pending save.</returns>
        public Dictionary<Guid, bool> CheckPendingSave()
        {
            try
            {
                var pendingSave = new Dictionary<Guid, bool>();
                foreach (var pair in _stations)
                {
                    pendingSave[pair.Key] = pair.Value.Key.IsPendingSave 
                                            & StationsAsBindingList.First(s => s.Id == pair.Key).IsPendingSave;
                }
                return pendingSave;
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error checking for pending saves.");
                return new Dictionary<Guid, bool>();
            }
        }

        /// <summary>
        /// Processes a directory by loading the metadata and songs from the files in the directory and adding them to the manager.
        /// </summary>
        /// <param name="directory">The directory to process.</param>
        private void ProcessDirectory(string directory)
        {
            try
            {
                var files = FileHelper.SafeEnumerateFiles(directory).ToList();
                var metadata = files.Where(file => file.EndsWith("metadata.json")).Select(_metaDataJson.LoadJson)
                    .FirstOrDefault();
                var songList = files.Where(file => file.EndsWith("songs.sgls")).Select(_songListJson.LoadJson)
                    .FirstOrDefault() ?? [];
                var songFiles = files.Where(file => _validAudioExtensions.Contains(Path.GetExtension(file).ToLower()))
                    .ToList();

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
                AddStation(trackedStation);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationManager>().Error(ex, "Error processing directory.");
            }
        }
    }
}