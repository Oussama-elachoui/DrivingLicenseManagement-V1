using DrivingLicenseManagement_V1.Application;
using DrivingLicenseManagement_V1.Application.ApplicationTypes;
using DrivingLicenseManagement_V1.Application.LocalDrivingLisence;
using DrivingLicenseManagement_V1.People;
using DrivingLicenseManagement_V1.Test;
using DrivingLicenseManagement_V1.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_LDLLISTE());
        }
    }
