namespace RadioExt_Helper.forms;

public partial class RadioGardenInput : Form
{
    public event EventHandler<string>? UrlParsed;

    public RadioGardenInput()
    {
        InitializeComponent();
    }

    private void RadioGardenInput_Load(object sender, EventArgs e)
    {
        Translate();
    }

    private void Translate()
    {
        Text = Strings.RadioGardenURLCaption;
        lblRadioGardenDesc.Text = Strings.RadioGardenInput;
        btnParseUrl.Text = Strings.RadioGardenParse;
        btnCancel.Text = Strings.Cancel;
    }

    private void btnParseUrl_Click(object sender, EventArgs e)
    {
        UrlParsed?.Invoke(this, txtRadioGardenInput.Text);
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}