namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Interface for trackable objects.
    /// </summary>
    public interface ITrackable
    {
        /// <summary>
        /// Defines the method to accept changes to the object.
        /// </summary>
        void AcceptChanges();

        /// <summary>
        /// Defines the method to decline changes to the object.
        /// </summary>
        void DeclineChanges();

        /// <summary>
        /// Defines the method to check if the object has pending changes.
        /// </summary>
        /// <returns></returns>
        bool CheckPendingSaveStatus();

        /// <summary>
        /// Get a value indicating whether the object has pending changes.
        /// </summary>
        bool IsPendingSave { get; }
    }
}
