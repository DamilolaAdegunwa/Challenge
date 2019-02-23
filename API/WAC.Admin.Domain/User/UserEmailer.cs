using WAC.Admin.Domain.Emailing;
using WAC.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using FWK.Domain.bus;
using FWK.Domain.Extensions;
using FWK.Domain.Mail;
using FWK.Extensions;

namespace WAC.Admin.Domain.Interfaces.Mail
{
    public class UserEmailer : IUserEmailer, ITransientDependency
    {        
        private readonly IEmailSender _emailSender;
        private readonly IEmailTemplateProvider _emailTemplateProvider;
        public UserEmailer(
            IEmailTemplateProvider emailTemplateProvider,
            IEmailSender emailSender)
        {
            _emailTemplateProvider = emailTemplateProvider;
            _emailSender = emailSender;            
        }



        public async Task SendPasswordResetLinkAsync(SysUsers user, string link = null)
        {
            if (user.PasswordResetCode.IsNullOrEmpty())
            {
                throw new Exception("PasswordResetCode should be set in order to send password reset link.");
            }

            
            var emailTemplate = GetTitleAndSubTitle("Restablecer contraseña", string.Empty);
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<b>" + "Nombre completo" + "</b>: " + user.DisplayName + "<br />");

            

            mailMessage.AppendLine("<b>" + "Usuario" + "</b>: " + user.DisplayName + "<br />");
            //mailMessage.AppendLine("<b>" + "Codigo de Reseteo" + "</b>: " + user.PasswordResetCode + "<br />");

            if (!link.IsNullOrEmpty())
            {
                link = link.Replace("{userId}", user.Id.ToString());
                link = link.Replace("{resetCode}", Uri.EscapeDataString(user.PasswordResetCode));
                               

                mailMessage.AppendLine("<br />");

                var linkHtml = "<a href=\"" + link + "\">" + "restablecer su contraseña" + "</a>";

                mailMessage.AppendLine(("Por favor siga el enlace a continuación para " + linkHtml) + "<br /><br />");
                //mailMessage.AppendLine("<a href=\"" + link + "\">" + "restablecer su contraseña" + "</a>");
            }

            await ReplaceBodyAndSend(user.Email, ("Rosario Bus | Restablecer contraseña"), emailTemplate, mailMessage);
        }


        private StringBuilder GetTitleAndSubTitle(string title, string subTitle)
        {
            var emailTemplate = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate());
            emailTemplate.Replace("{EMAIL_TITLE}", title);
            emailTemplate.Replace("{EMAIL_SUB_TITLE}", subTitle);

            return emailTemplate;
        }

        private async Task ReplaceBodyAndSend(string emailAddress, string subject, StringBuilder emailTemplate, StringBuilder mailMessage)
        {
            emailTemplate.Replace("{EMAIL_BODY}", mailMessage.ToString());
            await _emailSender.SendAsync(new MailMessage
            {
                To = { emailAddress },
                Subject = subject,
                Body = emailTemplate.ToString(),
                IsBodyHtml = true
            });
        }
    }
}
