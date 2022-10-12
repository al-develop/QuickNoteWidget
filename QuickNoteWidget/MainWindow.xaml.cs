using System;
using System.Windows;
using System.Windows.Input;
using Application = System.Windows.Application;
using Clipboard = System.Windows.Clipboard;


namespace QuickNoteWidget
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;
        private const double FONT_MIN_SIZE = 5d;
        private const double FONT_MAX_SIZE = 60d;
        private const int WINDOW_RESIZE_FACTOR = 15;

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
            _mainWindowViewModel.SaveSettingsOnClose();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            var _infoWindow = new InfoWindow(_mainWindowViewModel.SelectedAccent ?? "Cyan");
            _infoWindow.Show();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            bool ctrlKeyDown = GetCtrlKeyDown();
            bool addKeyDown = GetAddKeyDown();
            bool subtractKeyDown = GetSubtractKeyDown();

            if (ctrlKeyDown && addKeyDown)
                UpdateFontSize(true);
            else if (ctrlKeyDown && subtractKeyDown)
                UpdateFontSize(false);
        }

        private void tbxMultiLine_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool ctrlKeyDown = GetCtrlKeyDown();
            bool altKeyDown = GetAltKeyDown();
            bool hKeyDown = GetHKeyDown(); // h - height
            bool wKeyDown = GetWKeyDown(); // w - Width


            HandleFontSizeChange(e, ctrlKeyDown);
            if (e.Handled)
                return;

            HandleWindowResize(e, altKeyDown, hKeyDown, wKeyDown);

        }
        private void HandleFontSizeChange(MouseWheelEventArgs e, bool ctrlButtonPressed)
        {
            if (ctrlButtonPressed)
            {
                bool increase = e.Delta > 0;
                this.UpdateFontSize(increase);
                e.Handled = true;
            }
        }

        private void HandleWindowResize(MouseWheelEventArgs e, bool altKeyDown, bool hKeyDown, bool wKeyDown)
        {
            if (altKeyDown && hKeyDown)
            {
                bool increase = e.Delta > 0;
                double currentSize = this.Height;
                if (increase)
                {
                    double newHeight = currentSize + WINDOW_RESIZE_FACTOR;
                    this.Height = newHeight;
                }
                else if(this.Height > 100)
                {
                    double newHeight = currentSize - WINDOW_RESIZE_FACTOR;
                    this.Height = newHeight;
                }
                else
                {
                    return;
                }

            }
            else if (altKeyDown && wKeyDown)
            {
                bool increase = e.Delta > 0;
                double currentSize = this.Width;
                if (increase)
                {
                    double newWidth = currentSize + WINDOW_RESIZE_FACTOR;
                    this.Width = newWidth;
                }
                else if (this.Width > 100)
                {
                    double newWidth = currentSize - WINDOW_RESIZE_FACTOR;
                    this.Width = newWidth;
                }
                else
                {
                    return;
                }
            }
            e.Handled = true;
        }

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

        #region GetKeyDownMethods
        private static bool GetSubtractKeyDown()
        {
            return Keyboard.IsKeyDown(Key.Subtract);
        }
        private static bool GetAddKeyDown()
        {
            return Keyboard.IsKeyDown(Key.Add);
        }
        private static bool GetCtrlKeyDown()
        {
            return (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
        }
        private static bool GetWKeyDown()
        {
            return Keyboard.IsKeyDown(Key.W);
        }
        private static bool GetHKeyDown()
        {
            return Keyboard.IsKeyDown(Key.H);
        }
        private static bool GetAltKeyDown()
        {
            return (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt));
        }
        #endregion GetKeyDownMethods
    }
}