using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AetherUtils.Core.Files;
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
        /// The dictionary of stations and editors managed by the manager.
        /// </summary>
        private readonly Dictionary<Guid, Dictionary<TrackableObject<Station>, StationEditor>> _stations = [];

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

        #region Private Members
        //
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
        public BindingList<TrackableObject<Station>> Stations => _stations.Values.SelectMany(s => s.Keys).ToBindingList();

        /// <summary>
        /// Get a value indicating whether the station manager is empty (i.e., has no stations).
        /// </summary>
        public bool IsEmpty => Stations.Count == 0;

        #endregion

        private StationManager() { }

        /// <summary>
        /// Loads stations from the specified directory into the manager.
        /// </summary>
        /// <param name="directory">The directory to load stations from.</param>
        public void LoadStations(string directory)
        {
            foreach (var d in FileHelper.SafeEnumerateDirectories(directory))
                ProcessDirectory(d);
        }

        /// <summary>
        /// Add a new station and editor to the manager.
        /// </summary>
        /// <param name="station">The station to add.</param>
        public void AddStation(TrackableObject<Station> station)
        {
            lock (_stations)
            {
                if (_stations.ContainsKey(station.Id))
                {
                    _newStations[station.Id]++;
                    return;
                }

                _stations.Add(station.Id);
                //TODO: add event handlers?
            }
            Stations.Add(station);
            var editor = new StationEditor(station);
            lock (_stationEditorsDict)
            {
                _stationEditorsDict.Add(station.Id, editor);
                //TODO: add event handlers?
            }
            AddEditor(station);
        }

        /// <summary>
        /// Remove a station from the manager.
        /// </summary>
        /// <param name="station"></param>
        public void RemoveStation(Station station)
        {
            var stationToRemove = Stations.FirstOrDefault(s => s.TrackedObject.Equals(station));
            if (stationToRemove != null)
            {
                Stations.Remove(stationToRemove);
            }
        }

        /// <summary>
        /// Get the station count string, formatted with the enabled station count and localized.
        /// </summary>
        /// <returns>A formatted string showing enabled and disabled station counts: <c>Enabled Stations: {0} / {1}</c></returns>
        public string GetStationCount()
        {
            var enabledCount = Stations.Count(s => s.TrackedObject.MetaData.IsActive);
            return string.Format(StationCountFormat, enabledCount, Stations.Count);
        }

        /// <summary>
        /// Clear all stations and their editors from the manager.
        /// </summary>
        public void ClearStations()
        {
            Stations.Clear();
            lock (_stationEditorsDict)
            {
                foreach (var editor in _stationEditorsDict.Values)
                {
                    editor.Dispose();
                }
                _stationEditorsDict.Clear();
            }
        }

        /// <summary>
        /// Processes a directory by loading the metadata and songs from the files in the directory and adding them to the manager.
        /// </summary>
        /// <param name="directory">The directory to process.</param>
        private void ProcessDirectory(string directory)
        {
            var files = FileHelper.SafeEnumerateFiles(directory).ToList();
            var metadata = files.Where(file => file.EndsWith("metadata.json")).Select(_metaDataJson.LoadJson)
                .FirstOrDefault();
            var songList = files.Where(file => file.EndsWith("songs.sgls")).Select(_songListJson.LoadJson)
                .FirstOrDefault() ?? [];
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
            Stations.Add(trackedStation);
            AddEditor(trackedStation);
        }

        /// <summary>
        /// Add a new station editor to the manager.
        /// </summary>
        /// <param name="station">The station to add an editor for.</param>
        /// <returns></returns>
        private void AddEditor(TrackableObject<Station> station)
        {
            
        }

        /// <summary>
        /// Remove a station editor from the manager.
        /// </summary>
        /// <param name="station">The station to remove the editor for.</param>
        private void RemoveEditor(TrackableObject<Station> station)
        {
            lock (_stationEditorsDict)
            {
                if (!_stationEditorsDict.Remove(station.Id, out var editor)) return;
                //TODO: remove event handlers?
                editor.Dispose();
            }
        }

        /// <summary>
        /// Remove a station editor from the manager by the station ID.
        /// </summary>
        /// <param name="stationId">The ID of the station to remove the editor for.</param>
        private void RemoveEditor(Guid stationId)
        {
            lock (_stationEditorsDict)
            {
                if (!_stationEditorsDict.Remove(stationId, out var editor)) return;
                editor.Dispose();
            }
        }
    }
}
