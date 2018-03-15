﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MimeKit.Utils;

namespace Lykke.Service.IcoCommon.Services
{
    public class SmtpService : ISmtpService
    {
        private readonly SmtpSettings _defaultSmtpSettings;

        public SmtpService(SmtpSettings defaultSmtpSettings)
        {
            _defaultSmtpSettings = defaultSmtpSettings;
        }

        public async Task SendAsync(IEmail email, SmtpSettings smtpSettings = null)
        {
            var settings = smtpSettings ?? _defaultSmtpSettings;

            MimeEntity messageBody = null;

            if (email.Attachments == null)
            {
                messageBody = new TextPart(TextFormat.Html) { Text = email.Body };
            }
            else
            {
                var builder = new BodyBuilder
                {
                    HtmlBody = email.Body
                };

                foreach (var item in email.Attachments)
                {
                    var image = builder.LinkedResources.Add(item.Key, item.Value);
                    image.ContentId = MimeUtils.GenerateMessageId();
                    image.IsAttachment = false;

                    builder.HtmlBody = builder.HtmlBody.Replace($"{{{item.Key}}}", image.ContentId);
                }

                messageBody = builder.ToMessageBody();
            }

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(settings.DisplayName, settings.From));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email.To));
            emailMessage.Subject = email.Subject;
            emailMessage.Body = messageBody;

            if (settings.Host.ToLower().Contains("smtp.office365.com"))
            {
                await SendViaOffice365Smtp(settings, emailMessage);

                return;
            }

            await SendViaDefaultSmtp(settings, emailMessage);
        }

        private async Task SendViaDefaultSmtp(SmtpSettings settings, MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.LocalDomain = settings.LocalDomain;

                    await client.ConnectAsync(settings.Host, settings.Port, SecureSocketOptions.None).ConfigureAwait(false);
                    await client.AuthenticateAsync(settings.Login, settings.Password).ConfigureAwait(false);
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
                catch
                {
                    await Task.Delay(500);
                    throw;
                }
            }
        }

        private async Task SendViaOffice365Smtp(SmtpSettings settings, MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.LocalDomain = settings.LocalDomain;
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    await client.ConnectAsync(settings.Host, settings.Port, SecureSocketOptions.StartTls).ConfigureAwait(false);
                    await client.AuthenticateAsync(settings.Login, settings.Password).ConfigureAwait(false);
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
                catch
                {
                    await Task.Delay(500);
                    throw;
                }
            }
        }
    }
}
