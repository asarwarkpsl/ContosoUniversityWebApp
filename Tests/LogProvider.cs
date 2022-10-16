using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Tests
{
    internal class LogProvider : ILoggerProvider
    {
        private readonly Action<string> _loggerAction;
        private readonly LogLevel _logLevel;

        public LogProvider(Action<string> loggerAction, LogLevel logLeve = LogLevel.Information)
        {
            _loggerAction = loggerAction;
            _logLevel = logLeve;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new TestLogger(_loggerAction, _logLevel);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
