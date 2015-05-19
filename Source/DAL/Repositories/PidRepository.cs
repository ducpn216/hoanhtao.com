using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PidRepository
    {
        public List<Pid> GetList()
        {
            List<Pid> list = DBContext.Context.Pids.OrderByDescending(x => x.Pid1).ToList();

            return list;
        }

        public List<Pid> GetList(int page, int numberOfRow, ref int total)
        {
            IQueryable<Pid> list = DBContext.Context.Pids.OrderByDescending(x => x.Pid1);

            List<Pid> PidList = list.Skip((page - 1) * numberOfRow).Take(numberOfRow).ToList();

            if (PidList != null && PidList.Count > 0)
            {
                total = list.Count();
            }

            return PidList;
        }

        public Pid GetById(Guid pid)
        {
            return DBContext.Context.Pids.Where(x => x.Pid1 == pid).FirstOrDefault();
        }

        public Guid Insert(Pid pid)
        {
            DBContext.Context.Pids.InsertOnSubmit(pid);
            DBContext.Context.SubmitChanges();
            return pid.Pid1;
        }

        public bool Update(Pid pid)
        {
            Pid item = GetById(pid.Pid1);
            if (item != null)
            {
                item.Link = pid.Link;
                item.Title = pid.Title;

                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Guid pid)
        {
            Pid item = GetById(pid);
            if (item != null)
            {
                DBContext.Context.Pids.DeleteOnSubmit(item);
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
