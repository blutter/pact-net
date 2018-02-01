using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Abstractions;

namespace Provider.Api.Web.DotnetCore.Tests
{
    public class EventApiTests : IDisposable
    {
        private readonly ITestOutputHelper _output;

        public EventApiTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void EnsureEventApiHonoursPactWithConsumer()
        {
            //Arrange
            const string serviceUri = "http://localhost:9223";
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(_output)
                }
            };

            using (var webHost = new WebHostBuilder()
                .UseKestrel()
                .UseIISIntegration()
                .UseStartup<TestStartup>()
                .UseUrls(serviceUri)
                .Build())
            {
                webHost.Start();

                //Act / Assert
                IPactVerifier pactVerifier = new PactVerifier(config);
                pactVerifier
                    .ProviderState($"{serviceUri}/provider-states")
                    .ServiceProvider("Event API", serviceUri)
                    .HonoursPactWith("Event API Consumer")
                    .PactUri($"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Consumer.Tests{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}event_api_consumer-event_api.json")
                    .Verify();
            }
        }

        public virtual void Dispose()
        {
        }
    }
}
