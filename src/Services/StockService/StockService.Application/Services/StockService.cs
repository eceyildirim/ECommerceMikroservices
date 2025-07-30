using System;
using System.Net;
using AutoMapper;
using StockService.Application.Contracts;
using StockService.Application.Models.Responses;
using StockService.Application.Models.Requests;
using StockService.Domain.Contracts;
using StockService.Common.Exceptions;
using StockService.Domain.Entities;
using StockService.Domain.Enums;
using StockService.Application.Enums;

namespace StockService.Application.Services;

public class StockService : IStockService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStockRepository _stockRepository;
    private readonly IRepository<StockTransaction> _stockTransactionRepository;
    private readonly IMapper _mapper;
    public StockService(IUnitOfWork unitOfWork, IStockRepository stockRepository, IMapper mapper, IRepository<StockTransaction> stockTransactionRepository)
    {
        _unitOfWork = unitOfWork;
        _stockRepository = stockRepository;
        _mapper = mapper;
        _stockTransactionRepository = stockTransactionRepository;
    }

    public async Task UpdateStockAsync(UpdateStockRequestModel requestModel)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            foreach (var item in requestModel.Items)
            {
                //Ürünü stoktan bul
                var stockItem = await _stockRepository.GetStockByProductId(item.ProductId);

                if (stockItem == null)
                    throw new StockNotfoundException($"Product with Id {item.ProductId} not found in stock");

                if (stockItem.QuantityAvailable < item.Quantity)
                    throw new StockException($"Stock quantity error for product {item.ProductId}. Available: {stockItem.QuantityAvailable}, Requested: {item.Quantity}");

                stockItem.QuantityAvailable -= item.Quantity;

                _stockRepository.Update(stockItem);

                //stockTransaction kaydı 
                var stockTransaction = new StockTransaction
                {
                    Id = Guid.NewGuid(),
                    OrderId = requestModel.OrderId,
                    Quantity = item.Quantity,
                    Type = StockTransactionType.Order,
                    StockId = stockItem.Id
                };

                await _stockTransactionRepository.AddAsync(stockTransaction);
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();

            throw new StockException("Update stock service Erro");
        }

    }
}