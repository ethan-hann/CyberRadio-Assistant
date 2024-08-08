using System.Drawing;
using System.Drawing.Imaging;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using RadioExt_Helper.utility.event_args;

namespace RadioExt_Helper.forms
{
    public partial class IconImportForm : Form
    {
        public event EventHandler<IconImportEventArgs>? IconImported;

        private readonly TrackableObject<Station> _station;

        private CustomIcon? icon;

        public IconImportForm(TrackableObject<Station> station, Image iconImage)
        {
            InitializeComponent();

            _station = station;
            picIconPreview.Image = iconImage;

            //IconManager.Instance.StatusChanged += Instance_StatusChanged;
            //IconManager.Instance.ProgressChanged += Instance_ProgressChanged;
            //IconManager.Instance.ErrorOccurred += Instance_ErrorOccurred;
            //IconManager.Instance.WarningOccurred += Instance_WarningOccurred;
        }

        private void Instance_WarningOccurred(string warning)
        {
            this.SafeInvoke(() => rtbImportProgress.Text += $"\n{warning}\n");
        }

        private void Instance_ErrorOccurred(string error)
        {
            this.SafeInvoke(() => rtbImportProgress.Text += $"\n{error}\n");
        }

        private void Instance_ProgressChanged(int progress)
        {
            this.SafeInvoke(() => pgProgress.Value = progress);
        }

        private void Instance_StatusChanged(string status)
        {
            this.SafeInvoke(() => lblStatus.Text = status);
        }

        private void IconImportForm_Load(object sender, EventArgs e)
        {
            Translate();

        }

        private void Translate()
        {
            //TODO: translations
            Text = string.Format(GlobalData.Strings.GetString("IconImportForm_Title") ?? "Importing Icon: {0}", _station.TrackedObject.MetaData.DisplayName);

        }

        private void BtnImportIcon_Click(object sender, EventArgs e)
        {
            //var customIcon = IconManager.Instance.SaveIconToStation(_station, txtAtlasName.Text, picIconPreview.Image, true);
            //IconImported?.Invoke(this, new IconImportEventArgs(customIcon));
        }

        private void bgImportWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }

        private void bgImportWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {

        }

        private void bgImportWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //IconManager.Instance.CancelIconImport();
        }
    }
}
