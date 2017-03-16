using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Logs
{
    public class ConsoleLogger : ILogger
    {
        public void Trace(string message)
        {
            Console.WriteLine("[TRACE] " + message);
        }

        public void Trace(string message, params object[] args)
        {
            Console.WriteLine("[TRACE] " + message, args);
        }

        public void Debug(string message)
        {
            Console.WriteLine("[DEBUG] " + message);
        }

        public void Debug(string message, params object[] args)
        {
            Console.WriteLine("[DEBUG] " + message, args);
        }

        public void Info(string message)
        {
            Console.WriteLine("[INFO] " + message);
        }

        public void Info(string message, params object[] args)
        {
            Console.WriteLine("[INFO] " + message, args);
        }

        public void Warn(string message)
        {
            Console.WriteLine("[WARN] " + message);
        }

        public void Warn(string message, params object[] args)
        {
            Console.WriteLine("[WARN] " + message, args);
        }

        public void Error(string message)
        {
            Console.WriteLine("[ERROR] " + message);
        }

        public void Error(string message, params object[] args)
        {
            Console.WriteLine("[ERROR] " + message, args);
        }

        public void Fatal(string message)
        {
            Console.WriteLine("[FATAL] " + message);
        }

        public void Fatal(string message, params object[] args)
        {
            Console.WriteLine("[FATAL] " + message, args);
        }
    }
}
