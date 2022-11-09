using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ButtonEx
{
    /// <summary>
    /// Standard button with extensions
    /// </summary>
    public partial class KeystrokeButton : Button
    {
        readonly static Brush DefaultPressedBackgroundValue = new BrushConverter().ConvertFromString("#FFBEE6FD") as Brush;

        public KeystrokeButton()
        {
            InitializeComponent();
        }

        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }
        public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register(
            "PressedBackground", typeof(Brush), typeof(KeystrokeButton), new PropertyMetadata(DefaultPressedBackgroundValue));
    }
}