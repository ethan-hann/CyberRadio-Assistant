// ConvertCandidate.cs : RadioExt-Helper
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

using System.ComponentModel;
using AetherUtils.Core.Extensions;
using RadioExt_Helper.utility.localization;

namespace RadioExt_Helper.models;

/// <summary>
/// Represents one file-to-file conversion job. Specifically designed to be shown in a property grid.
/// Localizes the property descriptions and display names using a custom type description provider.
/// </summary>
public class ConvertCandidate
{
    private ValidAudioFiles _targetFormat;

    /// <summary>
    /// Creates a new conversion candidate.
    /// </summary>
    /// <param name="inputPath">The source file to convert.</param>
    /// <param name="targetFormat">The target format to convert to.</param>
    /// <param name="outputDirectory">The base directory to save converted file to.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public ConvertCandidate(string inputPath, ValidAudioFiles targetFormat, string outputDirectory)
    {
        InputPath = inputPath ?? throw new ArgumentNullException(nameof(inputPath));
        TargetFormat = targetFormat;
        var ext = TargetFormat.ToDescriptionString();
        var name = Path.GetFileNameWithoutExtension(inputPath) + ext;
        OutputPath = Path.Combine(outputDirectory, name);
    }

    /// <summary>
    /// The source file to convert.
    /// </summary>
    [Description("ConvertCandidate_InputPathDesc")]
    [DisplayName("ConvertCandidate_InputPathDisplayName")]
    [Category("ConvertCandidate_Category")]
    [Browsable(true)]
    [ReadOnly(true)]
    public string InputPath { get; }

    /// <summary>
    /// The target format to convert to.
    /// </summary>
    [Description("ConvertCandidate_TargetFormatDesc")]
    [DisplayName("ConvertCandidate_TargetFormatDisplayName")]
    [Category("ConvertCandidate_Category")]
    [Browsable(true)]
    [ReadOnly(false)]
    public ValidAudioFiles TargetFormat
    {
        get => _targetFormat;
        set
        {
            _targetFormat = value;
            var ext = _targetFormat.ToDescriptionString();
            var name = Path.GetFileNameWithoutExtension(InputPath) + ext;
            OutputPath = Path.Combine(Path.GetDirectoryName(OutputPath) ?? string.Empty, name);
        }
    }

    /// <summary>
    /// The output path where the converted file will be saved. Computed from the input path and target format.
    /// </summary>
    [Description("ConvertCandidate_OutputPathDesc")]
    [DisplayName("ConvertCandidate_OutputPathDisplayName")]
    [Category("ConvertCandidate_Category")]
    [Browsable(true)]
    [ReadOnly(true)]
    public string OutputPath { get; set; }

    /// <summary>
    /// Static constructor to register the type with a localized type description provider.
    /// </summary>
    static ConvertCandidate()
    {
        TypeDescriptor.AddProvider(new LocalizedTypeDescriptionProvider(typeof(ConvertCandidate)), typeof(ConvertCandidate));
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Path.GetFileName(InputPath);
    }
}