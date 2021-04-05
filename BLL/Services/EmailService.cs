using BLL.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class EmailService : IEmailService
	{
		public async Task SendEmailAsync(string email, string subject, string message)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress("Task Tracking System", "epam.tasktracking@gmail.com"));
			emailMessage.To.Add(new MailboxAddress("", email));
			emailMessage.Subject = subject;
			emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
			{
				Text = message
			};

			using (var client = new SmtpClient())
			{
				await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
				await client.AuthenticateAsync("epam.tasktracking@gmail.com", "TestPassword123");
				await client.SendAsync(emailMessage);
				await client.DisconnectAsync(true);
			}
		}
	}
}
