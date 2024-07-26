namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Simple class to hold information about a file for previewing purposes. Holds the file name and size.
    /// </summary>
    public class FilePreview
    {
        /// <summary>
        /// The filename of the file.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// The size, in bytes, of the file.
        /// </summary>
        public long Size { get; set; }
    }
}
