using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using paymentgateway.Config;
using Stripe;
using Stripe.Checkout;
using Newtonsoft.Json;
using paymentgateway.Models;
using Microsoft.Extensions.Options;
using AutoMapper;
using System.Threading;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace paymentgateway.Services
{
	
	public class StripeService : IStripeService 
	{
		private StripeSettings _settings;

        //IMapper _mapper;
		public StripeService(IOptions<StripeSettings> settings){
            _settings = settings.Value;
            //_mapper = mapper;
			StripeConfiguration.ApiKey = _settings.ApiKey;

		}


		public async Task<string> GetTokenId(StripeClientModel model){

			var myToken = new TokenCreateOptions
				{
					
					Card = new TokenCardOptions
                    {
						AddressCountry = model.CardAddressCountry,
						AddressLine1 = model.CardAddressLine1,
						AddressLine2 = model.CardAddressLine2,
						AddressCity = model.CardAddressCity,
						AddressZip = model.CardAddressZip,
						Cvc = model.CardCvc,
						ExpYear = model.CardExpirationYear,
						ExpMonth = model.CardExpirationMonth,
						Name = model.CardName,
						Number = model.CardNumber
					}
					
				};

		var tokenService = new TokenService();
		var stripeToken = await tokenService.CreateAsync(myToken);

		return stripeToken.Id;


		}

		public async Task<string> ChargeCustomer(string TokenId, int Amount, string Currency, string Description)

		{

			var myCharge = new ChargeCreateOptions
			{
				Amount = Amount,
				Currency = Currency, //"gbp"
				Description = Description, //"Charge for property sign and postage",
				Source = TokenId
			};

			var chargeService = new ChargeService();
			var stripeCharge = await chargeService.CreateAsync(myCharge);

			return stripeCharge.Id;

		}

		public async Task<object> CreateCustomer(CreateCustomerRequest request)
		{
			Stripe.Customer stripeCreateCustomerResponse;
            var options = new CustomerCreateOptions
            {
                Address = new AddressOptions
                {
                    City = request.Address.City,
                    Country = request.Address.Country,
                    Line1 = request.Address.Line1,
                    Line2 = request.Address.Line2,
                    PostalCode = request.Address.PostalZipCode,
                    State = request.Address.StateRegion
                },
                Balance = request.Balance,
                Description = "This is my very first Created Customer on Stripe in .NET Core",
                Email = request.Email,
                Name = $"{request.Name}",
                Phone = request.Phone,
                Shipping = new ShippingOptions
                {
                    Address = new AddressOptions
                    {
                        City = request.Shipping.Address.City,
                        Country = request.Shipping.Address.Country,
                        Line1 = request.Shipping.Address.Line1,
                        Line2 = request.Shipping.Address.Line2,
                        PostalCode = request.Shipping.Address.PostalZipCode,
                        State = request.Shipping.Address.StateRegion
                    },
                    Name = $"{request.Name}",
                    Phone = request.Phone
                }

            };
            var service = new CustomerService();
            stripeCreateCustomerResponse = await service.CreateAsync(options);
            var response = MapStripeCustomerToLocalResponse(stripeCreateCustomerResponse);
            //var customerResponse = JsonConvert.DeserializeObject<CreateCustomerResponse>(stripeCreateCustomerResponse);
            ///var customerResponse = _mapper.Map<CreateCustomerResponse>(stripeCreateCustomerResponse);

            return response;
		}

        public async Task<object> RetrieveCustomer(string CustomerId)
        {
			///Stripe.Customer retrieveCustomerResponse;
			var service = new CustomerService();
			var retrieveCustomerResponse = await service.GetAsync(CustomerId);
            //var response = _mapper.Map<CreateCustomerResponse>(retrieveCustomerResponse);
            var response = MapStripeCustomerToLocalResponse(retrieveCustomerResponse);
			return response;
        }

        private static CreateCustomerResponse MapStripeCustomerToLocalResponse(Stripe.Customer stripeCustomer)
        {
            return new CreateCustomerResponse
            {
                Id = stripeCustomer.Id,
                Address = stripeCustomer.Address,
                Balance = stripeCustomer.Balance,
                Currency = stripeCustomer.Currency,
                Email = stripeCustomer.Email,
                Discount = stripeCustomer.Discount,
                Phone = stripeCustomer.Phone,
                InvoicePrefix = stripeCustomer.InvoicePrefix,
                InvoiceSettings = stripeCustomer.InvoiceSettings,
                NextInvoiceSequence = stripeCustomer.NextInvoiceSequence,
                Shipping = stripeCustomer.Shipping,
                DefaultSource = stripeCustomer.DefaultSource,
                Description = stripeCustomer.Description,
                Name = stripeCustomer.Name,
                Delinquent = stripeCustomer.Delinquent,
                Created = stripeCustomer.Created,
                Livemode = stripeCustomer.Livemode,
                Object = stripeCustomer.Object,
                Metadata = stripeCustomer.Metadata,
                PreferredLocales = stripeCustomer.PreferredLocales,
                Sources = stripeCustomer.Sources,
                Deleted = stripeCustomer.Deleted
                //TaxExempt = stripeCustomer.Tax,



            };
        }

        public async Task<object> UpdateCustomer(string CustomerId, CreateCustomerRequest request)
        {
			Stripe.Customer updateCustomerResponse;
			var options = new CustomerUpdateOptions
			{
                Balance = request.Balance,
                Address = new AddressOptions
                {
                    City = request.Address.City,
                    Country = request.Address.Country,
                    Line1 = request.Address.Line1,
                    Line2 = request.Address.Line2,
                    PostalCode = request.Address.PostalZipCode,
                    State = request.Address.StateRegion
                },
                Description = "This is my very first Created Customer on Stripe in .NET Core",
                Email = request.Email,
                Name = $"{request.Name}",
                Phone = request.Phone,
                Shipping = new ShippingOptions
                {
                    Address = new AddressOptions
                    {
                        City = request.Shipping.Address.City ,
                        Country = request.Shipping.Address.Country,
                        Line1 = request.Shipping.Address.Line1,
                        Line2 = request.Shipping.Address.Line2,
                        PostalCode = request.Shipping.Address.PostalZipCode,
                        State = request.Shipping.Address.StateRegion
                    },
                    Name = $"{request.Name}",
                    Phone = request.Phone
                }
            };

            var service = new CustomerService();
            updateCustomerResponse = await service.UpdateAsync(CustomerId, options);
            var response = MapStripeCustomerToLocalResponse(updateCustomerResponse);
            //var response = _mapper.Map<CreateCustomerResponse>(updateCustomerResponse);
            return response;

		}

        public async Task<object> UpdateCustomerCreditBalance(string CustomerId, long? Balance)
        {
            Stripe.Customer updateCustomerResponse;
            var options = new CustomerUpdateOptions
            {
                Balance = Balance
                
            };

            var service = new CustomerService();
            updateCustomerResponse = await service.UpdateAsync(CustomerId, options);
            var response = MapStripeCustomerToLocalResponse(updateCustomerResponse);
            //var response = _mapper.Map<CreateCustomerResponse>(updateCustomerResponse);
            return response;
        }

        public async Task<object> UpdateCustomerAddress(string CustomerId, AddressOptions address)
        {
            Stripe.Customer updateCustomerResponse;
            var options = new CustomerUpdateOptions
            {
                Address = address

            };

            var service = new CustomerService();
            updateCustomerResponse = await service.UpdateAsync(CustomerId, options);
            var response = MapStripeCustomerToLocalResponse(updateCustomerResponse);
            //var response = _mapper.Map<CreateCustomerResponse>(updateCustomerResponse);
            return response;
        }

        public async Task<object> UpdateCustomerName(string CustomerId, string Name)
        {
            Stripe.Customer updateCustomerResponse;
            var options = new CustomerUpdateOptions
            {
                Name = Name

            };

            var service = new CustomerService();
            updateCustomerResponse = await service.UpdateAsync(CustomerId, options);
            var response = MapStripeCustomerToLocalResponse(updateCustomerResponse);
            //var response = _mapper.Map<CreateCustomerResponse>(updateCustomerResponse);
            return response;
        }
    }


}