using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace Vote_Final
{
    public class TwilioVerifyService
    {
        private string AccountSid;
        private string AuthToken;
        private string ServiceSid;

        public TwilioVerifyService(string accountSid, string authToken, string serviceSid)
        {
            AccountSid = accountSid;
            AuthToken = authToken;
            ServiceSid = serviceSid;
        }

        public bool SendVerificationCode(string phoneNumber)
        {
            try
            {
                TwilioClient.Init(AccountSid, AuthToken);

                var verification = VerificationResource.Create(
                    to: phoneNumber,
                    channel: "sms",
                    pathServiceSid: ServiceSid
                    );
                return verification.Status == "pending";
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            // Initialize TwilioVerifyService with your Twilio credentials
            var twilioVerifyService = new TwilioVerifyService(
                "AC3f1a815d57ba50d769ec0e23273a3f71",
                "7263663b20f2c0a482944c2461438afb",
                "VA943cea128e29ef581769164fa4c871d3"
            );

            // Sample phone number for testing (replace with actual number)
            string phoneNumber = "+14064590721";

            // Send verification code and handle the result
            bool codeSent = twilioVerifyService.SendVerificationCode(phoneNumber);

            if (codeSent)
            {
                Console.WriteLine("Verification code sent successfully!");
            }
            else
            {
                Console.WriteLine("Failed to send verification code. Check logs for details.");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
