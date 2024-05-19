using Twilio.Rest.Api.V2010.Account;

namespace E_Commerece_PL.Helper
{
    public interface ISmsService
    {
        MessageResource Send(SmsMessage sms);
    }
}
