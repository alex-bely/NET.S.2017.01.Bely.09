using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.UI
{
    /// <summary>
    /// Provides logging methods
    /// </summary>
    public class NLogAdapter : ILogger
    {
        private readonly Logger logger;

        /// <summary>
        /// Initializes Logger with existing one
        /// </summary>
        /// <param name="logger">Exisiting NLogLogger</param>
        public NLogAdapter(Logger logger)
        {
            if (ReferenceEquals(logger, null))
                throw new ArgumentNullException($"{nameof(logger)} is null.");

            this.logger = logger;
        }

        /// <summary>
        /// Creates new Logger instance
        /// </summary>
        public NLogAdapter()
        {
           logger= LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Writes the diagnostic message at the Info level.
        /// </summary>
        /// <param name="message">Log message</param>
        public void Debug(string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Error level.
        /// </summary>
        /// <param name="message">Log message</param>
        public void Error(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="message">Log message</param>
        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Info level.
        /// </summary>
        /// <param name="message">Log message</param>
        public void Info(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Trace level.
        /// </summary>
        /// <param name="message">Log message</param>
        public void Trace(string message)
        {
            logger.Trace(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="message">Log message</param>
        public void Warn(string message)
        {
            logger.Warn(message);
        }
    }
}
