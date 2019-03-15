using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace WebStore.Logger
{
    public static class Log4NetExtensions
    {
        /// <summary> Расширение интерфейса ILoggerFactory. Добавляет провайдер. </summary>
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string log4NetConfigFile = "log4net.config")
        {
            //Путь к файлу
            var file = new System.IO.FileInfo(log4NetConfigFile);
            if (!Path.IsPathRooted(log4NetConfigFile))
            {
                var assembly = Assembly.GetEntryAssembly();
                var dir = Path.GetDirectoryName(assembly.Location);
                log4NetConfigFile = Path.Combine(dir, log4NetConfigFile);
            }

            factory.AddProvider(new Log4NetLoggerProvider(log4NetConfigFile));
            return factory;
        }
    }
}
