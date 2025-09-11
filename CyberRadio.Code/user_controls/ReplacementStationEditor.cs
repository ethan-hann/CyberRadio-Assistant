using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.user_controls
{
    public sealed partial class ReplacementStationEditor : UserControl, IEditor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public EditorType Type { get; set; } = EditorType.StationEditor;

        /// <summary>
        /// Event that is raised when the station is updated.
        /// </summary>
        public event EventHandler? StationUpdated;

        /// <summary>
        /// Null for this editor type.
        /// </summary>
        public TrackableObject<AdditionalStation>? Station => null;

        /// <summary>
        /// Gets the tracked replacement station associated with this control.
        /// </summary>
        public TrackableObject<ReplacementStation>? ReplacedStation { get; }

        public ReplacementStationEditor(TrackableObject<ReplacementStation> station)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            ReplacedStation = station;
        }

        private void ReplacementStationEditor_Load(object sender, EventArgs e)
        {
            SuspendLayout();
            label1.Text = ReplacedStation?.TrackedObject?.VanillaStation?.StationName ?? "Unknown Station";

            Translate();

            ResumeLayout();
        }

        public void Translate()
        {
            //todo: implement translation
        }
    }
}
