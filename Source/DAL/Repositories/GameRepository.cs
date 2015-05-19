using Core;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class GameRepository
    {
        public List<Game> GetList(Enums.Status? status)
        {
            IEnumerable<Game> list = DBContext.Context.Games;

            if (status.HasValue)
            {
                list = list.Where(x => x.Status == Convert.ToInt32(status)).ToList();
            }

            return list.ToList();
        }
    }
}
