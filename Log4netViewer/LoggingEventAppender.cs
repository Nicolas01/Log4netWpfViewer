/*
 *  Log4netViewer
 *
 *  Copyright (c) 2017, by Nicolas LLOBERA <nllobera@gmail.com>
 *  Under LGPL license
 */
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Log4netViewer
{
    public class LoggingEventAppender : AppenderSkeleton
    {
        public ObservableCollection<LoggingEventVM> Events { get; private set; } = new ObservableCollection<LoggingEventVM>();

        protected override void Append(LoggingEvent log4netLoggingEvent)
        {
            var loggingEvent = new LoggingEventLight(log4netLoggingEvent);
            Events.Add(new LoggingEventVM() { LoggingEvent = loggingEvent });
        }

        public void LoadLogFile(string filePath)
        {
            foreach (var loggingEvent in Util.ReadLogFromFile(filePath))
            {
                Events.Add(new LoggingEventVM() { LoggingEvent = loggingEvent });
            }
        }
    }
}
