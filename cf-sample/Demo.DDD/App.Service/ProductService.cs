using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Domain;
using App.Data;
using App.DomainViewModel;

namespace App.Service
{
    #region Interface Implement : Product

    public class ProductService : IProductService
    {
        #region Global Variable Declaration

        private readonly Repository<Product> _productRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public ProductService(Repository<Product> productRepository, IUnitOfWork iUnitOfWork)
        {
            this._productRepository = productRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<ProductViewModel> GetAll()
        {
            var productViewModels = new List<ProductViewModel>();
            try
            {

                List<Product> products = _productRepository.GetAll();

                foreach (Product product in products)
                {
                    var productViewModel = product.ConvertModelToViewModel<Product, ProductViewModel>();
                    productViewModels.Add(productViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productViewModels.AsQueryable();
        }

        public ProductViewModel GetById(long id)
        {
            var productViewModel = new ProductViewModel();

            try
            {
                Product product = _productRepository.GetById(id);
                productViewModel = product.ConvertModelToViewModel<Product, ProductViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return productViewModel;
        }

        #endregion

        #region Create Method

        public int Create(ProductViewModel productViewModel)
        {
            int isSave = 0;
            try
            {
                if (productViewModel != null)
                {
                    Product product = productViewModel.ConvertViewModelToModel<ProductViewModel, Product>();
                    _productRepository.Insert(product);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("ProductViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }

        #endregion

        #region Update Method

        public int Update(ProductViewModel productViewModel)
        {
            int isSave = 0;
            try
            {
                if (productViewModel != null)
                {
                    Product product = productViewModel.ConvertViewModelToModel<ProductViewModel, Product>();
                    _productRepository.Update(product);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("ProductViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        #endregion

        #region Delete Method

        public int Delete(ProductViewModel productViewModel)
        {
            int isSave = 0;
            try
            {
                if (productViewModel != null)
                {
                    var viewModel = GetById(productViewModel.Id);
                    Product product = viewModel.ConvertViewModelToModel<ProductViewModel, Product>();
                    _productRepository.Delete(product);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("ProductViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(long id)
        {
            int isSave = 0;
            try
            {
                var productViewModel = GetById(id);
                if (productViewModel != null)
                {
                    Product product = productViewModel.ConvertViewModelToModel<ProductViewModel, Product>();
                    _productRepository.Delete(product);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("ProductViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<ProductViewModel> productViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var productViewModel in productViewModels)
                {
                    ProductViewModel viewModel = GetById(productViewModel.Id);
                    Delete(viewModel);
                }


                isSave = Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        #endregion

        #region Save By Commit

        public int Save()
        {
            return _iUnitOfWork.Commit();
        }

        #endregion

    }

    #endregion

    #region Interface : Product

    public interface IProductService : IGeneric<ProductViewModel>
    {
        int Delete(List<ProductViewModel> productViewModels);
    }

    #endregion
}
