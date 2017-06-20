// ****************************************
// FileName:InvitationMessagerProxy.cs
// Description:邀请查询代理类
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

    /// <summary>
    /// 邀请消息代理类
    /// </summary>
    class InvitationMessagerProxy
    {
        /// <summary>
        /// 接口地址
        /// </summary>
        private string mBaseurl = "http://localhost:9000/api/Message";

        /// <summary>
        /// 查询是否有邀请
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public dynamic Query(int playerId)
        {
            var url = mBaseurl + "/PullMessage";

            using (var client = new HttpClient())
            {
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("", playerId.ToString())
                });

                var response = client.PostAsync(url, postData).Result;

                return JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
