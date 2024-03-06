using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace WelcomeExtended.Loggers
{
    internal class LoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            // throw new NotImplementedException();
            return new HashLogger(categoryName);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }
    }
}
