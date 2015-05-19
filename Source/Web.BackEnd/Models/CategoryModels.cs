using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.BackEnd.Models
{
    public class CategoryListModel
    {
        public CategoryListModel()
        {
            ParentCategoryList = new List<SelectListItem>();
            CategoryList = new List<CategoryModel>();
        }

        public List<SelectListItem> ParentCategoryList { get; set; }
        public int SelectedParentId { get; set; }
        public List<CategoryModel> CategoryList { get; set; }
    }

    public class CategoryFormModel
    {
        public CategoryFormModel()
        {
            //GameList = new List<SelectListItem>();
            ParentCategoryList = new List<SelectListItem>();
        }

        public List<SelectListItem> ParentCategoryList { get; set; }
        public int SelectedParentId { get; set; }

        public CategoryModel CategoryModel { get; set; }
    }

    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public int ParentId { get; set; }
    }
}