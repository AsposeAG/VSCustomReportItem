using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Microsoft.Samples.ReportingServices
{
    class OnCreateLogger
    {
        public OnCreateLogger(string FileName, string aMessage)
        {
            Logger.LogStringToFile(FileName, aMessage);
        }
    }

    class Logger
    {
        public static void LogStringToFile(string Filename, string aString)
        {
            string lString = DateTime.Now.ToString() + ":" + aString + "\n";
            WriteStringToFile(Filename, lString, true);
        }

        public static void WriteStringToFile(string Filename, string aString, bool Append)
        {
            if (Append)
            {
                if (System.IO.File.Exists(Filename))
                {
                    using (StreamWriter lWriter = System.IO.File.AppendText(Filename))
                    {
                        lWriter.Write(aString);
                        lWriter.Flush();
                        lWriter.Close();
                    }
                }
                else
                    System.IO.File.WriteAllText(Filename, aString, Encoding.UTF8);
            }
            else
            {
                if (System.IO.File.Exists(Filename))
                    System.IO.File.Delete(Filename);
                System.IO.File.WriteAllText(Filename, aString, Encoding.UTF8);
            }
        }
    }
}
