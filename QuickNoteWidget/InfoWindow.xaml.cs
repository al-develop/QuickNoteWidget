using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;
using QuickNoteWidget.Converter;
using System;

namespace QuickNoteWidget
{
    public partial class InfoWindow : Window
    {

        private string _currentAccent;
        public InfoWindow(string currentAccent)
        {
            InitializeComponent();
            _currentAccent = currentAccent;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetAccentToTextBlock();
        }

        private void SetAccentToTextBlock()
        {
            IEnumerable<TextBlock> textBlocks = GetElementByType<TextBlock>(this);
            foreach (TextBlock textBlock in textBlocks)
            {
                string hexColor = new AccentToColorConverter().Convert(_currentAccent);
                Brush brushColor = new BrushConverter().ConvertFromString(hexColor) as Brush;
                textBlock.Foreground = brushColor;
                textBlock.InvalidateVisual();
            }
        }

        private static IEnumerable<T> GetElementByType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null)
                yield return (T)Enumerable.Empty<T>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);

                if (ithChild == null) 
                    continue;

                if (ithChild is T t) 
                    yield return t;

                foreach (T childOfChild in GetElementByType<T>(ithChild)) 
                    yield return childOfChild;
            }
        }
    }
}