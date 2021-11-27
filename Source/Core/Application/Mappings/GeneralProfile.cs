using Application.DTOs.Filters;
using Application.DTOs.Queries;
using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using AutoMapper;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
         public GeneralProfile()
        {
            CreateMap<GetCustomerTransactionsQueryFilter, GetCustomerTransactionsUseCaseRequestModel>();
            CreateMap<GetCustomerTransactionsUseCaseResponseModel, GetCustomerTransactionsQuery>();
        }
    }
}