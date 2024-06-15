using RadioExt_Helper.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    public static class GlobalData
    {
        /// <summary>
        /// A list of strings containing all UIIcon records in the game. This list is populated from an embedded text file.
        /// </summary>
        public static BindingList<string> UiIcons { get; private set; } = [];

        /// <summary>
        /// The resource manager responsible for keeping translations of strings.
        /// </summary>
        public static readonly ResourceManager Strings = new("RadioExt_Helper.Strings", typeof(MainForm).Assembly);

        internal static Assembly ExecAssembly { get; private set; } = Assembly.GetExecutingAssembly();

        private static bool _uiIconsInitialized = false;

        //Initialize all global data.
        public static void Initialize()
        {
            GetUiIcons();
            SetCulture("English (en)");
        }

        /// <summary>
        /// Sets the UI culture to the value passed in. This method expects the culture to be formatted like so:
        /// <c>English (en)</c>, where the culture code is in parentheses at the end of the string.
        /// </summary>
        /// <param name="culture">The culture to change the UI to.</param>
        public static void SetCulture(string culture)
        {
            var parsedCulture = culture.Substring(culture.IndexOf('(') + 1, 
                culture.Length - culture.LastIndexOf(')') + 1);
            CultureInfo.CurrentUICulture = new CultureInfo(parsedCulture);
        }

        /// <summary>
        /// Opens the specified URL in the default web browser.
        /// </summary>
        /// <param name="url">The URL to open.</param>
        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(Strings.GetString("OpenURLErrorOccured") + ex.Message);
            }
        }

        /// <summary>
        /// Creates a new <see cref="Bitmap"/> image from the specified text. This image can then be used for a drag-drop operation to
        /// show a preview of the data being dragged.
        /// </summary>
        /// <param name="text">The text to capture as a Bitmap.</param>
        /// <returns>A new Bitmap "screenshot" of the text.</returns>
        public static Bitmap CreateDragImage(string text)
        {
            Font font = new Font("Arial", 12);
            SizeF textSize;

            using Graphics g = Graphics.FromImage(new Bitmap(1, 1));
            textSize = g.MeasureString(text, font);

            Bitmap bitmap = new Bitmap((int)textSize.Width, (int)textSize.Height);

            using Graphics gr = Graphics.FromImage(bitmap);
            gr.Clear(Color.Transparent);
            gr.DrawString(text, font, Brushes.Black, 0, 0);

            return bitmap;
        }

        /// <summary>
        /// Checks if the two lists contain the same items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static bool AreListsEqual<T>(IList<T> list1, IList<T> list2) where T : notnull
        {
            if (list1.Count != list2.Count) return false;

            var dict1 = list1.GroupBy(x => x).ToDictionary(g => g.Key, g =>g.Count());
            var dict2 = list2.GroupBy(x => x).ToDictionary(g => g.Key, g =>g.Count());

            return dict1.Count == dict2.Count && !dict1.Except(dict2).Any();
        }

        private static void GetUiIcons()
        {
            if (_uiIconsInitialized) { return; }
            try
            {
                List<string> strings = [];

                var txtData = ExecAssembly.ReadResource("RadioExt_Helper.resources.final_ui_icon_strings.txt");
                foreach (string line in txtData.Split('\n'))
                    strings.Add(line.TrimEnd().TrimStart());
                UiIcons = new BindingList<string>(strings.Distinct().ToList());

                _uiIconsInitialized = true;
            } catch (Exception ex) { Debug.WriteLine(ex); }
        }
    }
}
