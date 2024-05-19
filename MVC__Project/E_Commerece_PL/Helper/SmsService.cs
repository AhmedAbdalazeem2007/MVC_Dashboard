using E_Commerece_PL.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace E_Commerece_PL.Helper
{

    public class SmsService : ISmsService
    {
        private readonly TwillioSettings options;

        public SmsService(IOptions<TwillioSettings> options)
        {
            this.options = options.Value;
        }

        public MessageResource Send(SmsMessage sms)
        {
            TwilioClient.Init(options.AccountSID, options.AuthToken);
            var result = MessageResource.Create(
                body: sms.Body,
                from: new Twilio.Types.PhoneNumber(options.TwilioPhoneNumber),
                to: sms.phoneNumber
                );
            return result;
        }
    }
}
