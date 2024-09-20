namespace RadioExt_Helper.utility
{
    public interface ITrackable
    {
        void AcceptChanges();
        void DeclineChanges();
        bool CheckPendingSaveStatus();
        bool IsPendingSave { get; }
    }
}
