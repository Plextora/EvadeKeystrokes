using System.Windows;

using Gma.System.MouseKeyHook;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace EvadeKeystrokes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IKeyboardMouseEvents m_GlobalHook;

        public MainWindow()
        {
            InitializeComponent();
            Subscribe();
        }

        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Unsubscribe();
        }

        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // do cool stuff
        }

        public void Unsubscribe()
        {
            m_GlobalHook.KeyDown -= OnKeyDown;

            m_GlobalHook.Dispose();
        }
    }
}
