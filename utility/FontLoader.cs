using System.Drawing.Text;
using System.Reflection;

namespace RadioExt_Helper.utility;

/// <summary>
/// Facilitates loading custom, embedded fonts to prevent users from having to install custom fonts
/// on their machines.
/// </summary>
public static class FontLoader
{
    private static bool _isInitialized = false;
    private static PrivateFontCollection _privateFonts = new();
    
    private static readonly Dictionary<string, string> Fonts = new()
    {
        {"CyberPunk_Regular", "RadioExt_Helper.resources.fonts.CFNotcheDemo-Regular.ttf"},
        {"CyberPunk_Bold", "RadioExt_Helper.resources.fonts.CFNotcheDemo-Bold.ttf"}
    };
    
    public static void Initialize()
    {
        if (!_isInitialized)
            LoadCustomFonts();
    }

    private static void LoadCustomFonts()
    {
        foreach (var fontPair in Fonts)
        {
            var fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fontPair.Value);
            if (fontStream == null) continue;
            
            // Read the font data from the stream into a new array of bytes.
            var fontData = new byte[fontStream.Length];
            var bytesReadCount = fontStream.Read(fontData, 0, (int)fontStream.Length);
            fontStream.Close();
                
            // Allocate memory for the font data and copy it
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
                
            // Add the font to the PrivateFontCollection
            _privateFonts.AddMemoryFont(fontPtr, fontData.Length);
                
            // Free the allocated memory
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
        }

        if (_privateFonts.Families.Length > 0)
            _isInitialized = true;
    }

    public static void ApplyCustomFont(Control c, float emSize, bool isBold = false)
    {
        if (_privateFonts.Families.Length <= 0) return;

        var customFont = new Font(_privateFonts.Families.First(), emSize,
            isBold ? FontStyle.Bold : FontStyle.Regular);

        if (c.InvokeRequired)
        {
            c.Invoke(() =>
            {
                c.Font = customFont;
            });
        }
        else
        {
            c.Font = customFont;
        }
    }
}