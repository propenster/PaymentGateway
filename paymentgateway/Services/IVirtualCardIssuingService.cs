using paymentgateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentgateway.Services
{
    public interface IVirtualCardIssuingService
    {

        Task<object> CreateCardHolder(CreateCardHolderRequest request);
        Task<object> CreateVirtualCard(CreateVirtualCardRequest request);
        Task<object> UpdateVirtualCardStatus(string CardholderId, UpdateVirtualCardStatus request);
        Task<object> RetrieveAVirtualCard(string CardId);
        Task<object> RetrieveACard(string CardId);
        Task<object> DeactivateLostOrStolenCard(string CardId);

        //Admin methods
        Task<object> GetListOfAllCards(int? Limit);
        Task<object> GetPhysicalCards(int? Limit, string Type = "physical");
        Task<object> GetVirtualCards(int? Limit, string Type = "virtual");
    }
}
