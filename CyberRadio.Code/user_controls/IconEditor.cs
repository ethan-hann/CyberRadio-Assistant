using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using RadioExt_Helper.utility.event_args;
using WIG.Lib.Models;
using WIG.Lib.Utility;
using Icon = RadioExt_Helper.models.Icon;

namespace RadioExt_Helper.user_controls
{//TODO: Translations
    public sealed partial class IconEditor : UserControl, IEditor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public EditorType Type { get; set; } = EditorType.IconEditor;
        public TrackableObject<Station> Station { get; }

        public WolvenIcon Icon { get; private set; }
        private string _iconPath = string.Empty;

        private readonly ImageList _tabImageList = new();
        private bool _isImporting;

        public IconEditor(TrackableObject<Station> station, ref WolvenIcon icon)
        {
            InitializeComponent();

            Station = station;
            Icon = icon;
            Dock = DockStyle.Fill;

            _tabImageList.Images.Add("import", Resources.download_16x16);
            _tabImageList.Images.Add("export", Resources.export_16x16);
            tabImportExport.ImageList = _tabImageList;
            tabImport.ImageKey = @"import";
            tabExport.ImageKey = @"export";
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
            if (Path.Exists(Icon.ImagePath))
                Icon.EnsureImage();

            picStationIcon.Image = Icon.IconImage ?? Resources.drag_and_drop_128x128;
            lblEditingText.Text = $"Editing Icon: {Icon.IconName}";

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
            Icon.ImagePath = _iconPath;
            Icon.IconName = Path.GetFileNameWithoutExtension(_iconPath);

            txtImagePath.Text = _iconPath;
            lblEditingText.Text = $"Editing Icon: {Icon.IconName}";
        }

        private void btnImportIcon_Click(object sender, EventArgs e)
        {
            if (_isImporting) return;

            pgProgress.Visible = true;
            _isImporting = true;

            Task.Run(async () =>
            {
                var stagingIcons = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;
                if (stagingIcons == string.Empty)
                {
                    AddStatusRow("Staging path not set.");
                    return;
                }

                var outputPath = Path.Combine(stagingIcons, "icons");
                var icon = await IconManager.Instance.ImportIconImageAsync(Icon.ImagePath, txtAtlasName.Text, outputPath);
                if (icon == null)
                    AddStatusRow("Failed to import icon.");
                else
                    Icon = icon;
                SetIconFields();
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
                txtArchivePath.Text = Icon.ArchivePath;
                txtSha256Hash.Text = Icon.Sha256HashOfArchiveFile;
                txtImagePath.Text = Icon.ImagePath;
                txtIconPath.Text = Icon?.CustomIcon?.InkAtlasPath;
                txtIconPart.Text = Icon?.CustomIcon?.InkAtlasPart;
                //Icon?.EnsureImage();
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
    }
}
