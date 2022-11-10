using Gma.System.MouseKeyHook;
using System.IO;
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
        private static readonly Brush DefaultPressedBackgroundValue = new BrushConverter().ConvertFromString("#FFBEE6FD") as Brush;
        private static readonly Brush DefaultBackgroundValue = new BrushConverter().ConvertFromString("#FFDDDDDD") as Brush;

        public MainWindow()
        {
            InitializeComponent();
            Subscribe();
            Config();
        }

        private void Config()
        {
            if (!File.Exists("config.txt"))
            {
                File.Create("config.txt").Close();
            }
        }

        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void ActivateButton(Button buttonName)
        {
            buttonName.Background = DefaultPressedBackgroundValue;
        }

        private void DeactivateButton(Button buttonName)
        {
            buttonName.Background = DefaultBackgroundValue;
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
            }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.KeyDown -= OnKeyDown;

            m_GlobalHook.Dispose();
        }

        private void WindowMove(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
