using Newtonsoft.Json;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using System.Diagnostics;

namespace RadioExt_Helper.forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ApplyFonts();

        }

        private void ApplyFonts()
        {
            FontLoader.Initialize();
            foreach (Control control in Controls)
            {
                if (control.GetType() == typeof(MenuStrip))
                {
                    FontLoader.ApplyCustomFont(control, 10, true);
                }
                else
                {
                    FontLoader.ApplyCustomFont(control, 12);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MetaData md = new MetaData();
            string json = JsonConvert.SerializeObject(md);
            Debug.WriteLine(json);
        }
    }
}
