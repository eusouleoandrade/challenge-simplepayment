using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.Filters;
using Application.DTOs.Queries;
using Application.DTOs.RequestModel;
using Application.DTOs.Wrappers;
using Application.Interfaces;
using Application.UseCases;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CustomerTransactionsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IGetCustomerTransactionsUseCase _getCustomerTransactionsUseCase;

        public CustomerTransactionsController(IMapper mapper, GetCustomerTransactionsUseCase getCustomerTransactionsUseCase)
        {
            _mapper = mapper;
            _getCustomerTransactionsUseCase = getCustomerTransactionsUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<GetCustomerTransactionsQuery>>>> Get(
            [FromQuery] GetCustomerTransactionsFilter filter)
        {
            var requestModel = _mapper.Map<GetCustomerTransactionsUseCaseRequestModel>(filter);
            
            var responseModel = await _getCustomerTransactionsUseCase.Handler(requestModel);

            if(_getCustomerTransactionsUseCase.Invalid)
                return BadRequest(new Response(_getCustomerTransactionsUseCase.ValidationResult));

            return Ok();
        }
    }
}