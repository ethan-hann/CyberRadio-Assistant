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
    public string InputPath { get; }

    /// <summary>
    /// The target format to convert to.
    /// </summary>
    public ValidAudioFiles TargetFormat { get; set; }

    /// <summary>
    /// The output path where the converted file will be saved. Computed from the input path and target format.
    /// </summary>
    public string OutputPath { get; set; }

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