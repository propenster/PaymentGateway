using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using paymentgateway.Models;

namespace paymentgateway.Services
{


    public interface IStripeService
    {

        Task<string> GetTokenId(StripeClientModel model);
        Task<string> ChargeCustomer(string TokenId, int Amount, string Currency, string Description);


        Task<object> CreateCustomer(CreateCustomerRequest request);
        Task<object> RetrieveCustomer(string CustomerId);
        Task<object> UpdateCustomer(string CustomerId, CreateCustomerRequest request);
        Task<object> UpdateCustomerCreditBalance(string CustomerId, long? Balance);
        Task<object> UpdateCustomerAddress(string CustomerId, Stripe.AddressOptions address);
        Task<object> UpdateCustomerName(string CustomerId, string Name);



    }

}