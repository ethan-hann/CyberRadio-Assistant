using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using RadioExt_Helper.utility.event_args;
using WIG.Lib.Models;
using WIG.Lib.Utility;

namespace RadioExt_Helper.user_controls
{//TODO: Translations
    public sealed partial class IconEditor : UserControl, IEditor
    {
        /// <summary>
        /// Event that occurs when the icon is updated.
        /// </summary>
        public event EventHandler<TrackableObject<WolvenIcon>>? IconUpdated;

        public Guid Id { get; set; } = Guid.NewGuid();
        public EditorType Type { get; set; } = EditorType.IconEditor;

        /// <summary>
        /// The station that the icon is associated with.
        /// </summary>
        public TrackableObject<Station> Station { get; }

        /// <summary>
        /// The icon that is being edited.
        /// </summary>
        public TrackableObject<WolvenIcon> Icon { get; }

        private string _iconPath = string.Empty;

        private bool _isImporting;

        /// <summary>
        /// Create a new icon editor.
        /// </summary>
        /// <param name="station"></param>
        /// <param name="icon"></param>
        public IconEditor(TrackableObject<Station> station, TrackableObject<WolvenIcon> icon)
        {
            InitializeComponent();

            Station = station;
            Icon = icon;
            Dock = DockStyle.Fill;
        }

        ~IconEditor()
        {
            IconManager.Instance.IconImportStarted -= Instance_IconImportStarted;
            IconManager.Instance.CliStatus -= Instance_IconImportStatus;
        }

        public void Translate()
        {

        }

        private void IconEditor_Load(object sender, EventArgs e)
        {
            if (Path.Exists(Icon.TrackedObject.ImagePath))
                Icon.TrackedObject.EnsureImage();

            picStationIcon.Image = Icon.TrackedObject.IconImage ?? Resources.drag_and_drop_128x128;
            lblEditingText.Text = $"Editing Icon: {Icon.TrackedObject.IconName}";

            IconManager.Instance.IconImportStarted += Instance_IconImportStarted;
            IconManager.Instance.CliStatus += Instance_IconImportStatus;

            SetIconFields();
            dgvStatus.Rows.Clear();
        }

        private void Instance_IconImportStatus(object? sender, StatusEventArgs e)
        {
            this.SafeInvoke(() => pgProgress.Value = e.ProgressPercentage);
            AddStatusRow(e.Message);
        }

        private void Instance_IconImportStarted(object? sender, StatusEventArgs e)
        {
            this.SafeInvoke(() => pgProgress.Value = 0);
            this.SafeInvoke(() => lblStatus.Text = e.Message);
        }

        private void AddStatusRow(string? status)
        {
            this.SafeInvoke(() =>
            {
                if (status == null) return;
                dgvStatus.Rows.Add([DateTime.Now, status]);
            });
        }

        private void picStationIcon_DragDrop(object sender, DragEventArgs e)
        {
            _iconPath = picStationIcon.ImagePath;
            Icon.TrackedObject.ImagePath = _iconPath;
            Icon.TrackedObject.IconName = Path.GetFileNameWithoutExtension(_iconPath);
            Icon.TrackedObject.EnsureImage();

            txtImagePath.Text = _iconPath;
            lblEditingText.Text = $"Editing Icon: {Icon.TrackedObject.IconName}";

            IconUpdated?.Invoke(this, Icon);
        }

        private void btnImportIcon_Click(object sender, EventArgs e)
        {
            if (_isImporting) return;

            pgProgress.Visible = true;
            _isImporting = true;
            btnImportIcon.Enabled = false;
            btnCancelImport.Enabled = true;

            Task.Run(async () =>
            {
                var stagingIcons = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;
                if (stagingIcons == string.Empty)
                {
                    AddStatusRow("Staging path not set.");
                    return;
                }

                var outputPath = Path.Combine(stagingIcons, "icons");
                var icon = await IconManager.Instance.GenerateIconImageAsync(txtImagePath.Text, txtAtlasName.Text, outputPath);
                if (icon == null)
                    AddStatusRow("Failed to import icon.");
                else
                {
                    Icon.TrackedObject.IconName = icon.AtlasName; //TODO: remove the set here and make the ICON NAME it's own entry field in the UI.
                    Icon.TrackedObject.ImagePath = icon.ImagePath;
                    Icon.TrackedObject.ArchivePath = icon.ArchivePath;
                    Icon.TrackedObject.Sha256HashOfArchiveFile = icon.Sha256HashOfArchiveFile;
                    Icon.TrackedObject.CustomIcon = icon.CustomIcon;

                    IconUpdated?.Invoke(this, Icon);
                }
                SetIconFields();

                btnImportIcon.Enabled = true;
                btnCancelImport.Enabled = false;
            });

            _isImporting = false;
            pgProgress.Visible = false;
        }

        private void btnCancelImport_Click(object sender, EventArgs e)
        {
            IconManager.Instance.CancelOperation();
            _isImporting = false;
        }

        private void SetIconFields()
        {
            this.SafeInvoke(() =>
            {
                txtArchivePath.Text = Icon.TrackedObject.ArchivePath;
                txtSha256Hash.Text = Icon.TrackedObject.Sha256HashOfArchiveFile;
                txtImagePath.Text = Icon.TrackedObject.ImagePath;
                txtIconPath.Text = Icon.TrackedObject.CustomIcon?.InkAtlasPath;
                txtIconPart.Text = Icon.TrackedObject.CustomIcon?.InkAtlasPart;
                if (Path.Exists(Icon.TrackedObject.ImagePath))
                    Icon.TrackedObject.EnsureImage();
            });
        }

        private void btnCopyImagePath_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtImagePath.Text, TextDataFormat.Text);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<IconEditor>("btnCopyImagePath_Click").Error(ex);
            }
        }

        private void btnCopyArchivePath_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtArchivePath.Text, TextDataFormat.Text);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<IconEditor>("btnCopyArchivePath_Click").Error(ex);
            }
        }

        private void btnCopySha256Hash_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtSha256Hash.Text, TextDataFormat.Text);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<IconEditor>("btnCopySha256Hash_Click").Error(ex);
            }
        }

        private void btnCopyIconPath_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtIconPath.Text, TextDataFormat.Text);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<IconEditor>("btnCopyIconPath_Click").Error(ex);
            }
        }

        private void btnCopyIconPart_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtIconPart.Text, TextDataFormat.Text);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<IconEditor>("btnCopyIconPart_Click").Error(ex);
            }
        }

        private void lblAtlasName_MouseEnter(object sender, EventArgs e)
        {
            lblStatus.Text = GlobalData.Strings.GetString("IconEditor_AtlasName_Hint") ?? "The name of the atlas file that the icon is stored in.";
        }

        private void lblSha256Hash_MouseEnter(object sender, EventArgs e)
        {
            lblStatus.Text = GlobalData.Strings.GetString("IconEditor_Sha256Hash_Hint") ?? "The SHA256 hash of the archive file.";
        }

        private void lblArchivePath_MouseEnter(object sender, EventArgs e)
        {
            lblStatus.Text = GlobalData.Strings.GetString("IconEditor_ArchivePath_Hint") ?? "The path to the archive file that contains the icon.";
        }

        private void lblImagePath_MouseEnter(object sender, EventArgs e)
        {
            lblStatus.Text = GlobalData.Strings.GetString("IconEditor_ImagePath_Hint") ?? "The path to the image file that created the icon.";
        }

        private void lblIconPath_MouseEnter(object sender, EventArgs e)
        {
            lblStatus.Text = GlobalData.Strings.GetString("IconEditor_IconPath_Hint") ?? "The path to the icon within the archive file. Autogenerated.";
        }

        private void lblIconPart_MouseEnter(object sender, EventArgs e)
        {
            lblStatus.Text = GlobalData.Strings.GetString("IconEditor_IconPart_Hint") ?? "The part of the icon within the icon path. Autogenerated.";
        }

        private void LblMouseLeave(object sender, EventArgs e)
        {
            lblStatus.Text = GlobalData.Strings.GetString("Ready") ?? "Ready";
        }

        private void txtAtlasName_TextChanged(object sender, EventArgs e)
        {
            Icon.TrackedObject.IconName = txtAtlasName.Text;
            Icon.TrackedObject.AtlasName = txtAtlasName.Text;

            lblEditingText.Text = $"Editing Icon: {Icon.TrackedObject.IconName}";

            IconUpdated?.Invoke(this, Icon);
        }
    }
}
