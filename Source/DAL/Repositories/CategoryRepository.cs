using Core;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository
    {
        //public List<Category> GetList()
        //{
        //    IQueryable<Category> iCategory = DBContext.Context.Categories.OrderBy(x => x.CategoryId);
        //    List<Category> list = null;

        //    if (iCategory != null)
        //    {
        //        list = iCategory.ToList();
        //    }

        //    return list;
        //}

        public List<Category> GetAll(Enums.Status? status)
        {
            List<Category> list = DBContext.Context.Categories.ToList();

            if (status.Value == Enums.Status.Active)
            {
                list = list.Where(x => x.IsActive).OrderBy(x => x.Order).ToList();
            }
            else if (status.Value == Enums.Status.Active)
            {
                list = list.Where(x => x.IsActive == false).OrderBy(x => x.Order).ToList();
            }

            return list;
        }

        public int GetParentIdByCategoryId(int categoryId)
        {
            int parentId = -1;

            Category category = DBContext.Context.Categories.Where(x => x.CategoryId == categoryId).OrderBy(x => x.CategoryId).FirstOrDefault();
            if (category != null)
            {
                parentId = category.ParentId;
            }

            return parentId;
        }

        public int GetCategoryIdByPostId(int postId)
        {
            int categoryId = -1;

            Post post = DBContext.Context.Posts.Where(x => x.PostId == postId).FirstOrDefault();
            if (post != null)
            {
                if (post.CategoryId.HasValue)
                {
                    categoryId = post.CategoryId.Value;
                }
                
            }

            return categoryId;
        }

        public List<Category> GetListByParentId(int parentId)
        {
            List<Category> list = DBContext.Context.Categories.Where(x => x.ParentId == parentId).OrderBy(x => x.CategoryId).ToList();

            return list;
        }

        public int Insert(Category category)
        {
            DBContext.Context.Categories.InsertOnSubmit(category);
            DBContext.Context.SubmitChanges();

            category.Order = category.CategoryId;
            DBContext.Context.SubmitChanges();

            return category.CategoryId;
        }

        public bool Update(Category category)
        {
            Category item = GetDetail(category.CategoryId);
            if (item != null)
            {
                item.ImagePath = category.ImagePath;
                item.CategoryName = category.CategoryName;
                item.GameId = category.GameId;
                item.IsActive = category.IsActive;
                item.Order = category.Order == 0 ? 1 : category.Order;
                item.ParentId = category.ParentId;

                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateOrder(List<int> idList, List<int> orderList)
        {
            if (idList != null && idList.Count > 0 && orderList != null && orderList.Count > 0)
            {
                List<Category> categoryList = DBContext.Context.Categories.ToList();

                for (int i = 0; i < idList.Count; i++)
                {
                    Category category = categoryList.Where(x => x.CategoryId == idList[i]).FirstOrDefault();
                    if (category != null)
                    {
                        category.Order = orderList[i];
                    }
                }

                DBContext.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool Delete(int categoryId)
        {
            Category item = GetDetail(categoryId);
            if (item != null)
            {
                DBContext.Context.Categories.DeleteOnSubmit(item);
                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Hiearachy

        public Category GetDetail(int categoryId)
        {
            Category category = DBContext.Context.Categories.Where(x => x.CategoryId == categoryId).FirstOrDefault();
            return category;
        }

        public List<Category> GetList(int? categoryId = null)
        {
            List<Category> temp = new List<Category>();
            List<Category> categoryList = new List<Category>();

            categoryList = DBContext.Context.Categories.OrderBy(x => x.ParentId).OrderBy(x => x.Order).ToList();

            if (categoryList != null && categoryList.Count > 0)
            {
                if (categoryId != null && categoryId == 0)
                {
                    categoryList = categoryList.Where(x => x.ParentId == 0).ToList();
                    foreach (var item in categoryList)
                    {
                        temp.Add(item);
                    }
                }
                else if (categoryId != null && categoryId > 0)
                {
                    Category category = categoryList.Where(x => x.CategoryId == categoryId).FirstOrDefault();

                    if (category != null)
                    {
                        temp.Add(category);

                        List<Category> childList = GetChildCategory(category.CategoryId, 1, categoryList);
                        if (childList != null)
                        {
                            temp.AddRange(childList);
                        }
                    }
                }
                else
                {
                    foreach (var item in categoryList)
                    {
                        if (item.ParentId == 0)
                        {
                            temp.Add(item);

                            List<Category> childList = GetChildCategory(item.CategoryId, 1, categoryList);
                            if (childList != null)
                            {
                                temp.AddRange(childList);
                            }
                        }
                    }
                }
            }

            return temp;
        }

        string GetCategoryLevelString(int categoryLevel)
        {
            string str = "";

            for (int i = 0; i < categoryLevel; i++)
            {
                str += "- - - ";
            }

            return str;
        }

        public List<Category> GetChildCategory(int parentId, int categoryLevel, List<Category> allList)
        {
            List<Category> list = new List<Category>();

            foreach (var item in allList)
            {
                if (item.ParentId == parentId)
                {
                    //item.CategoryName = GetCategoryLevelString(categoryLevel) + item.CategoryName;
                    list.Add(new Category()
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = GetCategoryLevelString(categoryLevel) + item.CategoryName,
                        Order = item.Order,
                        ParentId = item.ParentId,
                        IsActive = item.IsActive
                    });

                    int childCategoryLevel = categoryLevel + 1;

                    List<Category> childList = GetChildCategory(item.CategoryId, childCategoryLevel, allList);
                    if (childList != null)
                    {
                        list.AddRange(childList);
                    }
                }
            }

            return list;
        }


        #endregion
    }
}
