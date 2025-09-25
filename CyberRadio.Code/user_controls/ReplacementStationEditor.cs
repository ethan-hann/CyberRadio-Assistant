using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.user_controls
{
    public sealed partial class ReplacementStationEditor : UserControl, IEditor
    {
        private readonly ImageList _tabImages = new();

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

        /// <summary>
        /// Create a new ReplacementStationEditor for the specified replacement station.
        /// </summary>
        /// <param name="station"></param>
        public ReplacementStationEditor(TrackableObject<ReplacementStation> station)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            SetTabImages();

            ReplacedStation = station;
        }

        private void ReplacementStationEditor_Load(object sender, EventArgs e)
        {
            SuspendLayout();

            SetDisplayTabValues();
            SetMusicTabValues();
            Translate();

            ResumeLayout();
        }

        /// <summary>
        /// Sets the images for the tabs.
        /// </summary>
        private void SetTabImages()
        {
            _tabImages.Images.Add("display", Resources.display_frame);
            _tabImages.Images.Add("music", Resources.sound_waves);
            tabControl.ImageList = _tabImages;
            tabDisplay.ImageKey = @"display";
            tabMusic.ImageKey = @"music";
        }

        /// <inheritdoc />
        public void Translate()
        {
            //todo: implement translation

            lblStatus.Text = Strings.Ready;
        }

        private void SetDisplayTabValues()
        {
            txtVanillaStationName.Text = ReplacedStation?.TrackedObject?.VanillaStation?.StationName ?? "Unknown Station";
            txtDisplayName.Text = ReplacedStation?.TrackedObject?.DisplayName ?? "New Replacement Station";
        }

        private void SetMusicTabValues()
        {

        }

        /// <summary>
        /// Resets the UI values to the defaults for the station.
        /// </summary>
        public void ResetUi()
        {
            SetDisplayTabValues();
            SetMusicTabValues();
        }

        /// <summary>
        /// Updates the station's display name. Does not affect the in-game name. Mainly used when the main form detects a duplication.
        /// </summary>
        /// <param name="newName"></param>
        public void UpdateStationName(string newName)
        {
            txtDisplayName.Text = newName;
            ReplacedStation!.TrackedObject.DisplayName = newName;
        }

        #region Hover Help
        private void lblVanillaName_MouseEnter(object sender, EventArgs e)
        {
            lblStatus.Text = Strings.VanillaStationNameHelp;
        }

        private void lblDisplayName_MouseEnter(object sender, EventArgs e)
        {
            lblStatus.Text = Strings.VanillaStationDisplayNameHelp;
        }

        /// <summary>
        /// Set the status text to the default text.
        /// </summary>
        private void ResetStatusText()
        {
            lblStatus.Text = Strings.Ready;
        }

        /// <summary>
        /// Occurs when the mouse leaves a label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lbl_MouseLeave(object sender, EventArgs e)
        {
            lblStatus.Text = Strings.Ready;
        }

        #endregion

        private void txtDisplayName_TextChanged(object sender, EventArgs e)
        {
            ReplacedStation!.TrackedObject.DisplayName = txtDisplayName.Text;
            StationUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
