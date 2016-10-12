using Cyrus.Core.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;

namespace Cyrus.Data.Identity
{
    public class SecureTokenFormatter : ISecureTokenFormatter
    {

        // Fields
        private TicketSerializer _serializer;
        private IDataProtector _protector;
        private ITextEncoder _encoder;
        private readonly ISecureDataFormat<AuthenticationTicket> _dataFormat;

        // Constructors
        public SecureTokenFormatter(string key, ISecureDataFormat<AuthenticationTicket> dataFormat)
        {
            _serializer = new TicketSerializer();
            _protector = new AesDataProtectorProvider(key);
            _encoder = TextEncodings.Base64Url;
            _dataFormat = dataFormat;

        }

        // ISecureDataFormat<AuthenticationTicket> Members
        public string Protect(AuthenticationTicket ticket)
        {
            var ticketData = this.serializer.Serialize(ticket);
            var protectedData = this.protector.Protect(ticketData);
            var protectedString = this.encoder.Encode(protectedData);
            return protectedString;
        }

        public AuthenticationTicket Unprotect(string text)
        {
            var protectedData = this.encoder.Decode(text);
            var ticketData = this.protector.Unprotect(protectedData);
            var ticket = this.serializer.Deserialize(ticketData);
            return ticket;
        }
    }
}
