using Application.DTOs.Filters;
using Application.DTOs.Models;
using Application.DTOs.Queries;
using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Application.DTOs.RequestModels;
using Application.DTOs.Requests;
using Application.DTOs.ResponseModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            // Transactions
            CreateMap<GetTransactionsQueryFilter, GetTransactionsUseCaseRequestModel>();

            CreateMap<GetTransactionsUseCaseResponseModel, GetTransactionsQuery>();

            CreateMap<Transaction, TransactionModel>()
                .ForMember(dest => dest.TransacionId, opt => opt.MapFrom(src => src.Id));

            CreateMap<CreateTransactionRequest, CreateTransactionUseCaseRequestModel>();

            CreateMap<CreateTransactionUseCaseRequestModel, Transaction>();

            CreateMap<Transaction, CreateTransactionUseCaseResponseModel>()
                .ForMember(dest => dest.TransacionId, opt => opt.MapFrom(src => src.Id));

            CreateMap<CreateTransactionUseCaseResponseModel, CreateTransactionQuery>();

            // Customers
            CreateMap<Customer, GetCustomerUseCaseResponseModel>();
        }
    }
}