using System;
using System.Net;
using AutoMapper;
using StockService.Application.Contracts;
using StockService.Application.Models.Responses;
using StockService.Application.Models.Requests;
using StockService.Domain.Contracts;
using StockService.Domain.Entities;
using StockService.Application.Enums;

namespace StockService.Application.Services;

public class StockService : IStockService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;
    public StockService(IUnitOfWork unitOfWork, IStockRepository stockRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _stockRepository = stockRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse> UpdateStockAsync(UpdateStockRequestModel requestModel)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            foreach (var item in requestModel.Items)
            {
                //Ürünü stoktan bul
                var stockItem = await _stockRepository.GetStockByProductId(item.ProductId);

                if (stockItem == null)
                    return new ServiceResponse
                    {
                        Successed = false,
                        Code = (short)ServiceErrorCodes.NotFound,
                        Message = $"Product with Id {item.ProductId} not found in stock."
                    };

                if (stockItem.QuantityAvailable < item.Quantity)
                    return new ServiceResponse
                    {
                        Successed = false,
                        Code = (short)ServiceErrorCodes.BadRequest,
                        Message = $"stock quantity error for product {item.ProductId}. Available: {stockItem.QuantityAvailable}, Requested: {item.Quantity}"
                    };

                stockItem.QuantityAvailable -= item.Quantity;

                _stockRepository.Update(stockItem);
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return new ServiceResponse
            {
                Successed = true,
                Code = (short)ServiceErrorCodes.Ok,
                Message = $"Update stock for product {requestModel.OrderId}"
            };
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();

            return new ServiceResponse
            {
                Successed = false,
                Code = (short)ServiceErrorCodes.Status500InternalServerError,
                Message = $"Update stock service error"
            };
        }

    }
}