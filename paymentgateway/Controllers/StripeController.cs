using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using paymentgateway.Models;
using paymentgateway.Services;

namespace paymentgateway.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class StripeController : ControllerBase
    {

        private readonly IStripeService _stripeService;

        public StripeController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }


        [HttpPost("create_stripe_token")]
        public async Task<IActionResult> CreateStripCardToken(StripeClientModel model)
        {

            string TokenId = await _stripeService.GetTokenId(model);
            return Ok(new { CardToken = TokenId });
        }

        [HttpPost("charge_customer")]
        public async Task<IActionResult> ChargeCustomer(string TokenId, int Amount, string Currency, string Description)
        {
            var createCustomerResult = await _stripeService.ChargeCustomer(TokenId, Amount, Currency, Description);
            return Ok(new { CustomerId = createCustomerResult });
        }

        [HttpPost("create_customer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request)
        {

            var newCustomer = await _stripeService.CreateCustomer(request);
            return Ok(newCustomer);
        }

        [HttpGet("retrieve_customer/{CustomerId}")]
        public async Task<IActionResult> RetrieveCustomer(string CustomerId)
        {
            var customer = await _stripeService.RetrieveCustomer(CustomerId);
            return Ok(customer);
        }

        [HttpPost("update_customer")]
        public async Task<IActionResult> UpdateCustomer(string CustomerId, CreateCustomerRequest request)
        {
            var customer = await _stripeService.UpdateCustomer(CustomerId, request);
            return Ok(customer);
        }
        [HttpPost("update_customer_balance")]
        public async Task<IActionResult> UpdateCustomerBalance(string CustomerId, long Balance)
        {
            var customer = await _stripeService.UpdateCustomerCreditBalance(CustomerId, Balance);
            return Ok(customer);
        }

        [HttpPost("update_customer_address")]
        public async Task<IActionResult> UpdateCustomerAddress([FromQuery] string CustomerId, [FromBody] Stripe.AddressOptions address)
        {
            var customer = await _stripeService.UpdateCustomerAddress(CustomerId, address);
            return Ok(customer);
        }

        [HttpPost("update_customer_name")]
        public async Task<IActionResult> UpdateCustomerName([FromQuery] string CustomerId, [FromQuery]string Name)
        {
            var customer = await _stripeService.UpdateCustomerName(CustomerId, Name);
            return Ok(customer);
        }

    }



}