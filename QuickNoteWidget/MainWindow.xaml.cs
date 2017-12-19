using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using OpenFolderDialog;
using Application = System.Windows.Application;
using Clipboard = System.Windows.Clipboard;
using DataFormats = System.Windows.DataFormats;


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

        private void btnCopyMultiLine_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetDataObject(tbxMultiLine.Text, true);
        }

        private void btnCopySingleLine_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetDataObject(tbxSingleLine.Text, true);
        }

        private void btnSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderSelectDialog();
            dialog.ShowDialog();
            string selection = dialog.FileName;

            using (var file = new System.IO.StreamWriter($"{selection}\\QuickNoteWidget_{DateTime.Now.ToShortDateString()}.txt", true))
            {
                file.WriteLine(tbxMultiLine.Text);
            }
        }

        private void btnLoadFromFile_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Text Files | *.txt";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = dialog.FileName;
                string textContent = System.IO.File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(tbxMultiLine.Text))
                {
                    MessageBoxResult keepContent = System.Windows.MessageBox.Show("Override existing content?",
                                                                                  "Content is not empty",
                                                                                  MessageBoxButton.YesNoCancel,
                                                                                  MessageBoxImage.Question);

                    switch (keepContent)
                    {
                        case MessageBoxResult.Yes:
                            tbxMultiLine.Text = textContent;
                            break;

                        case MessageBoxResult.No:
                            tbxMultiLine.Text += textContent;
                            break;

                        default:
                            return;
                    }
                }
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            var info = new InfoWindow();
            info.Show();
        }

        private void listBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _mainWindowViewModel.stpCbxWrapper_MouseDown();

        }
    }
}