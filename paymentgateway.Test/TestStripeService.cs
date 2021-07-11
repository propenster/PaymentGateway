using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using paymentgateway.Config;
using paymentgateway.Controllers;
using paymentgateway.Models;
using paymentgateway.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace paymentgateway.Test
{
    public class TestStripeService
    {
        private readonly IStripeService _stripeService;
        //IOptions<StripeSettings> _settings;
        //StripeService _stripeService;
        // IConfiguration _config;
        private readonly StripeController _stripeController;

        public TestStripeService()
        {
            _stripeController = new StripeController(_stripeService);
        }

        [Fact]
        public async void Test_Retrieve_Customer_Returns_A_StripeCustomer()
        {
            //Arrange
            string CustomerId = "cus_JoKf7kAQVfXjlN";
          

            //Act
            var result = await _stripeController.RetrieveCustomer(CustomerId);
            var objectValue = result as CreateCustomerResponse;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<CreateCustomerResponse>(objectValue);
            Assert.Equal("John Wick", objectValue.Name);

        }
    }
}
