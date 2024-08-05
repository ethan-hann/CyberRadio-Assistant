using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms
{
    public partial class IconGeneratorForm : Form
    {
        private IconGenerator _iconGenerator;

        public IconGeneratorForm()
        {
            InitializeComponent();

            _iconGenerator = new IconGenerator();
        }

        private void IconGeneratorForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = _iconGenerator.GenerateInkAtlasJson("X:\\Files\\Downloads\\generate inkatlas script\\generate_test_2", "awesomeStation");
            richTextBox1.Text = string.Concat(result.output, "\n\n", result.error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = _iconGenerator.ConvertToInkAtlas("X:\\Files\\Downloads\\generate inkatlas script\\generate_test_2\\source\\raw\\awesomeStation.inkatlas.json");
            richTextBox1.Text = string.Concat(result.output, "\n\n", result.error);
        }
    }
}
