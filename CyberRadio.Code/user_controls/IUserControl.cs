using RadioExt_Helper.models;

namespace RadioExt_Helper.user_controls;

/// <summary>
///     Interface for a custom user control. Implementors should define how to apply fonts and how to translate
///     their controls.
/// </summary>
public interface IUserControl
{
    /// <summary>
    ///     The station associated with this control.
    /// </summary>
    public Station Station { get; }

    /// <summary>
    ///     Specify how to translate the strings of this control.
    /// </summary>
    public void Translate();
}