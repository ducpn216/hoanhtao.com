using AutoMapper;
using Core;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.BackEnd.App_Code;
using Web.BackEnd.App_Code.Attributes;
using Web.BackEnd.Models;
using Web.FrontEnd.App_Code.Controllers;

namespace Web.BackEnd.Controllers
{
    [CustomAuthorize(ForbiddenGroupId = new[] { 2, 3 })]
    public class CategoryController : BackEndController
    {
        CategoryRepository categoryRepository = new CategoryRepository();

        public CategoryController()
        {
            Mapper.CreateMap<Category, CategoryModel>();
            Mapper.CreateMap<CategoryModel, Category>();
        }

        public ActionResult Index(int? parentId)
        {
            parentId = parentId.HasValue ? parentId.Value : 0;

            CategoryListModel categoryListModel = new CategoryListModel()
            {
                ParentCategoryList = BackEndUtilities.GetCategoryList(),
                CategoryList = new List<CategoryModel>()
            };

            if (parentId.HasValue)
            {
                categoryListModel.SelectedParentId = parentId.Value;
            }

            var categoryList = categoryRepository.GetList(parentId);
            foreach (var category in categoryList)
            {
                categoryListModel.CategoryList.Add(Mapper.Map<Category, CategoryModel>(category));
            }

            return View(categoryListModel);
        }

        [HttpGet]
        public ActionResult Edit(int? categoryId, int? parentId)
        {
            CategoryFormModel categoryFormModel = new CategoryFormModel()
            {
                ParentCategoryList = BackEndUtilities.GetCategoryList(categoryId),
                CategoryModel = new CategoryModel()
            };

            if (categoryId.HasValue)
            {
                Category category = categoryRepository.GetDetail(categoryId.Value);
                if (category != null)
                {
                    categoryFormModel.CategoryModel = Mapper.Map<Category, CategoryModel>(category);
                    categoryFormModel.CategoryModel.CategoryName = category.CategoryName.Trim(new char[]{'-', ' '});
                    categoryFormModel.SelectedParentId = category.ParentId;
                }
            }

            ViewBag.ParentId = parentId.HasValue ? parentId.Value : 0;

            return View(categoryFormModel);
        }

        [HttpPost]
        public ActionResult Edit(CategoryFormModel categoryFormModel)
        {
            if (categoryFormModel != null)
            {
                string message = CheckCategoryValid(categoryFormModel);
                if (string.IsNullOrEmpty(message))
                {
                    CategoryModel categoryModel = categoryFormModel.CategoryModel;

                    Category category = Mapper.Map<CategoryModel, Category>(categoryModel);
                    category.CategoryName = categoryFormModel.CategoryModel.CategoryName.Trim(new char[] { '-', ' ' });
                    category.IsActive = true;
                    category.ParentId = categoryFormModel.SelectedParentId;

                    if (categoryModel.CategoryId > 0)
                    {
                        if (categoryRepository.Update(category))
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Cập nhật thành công", Enums.ErrorCode.Success);
                        }
                        else
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Cập nhật thành công", Enums.ErrorCode.Success);
                        }
                    }
                    else
                    {
                        category.CategoryId = categoryRepository.Insert(category);
                        if (category.CategoryId > 0)
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Thêm thành công", Enums.ErrorCode.Success);
                            return RedirectToAction("Index", "Category", new { parentId = categoryFormModel.SelectedParentId });
                        }
                        else
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Thêm thất bại", Enums.ErrorCode.Error);
                        }
                    }
                }
                else
                {
                    TempData["Message"] = BackEndUtilities.SetMessage(message, Enums.ErrorCode.Error);
                }
            }

            categoryFormModel.ParentCategoryList = BackEndUtilities.GetCategoryList(categoryFormModel.SelectedParentId);

            return View(categoryFormModel);
        }

        public ActionResult Delete(int? categoryId, int? parentId)
        {
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                Category category = categoryRepository.GetDetail(categoryId.Value);
                if (category != null)
                {
                    if (category.ParentId == 0)
                    {
                        TempData["Message"] = BackEndUtilities.SetMessage("Không được xóa danh mục chính", Enums.ErrorCode.Error);
                    }
                    else
                    {
                        if (categoryRepository.Delete(categoryId.Value))
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Xóa thành công", Enums.ErrorCode.Success);
                        }
                        else
                        {
                            TempData["Message"] = BackEndUtilities.SetMessage("Xóa thất bại", Enums.ErrorCode.Error);
                        }
                    }
                    
                }
            }

            return RedirectToAction("Index", "Category", new { parentId = parentId.Value }); 
        }

        [HttpPost]
        public ActionResult UpdateOrder(CategoryListModel categoryListModel)
        {
            if (categoryListModel != null)
            {
                List<int> idList = new List<int>();
                List<int> orderList = new List<int>();

                foreach (var category in categoryListModel.CategoryList)
                {
                    idList.Add(category.CategoryId);
                    orderList.Add(category.Order);
                }
                FileLog.Write("Category", "", 
                    categoryListModel.CategoryList.Count.ToString() + "&" + idList.Count.ToString() + "&" + orderList.Count.ToString());
                if (categoryRepository.UpdateOrder(idList, orderList))
                {
                    TempData["Message"] = BackEndUtilities.SetMessage("Cập nhật thành công", Enums.ErrorCode.Success);
                }
                else
                {
                    TempData["Message"] = BackEndUtilities.SetMessage("Cập nhật thất bại", Enums.ErrorCode.Error);
                }
            }

            return RedirectToAction("Index", "Category", new { parentId = categoryListModel.SelectedParentId });
        }

        string CheckCategoryValid(CategoryFormModel categoryFormModel)
        {
            string message = "";

            if (string.IsNullOrEmpty(categoryFormModel.CategoryModel.CategoryName))
            {
                message = "Nhập tên danh mục<br/>";
            }

            return message;
        }
    }
}