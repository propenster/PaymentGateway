using AutoMapper;
using paymentgateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentgateway.Profiles
{
    public class PaygateProfile : Profile
    {

        public PaygateProfile()
        {
            CreateMap<Stripe.Customer, CreateCustomerResponse>()
                .ForMember(target => target.Address, opt => opt.MapFrom(source => source.Address))
                .ForMember(target => target.Balance, opt => opt.MapFrom(source => source.Balance))
                .ForMember(target => target.Created, opt => opt.MapFrom(source => source.Created))
                .ForMember(target => target.Currency, opt => opt.MapFrom(source => source.Currency))
                .ForMember(target => target.DefaultSource, opt => opt.MapFrom(source => source.DefaultSource))
                .ForMember(target => target.Delinquent, opt => opt.MapFrom(source => source.Delinquent))
                .ForMember(target => target.Description, opt => opt.MapFrom(source => source.Description))
                .ForMember(target => target.Discount, opt => opt.MapFrom(source => source.Discount))
                .ForMember(target => target.Email, opt => opt.MapFrom(source => source.Email))
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(target => target.InvoicePrefix, opt => opt.MapFrom(source => source.InvoicePrefix))
                .ForMember(target => target.InvoiceSettings, opt => opt.MapFrom(source => source.InvoiceSettings))
                .ForMember(target => target.Shipping, opt => opt.MapFrom(source => source.Shipping))
                .ForMember(target => target.NextInvoiceSequence, opt => opt.MapFrom(source => source.NextInvoiceSequence))
                .ForMember(target => target.TaxExempt, opt => opt.MapFrom(source => source.TaxExempt))
                .ForMember(target => target.PreferredLocales, opt => opt.MapFrom(source => source.PreferredLocales))
                .ForMember(target => target.Metadata, opt => opt.MapFrom(source => source.Metadata))

               .ForMember(target => target.Object, opt => opt.MapFrom(source => source.Object));


        }
    }
}
