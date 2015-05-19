using DAL.DAOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ServerRepository
    {
        static Dictionary<int, string> serverList = new Dictionary<int, string>()
        {
            {1, "Tương Dương"}
            //{2, "Huyền thoại"}
        };


        public ServerRepository()
        {

        }

        //public List<ServerDAO> GetServerList()
        //{
        //    List<ServerDAO> list = new List<ServerDAO>();

        //    foreach (KeyValuePair<int, string> item in serverList)
        //    {
        //        ServerDAO view = new ServerDAO()
        //        {
        //            ServerID = item.Key,
        //            ServerName = item.Value.ToString()
        //        };

        //        list.Add(view);
        //    }

        //    return list;
        //}

        public static string GetServerName(object serverId)
        {
            if (serverId != null)
            {
                return serverList[Convert.ToInt32(serverId)].ToString();
            }
            else
            {
                return "";
            }
        }

        public List<Server> GetAllServer()
        {
            return DBContext.Context.Servers.ToList();
        }

        public List<Server> GetList(bool isActive = true)
        {
            return DBContext.Context.Servers.Where(x => x.IsActive == isActive).ToList();
        }

        public List<Server> GetList(bool isActive, bool isReal, int page, int numberOfRow, ref int total)
        {
            IQueryable<Server> list = DBContext.Context.Servers.OrderByDescending(x => x.ServerId)
                .Where(x => x.IsActive == isActive && x.IsReal == isReal);

            List<Server> serverList = list.Skip((page - 1) * numberOfRow).Take(numberOfRow).ToList();

            if (serverList != null && serverList.Count > 0)
            {
                total = list.Count();
            }

            return serverList;
        }

        public List<Server> GetList(bool isActive, bool isReal)
        {
            List<Server> serverList = DBContext.Context.Servers.OrderByDescending(x => x.ServerId)
                .Where(x => x.IsActive == isActive && x.IsReal == isReal).ToList();

            return serverList;
        }

        public Server GetLatestServer()
        {
            //int serverId = 1;
            Server server = DBContext.Context.Servers.Where(x => x.IsActive && x.IsReal).OrderByDescending(x => x.ServerId).FirstOrDefault();

            if (server != null)
            {
                return server;
            }

            return null;
        }

        public Server GetById(int serverId)
        {
            return DBContext.Context.Servers.Where(x => x.ServerId == serverId).FirstOrDefault();
        }

        public int Insert(Server server)
        {
            Server _server = GetById(server.ServerId);
            if (_server != null)
            {
                DBContext.Context.Servers.InsertOnSubmit(server);
                DBContext.Context.SubmitChanges();
                return server.ServerId;
            }
            else
            {
                return -1;
            }
        }

        public bool Update(Server server)
        {
            Server item = GetById(server.ServerId);
            if (item != null)
            {
                item.ServerName = server.ServerName;
                item.IsActive = server.IsActive;
                item.IsNew = server.IsNew;

                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetActive(int serverId)
        {
            Server server = GetById(serverId);
            if (server != null)
            {
                server.IsActive = !server.IsActive;

                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetNew(int serverId)
        {
            Server server = GetById(serverId);
            if (server != null)
            {
                server.IsActive = !server.IsActive;

                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int serverId)
        {
            Server item = GetById(serverId);
            if (item != null)
            {
                DBContext.Context.Servers.DeleteOnSubmit(item);
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
