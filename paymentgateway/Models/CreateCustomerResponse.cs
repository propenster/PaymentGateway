using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentgateway.Models
{
    public class CreateCustomerResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("object")]
        public string Object { get; set; }
        [JsonProperty("address")]
        public object Address { get; set; }
        [JsonProperty("balance")]
        public long? Balance { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("default_source")]
        public object DefaultSource { get; set; }
        [JsonProperty("delinquent")]
        public bool Delinquent { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("discount")]
        public object Discount { get; set; }
        [JsonProperty("email")]
        public object Email { get; set; }
        [JsonProperty("invoice_prefix")]
        public string InvoicePrefix { get; set; }
        [JsonProperty("invoice_settings")]
        public Stripe.CustomerInvoiceSettings InvoiceSettings { get; set; }
        [JsonProperty("livemode")]
        public bool Livemode { get; set; }
        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
        [JsonProperty("name")]
        public object Name { get; set; }
        [JsonProperty("next_invoice_sequence")]
        public long NextInvoiceSequence { get; set; }
        [JsonProperty("phone")]
        public object Phone { get; set; }
        [JsonProperty("preferred_locales")]
        public List<string> PreferredLocales { get; set; }
        [JsonProperty("shipping")]
        public object Shipping { get; set; }
        [JsonProperty("tax_exempt")]
        public string TaxExempt { get; set; }
        public Stripe.StripeList<Stripe.IPaymentSource> Sources { get; set; }
        public bool? Deleted { get; set; }
    }


    public class InvoiceSettings
    {
        [JsonProperty("custom_fields")]
        public object CustomFields { get; set; }
        [JsonProperty("default_payment_method")]
        public object DefaultPaymentMethod { get; set; }
        [JsonProperty("footer")]
        public object Footer { get; set; }
    }

    public class Metadata
    {
    }



}