// Copyright 2022 Plextora
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
        private static Brush BackgroundBorderValue;
        private static Brush PressedValue;
        private static Brush ForegroundValue;
        private static string buttonBorderValue;
        private static string pressedButtonValue;
        private static string buttonForeground;

        private readonly string DefaultPressedValue = "#FFBEE6FD";
        private readonly string DefaultBackgroundBorderValue = "#FFDDDDDD";
        private readonly string DefaultForegroundValue = "#FF000000";
        private readonly Brush EmptyBrush = new BrushConverter().ConvertFrom("#00FFFFFF") as Brush;

        private bool useBorder;

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
                    "# 2nd line is for button background/border when pressed\n" +
                    "# 3rd line is for button foreground\n" +
                    "# 4rd line is to tell Evade Keystrokes if you want to highlight the background or border when a key is pressed\n" +
                    "# For example, useborder:false would tell Evade Keystrokes to highlight the background\n" +
                    "# useborder:true would tell Evade Keystrokes to highlight the border\n\n" +
                    $"{DefaultBackgroundBorderValue}\n" +
                    $"{DefaultPressedValue}\n" +
                    $"{DefaultForegroundValue}\n" +
                    "useborder:false";

                File.WriteAllText("config.txt", text);

                BackgroundBorderValue = new BrushConverter().ConvertFromString(DefaultBackgroundBorderValue) as Brush;
                PressedValue = new BrushConverter().ConvertFromString(DefaultPressedValue) as Brush;
                ForegroundValue = new BrushConverter().ConvertFromString(DefaultForegroundValue) as Brush;
            }
            else
            {
                LoadConfig();
            }
        }

        private void LoadConfig()
        {
            buttonBorderValue = GetLine(8);
            pressedButtonValue = GetLine(9);
            buttonForeground = GetLine(10);
            useBorder = GetLine(11) == "useborder:true";

            BackgroundBorderValue = new BrushConverter().ConvertFromString(buttonBorderValue) as Brush;
            PressedValue = new BrushConverter().ConvertFromString(pressedButtonValue) as Brush;
            ForegroundValue = new BrushConverter().ConvertFromString(buttonForeground) as Brush;

            foreach (Button button in FindVisualChildren<Button>(WindowGrid))
            {
                button.Background = BackgroundBorderValue;
                button.Foreground = ForegroundValue;
            }
        }

        string GetLine(int line)
        {
            using (var sr = new StreamReader("config.txt"))
            {
                for (int i = 1; i < line; i++)
                    sr.ReadLine();
                return sr.ReadLine();
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

        private void ActivateButton(Button buttonName)
        {
            if (useBorder)
            {
                buttonName.BorderBrush = PressedValue;
            }
            else
            {
                buttonName.Background = PressedValue;
            }
        }

        private void DeactivateButton(Button buttonName)
        {
            if (useBorder)
            {
                buttonName.BorderBrush = EmptyBrush;
            }
            else
            {
                buttonName.Background = BackgroundBorderValue;
            }
        }

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
