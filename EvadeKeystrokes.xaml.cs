﻿// Copyright 2022 Plextora
// This file is licensed under GPL-v3.0

using Gma.System.MouseKeyHook;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Button = System.Windows.Controls.Button;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace EvadeKeystrokes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IKeyboardMouseEvents m_GlobalHook;
        private static Brush BackgroundValue;
        private static Brush PressedBackgroundValue;
        private static Brush ForegroundValue;
        private static string buttonBackground;
        private static string pressedButtonBackground;
        private static string buttonForeground;

        private readonly string DefaultPressedBackgroundValue = "#FFBEE6FD";
        private readonly string DefaultBackgroundValue = "#FFDDDDDD";
        private readonly string DefaultForegroundValue = "#FF000000";

        public MainWindow()
        {
            InitializeComponent();
            Subscribe();
            CreateConfig();
        }

        private void CreateConfig()
        {
            if (!File.Exists("config.txt"))
            {
                File.Create("config.txt").Close();

                string text =
                    "# 1st line is for button background\n" +
                    "# 2nd line is for button background when pressed\n" +
                    "# 3rd line is for button foreground\n\n" +
                    $"{DefaultBackgroundValue}\n" +
                    $"{DefaultPressedBackgroundValue}\n" +
                    $"{DefaultForegroundValue}";

                File.WriteAllText("config.txt", text);

                BackgroundValue = new BrushConverter().ConvertFromString(DefaultBackgroundValue) as Brush;
                PressedBackgroundValue = new BrushConverter().ConvertFromString(DefaultPressedBackgroundValue) as Brush;
                ForegroundValue = new BrushConverter().ConvertFromString(DefaultForegroundValue) as Brush;
            }
            else
            {
                LoadConfig();
            }
        }

        private void LoadConfig()
        {
            buttonBackground = File.ReadLines("config.txt").Skip(4).Take(1).First();
            pressedButtonBackground = File.ReadLines("config.txt").Skip(5).Take(1).First();
            buttonForeground = File.ReadLines("config.txt").Skip(6).Take(1).First();

            BackgroundValue = new BrushConverter().ConvertFromString(buttonBackground) as Brush;
            PressedBackgroundValue = new BrushConverter().ConvertFromString(pressedButtonBackground) as Brush;
            ForegroundValue = new BrushConverter().ConvertFromString(buttonForeground) as Brush;

            foreach (Button button in FindVisualChildren<Button>(WindowGrid))
            {
                button.Background = BackgroundValue;
                button.Foreground = ForegroundValue;
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
            }
        }

        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e) => Unsubscribe();

        private void ActivateButton(Button buttonName) => buttonName.Background = PressedBackgroundValue;

        private void DeactivateButton(Button buttonName) => buttonName.Background = BackgroundValue;

        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.KeyDown += OnKeyDown;
            m_GlobalHook.KeyUp += OnKeyUp;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    ActivateButton(WKeystroke);
                    break;
                case Keys.A:
                    ActivateButton(AKeystroke);
                    break;
                case Keys.S:
                    ActivateButton(SKeystroke);
                    break;
                case Keys.D:
                    ActivateButton(DKeystroke);
                    break;
                case Keys.Space:
                    ActivateButton(SpacebarKeystroke);
                    break;
                case Keys.D1:
                    ActivateButton(OneKeystroke);
                    break;
                case Keys.D2:
                    ActivateButton(TwoKeystroke);
                    break;
                case Keys.R:
                    ActivateButton(RKeystroke);
                    break;
                case Keys.T:
                    ActivateButton(TKeystroke);
                    break;
                case Keys.LControlKey:
                    ActivateButton(CtrlorCKeystroke);
                    break;
                case Keys.C:
                    ActivateButton(CtrlorCKeystroke);
                    break;
                case Keys.E:
                    ActivateButton(EKeystroke);
                    break;
                case Keys.Q:
                    ActivateButton(QKeystroke);
                    break;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    DeactivateButton(WKeystroke);
                    break;
                case Keys.A:
                    DeactivateButton(AKeystroke);
                    break;
                case Keys.S:
                    DeactivateButton(SKeystroke);
                    break;
                case Keys.D:
                    DeactivateButton(DKeystroke);
                    break;
                case Keys.Space:
                    DeactivateButton(SpacebarKeystroke);
                    break;
                case Keys.D1:
                    DeactivateButton(OneKeystroke);
                    break;
                case Keys.D2:
                    DeactivateButton(TwoKeystroke);
                    break;
                case Keys.R:
                    DeactivateButton(RKeystroke);
                    break;
                case Keys.T:
                    DeactivateButton(TKeystroke);
                    break;
                case Keys.LControlKey:
                    DeactivateButton(CtrlorCKeystroke);
                    break;
                case Keys.C:
                    DeactivateButton(CtrlorCKeystroke);
                    break;
                case Keys.E:
                    DeactivateButton(EKeystroke);
                    break;
                case Keys.Q:
                    DeactivateButton(QKeystroke);
                    break;
            }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.KeyDown -= OnKeyDown;

            m_GlobalHook.Dispose();
        }

        private void WindowMove(object sender, MouseButtonEventArgs e) => DragMove();
    }
}
