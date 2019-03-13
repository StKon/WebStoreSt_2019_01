using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly string _log4NetConfigFile;
        private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers = new ConcurrentDictionary<string, Log4NetLogger>();

        /// <summary> конструктор </summary>
        /// <param name="log4NetConfigFile">файл конфигурации</param>
        public Log4NetLoggerProvider(string log4NetConfigFile)
        {
            _log4NetConfigFile = log4NetConfigFile;
        }

        /// <summary> Создать логгер </summary>
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, CreateLoggerImplementation(categoryName));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }

        /// <summary> создание нового логгера </summary>
        private Log4NetLogger CreateLoggerImplementation(string name)
        {
            var log4NetConfig = new XmlDocument();
            log4NetConfig.Load(_log4NetConfigFile);
            return new Log4NetLogger(name, log4NetConfig["log4net"]);
        }
    }
}
