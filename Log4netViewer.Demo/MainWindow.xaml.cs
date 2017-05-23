/*
 *  Log4netViewer.Demo
 *
 *  Copyright (c) 2017, by Nicolas LLOBERA <nllobera@gmail.com>
 *  Under LGPL license
 */
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Log4netViewer.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            //log4netViewer.ItemsSource = Appender.Events;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                RestoreDirectory = true,
                Filter = "log4net logs|*.xml;*.log|All Files|*.*"
            };

            if (ofd.ShowDialog() == true)
            {
                var selectedFile = ofd.FileName;
                foreach (var loggingEvent in Util.ReadLogFromFile(ofd.FileName))
                {
                    Appender.Events.Add(new LoggingEventVM() { LoggingEvent = loggingEvent });
                }
            }
        }

        private void LogFatal_Click(object sender, RoutedEventArgs e)
        {
            log.Fatal("Log Fatal !!!", new Exception("Fatal"));
        }

        private void LogError_Click(object sender, RoutedEventArgs e)
        {
            log.Error("Log Error !!!", new Exception("Error"));
        }

        private void LogWarn_Click(object sender, RoutedEventArgs e)
        {
            log.Warn("Log Warn !!!");
        }

        private void LogInfo_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Log Info !!!");
        }

        private void LogDebug_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("Log Debug !!!");
        }

        public LoggingEventAppender Appender
        {
            get
            {
                return LogManager.GetRepository().GetAppenders().FirstOrDefault(a => a is LoggingEventAppender) as LoggingEventAppender;
            }
        }
    }
}
