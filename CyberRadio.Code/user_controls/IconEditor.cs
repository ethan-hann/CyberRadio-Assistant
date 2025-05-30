﻿// IconEditor.cs : RadioExt-Helper
// Copyright (C) 2025  Ethan Hann
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Globalization;
using System.Text.RegularExpressions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.custom_controls;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using WIG.Lib.Models;
using WIG.Lib.Utility;

namespace RadioExt_Helper.user_controls;

public sealed partial class IconEditor : UserControl, IEditor
{
    private readonly LogViewerControl _logViewer;
    private readonly ImageList _tabImages;

    private CancellationTokenSource _cancellationTokenSource;

    private string _iconPath = string.Empty;
    private bool _isExtracting;

    private bool _isImporting;
    private bool _isReadOnly;

    /// <summary>
    /// Create a new icon editor.
    /// </summary>
    /// <param name="station">The station associated with the editor.</param>
    /// <param name="icon">The icon to initialize the editor with.</param>
    /// <param name="type">The <see cref="IconEditorType"/> to set the initial state of the editor.</param>
    public IconEditor(TrackableObject<Station> station, TrackableObject<WolvenIcon> icon, IconEditorType type)
    {
        InitializeComponent();

        Station = station;
        Icon = icon;
        Dock = DockStyle.Fill;
        IconEditorType = type;

        _cancellationTokenSource = new CancellationTokenSource();

        //Set up the log viewer control
        _logViewer = new LogViewerControl
        {
            Dock = DockStyle.Fill
        };

        if (Icon.TrackedObject.CheckIconValid())
            _logViewer.Identifier = Icon.TrackedObject.IsFromArchive
                ? Icon.TrackedObject.ArchivePath
                : Icon.TrackedObject.AtlasName;

        panelLogControl.Controls.Add(_logViewer);

        _tabImages = new ImageList();
    }

    public IconEditorType IconEditorType { get; set; }

    /// <summary>
    /// The icon that is being edited.
    /// </summary>
    public TrackableObject<WolvenIcon> Icon { get; }

    public Guid Id { get; set; } = Guid.NewGuid();
    public EditorType Type { get; set; } = EditorType.IconEditor;

    /// <summary>
    /// The station that the icon is associated with.
    /// </summary>
    public TrackableObject<Station> Station { get; }

    public void Translate()
    {
        editorTabs.TabPages[0].Text = string.Format(Strings.IconEditor_Translate_Editing_Icon___0_, @"custom_icon");
        lblArchivePath.Text = Strings.IconEditor_Translate_Archive_Path_;
        lblAtlasName.Text = Strings.IconEditor_Translate_Atlas_Name_;
        lblIconName.Text = Strings.IconEditor_Translate_Icon_Name_;
        lblIconPath.Text = Strings.IconEditor_Translate_Icon_Path_;
        lblIconPart.Text = Strings.IconEditor_Translate_Icon_Part_;
        lblImagePath.Text = Strings.IconEditor_Translate_Image_Path_;
        lblSha256Hash.Text = Strings.IconEditor_Translate_SHA256_Hash_;
        lblImageWidth.Text = string.Format(Strings.IconEditor_Translate_W___0_, 0);
        lblImageHeight.Text = string.Format(Strings.IconEditor_Translate_H___0_, 0);
        lblImageFormat.Text = string.Format(Strings.IconEditor_Translate_Format___0_, "Undefined");
        lblImageColorMode.Text = string.Format(Strings.IconEditor_Translate_Color_Mode___0_, "Undefined");
        lblImageStatus.Text = Strings.IconEditor_Translate_No_Image_Issues;
        btnImportIcon.Text = Strings.IconEditor_Translate_Start_Import;
        btnStartExtract.Text = Strings.IconEditor_Translate_Start_Extraction;
        btnCancelImport.Text = Strings.IconEditor_Translate_Cancel_Action;

        grpIconPreview.Text = Strings.IconEditor_Translate_Preview;
        grpIconProperties.Text = Strings.IconEditor_Translate_Properties;
        grpActions.Text = Strings.IconEditor_Translate_Actions;

        lblStatus.Text = Strings.Ready;

        _logViewer.Translate();
    }

    /// <summary>
    /// Event that occurs when the icon is updated.
    /// </summary>
    public event EventHandler<TrackableObject<WolvenIcon>>? IconUpdated;

    /// <summary>
    /// Event that occurs when an icon import has started.
    /// </summary>
    public event EventHandler? IconImportStarted;

    /// <summary>
    /// Event that occurs when an icon import has finished.
    /// </summary>
    public event EventHandler? IconImportFinished;

    /// <summary>
    /// Event that occurs when an icon extraction has started.
    /// </summary>
    public event EventHandler? IconExtractStarted;

    /// <summary>
    /// Event that occurs when an icon extraction has finished.
    /// </summary>
    public event EventHandler? IconExtractFinished;

    ~IconEditor()
    {
        IconManager.Instance.IconImportStarted -= Instance_OperationStarted;
        IconManager.Instance.CliStatus -= Instance_OperationStatus;
    }

    /// <summary>
    /// Set the log identifier for the log viewer based on whether the icon is valid and whether it is from an archive or a .png originally.
    /// </summary>
    public void SetLogIdentifier()
    {
        var identifier = Icon.TrackedObject.CheckIconValid()
            ? Icon.TrackedObject.IsFromArchive ? Icon.TrackedObject.ArchivePath : Icon.TrackedObject.AtlasName
            : null;
        _logViewer.Identifier = identifier;
        _logViewer.LoadRelevantLogEntries();
        _logViewer.DisplayLastLines(50);
    }

    private void IconEditor_Load(object sender, EventArgs e)
    {
        IconManager.Instance.IconImportStarted += Instance_OperationStarted;
        IconManager.Instance.IconExportStarted += Instance_OperationStarted;
        IconManager.Instance.CliStatus += Instance_OperationStatus;

        _tabImages.Images.Add("icon_editing", Resources.customize_edit_16x16);
        editorTabs.ImageList = _tabImages;
        editorTabs.TabPages[0].ImageIndex = 0;

        SetIconFields();
        SetLogIdentifier();

        if (IconEditorType == IconEditorType.FromArchive)
        {
            picStationIcon.Enabled = false;
            picStationIcon.AllowDrop = false;
            btnImportIcon.Visible = false;
            btnStartExtract.Enabled = true;
            SetImagePreviewProperties(true);
        }
        else
        {
            btnStartExtract.Visible = false;
            btnStartExtract.Enabled = false;
            SetImagePreviewProperties();
        }

        SetFieldsBasedOnIsFromArchive();
    }

    private void SetFieldsBasedOnIsFromArchive()
    {
        try
        {
            Invoke(() =>
            {
                if (!Icon.TrackedObject.IsFromArchive)
                {
                    if (Path.Exists(Icon.TrackedObject.ImagePath))
                    {
                        txtAtlasName.Enabled = false;
                        btnStartExtract.Enabled = false;
                    }
                    else
                    {
                        txtAtlasName.Enabled = true;
                        btnStartExtract.Enabled = true;
                        lblImageStatus.Text = Strings
                            .IconEditor_SetFieldsBasedOnImagePath_The_extracted_image_could_not_be_found_;
                    }
                }
                else
                {
                    txtAtlasName.Enabled = false;

                    if (Path.Exists(Icon.TrackedObject.ImagePath))
                    {
                        txtAtlasName.Text = Icon.TrackedObject.AtlasName;
                    }
                    else
                    {
                        txtAtlasName.Text = string.Empty;
                        txtAtlasName.PlaceholderText =
                            Strings.IconEditor_SetFieldsBasedOnImagePath_To_be_determined_from_archive___;
                        lblImageStatus.Text = Strings
                            .IconEditor_SetFieldsBasedOnImagePath_The_extracted_image_could_not_be_found_;
                    }

                    // We only want to disable the import/extract button if the icon is already created. No need to extract again.
                    btnImportIcon.Enabled = Icon.TrackedObject.IsFromArchive
                        ? !Path.Exists(Icon.TrackedObject.ImagePath)
                        : !Path.Exists(Icon.TrackedObject.ArchivePath);
                    btnStartExtract.Enabled = btnImportIcon.Enabled;
                }
            });
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<IconEditor>("SetFieldsBasedOnIsFromArchive").Error(ex);
        }
    }

    private void Instance_OperationStatus(object? sender, StatusEventArgs e)
    {
        try
        {
            this.SafeInvoke(() => pgProgress.Value = e.ProgressPercentage);
            AddStatusRow(e.Message);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<IconEditor>("Instance_OperationStatus").Error(ex);
        }
    }

    private void Instance_OperationStarted(object? sender, StatusEventArgs e)
    {
        try
        {
            this.SafeInvoke(() => pgProgress.Value = 0);
            AddStatusRow(e.Message);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<IconEditor>("Instance_OperationStarted").Error(ex);
        }
    }

    private void AddStatusRow(string? status)
    {
        this.SafeInvoke(() =>
        {
            if (status == null) return;

            //Pass the status to the log viewer
            _logViewer.AddLogEntry(DateTime.Now, status);
        });
    }

    private void picStationIcon_DragDrop(object sender, DragEventArgs e)
    {
        if (Icon.TrackedObject.CheckIconValid())
        {
            MessageBox.Show(Strings.IconEditor_IconAlreadyCreated_DragDrop,
                Strings.IconEditor_IconAlreadyCreated_DragDrop_Caption,
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
        editorTabs.TabPages[0].Text =
            string.Format(Strings.IconEditor_Translate_Editing_Icon___0_, Icon.TrackedObject.IconName);

        SetImagePreviewProperties();

        SetLogIdentifier();

        IconUpdated?.Invoke(this, Icon);
    }

    private void btnImportIcon_Click(object sender, EventArgs e)
    {
        if (_isImporting || _isExtracting) return;

        pgProgress.Visible = true;
        _isImporting = true;
        btnImportIcon.Enabled = false;
        btnCancelImport.Enabled = true;

        const int maxValue = 250;
        var progress = new Progress<int>(value =>
        {
            // Scale progress to fit between 0 and 100
            var scaledValue = value * 100 / maxValue;
            Invoke(() => pgProgress.Value = Math.Min(100, scaledValue));
        });

        _logViewer.Identifier = txtAtlasName.Text;

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

            // Create a new CancellationTokenSource for each operation
            _cancellationTokenSource = new CancellationTokenSource();

            var icon = await IconManager.Instance.GenerateIconImageAsync(txtImagePath.Text, txtAtlasName.Text, progress,
                true, _cancellationTokenSource.Token);
            if (icon == null)
            {
                AddStatusRow("Failed to import icon.");
                UnmakeEditorReadOnly();
            }
            else
            {
                Icon.TrackedObject.AtlasName = icon.AtlasName;
                Icon.TrackedObject.OriginalArchivePath = icon.OriginalArchivePath;
                Icon.TrackedObject.Sha256HashOfArchiveFile = icon.Sha256HashOfArchiveFile;
                Icon.TrackedObject.CustomIcon = icon.CustomIcon;

                var newPaths = CopyIconToStaging(Icon.TrackedObject.OriginalArchivePath, icon.ImagePath, stagingPath);

                Icon.TrackedObject.ArchivePath = newPaths.archiveStagingPath;
                Icon.TrackedObject.ImagePath = newPaths.pngStagingPath;
                Icon.TrackedObject.IconId = icon.IconId;

                SetImagePreviewProperties();
                SetFieldsBasedOnIsFromArchive();
                SetIconFields();

                IconUpdated?.Invoke(this, Icon);
            }

            _isImporting = false;
            Invoke(() => pgProgress.Visible = false);
            Invoke(() => btnCancelImport.Enabled = false);
            IconImportFinished?.Invoke(this, EventArgs.Empty);
        });
    }

    private void btnStartExtract_Click(object sender, EventArgs e)
    {
        if (_isImporting || _isExtracting) return;

        pgProgress.Visible = true;
        _isExtracting = true;
        btnImportIcon.Enabled = false;
        btnStartExtract.Enabled = false;
        btnCancelImport.Enabled = true;

        const int maxValue = 250;
        var progress = new Progress<int>(value =>
        {
            // Scale progress to fit between 0 and max value (250)
            var scaledValue = value * 100 / maxValue;
            Invoke(() => pgProgress.Value = Math.Min(100, scaledValue));
        });

        _logViewer.Identifier = Icon.TrackedObject.ArchivePath;

        IconExtractStarted?.Invoke(this, EventArgs.Empty);
        Task.Run(async () =>
        {
            var stagingPath = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;
            if (stagingPath == string.Empty)
            {
                AddStatusRow("Staging path not set.");
                return;
            }

            MakeEditorReadOnly();

            // Create a new CancellationTokenSource for each operation
            _cancellationTokenSource = new CancellationTokenSource();

            var icon = await IconManager.Instance.ExtractIconImageAsync(txtArchivePath.Text, progress, true,
                _cancellationTokenSource.Token);
            if (icon == null)
            {
                AddStatusRow("Failed to extract icon.");
                UnmakeEditorReadOnly();
            }
            else
            {
                Icon.TrackedObject.AtlasName = icon.AtlasName;
                Icon.TrackedObject.ArchivePath = icon.ArchivePath;
                Icon.TrackedObject.OriginalArchivePath = icon.OriginalArchivePath;
                Icon.TrackedObject.Sha256HashOfArchiveFile = icon.Sha256HashOfArchiveFile;
                Icon.TrackedObject.CustomIcon = icon.CustomIcon;

                var newPaths = CopyIconToStaging(Icon.TrackedObject.ArchivePath, icon.ImagePath, stagingPath);

                Icon.TrackedObject.ArchivePath = newPaths.archiveStagingPath;
                Icon.TrackedObject.ImagePath = newPaths.pngStagingPath;

                SetIconFields();
                SetImagePreviewProperties();
                SetFieldsBasedOnIsFromArchive();

                IconUpdated?.Invoke(this, Icon);
            }

            _isExtracting = false;
            Invoke(() => pgProgress.Visible = false);
            Invoke(() => btnCancelImport.Enabled = false);
            IconExtractFinished?.Invoke(this, EventArgs.Empty);
        });
    }

    /// <summary>
    /// Copy the icon's .archive and .png file to the staging directory.
    /// </summary>
    /// <param name="iconArchivePath">The path to the final .archive generated with Wolven Icon Generator.</param>
    /// <param name="imagePath">The path to .png image file for the icon.</param>
    /// <param name="stagingPath">The path to the staging folder.</param>
    /// <returns>The path to the archive file within staging.</returns>
    private (string archiveStagingPath, string pngStagingPath) CopyIconToStaging(string? iconArchivePath,
        string? imagePath, string stagingPath)
    {
        var outputPath = Path.Combine(stagingPath, "icons");
        try
        {
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            if (string.IsNullOrEmpty(iconArchivePath))
                throw new ArgumentNullException(nameof(iconArchivePath), Strings.IconEditorCopyIconToStagingNullEmpty);

            if (string.IsNullOrEmpty(imagePath))
                throw new ArgumentNullException(nameof(imagePath), Strings.IconEditorCopyIconToStagingImageNullEmpty);

            var stagingIconPath = Path.Combine(outputPath, Path.GetFileName(iconArchivePath));
            var stagingPngPath = Path.Combine(outputPath, Path.GetFileName(imagePath));
            File.Copy(iconArchivePath, stagingIconPath, true);
            File.Copy(imagePath, stagingPngPath, true);
            return (stagingIconPath, stagingPngPath);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<IconEditor>("CopyIconToStaging").Error(ex);
        }

        return (string.Empty, string.Empty);
    }

    private void btnCancelImport_Click(object sender, EventArgs e)
    {
        IconManager.Instance.CancelOperation();

        if (Icon.TrackedObject.CheckIconValid())
            MakeEditorReadOnly();
        else
            UnmakeEditorReadOnly();

        _isImporting = false;
        _isExtracting = false;
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
            editorTabs.TabPages[0].Text = string.Format(Strings.IconEditor_Translate_Editing_Icon___0_,
                Icon.TrackedObject.IconName);
        });
    }

    /// <summary>
    /// Set the initial properties of the picture box image based on the icon for the editor.
    /// </summary>
    private void SetImagePreviewProperties()
    {
        SetImagePreviewProperties(false);
    }

    /// <summary>
    /// Set the initial properties of the picture box image based on the icon for the editor. Optionally, set the properties differently if the editor was created from an .archive file.
    /// </summary>
    /// <param name="fromInitialArchive">Indicates whether this editor was created from an .archive file. In this case, the initial properties are set differently.</param>
    private void SetImagePreviewProperties(bool fromInitialArchive)
    {
        this.SafeInvoke(() =>
        {
            // Dispose of the existing image in the PictureBox (if any) to avoid resource locking
            picStationIcon.ClearImage();

            //If the icon is valid, prevent changes to the fields, and set fromInitialArchive to false (indicating the icon is valid) so the image gets set.
            //if (Icon.TrackedObject.CheckIconValid())
            //{
            //    fromInitialArchive = false;
            //    MakeEditorReadOnly();
            //}
            //else
            //{
            //    UnmakeEditorReadOnly();
            //}

            if (fromInitialArchive)
            {
                if (!Path.Exists(Icon.TrackedObject.ImagePath))
                {
                    lblImageWidth.Text = string.Format(Strings.IconEditor_Translate_W___0_, "TBD");
                    lblImageHeight.Text = string.Format(Strings.IconEditor_Translate_H___0_, "TBD");
                    lblImageFormat.Text = string.Format(Strings.IconEditor_Translate_Format___0_, "TBD");
                    lblImageColorMode.Text = string.Format(Strings.IconEditor_Translate_Color_Mode___0_, "TBD");
                    picStationIcon.Image = Resources.pending_extraction_128x128;
                    UnmakeEditorReadOnly();
                }
                else
                {
                    Icon.TrackedObject.EnsureImage();
                    picStationIcon.SetImage(Icon.TrackedObject.ImagePath);

                    lblImageWidth.Text = string.Format(Strings.IconEditor_Translate_W___0_,
                        picStationIcon.ImageProperties.Width + " px");
                    lblImageHeight.Text = string.Format(Strings.IconEditor_Translate_H___0_,
                        picStationIcon.ImageProperties.Height + " px");
                    lblImageFormat.Text = string.Format(Strings.IconEditor_Translate_Format___0_,
                        picStationIcon.ImageProperties.ImageFormat);
                    lblImageColorMode.Text = string.Format(Strings.IconEditor_Translate_Color_Mode___0_,
                        picStationIcon.ImageProperties.PixelFormat);
                    MakeEditorReadOnly();
                }
            }
            else
            {
                Icon.TrackedObject.EnsureImage();
                picStationIcon.SetImage(Icon.TrackedObject.ImagePath);

                lblImageWidth.Text = string.Format(Strings.IconEditor_Translate_W___0_,
                    picStationIcon.ImageProperties.Width + " px");
                lblImageHeight.Text = string.Format(Strings.IconEditor_Translate_H___0_,
                    picStationIcon.ImageProperties.Height + " px");
                lblImageFormat.Text = string.Format(Strings.IconEditor_Translate_Format___0_,
                    picStationIcon.ImageProperties.ImageFormat);
                lblImageColorMode.Text = string.Format(Strings.IconEditor_Translate_Color_Mode___0_,
                    picStationIcon.ImageProperties.PixelFormat);
                if (Icon.TrackedObject.CheckIconValid())
                    MakeEditorReadOnly();
                else
                    UnmakeEditorReadOnly();
            }

            if (picStationIcon.Image == null) return;

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

            // We only want to disable the import/extract button if the icon is already created.
            btnImportIcon.Enabled = Icon.TrackedObject.IsFromArchive
                ? !Path.Exists(Icon.TrackedObject.ImagePath)
                : !Path.Exists(Icon.TrackedObject.ArchivePath);

            btnCancelImport.Enabled = _isImporting || _isExtracting;
            picStationIcon.AllowDrop = false;

            _isReadOnly = true;
        });
    }

    private void UnmakeEditorReadOnly()
    {
        this.SafeInvoke(() =>
        {
            txtAtlasName.ReadOnly = false;
            btnImportIcon.Enabled = true;
            btnCancelImport.Enabled = _isImporting || _isExtracting;
            picStationIcon.AllowDrop = true;

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

    private void lblIconName_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconEditor_IconName_Hint;
    }

    private void lblAtlasName_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconEditor_AtlasName_Hint;
    }

    private void lblSha256Hash_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconEditor_Sha256Hash_Hint;
    }

    private void lblArchivePath_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconEditor_ArchivePath_Hint;
    }

    private void lblImagePath_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconEditor_ImagePath_Hint;
    }

    private void lblIconPath_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconEditor_IconPath_Hint;
    }

    private void lblIconPart_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconEditor_IconPart_Hint;
    }

    private void LblMouseLeave(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.Ready;
    }

    private void txtAtlasName_TextChanged(object sender, EventArgs e)
    {
        if (!_isReadOnly)
        {
            //If the icon is from an existing archive, we don't want to format it as the atlas name comes from the archive file.
            if (Icon.TrackedObject.IsFromArchive)
                return;

            // Store the current cursor position
            var cursorPosition = txtAtlasName.SelectionStart;

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
        editorTabs.TabPages[0].Text = string.Format(Strings.IconEditor_Translate_Editing_Icon___0_, txtIconName.Text);
        Icon.TrackedObject.IconName = txtIconName.Text;

        if (Icon.TrackedObject.IsFromArchive && !_isExtracting) return;

        if (!_isReadOnly)
        {
            txtAtlasName.Text = FormatAtlasName(Icon.TrackedObject.IconName);
            Icon.TrackedObject.AtlasName = txtAtlasName.Text;
        }

        IconUpdated?.Invoke(this, Icon);
    }

    private static string FormatAtlasName(string iconName)
    {
        var lowerName = iconName.ToLower(CultureInfo.CurrentUICulture);
        // Replace spaces with underscores and remove all special characters except underscores.
        return NoSpecialCharactersRegEx().Replace(lowerName, "_");
    }

    [GeneratedRegex(@"[^a-z0-9_]+")]
    private static partial Regex NoSpecialCharactersRegEx();

    private void picStationIcon_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconEditor_CustomIconPictureHelp;
    }

    private void btnImportIcon_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconEditor_ImportIcon;
    }

    private void btnStartExtract_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconEditor_ExtractIcon;
    }

    private void btnCancelImport_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconEditor_CancelAction;
    }

    private void CopyBtnMouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = Strings.IconEditor_CopyToClipboard;
    }
}