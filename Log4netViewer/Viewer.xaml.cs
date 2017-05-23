/*
 *  Log4netViewer
 *
 *  Copyright (c) 2017, by Nicolas LLOBERA <nllobera@gmail.com>
 *  Under LGPL license
 */
using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Linq;
using System.Windows.Media;

namespace Log4netViewer
{
    /// <summary>
    /// Interaction logic for Log4netViewer.xaml
    /// </summary>
    public partial class Viewer : UserControl
    {
        public Viewer()
        {
            InitializeComponent();

            /*var collection = lb.Items.SourceCollection as INotifyCollectionChanged;
            collection.CollectionChanged += ListBox_CollectionChanged;*/
        }

        /*private void ListBox_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var border = VisualTreeHelper.GetChild(lb, 0) as Decorator;
                var scrollViewer = border.Child as ScrollViewer;
                scrollViewer.ScrollToEnd();
            }
        }*/

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource",
                typeof(IEnumerable),
                typeof(Viewer),
                new PropertyMetadata(null));

        private void ListBox_ScrollChanged(object sender, RoutedEventArgs e)
        {
            if (((ScrollChangedEventArgs)e).ExtentHeightChange > 0.0)
                ((ScrollViewer)e.OriginalSource).ScrollToEnd();
        }
    }
}
