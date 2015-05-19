using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ToolLogRepository
    {
        public List<ToolLog> GetList(int? userId, int? functionId)
        {
            IQueryable<ToolLog> query = DBContext.Context.ToolLogs;

            if (userId.HasValue)
            {
                query = query.Where(x => x.UserId == userId.Value);
            }

            if (functionId.HasValue)
            {
                query = query.Where(x => x.FunctionId == functionId.Value);
            }

            List<ToolLog> list = DBContext.Context.ToolLogs.Where(x => x.FunctionId == functionId).ToList();
            return list;
        }

        public List<ToolLog> GetList(int functionId)
        {
            List<ToolLog> list = DBContext.Context.ToolLogs.Where(x => x.FunctionId == functionId).ToList();
            return list;
        }

        public int Insert(ToolLog toolLog)
        {
            DBContext.Context.ToolLogs.InsertOnSubmit(toolLog);
            DBContext.Context.SubmitChanges();
            return toolLog.Id;
        }
    }
}
