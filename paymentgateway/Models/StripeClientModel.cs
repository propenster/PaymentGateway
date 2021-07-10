using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentgateway.Models
{

    public class StripeClientModel
    {

        public string CardAddressCountry { get; set; }
        public string CardAddressLine1 { get; set; }
        public string CardAddressLine2 { get; set; }
        public string CardAddressCity { get; set; }
        public string CardAddressZip { get; set; }
        public string CardCvc { get; set; }
        public long CardExpirationMonth { get; set; }
        public long CardExpirationYear { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }




    }
}