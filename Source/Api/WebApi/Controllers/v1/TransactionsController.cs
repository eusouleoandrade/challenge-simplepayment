using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Filters;
using Application.DTOs.Queries;
using Application.DTOs.RequestModel;
using Application.DTOs.RequestModels;
using Application.DTOs.Requests;
using Application.DTOs.Wrappers;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TransactionsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IGetTransactionsUseCase _getTransactionsUseCase;

        private readonly ICreateTransactionUseCase _createTransactionUseCase;

        public TransactionsController(IMapper mapper, IGetTransactionsUseCase getTransactionsUseCase, ICreateTransactionUseCase createTransactionUseCase)
        {
            _mapper = mapper;
            _getTransactionsUseCase = getTransactionsUseCase;
            _createTransactionUseCase = createTransactionUseCase;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<Response<List<GetTransactionsQuery>>>> Get([FromQuery] GetTransactionsQueryFilter queryFilter, Guid customerId)
        {
            var requestModel = _mapper.Map<GetTransactionsUseCaseRequestModel>(queryFilter, opt => opt.AfterMap((src, dest) => dest.CustomerId = customerId));
            var responseModel = await _getTransactionsUseCase.Handler(requestModel);

            // Validation exemple in UseCase / Service
            if (_getTransactionsUseCase.HasErrorNotification)
                return BadRequest(new Response(_getTransactionsUseCase.ErrorNotificationResult.Select(s => s.Message)));

            var response = _mapper.Map<List<GetTransactionsQuery>>(responseModel);
            return Ok(new Response<List<GetTransactionsQuery>>(response));
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Create([FromBody] CreateTransactionRequest request)
        {
            var requestModel = _mapper.Map<CreateTransactionRequestModel>(request);
            
            // Validation exemple in RequestModel
            if (requestModel.HasErrorNotification)
                return BadRequest(new Response(requestModel.ErrorNotificationResult.Select(s => s.Message)));

            await _createTransactionUseCase.Handler(requestModel);
            
            // Validation exemple in UseCase
            if(_createTransactionUseCase.HasErrorNotification)
                return BadRequest(new Response(_createTransactionUseCase.ErrorNotificationResult.Select(s => s.Message)));

            return Ok(new Response(succeeded: true));
        }
    }
}