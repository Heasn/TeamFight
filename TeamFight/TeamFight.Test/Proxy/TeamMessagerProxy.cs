// ****************************************
// FileName:TeamMessagerProxy.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-19
// Revision History:
// ****************************************

using System;
using System.Collections.Generic;
using System.Net.Http;


namespace TeamFight.Test.Proxy
{
    using Newtonsoft.Json;
    using Models;

    /// <summary>
    /// 组队消息代理类
    /// </summary>
    public class TeamMessagerProxy
    {
        /// <summary>
        /// 接口地址
        /// </summary>
        private string mBaseurl = "http://localhost:9000/api/team";

        /// <summary>
        /// 创建队伍        
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public Guid Create(int playerId)
        {
            var url = mBaseurl + "/Create";

            using (var client = new HttpClient())
            {
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("", playerId.ToString())
                });

                var response = client.PostAsync(url, postData).Result;
                return JsonConvert.DeserializeObject<Guid>(response.Content.ReadAsStringAsync().Result);
            }
        }

        /// <summary>
        /// 获取推荐组队列表    
        /// </summary>
        /// <returns></returns>
        public List<RecommendTeamModel> PullRecommendTeams()
        {
            var url = mBaseurl + "/PullRecommendTeams";

            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, null).Result;
                return JsonConvert.DeserializeObject<List<RecommendTeamModel>>(response.Content.ReadAsStringAsync().Result);
            }
        }

        /// <summary>
        /// 邀请玩家加入组队
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <param name="invitedPlayerId">被邀请玩家Id</param>
        /// <returns></returns>
        public bool InviteCharacter(int playerId, int invitedPlayerId)
        {
            var url = mBaseurl + "/InviteCharacter";

            using (var client = new HttpClient())
            {
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("CharacterId", playerId.ToString()),
                    new KeyValuePair<string, string>("InvitedCharacterId", invitedPlayerId.ToString()),
                });

                var response = client.PostAsync(url, postData).Result;
                return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
            }
        }

        /// <summary>
        /// 加入队伍
        /// </summary>
        /// <param name="teamId">队伍Id</param>
        /// <param name="invitedPlayerId"></param>
        /// <returns></returns>
        public bool JoinTeam(Guid teamId, int invitedPlayerId)
        {
            var url = mBaseurl + "/JoinTeam";

            using (var client = new HttpClient())
            {
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("TeamId", teamId.ToString()),
                    new KeyValuePair<string, string>("InvitedCharacterId", invitedPlayerId.ToString()),
                });

                var response = client.PostAsync(url, postData).Result;
                return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
            }
        }

        /// <summary>
        /// 退出队伍
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public bool QuitTeam(int playerId)
        {
            var url = mBaseurl + "/QuitTeam";

            using (var client = new HttpClient())
            {
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("",playerId.ToString())
                });

                var response = client.PostAsync(url, postData).Result;
                return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
            }
        }

        /// <summary>
        /// 发送邀请战斗邀请
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public bool SendFightInvitation(int playerId)
        {
            var url = mBaseurl + "/SendFightInvitation";

            using (var client = new HttpClient())
            {
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("",playerId.ToString())
                });

                var response = client.PostAsync(url, postData).Result;
                return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
            }
        }

        /// <summary>
        /// 投票是否同意开战
        /// </summary>
        /// <param name="teamId">队伍Id</param>
        /// <param name="confirmResult">投票结果</param>
        /// <returns></returns>
        public bool VoteFightInvitation(Guid teamId, bool confirmResult)
        {
            var url = mBaseurl + "/VoteFightInvitation";

            using (var client = new HttpClient())
            {
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("TeamId", teamId.ToString()),
                    new KeyValuePair<string, string>("ConfirmResult", confirmResult.ToString()),
                });

                var response = client.PostAsync(url, postData).Result;
                return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
            }
        }

        /// <summary>
        /// 拉取投票结果
        /// </summary>
        /// <param name="teamId">队伍Id</param>
        /// <returns>NULL（还未完成投票）、True（投票通过）、False（投票拒绝）</returns>
        public bool? PullFightInvitationResult(Guid teamId)
        {
            var url = mBaseurl + "/PullFightInvitationResult";

            using (var client = new HttpClient())
            {
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("",teamId.ToString()),
                });

                var response = client.PostAsync(url, postData).Result;
                return JsonConvert.DeserializeObject<bool?>(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}