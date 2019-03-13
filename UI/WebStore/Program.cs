using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using WebStore.Services.Data;
using WebStore.DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Xml;
using System.IO;
using log4net;
using System.Reflection;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //конфигурация log4net
            //var log4netConfig = new XmlDocument();
            //var configFileName = "log4net.config";

            //var fileInfo = new FileInfo(configFileName);  //информация о файле
            
            //log4netConfig.Load(configFileName);  //загружаем XML
            //var repository = LogManager.CreateRepository(
            //    Assembly.GetEntryAssembly(), 
            //    typeof(log4net.Repository.Hierarchy.Hierarchy));

            //конфигурируем
            //log4net.Config.XmlConfigurator.Configure(repository, log4netConfig["log4net"]);

            //log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

            //log.Info("Приложение запущено");

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
