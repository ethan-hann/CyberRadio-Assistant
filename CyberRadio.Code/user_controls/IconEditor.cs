using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using System.Text.RegularExpressions;
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

        public event EventHandler? IconImportStarted;
        public event EventHandler? IconImportFinished;

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
        private readonly ImageList _tabImages = new();

        private bool _isImporting;
        private bool _isReadOnly;

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
            //TODO: add translations
        }

        private void IconEditor_Load(object sender, EventArgs e)
        {
            IconManager.Instance.IconImportStarted += Instance_IconImportStarted;
            IconManager.Instance.CliStatus += Instance_IconImportStatus;

            _tabImages.Images.Add("icon_editing", Resources.customize_edit_16x16);
            editorTabs.ImageList = _tabImages;
            editorTabs.TabPages[0].ImageIndex = 0;

            SetIconFields();
            SetImagePreviewProperties();
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
            if (Icon.TrackedObject.CheckIconValid())
            {
                MessageBox.Show(Strings.IconEditor_IconAlreadyCreated_DragDrop, Strings.IconEditor_IconAlreadyCreated_DragDrop_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Revert to the icon that was already created.
                picStationIcon.SetImage(Icon.TrackedObject.ImagePath ?? string.Empty);
                SetImagePreviewProperties();
                return;
            }

            _iconPath = picStationIcon.ImagePath;
            Icon.TrackedObject.ImagePath = _iconPath;
            Icon.TrackedObject.IconName = Path.GetFileNameWithoutExtension(_iconPath);

            SetIconFields();

            txtImagePath.Text = _iconPath;
            txtIconName.Text = Icon.TrackedObject.IconName;
            txtAtlasName.Text = Icon.TrackedObject.IconName.ToLower();
            editorTabs.TabPages[0].Text = string.Format(Strings.IconEditor_txtIconName_TextChanged_Editing_Tab___0_, Icon.TrackedObject.IconName);

            SetImagePreviewProperties();

            IconUpdated?.Invoke(this, Icon);
        }

        private void btnImportIcon_Click(object sender, EventArgs e)
        {
            if (_isImporting) return;

            pgProgress.Visible = true;
            _isImporting = true;
            btnImportIcon.Enabled = false;
            btnCancelImport.Enabled = true;

            IconImportStarted?.Invoke(this, EventArgs.Empty);
            Task.Run(async () =>
            {
                var stagingPath = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;
                if (stagingPath == string.Empty)
                {
                    AddStatusRow("Staging path not set.");
                    return;
                }

                MakeEditorReadOnly();

                var icon = await IconManager.Instance.GenerateIconImageAsync(txtImagePath.Text, txtAtlasName.Text);
                if (icon == null)
                {
                    AddStatusRow("Failed to import icon.");

                    _isImporting = false;
                    UnmakeEditorReadOnly();
                }
                else
                {
                    Icon.TrackedObject.AtlasName = icon.AtlasName;
                    Icon.TrackedObject.ImagePath = icon.ImagePath;

                    Icon.TrackedObject.OriginalArchivePath = icon.OriginalArchivePath;
                    Icon.TrackedObject.Sha256HashOfArchiveFile = icon.Sha256HashOfArchiveFile;
                    Icon.TrackedObject.CustomIcon = icon.CustomIcon;

                    Icon.TrackedObject.ArchivePath = CopyIconToStaging(Icon.TrackedObject.OriginalArchivePath, stagingPath);

                    SetIconFields();
                    SetImagePreviewProperties();

                    IconUpdated?.Invoke(this, Icon);
                    _isImporting = false;
                }
            });

            pgProgress.Visible = false;
            btnCancelImport.Enabled = false;
            IconImportFinished?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Copy the icon's .archive to the staging directory.
        /// </summary>
        /// <param name="iconArchivePath">The path to the final .archive generated with Wolven Icon Generator.</param>
        /// <param name="stagingPath">The path to the staging folder.</param>
        /// <returns>The path to the archive file within staging.</returns>
        private string CopyIconToStaging(string? iconArchivePath, string stagingPath)
        {
            var outputPath = Path.Combine(stagingPath, "icons");
            try
            {
                if (!Directory.Exists(outputPath))
                    Directory.CreateDirectory(outputPath);

                if (string.IsNullOrEmpty(iconArchivePath))
                    throw new ArgumentNullException(nameof(iconArchivePath), GlobalData.Strings.GetString("IconEditorCopyIconToStagingNullEmpty"));

                var stagingIconPath = Path.Combine(outputPath, Path.GetFileName(iconArchivePath));
                File.Copy(iconArchivePath, stagingIconPath, true);
                return stagingIconPath;
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<IconEditor>("CopyIconToStaging").Error(ex);
            }
            return string.Empty;
        }

        private void btnCancelImport_Click(object sender, EventArgs e)
        {
            IconManager.Instance.CancelOperation();

            if (Icon.TrackedObject.CheckIconValid())
                MakeEditorReadOnly();
            else
                UnmakeEditorReadOnly();

            _isImporting = false;
        }

        private void SetIconFields()
        {
            this.SafeInvoke(() =>
            {
                txtArchivePath.Text = Icon.TrackedObject.ArchivePath;
                txtSha256Hash.Text = Icon.TrackedObject.Sha256HashOfArchiveFile;
                txtImagePath.Text = Icon.TrackedObject.ImagePath;
                txtIconPath.Text = Icon.TrackedObject.CustomIcon.InkAtlasPath;
                txtIconPart.Text = Icon.TrackedObject.CustomIcon.InkAtlasPart;
                txtAtlasName.Text = Icon.TrackedObject.AtlasName;
                txtIconName.Text = Icon.TrackedObject.IconName;
                editorTabs.TabPages[0].Text = string.Format(Strings.IconEditor_txtIconName_TextChanged_Editing_Tab___0_, Icon.TrackedObject.IconName);
            });
        }

        private void SetImagePreviewProperties()
        {
            this.SafeInvoke(() =>
            {
                // Dispose of the existing image in the PictureBox (if any) to avoid resource locking
                picStationIcon.ClearImage();
                Icon.TrackedObject.EnsureImage();
                picStationIcon.SetImage(Icon.TrackedObject.ImagePath);

                lblImageWidth.Text = $@"W: {picStationIcon.ImageProperties.Width} px";
                lblImageHeight.Text = $@"H: {picStationIcon.ImageProperties.Height} px";
                lblImageFormat.Text = picStationIcon.ImageProperties.ImageFormat.ToString();
                lblImageColorMode.Text = picStationIcon.ImageProperties.PixelFormat.ToString();

                if (picStationIcon.Image != null)
                {
                    if (picStationIcon.Image.Width != picStationIcon.Image.Height)
                    {
                        lblImageWidth.ForeColor = Color.OrangeRed;
                        lblImageHeight.ForeColor = Color.OrangeRed;

                        lblImageStatus.Text = Strings.IconEditor_ImageNotSquare;
                        tblWarning.Visible = true;
                    }
                    else
                    {
                        lblImageWidth.ForeColor = Color.Black;
                        lblImageHeight.ForeColor = Color.Black;
                        lblImageStatus.Text = string.Empty;
                        tblWarning.Visible = false;
                    }
                }

                //If the icon is valid, prevent changes to the fields.
                if (Icon.TrackedObject.CheckIconValid())
                    MakeEditorReadOnly();
                else
                    UnmakeEditorReadOnly();
            });
        }

        private void MakeEditorReadOnly()
        {
            this.SafeInvoke(() =>
            {
                txtAtlasName.ReadOnly = true;
                txtArchivePath.ReadOnly = true;
                txtSha256Hash.ReadOnly = true;
                txtImagePath.ReadOnly = true;
                txtIconPath.ReadOnly = true;
                txtIconPart.ReadOnly = true;
                btnImportIcon.Enabled = false;
                btnCancelImport.Enabled = _isImporting;

                _isReadOnly = true;
            });
        }

        private void UnmakeEditorReadOnly()
        {
            this.SafeInvoke(() =>
            {
                txtAtlasName.ReadOnly = false;
                btnImportIcon.Enabled = true;
                btnCancelImport.Enabled = _isImporting;

                _isReadOnly = false;
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
            if (!_isReadOnly)
            {
                // Store the current cursor position
                int cursorPosition = txtAtlasName.SelectionStart;

                // Update the text box content with the transformed text
                txtAtlasName.Text = FormatAtlasName(txtAtlasName.Text);

                // Restore the cursor position to where it was before the text was changed
                txtAtlasName.SelectionStart = Math.Min(cursorPosition, txtAtlasName.Text.Length);

                Icon.TrackedObject.AtlasName = txtAtlasName.Text;
            }

            IconUpdated?.Invoke(this, Icon);
        }

        private void txtIconName_TextChanged(object sender, EventArgs e)
        {
            editorTabs.TabPages[0].Text = string.Format(Strings.IconEditor_txtIconName_TextChanged_Editing_Tab___0_, txtIconName.Text);
            Icon.TrackedObject.IconName = txtIconName.Text;

            if (!_isReadOnly)
            {
                txtAtlasName.Text = FormatAtlasName(Icon.TrackedObject.IconName);
                Icon.TrackedObject.AtlasName = txtAtlasName.Text;
            }

            IconUpdated?.Invoke(this, Icon);
        }

        private string FormatAtlasName(string iconName)
        {
            string lowerName = iconName.ToLower();
            // Replace spaces with underscores and remove all special characters except underscores.
            return NoSpecialCharactersRegEx().Replace(lowerName, "_");
        }

        [GeneratedRegex(@"[^a-z0-9_]+")]
        private static partial Regex NoSpecialCharactersRegEx();

        private void btnResetPicView_Click(object sender, EventArgs e) => picStationIcon.ResetView();
    }
}
