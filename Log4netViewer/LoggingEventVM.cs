/*
 *  Log4netViewer
 *
 *  Copyright (c) 2017, by Nicolas LLOBERA <nllobera@gmail.com>
 *  Under LGPL license
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Log4netViewer
{
    /// <summary>
    /// A View-Model for LoggingEventData
    /// </summary>
    public class LoggingEventVM : INotifyPropertyChanged
    {
        /// <summary>
        /// The LoggingEventData
        /// </summary>
        public LoggingEventLight LoggingEvent { get; set; }

        /// <summary>
        /// Get a Brush corresponding to the Level.
        /// Used by the ItemTemplate's Border
        /// </summary>
        public Brush BorderColor
        {
            get
            {
                switch (LoggingEvent.Level)
                {
                    case "FATAL":
                    case "ERROR":
                        return Brushes.Red;
                    case "WARN":
                        return Brushes.LightSalmon;
                    case "INFO":
                        return Brushes.LightBlue;
                    case "DEBUG":
                        return Brushes.LightGray;
                    default:
                        return Brushes.Black;
                }
            }
        }

        /// <summary>
        /// Display the Details Button if the LoggingEvent contains an exception.
        /// </summary>
        public Visibility DetailsButtonVisibility
        {
            get
            {
                return string.IsNullOrEmpty(LoggingEvent.Exception) ? Visibility.Hidden : Visibility.Visible;
            }
        }

        private bool _displayDetails = false;
        /// <summary>
        /// Binded to the Detail button to toogle the 
        /// </summary>
        public bool DisplayDetails
        {
            get
            {
                return _displayDetails;
            }
            set
            {
                if (_displayDetails == value)
                {
                    return;
                }

                _displayDetails = value;
                RaisePropertyChanged("DetailsVisibility");
            }
        }

        /// <summary>
        /// Display the Details Textblock according to the DisplayDetails property.
        /// </summary>
        public Visibility DetailsVisibility
        {
            get
            {
                return DisplayDetails ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
