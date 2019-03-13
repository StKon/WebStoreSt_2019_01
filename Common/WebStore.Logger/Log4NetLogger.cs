using log4net;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        /// <summary> конструктор </summary>
        public Log4NetLogger(string name, XmlElement xmlElement)
        {
            //репозиторий
            var _loggerRepository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            //логгер
            _log = LogManager.GetLogger(_loggerRepository.Name, name);
      
            //конфигурируем логгер
            log4net.Config.XmlConfigurator.Configure(_loggerRepository, xmlElement);
        }

        /// <summary> областей в логе не будет </summary>
        public IDisposable BeginScope<TState>(TState state) => null;

        /// <summary> включен ли логгер для указанного уровня логирования </summary>
        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Critical:
                    return _log.IsFatalEnabled;
                case LogLevel.Debug:
                case LogLevel.Trace:
                    return _log.IsDebugEnabled;
                case LogLevel.Error:
                    return _log.IsErrorEnabled;
                case LogLevel.Information:
                    return _log.IsInfoEnabled;
                case LogLevel.Warning:
                    return _log.IsWarnEnabled;
                case LogLevel.None:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        /// <summary> Сохраняет мнфо в log4net </summary>
        public void Log<TState>(LogLevel level, EventId id, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //для уровня логгер выключен
            if (!IsEnabled(level))
                return;

            //формата нет
            if (formatter is null)
                throw new ArgumentNullException(nameof(formatter));

            //формирование сообщения
            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message) && exception == null) return;

            switch (level)
            {
                case LogLevel.Critical:
                    _log.Fatal(message);
                    break;
                case LogLevel.Debug:
                case LogLevel.Trace:
                    _log.Debug(message);
                    break;
                case LogLevel.Error:
                    _log.Error(message);
                    break;
                case LogLevel.Information:
                    _log.Info(message);
                    break;
                case LogLevel.Warning:
                    _log.Warn(message);
                    break;
                case LogLevel.None:
                    break;
                default:
                    _log.Warn($"Encountered unknown log level {level}, writing out as Info." );
                    _log.Info(message, exception);
                    break;
            }
        }
    }
}
