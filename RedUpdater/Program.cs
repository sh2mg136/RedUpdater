using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using NLog;

namespace RedUpdater
{

    static class Program
    {

        public static Logger RedLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            Application.Run(new FormMain());
        }
    }
}