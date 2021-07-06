using System;
using System.Windows;
using System.Windows.Input;
using Application = System.Windows.Application;
using Clipboard = System.Windows.Clipboard;


namespace QuickNoteWidget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _mainWindowViewModel = new MainWindowViewModel();
            tbxMultiLine.Focus();
        }


        private void contextClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void contextMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        private void contextMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void contextAdd_Click(object sender, RoutedEventArgs e)
        {
            string clipboard = Clipboard.GetText();
            this.tbxMultiLine.Text += clipboard;
        }

        private void contextCopy_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.tbxMultiLine.Text))
                return;

            if (String.IsNullOrEmpty(this.tbxMultiLine.SelectedText))
                Clipboard.SetText(this.tbxMultiLine.Text);
            else
                Clipboard.SetText(this.tbxMultiLine.SelectedText);
        }
        
        private void contextSelect_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.tbxMultiLine.Text))
                return;

            this.tbxMultiLine.SelectAll();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _mainWindowViewModel.SaveSettings();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            var info = new InfoWindow();
            info.Show();
        }


        // React to ctrl + mouse wheel
        private void tbxMultiLine_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool ctrl = Keyboard.Modifiers == ModifierKeys.Control;
            if (ctrl)
            {
                this.UpdateFontSize(e.Delta > 0);
                e.Handled = true;
            }
        }

        // max and min font size values
        private const double FONT_MAX_SIZE = 60d;
        private const double FONT_MIN_SIZE = 5d;


        private void UpdateFontSize(bool increase)
        {
            double currentSize = tbxMultiLine.FontSize;

            if (increase)
            {
                if (currentSize < FONT_MAX_SIZE)
                {
                    double newSize = Math.Min(FONT_MAX_SIZE, currentSize + 1);
                    tbxMultiLine.FontSize = newSize;
                }
            }
            else
            {
                if (currentSize > FONT_MIN_SIZE)
                {
                    double newSize = Math.Max(FONT_MIN_SIZE, currentSize - 1);
                    tbxMultiLine.FontSize = newSize;
                }
            }
        }


        /// <summary>
        /// The "Document" Property of the Avalon TextEdit does not notify back to the VM
        /// Therefore, it's necessary to catch the Textchanged event in order to update the WordCount
        /// </summary>
        private void TbxMultiLine_TextChanged(object sender, EventArgs e)
        {
            if(_mainWindowViewModel != null)
                this._mainWindowViewModel.WordCount = this.tbxMultiLine.Document.TextLength.ToString();
        }
    }
}