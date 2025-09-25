namespace RadioExt_Helper.user_controls
{
    /// <summary>
    /// Control displayed when no station is selected in either the main station listbox or the vanilla station selector.
    /// </summary>
    public sealed partial class NoStationSelectedCtl : UserControl, IUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoStationSelectedCtl"/> class.
        /// </summary>
        public NoStationSelectedCtl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        /// <inheritdoc />
        public void Translate()
        {
            lblNoSelection.Text = Strings.VanillaStationSelector_NoStationSelected;
        }
    }
}
