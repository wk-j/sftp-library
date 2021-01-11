using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SftpLibrary;
using Xunit;

namespace Tests {
    public class UnitTest1 {
        [Fact]
        public void Test1() {
            var service = new ServiceCollection();
            service.AddSingleton(new ClientOptions {
                Host = "localhost",
                Port = 22,
                User = "demo",
                Password = "demo"
            });
            service.AddLogging(log => {
                log.AddConsole();
            });
            service.AddSingleton<SecureClient>();

            var client = service.BuildServiceProvider().GetService<SecureClient>();

            client.UploadFile("../../../../../README.md", "a/b/c/d/e/f/README.md");
        }
    }
}
