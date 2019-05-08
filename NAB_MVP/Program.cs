﻿using System;
using System.Windows.Forms;
using NAB_MVC.Models;
using NAB_MVC.Controllers;

namespace NAB_MVC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BankingFile File;
            frmMainForm Form;
            ApplicationController Controller;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            File = new BankingFile();
            Form = new frmMainForm();
            Controller = new ApplicationController(Form, File);
            Application.Run(Form);
        }
    }
}
