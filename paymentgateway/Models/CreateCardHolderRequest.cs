using Newtonsoft.Json;
using Stripe.Issuing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentgateway.Models
{
    public class CreateCardHolderRequest
    {
        public Stripe.AddressOptions Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; } //active
        public string Type { get; set; } //individual or corporate
        public CardholderIndividualOptions Individual { get; set; }
        //[JsonProperty("verification")]
        //public CardholderIndividualVerificationOptions Verification { get; set; }
    }
}
