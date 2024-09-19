using RadioExt_Helper.models;

namespace RadioExt_Helper.utility
{
    public interface IEditor
    {
        /// <summary>
        /// The unique identifier for this editor.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The type of editor.
        /// </summary>
        public EditorType Type { get; set; }

        /// <summary>
        ///     The tracked station associated with this control.
        /// </summary>
        public TrackableObject<Station> Station { get; }

        /// <summary>
        /// Defines the method to translate the control into the current language.
        /// </summary>
        public void Translate();
    }
}
