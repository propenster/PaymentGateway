using BitPaySDK.Models.Invoice;
using BitPaySDK;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentgateway.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BitpayController : ControllerBase
    {

        public async Task<IActionResult> CreateInvoice(CreateBitPayInvoice request)
        {
            var bitPayInvoice = await BitPay.CreateInvoice(new Invoice()
            {
                Price = request.Price,
                Currency = request.Currency,
                PosData = request.PosData,
                OrderId = request.OrderId,
                RedirectUrl = request.RedirectURL,
                NotificationUrl = request.NotificationURL,
                ItemDesc = request.ItemDesc,
                FullNotifications = request.FullNotifications
            }, facade: "merchant");

            return Ok(bitPayInvoice.Result.Url);
        }
    }

    public class CreateBitPayInvoice
    {
        public double Price { get; set; }
        public string Currency { get; set; }
        public string OrderId => $"{Guid.NewGuid().ToString().Replace("-", "")}";
        public string RedirectURL { get; set; }
        public string NotificationURL { get; set; } //This should be real URL
        public string ItemDesc { get; set; }
        public string PosData { get; set; }
        public bool FullNotifications { get; set; }
    }
}
