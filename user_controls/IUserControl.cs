using RadioExt_Helper.models;

namespace RadioExt_Helper.user_controls
{
    public interface IUserControl
    {
        /// <summary>
        /// The station associated with this control.
        /// </summary>
        public Station Station { get; }

        /// <summary>
        /// Specify how embedded fonts are to be applied to children of this control.
        /// </summary>
        public void ApplyFonts();

        /// <summary>
        /// Specify how to translate the strings of this control.
        /// </summary>
        public void Translate();
    }
}
