using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EgoPadel.Utilidades
{
	public class EmailSender : IEmailSender
	{
		public string SendGridSecret { get; set; }
		public EmailSender(IConfiguration _config)
		{
			SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
		}
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{

			var cliente = new SendGridClient(SendGridSecret);
			var from = new EmailAddress("phpochita@gmail.com");
			var to = new EmailAddress(email);
			var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

			return cliente.SendEmailAsync(msg);
		}
	}
}
