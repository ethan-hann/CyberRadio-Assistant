using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    public static class GlobalData
    {
        /// <summary>
        /// Get a list of strings containing all UIIcon records in the game. This list is populated from an embedded text file.
        /// </summary>
        public static BindingList<string> UIIcons { get; private set; } = [];

        internal static Assembly ExecAssembly { get; private set; } = Assembly.GetExecutingAssembly();

        private static bool _uiIconsInitialized = false;

        //Initialize all global data.
        public static void Initialize()
        {
            GetUIIcons();
        }

        private static void GetUIIcons()
        {
            if (_uiIconsInitialized) { return; }
            try
            {
                List<string> strings = [];

                var txtData = ExecAssembly.ReadResource("RadioExt_Helper.resources.final_ui_icon_strings.txt");
                foreach (string line in txtData.Split('\n'))
                    strings.Add(line.TrimEnd().TrimStart());
                UIIcons = new BindingList<string>(strings.Distinct().ToList());

                _uiIconsInitialized = true;
            } catch (Exception ex) { Debug.WriteLine(ex); }
        }
    }
}
