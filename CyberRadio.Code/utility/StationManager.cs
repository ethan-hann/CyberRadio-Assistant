using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static StationManager? _instance;
        private static readonly object Lock = new();

        private readonly Dictionary<Guid, StationEditor> _stationEditorsDict = [];
        private readonly BindingList<TrackableObject<Station>> _stations = [];

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

        private StationManager() { }

        /// <summary>
        /// Add a new station to the manager.
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            _stations.Add(new TrackableObject<Station>(station));
        }

        /// <summary>
        /// Remove a station from the manager.
        /// </summary>
        /// <param name="station"></param>
        public void RemoveStation(Station station)
        {
            var stationToRemove = _stations.FirstOrDefault(s => s.TrackedObject.Equals(station));
            if (stationToRemove != null)
            {
                _stations.Remove(stationToRemove);
            }
        }
    }
}
