using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Domain;
using App.Data;
using App.DomainViewModel;

namespace App.Service
{
    #region Interface Implement : Category

    public class CategoryService : ICategoryService
    {
        #region Global Variable Declaration

        private readonly Repository<Category> _categoryRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor
        
        public CategoryService(Repository<Category> categoryRepository, IUnitOfWork iUnitOfWork)
        {
            this._categoryRepository = categoryRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<CategoryViewModel> GetAll()
        {
            var categoryViewModels = new List<CategoryViewModel>();
            try
            {

                List<Category> categorys = _categoryRepository.GetAll();

                foreach (Category category in categorys)
                {
                    var categoryViewModel = category.ConvertModelToViewModel<Category, CategoryViewModel>();
                    categoryViewModels.Add(categoryViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return categoryViewModels.AsQueryable();
        }

        public CategoryViewModel GetById(long id)
        {
            var categoryViewModel = new CategoryViewModel();

            try
            {
                Category category = _categoryRepository.GetById(id);
                categoryViewModel = category.ConvertModelToViewModel<Category, CategoryViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return categoryViewModel;
        }

        #endregion

        #region Create Method

        public int Create(CategoryViewModel categoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (categoryViewModel != null)
                {
                    Category category = categoryViewModel.ConvertViewModelToModel<CategoryViewModel, Category>();
                    _categoryRepository.Insert(category);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("CategoryViewModel", "Request data is null.");
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

        public int Update(CategoryViewModel categoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (categoryViewModel != null)
                {
                    Category category = categoryViewModel.ConvertViewModelToModel<CategoryViewModel, Category>();
                    _categoryRepository.Update(category);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("CategoryViewModel", "Request data is null.");
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

        public int Delete(CategoryViewModel categoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (categoryViewModel != null)
                {
                    var viewModel = GetById(categoryViewModel.Id);
                    Category category = viewModel.ConvertViewModelToModel<CategoryViewModel, Category>();
                    _categoryRepository.Delete(category);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("CategoryViewModel", "Request data is null.");
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
                var categoryViewModel = GetById(id);
                if (categoryViewModel != null)
                {
                    Category category = categoryViewModel.ConvertViewModelToModel<CategoryViewModel, Category>();
                    _categoryRepository.Delete(category);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("CategoryViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<CategoryViewModel> categoryViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var categoryViewModel in categoryViewModels)
                {
                    CategoryViewModel viewModel = GetById(categoryViewModel.Id);
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

    #region Interface : Category

    public interface ICategoryService : IGeneric<CategoryViewModel>
    {
        int Delete(List<CategoryViewModel> categoryViewModels);
    }

    #endregion
}
