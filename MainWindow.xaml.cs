﻿using MaterialDesignThemes.Wpf;
using SourceChord.FluentWPF;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Resources;

namespace MCenters
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    
    public partial class MainWindow
    {
        public BindingExpression EnableUninstall;
        public BindingExpression EnableInstall;
        public readonly string CurrentVersion = "";
        public MainWindow()
        {
            Screens.InstallScreen = new InstallScreen();
            Screens.UninstallScreen = new InstallScreen
            {
                Mode = InstallScreenModeEnum.Uninstall
            };
            var versionInfo = Process.GetCurrentProcess().MainModule.FileVersionInfo;
            CurrentVersion = versionInfo.FileVersion;
            ResourceDictionaryEx.GlobalTheme = ElementTheme.Light;
            InitializeComponent();
            Title = $"M Centers {versionInfo.FileMajorPart}.{versionInfo.FileMinorPart}";
            Methods.Method.LogFileName = DateTime.Now.ToString("dddd_d_MMMM_yyyy hh_mm_ss_tt").Replace(':', '_') + ".txt";
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            Screens.SettingsScreen = new Setting_Screen();
            Screens.MainScreen = this.Content;
            Screens.ErrorScreen = new ErrorScreen();
            Screens.DllErrorScreen = new ErrorScreen
            {
                CurrentMode = ErrorTypeEnum.ReportDll
            };
            settingsButton.ConnectedImage = settingsLogo;
            installButton.ConnectedImage = installIcon;
            uninstallButton.ConnectedImage = uninstallIcon;
            modOptionsButton.ConnectedImage = modOptionsIcon;
            Screens.Window = this;
            if (!Directory.Exists(Methods.Method.ClipboardFolder)) Directory.CreateDirectory(Methods.Method.ClipboardFolder);

            EnableUninstall = uninstallButton.GetBindingExpression(IsEnabledProperty);
            EnableInstall = installButton.GetBindingExpression(IsEnabledProperty);
        }




        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Content = Screens.SettingsScreen;
        }



        private void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            Content = Screens.InstallScreen;
        }

        private void UninstallButton_Click(object sender, RoutedEventArgs e)
        {
            Content = Screens.UninstallScreen;
        }

    }
}
