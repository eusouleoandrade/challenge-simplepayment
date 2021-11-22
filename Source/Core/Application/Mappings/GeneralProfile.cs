using Application.DTOs.Filters;
using Application.DTOs.RequestModel;
using AutoMapper;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
         public GeneralProfile()
        {
            CreateMap<GetCustomerTransactionsFilter, GetCustomerTransactionsUseCaseRequestModel>();
        }
    }
}