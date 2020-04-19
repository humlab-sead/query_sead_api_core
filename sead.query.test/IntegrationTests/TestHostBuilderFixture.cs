﻿using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using SQT.Infrastructure;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class TestHostBuilderFixture : IDisposable
    {
        public IHostBuilder Builder;
        public Task<IHost> Server;
        public HttpClient Client;
        public TestHostBuilderFixture()
        {
            Builder = new SeadTestHostBuilder().Create<TestStartup<TestDependencyService>>();
            Server = Builder.StartAsync();
            Client = Server.Result.GetTestClient();
        }

        public void Dispose()
        {
            Server.Dispose();
            Client.Dispose();
        }
    }
}
