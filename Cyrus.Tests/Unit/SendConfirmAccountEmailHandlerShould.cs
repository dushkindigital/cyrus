using System.Threading.Tasks;
using Cyrus.Core.DomainServices.Command;
using Moq;
using NUnit.Framework;

namespace Cyrus.Tests.Unit
{
    public class SendConfirmAccountEmailHandlerShould
    {
        [Test]
        public async Task SendNewEmailAddressApprovalEamilHandlerInvokesSendEmailAsyncWithTheCorrectParameters()
        {
            //var message = new SendConfirmAccountEmail { Email = "email", CallbackUrl = "CallbackUrl" };
            //var emailMessage = $"Please confirm your account by clicking this link: <a href=\"{message.CallbackUrl}\">{message.CallbackUrl}</a>";

            //var emailSender = new Mock<IEmailService>();
            //var obj = new SendConfirmAccountEmailHandler(emailSender.Object);
            //await obj.Handle(message);

            //emailSender.Verify(x => x.SendEmailAsync(message.Email, "Confirm your account", emailMessage));
            
        }
    }
}
