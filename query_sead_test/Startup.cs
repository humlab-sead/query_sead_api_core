﻿using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql.Logging;
using QuerySeadDomain;
using System.Diagnostics;

namespace QuerySeadTests
{
   

    [TestClass]
    public class Startup
    {
        public static IConfigurationRoot Configuration;
        public static QueryBuilderSetting Options;

        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            Configure();
            ConfigureServices();
        }

        public static void Configure()
        {
            NpgsqlLogManager.Provider = new ConsoleLoggingProvider(NpgsqlLogLevel.Debug, false, false);
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.test.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Options = Configuration.GetSection("QueryBuilderSetting").Get<QueryBuilderSetting>();
        }

        public static void ConfigureServices()
        {
        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            Debug.WriteLine("AssemblyCleanup");
        }
    }
}
