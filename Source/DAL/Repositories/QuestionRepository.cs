using Core;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class QuestionRepository
    {
        //string className = "Domain.Repository.QuestionRepository";

        public List<Question> GetList(Enums.Status? status)
        {
            List<Question> list = DBContext.Context.Questions.ToList();

            if (status.HasValue)
            {
                bool questionStatus = (status == Enums.Status.Active) ? true : false;
                list = list.Where(x => x.IsActive == Convert.ToBoolean(questionStatus)).ToList();
            }

            return list;
        }

        public Question GetById(int questionId)
        {
            return DBContext.Context.Questions.Where(x => x.QuestionId == questionId).FirstOrDefault();
        }

        public int Insert(Question question)
        {
            DBContext.Context.Questions.InsertOnSubmit(question);
            DBContext.Context.SubmitChanges();
            return question.QuestionId;
        }

        public bool Update(Question question)
        {
            Question item = GetById(question.QuestionId);
            if (item != null)
            {
                DBContext.Context.Questions.InsertOnSubmit(question);
                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Question question)
        {
            Question item = GetById(question.QuestionId);
            if (item != null)
            {
                DBContext.Context.Questions.DeleteOnSubmit(question);
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
