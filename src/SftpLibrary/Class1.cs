using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using System.Linq;

namespace SftpLibrary {
    public class ClientOptions {
        public string Host { set; get; }
        public int Port { set; get; }
        public string User { set; get; }
        public string Password { set; get; }
    }
    public class SecureClient {
        private readonly ClientOptions _options;
        private readonly ILogger<SecureClient> _logger;

        public SecureClient(ClientOptions options, ILogger<SecureClient> logger) {
            _options = options;
            _logger = logger;
        }

        private void CreateDirectory(SftpClient client, string destination) {
            var paths = destination.TrimEnd('/').Split("/");

            if (paths[0] == string.Empty) {
                paths[0] = "/";
            }

            var currentPath = paths.ElementAt(0);

            if (!client.Exists(currentPath)) {
                _logger.LogInformation("Create directory {0}", currentPath);
                client.CreateDirectory(currentPath);
            }

            foreach (var item in paths.Skip(1)) {
                currentPath = Path.Combine(currentPath, item);

                if (!client.Exists(currentPath)) {
                    _logger.LogInformation("Create directory {0}", currentPath);
                    client.CreateDirectory(currentPath);
                }
            }
        }

        private string GetPath(string fullName) {
            var fileName = Path.GetFileName(fullName);
            return fullName.Replace(fileName, string.Empty).TrimEnd('/');
        }

        public (bool, string) UploadFile(string localPath, string destination) {
            var fullPath = new FileInfo(localPath).FullName;

            _logger.LogInformation("Upload file {0} to {1}", fullPath, destination);

            using var client = new SftpClient(_options.Host, _options.User, _options.Password);
            client.Connect();

            var path = GetPath(destination);
            CreateDirectory(client, path);

            using var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            client.UploadFile(stream, destination);

            return (true, "");
        }
    }
}
