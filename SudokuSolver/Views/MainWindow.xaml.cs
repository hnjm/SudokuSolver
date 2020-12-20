﻿using System;
using System.Diagnostics;
using System.IO;
using ControlzEx.Theming;
using Sudoku.ViewModels;

namespace Sudoku.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Activated += MainWindow_Activated;

            InitializeTheme();
            ProcessCommandLine(Environment.GetCommandLineArgs());
        }


        private void MainWindow_Activated(object? sender, EventArgs e)
        {
            // this app's light/dark theme setting over rides the OS setting
            // so only check if the title bar and borders setting has changed
            PuzzleViewModel vm = (PuzzleViewModel)DataContext;
            vm.AccentTitleBar = WindowsThemeHelper.ShowAccentColorOnTitleBarsAndWindowBorders();
        }


        private void ProcessCommandLine(string[] args)
        {
            if (args?.Length == 2)  // args[0] is typically the full path of the executing assembly
            {
                if ((Path.GetExtension(args[1]).ToLower() == PuzzleViewModel.cDefaultFileExt) && File.Exists(args[1]))
                {
                    try
                    {
                        using FileStream fs = File.OpenRead(args[1]);
                        ((PuzzleViewModel)DataContext).OpenFile(fs, args[1]);
                    }
                    catch (Exception e)
                    {
                        // TODO - failed to open message box
                        Debug.Fail(e.Message);
                    }
                }
            }
        }


        private void InitializeTheme()
        {
            PuzzleViewModel vm = (PuzzleViewModel)DataContext;

            vm.DarkThemed = !WindowsThemeHelper.AppsUseLightTheme();
            vm.AccentTitleBar = WindowsThemeHelper.ShowAccentColorOnTitleBarsAndWindowBorders();
        }
    }
}
