using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    /// <summary>
    /// Logger interface
    /// </summary>
    public interface ILogger
    {
        void Trace(string message);

        void Debug(string message);

        void Info(string message);

        void Warn(string message);

        void Fatal(string message);

        void Error(string message);
    }
}
