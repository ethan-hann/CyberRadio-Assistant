namespace RadioExt_Helper.utility
{
    public interface ITrackable
    {
        void AcceptChanges();
        void DeclineChanges();
        bool IsPendingSave { get; }
    }
}
