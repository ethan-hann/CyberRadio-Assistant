using AetherUtils.Core.Extensions;
using AetherUtils.Core.Reflection;
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
        private static bool _globalDataInitialized = false;

        //Initialize all global data.
        public static void Initialize()
        {
            if (_globalDataInitialized) return;

            GetUiIcons();
            SetCulture("English (en)");
            AddFontsToAssembly();
        }

        private static void AddFontsToAssembly()
        {
            FontHandler.Instance.AddFont(ExecAssembly, "CyberPunk_Regular", "RadioExt_Helper.resources.fonts.CFNotcheDemo-Regular.ttf");
            FontHandler.Instance.AddFont(ExecAssembly, "CyberPunk_Bold", "RadioExt_Helper.resources.fonts.CFNotcheDemo-Bold.ttf");
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
