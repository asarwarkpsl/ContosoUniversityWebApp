using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Tests
{
    internal class TestLogger : ILogger
    {
        private readonly Action<string> _loggerAction;
        private readonly LogLevel _logLevel;

        public TestLogger(Action<string> loggerAction,LogLevel logLeve)
        {
            _loggerAction = loggerAction;
            _logLevel = logLeve;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, 
            Exception? exception, Func<TState, Exception?, string> formatter)
        {
            _loggerAction($"Loglevel: {logLevel},{state}");
        }
    }
}
