// RadioGardenInput.cs : RadioExt-Helper
// Copyright (C) 2024  Ethan Hann
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

namespace RadioExt_Helper.forms;

public partial class RadioGardenInput : Form
{
    public RadioGardenInput()
    {
        InitializeComponent();
    }

    public event EventHandler<string>? UrlParsed;

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