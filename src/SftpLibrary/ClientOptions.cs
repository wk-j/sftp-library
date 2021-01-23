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
}
