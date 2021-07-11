using Microsoft.AspNetCore.Mvc;
using paymentgateway.Models;
using paymentgateway.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentgateway.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VirtualCardIssuingController : ControllerBase
    {


        private readonly IVirtualCardIssuingService _cardService;

        public VirtualCardIssuingController(IVirtualCardIssuingService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("create_cardholder")]
        public async Task<IActionResult> CreateCardHolder([FromBody] CreateCardHolderRequest request)
        {
            var cardHolder = await _cardService.CreateCardHolder(request);
            return Ok(cardHolder);
        }
        [HttpPost("create_virtual_card")]
        public async Task<IActionResult> CreateVirtualCard(CreateVirtualCardRequest request)
        {
            var virtualCard = await _cardService.CreateVirtualCard(request);
            return Ok(virtualCard);
        }

        [HttpPost("deactivate_lost_card")]
        public async Task<object> DeactivateLostOrStolenCard(string CardId)
        {
            var result = await _cardService.DeactivateLostOrStolenCard(CardId);
            return Ok(result);
        }
        [HttpGet("get_all_cards")]
        public async Task<IActionResult> GetListOfAllCards(int? Limit)
        {
            var cards = await _cardService.GetListOfAllCards(Limit);
            return Ok(cards);
        }
        [HttpGet("retrieve_physical_card")]
        public async Task<IActionResult> GetPhysicalCards(int? Limit, string Type)
        {
            var cards = await _cardService.GetPhysicalCards(Limit, Type);
            return Ok(cards);
        }
        [HttpGet("get_virtual_card")]
        public async Task<IActionResult> GetVirtualCards(int? Limit, string Type)
        {
            var cards = await _cardService.GetVirtualCards(Limit, Type);
            return Ok(cards);
        }
        [HttpGet("retrieve_card")]
        public async Task<IActionResult> RetrieveACard([FromQuery] string CardId)
        {
            var card = await _cardService.RetrieveACard(CardId);
            return Ok(card);
        }
        [HttpGet("retrieve_virtual_card")]
        public async Task<IActionResult> RetrieveAVirtualCard([FromQuery] string CardId)
        {
            var card = await _cardService.RetrieveAVirtualCard(CardId);

            return Ok(card);
        }
        [HttpPost("update_virtual_card_status")]
        public async Task<IActionResult> UpdateVirtualCardStatus([FromQuery]string CardholderId, [FromBody]UpdateVirtualCardStatus request)
        {
            var result = await _cardService.UpdateVirtualCardStatus(CardholderId, request);
            return Ok(result);
        }
    }
}
