using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Service;
using App.Domain;
using App.DomainViewModel;
using App.Web.Helpers;
using App.Web.ViewModels;

namespace App.Web.Controllers
{
    public class CategoryController : Controller
    {
        #region Global Variable Declaration

        private readonly ICategoryService _iCategoryService;

        #endregion

        #region Constructor

        public CategoryController(ICategoryService iCategoryService)
        {
            this._iCategoryService = iCategoryService;
        }

        #endregion

        #region Action

        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CategoryRead(KendoUiGridParam request)
        {
            var categoryViewModels = GetCategoryDataList().AsQueryable();
            var models = KendoUiHelper.ParseGridData<CategoryViewModel>(categoryViewModels, request);

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Category/Details/By ID

        public ActionResult Details(int id)
        {
            var errorViewModel = new ErrorViewModel();

            try
            {
                var categoryViewModel = _iCategoryService.GetById(id);
                if (categoryViewModel != null)
                {
                    return PartialView("_Details", categoryViewModel);
                }

                errorViewModel = ExceptionHelper.ExceptionErrorMessageForNullObject();
            }
            catch (Exception ex)
            {
                errorViewModel = ExceptionHelper.ExceptionErrorMessageFormat(ex);
            }

            return PartialView("_ErrorPopup", errorViewModel);
        }

        //
        // GET: /Category/Add

        public ActionResult Add()
        {
            var viewModel = new CategoryViewModel() { Id = 0 };
            //return View();
            return PartialView("_AddOrEdit", viewModel);
        }

        //
        // GET: /Category/Edit/By ID

        public ActionResult Edit(int id)
        {
            var errorViewModel = new ErrorViewModel();

            try
            {
                var categoryViewModel = _iCategoryService.GetById(id);
                if (categoryViewModel != null)
                {
                    return PartialView("_AddOrEdit", categoryViewModel);
                }

                errorViewModel = ExceptionHelper.ExceptionErrorMessageForNullObject();
            }
            catch (Exception ex)
            {
                errorViewModel = ExceptionHelper.ExceptionErrorMessageFormat(ex);
            }

            return PartialView("_ErrorPopup", errorViewModel);
        }

        //
        // POST: /Category/Save

        [HttpPost]
        public ActionResult Save(CategoryViewModel categoryViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //add
                    if (categoryViewModel.Id == 0)
                    {

                        _iCategoryService.Create(categoryViewModel);
                    }
                    else //edit
                    {
                        var viewModel = _iCategoryService.GetById(categoryViewModel.Id);

                        if (viewModel != null)
                        {

                            _iCategoryService.Update(viewModel);

                        }

                        return Content(KendoUiHelper.GetKendoUiWindowAjaxSuccessMethod(Boolean.FalseString, MessageType.warn.ToString(), ExceptionHelper.ExceptionMessageForNullObject()));

                    }


                    _iCategoryService.Save();

                    return Content(KendoUiHelper.GetKendoUiWindowAjaxSuccessMethod(Boolean.TrueString, MessageType.success.ToString(), "Saved Successfully."));
                }

                return Content(KendoUiHelper.GetKendoUiWindowAjaxSuccessMethod(Boolean.TrueString, MessageType.success.ToString(), ExceptionHelper.ModelStateErrorFormat(ModelState)));
            }
            catch (Exception ex)
            {
                return Content(KendoUiHelper.GetKendoUiWindowAjaxSuccessMethod(Boolean.TrueString, MessageType.success.ToString(), ExceptionHelper.ExceptionMessageFormat(ex)));
            }
        }

        //
        // POST: /Category/Delete/By ID
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoryViewModel = _iCategoryService.GetById(id);
                if (categoryViewModel != null)
                {
                    _iCategoryService.Delete(categoryViewModel);
                    _iCategoryService.Save();

                    return Json(new { status = Boolean.FalseString, messageType = MessageType.success.ToString(), messageText = "Deleted Successfully." }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { status = Boolean.FalseString, messageType = MessageType.warn.ToString(), messageText = ExceptionHelper.ExceptionMessageForNullObject() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { status = Boolean.FalseString, messageType = MessageType.error.ToString(), messageText = ExceptionHelper.ExceptionMessageFormat(ex) }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Method

        private List<CategoryViewModel> GetCategoryDataList()
        {
            var dataList = _iCategoryService.GetAll().ToList();

            var viewModels = dataList.Select(
                md => new CategoryViewModel
                {
                    Id = md.Id,
                    Name = md.Name,

                    //ActionLink = KendoUiHelper.KendoUIGridActionLinkGenerate(md.Id.ToString())
                }).OrderBy(o => o.Name).ToList();

            return viewModels;
        }

        #endregion

    }
}
