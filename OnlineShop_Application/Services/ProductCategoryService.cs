using AutoMapper;
using OnlineShop_Application.Interfaces;
using OnlineShop_Application.ViewModels;
using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;
using OnlineShop_Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace OnlineShop_Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductCategoryService(IProductCategoryRepository productCategory, IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _productCategoryRepository = productCategory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ProductCategoryViewModel Add(ProductCategoryViewModel productCategoryVm)
        {
            var productCategory = _mapper.Map<ProductCategory>(productCategoryVm);
            _productCategoryRepository.Add(productCategory);
            _unitOfWork.Commit();
            return productCategoryVm;
        }



        public void Delete(int id)
        {
            var productCategory = _productCategoryRepository.FindById(id);
            _productCategoryRepository.Remove(productCategory);
            _unitOfWork.Commit();
        }

        public List<ProductCategoryViewModel> GetAll()
        {
            return _mapper.Map<List<ProductCategoryViewModel>>(_productCategoryRepository.FindAll().ToList());
        }

        public List<ProductCategoryViewModel> GetAll(string keyword)
        {
            throw new NotImplementedException();
        }

        public List<ProductCategoryViewModel> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public ProductCategoryViewModel GetById(int id)
        {
            var productCategory = _productCategoryRepository.FindById(id);
            return _mapper.Map<ProductCategoryViewModel>(productCategory);
        }

        public List<ProductCategoryViewModel> GetHomeCategories(int top)
        {
            var query = _productCategoryRepository
                .FindAll(x => x.HomeFlag == true, c => c.Products)
                .OrderBy(x => x.HomeOrder)
                .Take(top).ProjectTo<ProductCategoryViewModel>(_mapper.ConfigurationProvider);

            var categories = query.ToList();
            return categories;
        }

        public void ReOrder(int sourceId, int targetId)
        {
            var source = _productCategoryRepository.FindById(sourceId);
            var target = _productCategoryRepository.FindById(targetId);
            source.ParentId = target.ParentId;
            if (source.SortOrder == target.SortOrder)
            {

            }
            int temp = source.SortOrder;
            source.SortOrder = target.SortOrder;
            target.SortOrder = temp;

            _productCategoryRepository.Update(source);
            _productCategoryRepository.Update(source);
            _unitOfWork.Commit();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(ProductCategoryViewModel productCategoryVm)
        {
            var productCategory = _mapper.Map<ProductCategory>(productCategoryVm);
            _productCategoryRepository.Update(productCategory);
            _unitOfWork.Commit();
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            var source = _productCategoryRepository.FindById(sourceId);
            source.ParentId = targetId;

            _productCategoryRepository.Update(source);


            foreach (var item in items)
            {
                var child = _productCategoryRepository.FindById(item.Key);
                child.SortOrder = items[child.Id];
                _productCategoryRepository.Update(child);
            }
            _unitOfWork.Commit();
        }
    }
}
