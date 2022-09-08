using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuickNoteWidget
{
    public partial class InfoWindow : Window
    {
        public InfoWindow(string currentAccent)
        {
            InitializeComponent();

            Style textBlockForegroundStyle = new Style(typeof(TextBlock));
            textBlockForegroundStyle.Setters.Add(new Setter()
            {
                Property = TextBlock.ForegroundProperty,
                Value = currentAccent
            });

            // TODO: apply styles to all TextBox
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}