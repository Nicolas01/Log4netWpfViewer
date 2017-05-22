using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Log4netViewer
{
    public class Util
    {
        /// <summary>
        /// Read an xml formated log4net log file and extract LoggingEvent.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IEnumerable<LoggingEventLight> ReadLogFromFile(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // manage no root element
                var settings = new XmlReaderSettings()
                {
                    NameTable = new NameTable(),
                    ConformanceLevel = ConformanceLevel.Fragment
                };
                // manage log4net prefix: <log4net:event
                var nsmgr = new XmlNamespaceManager(settings.NameTable);
                nsmgr.AddNamespace("log4net", "https://logging.apache.org/log4net/");
                var context = new XmlParserContext(null, nsmgr, null, XmlSpace.Default);
                var reader = XmlReader.Create(stream, settings, context);

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "event")
                    {
                        DateTime timestamp;
                        DateTime.TryParse(reader.GetAttribute("timestamp"), out timestamp);

                        var loggingEvent = new LoggingEventLight()
                        {
                            Logger = reader.GetAttribute("logger"),
                            Timestamp = timestamp,
                            Level = reader.GetAttribute("level"),
                            Thread = reader.GetAttribute("thread"),
                            Domain = reader.GetAttribute("domain"),
                            Username = reader.GetAttribute("username")
                        };

                        int eventDepth = reader.Depth;
                        reader.Read();
                        while (reader.Depth > eventDepth)
                        {
                            if (reader.MoveToContent() == XmlNodeType.Element)
                            {
                                switch (reader.LocalName)
                                {
                                    case "message":
                                        loggingEvent.Message = reader.ReadElementContentAsString();
                                        continue;

                                    case "properties":
                                    case "data":
                                        // ignore
                                        break;

                                    case "exception":
                                        loggingEvent.Exception = reader.ReadElementContentAsString();
                                        continue;

                                    case "locationInfo":
                                        var file = reader.GetAttribute("file");
                                        var line = reader.GetAttribute("line");
                                        loggingEvent.Location = file.Substring(file.LastIndexOf("\\") + 1) + ":" + line;
                                        break;

                                    default:
                                        break;
                                }
                            }

                            reader.Read();
                        }

                        yield return loggingEvent;
                    }
                }
            }
        }
    }
}
