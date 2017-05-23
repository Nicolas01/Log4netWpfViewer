/*
 *  Log4netViewer
 *
 *  Copyright (c) 2017, by Nicolas LLOBERA <nllobera@gmail.com>
 *  Under LGPL license
 */
using log4net.Appender;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4netViewer
{
    public class RollingFileAppenderXmlLayoutLocation : RollingFileAppender
    {
        public RollingFileAppenderXmlLayoutLocation() : base()
        {
            Layout = new XmlLayout(true);
        }
    }
}
