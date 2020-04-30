using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop_Application.Interfaces;
using OnlineShop_Application.ViewModels;
using OnlineShop_Data.IRepositories;
using OnlineShop_Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop_Data.Entities;

namespace OnlineShop_Application.Services
{
    public class FunctionService : IFunctionService
    {
        private readonly IFunctionRepository _functionRepository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public FunctionService(IFunctionRepository functionRepository,IMapper mapper, IUnitOfWork unitOfWork)
        {
            _functionRepository = functionRepository;         
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Add(FunctionViewModel functionVm)
        {
            var function = _mapper.Map<Function>(functionVm);
            _functionRepository.Add(function);
            _unitOfWork.Commit();
            
        }
        public void Update(FunctionViewModel functionVm)
        {

            var functionDb = _functionRepository.FindById(functionVm.Id);
            var function = _mapper.Map<Function>(functionVm);
            _functionRepository.Update(function);
            _unitOfWork.Commit();
        }
        public async Task<List<FunctionViewModel>> GetAll()
        {
          
            var lstFunction = await  _functionRepository.FindAll().ToListAsync();
            var listFunctionVm = _mapper.Map<List<FunctionViewModel>>(lstFunction);
            
            return listFunctionVm;
             
                
        }
    }
}
