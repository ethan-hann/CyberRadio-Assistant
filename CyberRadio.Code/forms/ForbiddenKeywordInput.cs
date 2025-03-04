// ForbiddenKeywordInput.cs : RadioExt-Helper
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

using RadioExt_Helper.config;
using RadioExt_Helper.theming;

namespace RadioExt_Helper.forms;

/// <summary>
/// Form for inputting a forbidden keyword.
/// </summary>
public partial class ForbiddenKeywordInput : Form
{
    private readonly List<ListViewGroup> _currentGroups = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenKeywordInput"/> class with the current groups in the list view.
    /// </summary>
    /// <param name="currentGroups"></param>
    public ForbiddenKeywordInput(ListViewGroupCollection currentGroups)
    {
        InitializeComponent();

        foreach (ListViewGroup group in currentGroups)
        {
            if (group.Header.ToLowerInvariant().Equals(Strings.SystemPaths.ToLowerInvariant()))
                continue;
            _currentGroups.Add(group);
        }

        ////Remove system paths from groups so it doesn't show up in the dropdown
        //foreach (ListViewGroup group in currentGroups)
        //{
        //    if (group.Header.ToLowerInvariant().Equals(Strings.SystemPaths.ToLowerInvariant()))
        //    {
        //        _currentGroups.Remove(group);
        //        break;
        //    }
        //}
    }

    /// <summary>
    /// Event that is raised when a forbidden keyword is added.
    /// </summary>
    public event EventHandler<ForbiddenKeyword>? ForbiddenKeywordAdded;

    private void ForbiddenKeywordInput_Load(object sender, EventArgs e)
    {
        foreach (var group in _currentGroups)
            cmbGroups.Items.Add(group);

        cmbGroups.SelectedIndex = 0;

        Translate();

        ThemeManager.Instance.ApplyThemeToControl(this);
    }

    private void Translate()
    {
        Text = Strings.ForbiddenKeywordInput_Title;
        lblKeyword.Text = Strings.ForbiddenKeywordInput_Keyword;
        lblGroup.Text = Strings.ForbiddenKeywordInput_Group;
        chkIsForbidden.Text = Strings.ForbiddenKeywordInput_IsForbidden;
        btnAddAndNew.Text = Strings.ForbiddenKeywordInput_AddAndNew;
        btnAddAndClose.Text = Strings.ForbiddenKeywordInput_Add;
    }

    private void AddKeyword()
    {
        if (string.IsNullOrEmpty(txtKeyword.Text))
        {
            MessageBox.Show(this, Strings.ForbiddenKeywordEmpty,
                Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (string.IsNullOrEmpty(cmbGroups.Text))
        {
            MessageBox.Show(this, Strings.ForbiddenKeywordNoGroup,
                Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (cmbGroups.Text.ToLowerInvariant().Equals(Strings.SystemPaths.ToLowerInvariant()))
        {
            MessageBox.Show(this, Strings.GroupNotAllowedToBeModified, Strings.Error, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        var group = _currentGroups.FirstOrDefault(g => g.Header.Equals(cmbGroups.Text));
        if (group == null)
        {
            group = new ListViewGroup(cmbGroups.Text, cmbGroups.Text);
            _currentGroups.Add(group);
            cmbGroups.Items.Add(group);
        }

        var keyword = txtKeyword.Text;
        var isForbidden = chkIsForbidden.Checked;
        var forbiddenKeyword = new ForbiddenKeyword(group.Header, keyword, isForbidden);
        ForbiddenKeywordAdded?.Invoke(this, forbiddenKeyword);
    }

    private void btnAddKeyword_Click(object sender, EventArgs e)
    {
        AddKeyword();
        Close();
    }

    private void btnAddAndNew_Click(object sender, EventArgs e)
    {
        AddKeyword();
        ClearInputs();
    }

    private void ClearInputs()
    {
        txtKeyword.Text = string.Empty;
        chkIsForbidden.Checked = false;
    }
}