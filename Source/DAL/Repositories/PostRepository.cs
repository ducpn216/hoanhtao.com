using Core;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class PostRepository
    {
        //string className = "Domain.Business.News";

        //public IQueryable<Post> GetList(int gameId, int categoryId,
        //    Enums.PostType postType, Enums.Status? status)
        //{
        //    IQueryable<Post> list = DBContext.Context.Posts
        //        .Where(x => x.GameId == gameId && x.PostTypeId == (int)postType)
        //        .OrderByDescending(x=>x.PostId);

        //    if (status.HasValue)
        //    {
        //        list = list.Where(x => x.Status == Convert.ToInt32(status));
        //    }

        //    return list;
        //}

        public List<Post> GetOthers(int postId, int categoryId, int postTypeId, Enums.Status? status, int numberOfRow)
        {
            IQueryable<Post> list = DBContext.Context.Posts.OrderByDescending(x => x.PostId)
                .Where(x => x.PostTypeId == postTypeId && x.CategoryId == categoryId && x.PostId != postId);

            if (status.HasValue)
            {
                list = list.Where(x => x.Status == Convert.ToInt32(status.Value));
            }

            List<Post> postList = list.Take(numberOfRow).ToList();

            return postList;
        }

        public List<Post> GetOthers(int postId)
        {
            List<Post> list = new List<Post>();
            Post post = GetById(postId);

            if (postId != null)
            {
                list = DBContext.Context.Posts
                .Where(x => x.CategoryId == post.CategoryId)
                .OrderBy(x => x.PostId).ToList();
            }

            return list;
        }

        public List<Post> GetTop(int gameId, int postTypeId, Enums.Status? status, int numberOfRow)
        {
            IQueryable<Post> list = DBContext.Context.Posts.OrderByDescending(x => x.PostId)
                .Where(x => x.PostTypeId == postTypeId && x.GameId == gameId);

            if (status.HasValue)
            {
                list = list.Where(x => x.Status == Convert.ToInt32(status.Value));
            }

            List<Post> postList = list.Take(numberOfRow).ToList();
            return postList;
        }

        public List<Post> GetList(int gameId, int? categoryId,
            int postTypeId, Enums.Status? status, int page, int numberOfRow, ref int total)
        {
            IQueryable<Post> list = DBContext.Context.Posts.OrderByDescending(x => x.PostId)
                .Where(x => x.PostTypeId == postTypeId && x.GameId == gameId);

            if (categoryId.HasValue)
            {
                list = list.Where(x => x.CategoryId == categoryId.Value);
            }

            if (status.HasValue)
            {
                list = list.Where(x => x.Status == Convert.ToInt32(status.Value));
            }

            List<Post> postList = list.Skip((page - 1) * numberOfRow).Take(numberOfRow).ToList();

            if (postList != null && postList.Count > 0)
            {
                total = list.Count();
            }

            return postList;
        }

        public List<Post> GetList(int gameId, Enums.Status? status, int numberOfRow)
        {
            IQueryable<Post> list = DBContext.Context.Posts.OrderByDescending(x => x.PostId)
                .Where(x => x.GameId == gameId);

            if (status.HasValue)
            {
                list = list.Where(x => x.Status == Convert.ToInt32(status.Value));
            }

            List<Post> postList = list.Take(numberOfRow).ToList();

            return postList;
        }

        public List<Post> GetList(List<int> categoryList, ref int rowTotal,
            Enums.PostType postType, Enums.Status? status,
            int page = 0, int numberOfRow = 0)
        {
            IQueryable<Post> list = DBContext.Context.Posts.OrderByDescending(x => x.PostId)
                .Where(x => categoryList.Contains(x.PostTypeId));

            if (status.HasValue)
            {
                list = list.Where(x => x.Status == Convert.ToInt32(status.Value));
            }

            if (list != null)
            {
                rowTotal = list.Count();

                int startRow = (page - 1) * numberOfRow;

                List<Post> postList = list.Skip(startRow).Take(numberOfRow).ToList();

                return postList;
            }
            else
            {
                return null;
            }
        }

        //public List<Post> GetListByCategoryId(int categoryId, Enums.ActiveTypes activeType)
        //{
        //    IEnumerable<Post> list = DBContext.Context.Posts.Where(x => x.CategoryId == categoryId).OrderByDescending(x => x.PostId);

        //    if (activeType != Enums.ActiveTypes.All)
        //    {
        //        list = list.Where(x => x.IsActive == Convert.ToBoolean(activeType));
        //    }

        //    return list.ToList();
        //}

        public Post GetById(int postId)
        {
            return DBContext.Context.Posts.Where(x => x.PostId == postId).FirstOrDefault();
        }

        public Post GetFirstPost(int categoryId, int postTypeId)
        {
            return DBContext.Context.Posts.Where(x => x.CategoryId == categoryId && x.PostTypeId == postTypeId).OrderBy(x => x.PostId).FirstOrDefault();
        }

        public List<Post> GetList(int postTypeId, int categoryId)
        {
            return DBContext.Context.Posts.Where(x => x.PostTypeId == postTypeId && x.CategoryId == categoryId).OrderBy(x => x.PostId).ToList();
        }

        public int Insert(Post post)
        {
            DBContext.Context.Posts.InsertOnSubmit(post);
            DBContext.Context.SubmitChanges();
            return post.PostId;
        }

        public bool Update(Post post)
        {
            Post item = GetById(post.PostId);
            if (item != null)
            {
                item.ImagePath = (post.ImagePath != "") ? post.ImagePath : item.ImagePath;
                item.Title = post.Title;
                item.Summary = post.Summary;
                item.Content = post.Content;
                item.PostTypeId = post.PostTypeId;
                item.CategoryId = post.CategoryId;
                item.Status = post.Status;

                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int postId)
        {
            Post item = GetById(postId);
            if (item != null)
            {
                DBContext.Context.Posts.DeleteOnSubmit(item);
                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
