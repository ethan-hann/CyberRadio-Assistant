using System.ComponentModel;
using AetherUtils.Core.Extensions;

namespace RadioExt_Helper.models;

/// <summary>
/// Represents one file-to-file conversion job.
/// </summary>
public class ConvertCandidate
{
    /// <summary>
    /// The source file to convert.
    /// </summary>
    [Description("The path of the input file.")]
    [DisplayName("Input File")]
    [Category("General")]
    [Browsable(true)]
    [ReadOnly(true)]
    public string InputPath { get; }

    /// <summary>
    /// The target format to convert to.
    /// </summary>
    [Description("The target format for the conversion.")]
    [DisplayName("Target Format")]
    [Category("General")]
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
    [Description("The path of the output file.")]
    [DisplayName("Output File")]
    [Category("General")]
    [Browsable(true)]
    [ReadOnly(true)]
    public string OutputPath { get; set; }

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

    /// <inheritdoc />
    public override string ToString() => Path.GetFileName(InputPath);
}