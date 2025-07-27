using System.Net;
using AutoMapper;
using StockService.Application.Contracts;
using StockService.Domain.Entities;
using StockService.Domain.Contracts;

namespace StockService.Application.Services;

public class StockService : IStockService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Stock> _stockRepository;
    private readonly IMapper _mapper;
    public StockService(IUnitOfWork unitOfWork, IRepository<Stock> stockRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _stockRepository = stockRepository;
        _mapper = mapper;
    }
}