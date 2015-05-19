using Core.API;
using Core.API.GameApiRequests;
using Core.API.GameApiResponses;
using DAL.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Web.BackEnd.Controllers
{
    public class GameApiController : ApiController
    {
        InsideRepository1 insideRepository = new InsideRepository1();
        UserRepository userRepository = new UserRepository();
        GameAPI gameAPI = new GameAPI();

        public async Task<string> GetRoleList(string accountName, int? serverId)
        {
            string json = "";

            if (!string.IsNullOrWhiteSpace(accountName) && serverId.HasValue && serverId.Value > 0)
            {
                int accountId = userRepository.GetAccountId(accountName);
                if (accountId > 0)
                {
                    RoleListRequest roleListRequest = new RoleListRequest()
                    {
                        AccountId = accountId,
                        ServerId = serverId.Value,
                        Page = 1,
                        PageSize = 3
                    };

                    List<RoleListResponse> roleListResponse = await gameAPI.GetRoleList(roleListRequest);

                    json = JsonConvert.SerializeObject(roleListResponse);
                }
            }

            return json;
        }

        public async Task<string> GetRoleList(int accountId, int? serverId)
        {
            string json = "";

            if (accountId > 0 && serverId.HasValue && serverId.Value > 0)
            {
                if (accountId > 0)
                {
                    RoleListRequest roleListRequest = new RoleListRequest()
                    {
                        AccountId = accountId,
                        ServerId = serverId.Value,
                        Page = 1,
                        PageSize = 3
                    };

                    List<RoleListResponse> roleListResponse = await gameAPI.GetRoleList(roleListRequest);

                    json = JsonConvert.SerializeObject(roleListResponse);
                }
            }

            return json;
        }
    }
}