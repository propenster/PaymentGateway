using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentgateway.Models
{
    public class CreateVirtualCardRequest
    {
        /// <summary>
        /// Unique Id of the Card Holder created at the CreateCardHolder endpoint
        /// </summary>
        public string CardHolder { get; set; }
        /// <summary>
        /// The Type of Card to be created - Virtual or Physical
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 3-char ISO code of the Card's charging currency e.g gbp or usd
        /// </summary>
        public string Currency { get; set; }
    }
}
