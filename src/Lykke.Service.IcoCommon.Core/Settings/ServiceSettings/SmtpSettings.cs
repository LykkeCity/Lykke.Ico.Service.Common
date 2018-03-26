using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.IcoCommon.Core.Settings.ServiceSettings
{
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string LocalDomain { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string From { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as SmtpSettings;
            if (other == null)
            {
                return false;
            }

            return
                Host == other.Host &&
                Port == other.Port &&
                LocalDomain == other.LocalDomain &&
                Login == other.Login &&
                Password == other.Password &&
                DisplayName == other.DisplayName &&
                From == other.From;
        }
    }
}
