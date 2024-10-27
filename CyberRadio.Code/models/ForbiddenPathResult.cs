namespace RadioExt_Helper.models;

/// <summary>
/// Represents the result of a forbidden path check.
/// </summary>
public class ForbiddenPathResult
{
    /// <summary>
    /// Indicates if the path is forbidden.
    /// </summary>
    public bool IsForbidden { get; set; }

    /// <summary>
    /// The reason the path is considered forbidden. The description of this enum can be used for displaying in message boxes.
    /// </summary>
    public ForbiddenPathReason Reason { get; set; }
}