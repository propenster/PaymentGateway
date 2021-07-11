using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using paymentgateway.Config;
using paymentgateway.Models;
using Stripe;
using Stripe.Issuing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace paymentgateway.Services
{
    public class VirtualCardIssuingService : IVirtualCardIssuingService
    {

        private StripeSettings _settings;

        public VirtualCardIssuingService(IOptions<StripeSettings> settings)
        {
            _settings = settings.Value;
            StripeConfiguration.ApiKey = _settings.ApiKey; 
        }
        public async Task<object> CreateCardHolder(CreateCardHolderRequest request)
        {
            var options = new CardholderCreateOptions
            {
                Billing = new CardholderBillingOptions
                {
                    Address = new AddressOptions
                    {
                        Line1 = request.Address.Line1,
                        City = request.Address.City,
                        PostalCode = request.Address.PostalCode,
                        Country = request.Address.Country //2chars
                    },
                    
                },
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
                Status = request.Status,
                Type = request.Type,
                //SpendingControls = 333
            };

            var service = new CardholderService();
            var cardHolder = await service.CreateAsync(options);

            return cardHolder;
        }

        public async Task<object> CreateVirtualCard(CreateVirtualCardRequest request)
        {
            var options = new Stripe.Issuing.CardCreateOptions
            {
                Cardholder = request.CardHolder,
                Type = request.Type,
                Currency = request.Currency,
            };
            var service = new Stripe.Issuing.CardService();
            var card = await service.CreateAsync(options);

            //var json = await new StreamReader(stream: HttpContext.Request.Body).ReadToEndAsync();
            //var stripeEvent = EventUtility.ParseEvent(json);


            return card;
        }

        public async Task<object> DeactivateLostOrStolenCard(string CardId)
        {
            var options = new Stripe.Issuing.CardUpdateOptions
            {
                Status = "inactive", //canceled, active
                CancellationReason = "lost", //stolen
            };
            var service = new Stripe.Issuing.CardService();
            var card = await service.UpdateAsync(CardId, options);

            return card;
        }

        public async Task<object> GetListOfAllCards(int? Limit)
        {
            var options = new Stripe.Issuing.CardListOptions { Limit = Limit };
            var service = new Stripe.Issuing.CardService();
            StripeList<Stripe.Issuing.Card> cards = await service.ListAsync(options);

            return cards;
        }

        public async Task<object> GetPhysicalCards(int? Limit, string Type = "physical")
        {
            var options = new Stripe.Issuing.CardListOptions { Limit = Limit, Type = Type };
            var service = new Stripe.Issuing.CardService();
            StripeList<Stripe.Issuing.Card> cards = await service.ListAsync(options);
            
            return cards;
        }

        public async Task<object> GetVirtualCards(int? Limit, string Type = "virtual")
        {
            var options = new Stripe.Issuing.CardListOptions { Limit = Limit, Type = Type };
            var service = new Stripe.Issuing.CardService();
            StripeList<Stripe.Issuing.Card> cards = await service.ListAsync(options);

            return cards;
        }

        public async Task<object> RetrieveACard(string CardId)
        {
            var service = new Stripe.Issuing.CardService();
            var card = await service.GetAsync(CardId);
            return card;
        }

        public async Task<object> RetrieveAVirtualCard(string CardId)
        {
            var service = new Stripe.Issuing.CardService();
            var options = new Stripe.Issuing.CardGetOptions();
            options.AddExpand("number");
            options.AddExpand("cvc");
            var details = await service.GetAsync(CardId, options);
            
            return details;
        }

        public async Task<object> UpdateVirtualCardStatus(string CardholderId, UpdateVirtualCardStatus request)
        {
            var options = new Stripe.Issuing.CardUpdateOptions
            {
                Status = request.Status
            };

            var service = new Stripe.Issuing.CardService();
            var card = await service.UpdateAsync(CardholderId, options);

            return card;
        }
    }
}
