using Microsoft.AspNetCore.Identity.UI.Services;

namespace COMP2139_CumulativeLabs.Services {
    public class EmailSender : IEmailSender {

        private readonly string _sendGridKey;

        public EmailSender(IConfiguration configuration){

        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage) {
            throw new NotImplementedException();
        }
    }
}
