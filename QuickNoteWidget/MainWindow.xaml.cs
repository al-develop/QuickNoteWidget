using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm.Native;
using Ookii.Dialogs.Wpf;
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
        private const int MIN_WINDOW_HEIGHT = 100;
        private const int MIN_WINDOW_WIDTH = 470;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _mainWindowViewModel = new MainWindowViewModel();
            tbxMultiLine.Focus();
            _mainWindowViewModel.ResetWindowSizeAction = new Action(ResetWindowSize);
        }

        #region Context Menu Events

        private void contextSave_Click(object sender, RoutedEventArgs e)
        {
            string content = this.tbxMultiLine.Document.Text;
            var dialog = new VistaSaveFileDialog();
            dialog.ShowDialog();
            string fileName = dialog.FileName;
            FileStream stream = File.Create(fileName);
            stream.Close();
            File.WriteAllText(fileName, content);
        }
        
        private void contextClose_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void contextMaximize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Normal;

        private void contextMinimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

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
        #endregion



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

            HandleFontSizeChange(e, ctrlKeyDown);
            if (e.Handled)
                return;
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




        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool altKeyDown = GetAltKeyDown();
            bool hKeyDown = GetHKeyDown(); // h - height
            bool wKeyDown = GetWKeyDown(); // w - Width

            // mouse wheel up equals e.Delta higher than 0
            // mouse wheel down equals e.Delta smaller than 0
            bool increase = e.Delta > 0;

            HandleWindowResize(increase, altKeyDown, hKeyDown, wKeyDown);
        }

        private void HandleWindowResize(bool increase, bool altKeyDown, bool hKeyDown, bool wKeyDown)
        {
            if (altKeyDown && hKeyDown)
                ResizeHeight(increase);
            else if (altKeyDown && wKeyDown)
                ResizeWidth(increase);
        }


        private void ResizeHeight(bool increase)
        {
            if (increase)
                IncreaseHeight();
            else if (this.Height > MIN_WINDOW_HEIGHT)
                DecreaseHeight();
        }

        private void IncreaseHeight() => this.Height += WINDOW_RESIZE_FACTOR;
        

        private void DecreaseHeight() => this.Height -= WINDOW_RESIZE_FACTOR;
        



        private void ResizeWidth(bool increase)
        {
            if (increase)
                IncreaseWidth();
            else if (this.Width > MIN_WINDOW_WIDTH)
                DecreaseWidth();
        }

        private void IncreaseWidth() => this.Width += WINDOW_RESIZE_FACTOR;

        private void DecreaseWidth() => this.Width -= WINDOW_RESIZE_FACTOR;

        private void btnIncreaseWindowSize_Click(object sender, RoutedEventArgs e)
        {
            ResizeHeight(increase: true);
            ResizeWidth(increase: true);
        }

        private void btnReduceWindowSize_Click(object sender, RoutedEventArgs e)
        {
            ResizeHeight(increase: false);
            ResizeWidth(increase: false);
        }




        /// <summary>
        /// The "Document" Property of the Avalon TextEdit does not notify back to the VM
        /// Therefore, it's necessary to catch the Textchanged event in order to update the WordCount
        /// </summary>
        private void TbxMultiLine_TextChanged(object sender, EventArgs e)
        {
            if (_mainWindowViewModel != null)
                this._mainWindowViewModel.WordCount = this.tbxMultiLine.Document.TextLength.ToString();
        }


        private void ResetWindowSize()
        {
            this.Width = 650;
            this.Height = 350;
        }


        #region GetKeyDownMethods
        private static bool GetSubtractKeyDown() => Keyboard.IsKeyDown(Key.Subtract);
        private static bool GetAddKeyDown() => Keyboard.IsKeyDown(Key.Add);
        private static bool GetCtrlKeyDown() => (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
        private static bool GetWKeyDown() => Keyboard.IsKeyDown(Key.W);
        private static bool GetHKeyDown() => Keyboard.IsKeyDown(Key.H);
        private static bool GetAltKeyDown() => (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt));
        #endregion GetKeyDownMethods
    }
}