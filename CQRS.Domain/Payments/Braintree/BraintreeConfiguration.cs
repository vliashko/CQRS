using Braintree;

namespace CQRS.Domain.Payments.Braintree
{
    public class BraintreeConfiguration
    {
        public string Environment { get; private set; }
        public string MerchantId { get; private set; }
        public string PublicKey { get; private set; }
        public string PrivateKey { get; private set; }
        private IBraintreeGateway BraintreeGateway { get; set; }

        public IBraintreeGateway CreateGateway()
        {
            Environment = "sandbox";
            MerchantId = "qrn7ckz69sddcprw";
            PublicKey = "t7j9w2jwt4mtpyf7";
            PrivateKey = "fd9b2c4d50ddc1a2c252ded4af0324af";

            return new BraintreeGateway(Environment, MerchantId, PublicKey, PrivateKey);
        }

        public IBraintreeGateway GetGateway()
        {
            if (BraintreeGateway == null)
            {
                BraintreeGateway = CreateGateway();
            }

            return BraintreeGateway;
        }
    }
}
