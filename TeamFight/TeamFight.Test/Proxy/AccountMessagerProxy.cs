// ****************************************
// FileName:AccountMessagerProxy.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-19
// Revision History:
// ****************************************

using System.Collections.Generic;
using System.Net.Http;

namespace TeamFight.Test.Proxy
{
    using Newtonsoft.Json;
    using Models;

    /// <summary>
    /// 账户类消息代理类
    /// </summary>
    class AccountMessagerProxy
    {
        /// <summary>
        /// 接口地址
        /// </summary>
        private string mBaseurl = "http://localhost:9000/api/account";

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public PlayerPropertiesModel Login(int playerId)
        {
            var url = mBaseurl + "/Login";

            using (var client = new HttpClient())
            {
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("", playerId.ToString())
                });

                var response = client.PostAsync(url, postData).Result;
                return JsonConvert.DeserializeObject<PlayerPropertiesModel>(response.Content.ReadAsStringAsync().Result);
            }
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public PlayerFriendModel[] GetFriends(int playerId)
        {
            var url = mBaseurl + "/PullFriends";

            using (var client = new HttpClient())
            {
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("", playerId.ToString())
                });

                var response = client.PostAsync(url, postData).Result;
                return JsonConvert.DeserializeObject<PlayerFriendModel[]>(response.Content.ReadAsStringAsync().Result);
            }
        }

    }
}
