using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Filters;
using Application.DTOs.Queries;
using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Application.DTOs.Wrappers;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CustomerTransactionsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IGetCustomerTransactionsUseCase _getCustomerTransactionsUseCase;

        public CustomerTransactionsController(IMapper mapper, IGetCustomerTransactionsUseCase getCustomerTransactionsUseCase)
        {
            _mapper = mapper;
            _getCustomerTransactionsUseCase = getCustomerTransactionsUseCase;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<Response<List<GetCustomerTransactionsQuery>>>> Get([FromQuery] GetCustomerTransactionsQueryFilter queryFilter, Guid customerId)
        {
            var requestModel = _mapper.Map<GetCustomerTransactionsUseCaseRequestModel>(queryFilter, opt => opt.AfterMap((src, dest) => dest.CustomerId = customerId));
            var responseModel = await _getCustomerTransactionsUseCase.Handler(requestModel);

            if(_getCustomerTransactionsUseCase.HasErrorNotification)
                return BadRequest(new Response(_getCustomerTransactionsUseCase.ErrorNotificationResult.Select(s => s.Message)));
            
            var response = _mapper.Map<List<GetCustomerTransactionsQuery>>(responseModel);
            return Ok(new Response<List<GetCustomerTransactionsQuery>>(response));
        }
    }
}