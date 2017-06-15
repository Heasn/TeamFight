using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using TeamFight.Host.Models;

namespace TeamFight.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var baseurl = "http://localhost:9955/api/teamservice";
            var encoding = Encoding.UTF8;
            int charId;
            Guid teamId;
            Guid inviteTeamId;

            Console.WriteLine("请输入一个角色Id，后续流程将会使用该Id的角色");
            Console.WriteLine(int.TryParse(Console.ReadLine(), out charId) ? "记录成功，请输入后续指令" : "角色Id错误，请关闭程序重试");

            var input = Console.ReadLine();
            while (input != "exit")
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    switch (input)
                    {
                        case "login":
                        {
                            var url = baseurl + "/login";
                            var postData = encoding.GetBytes("CharId=" + charId);
                            var responseData = client.UploadData(url, "POST", postData);
                            Console.WriteLine(encoding.GetString(responseData));
                        }
                            break;
                        case "create":
                        {
                            var url = baseurl + "/create";
                            var postData = encoding.GetBytes("CharId=" + charId);
                            var responseData = client.UploadData(url, "POST", postData);
                            teamId = JsonConvert.DeserializeObject<Guid>(encoding.GetString(responseData));
                            Console.WriteLine("已创建组队：队伍Id[" + teamId + "]");
                        }
                            break;
                        case "pullfriend":
                        {
                            var url = baseurl + "/PullFriendList";
                            var postData = encoding.GetBytes("CharId=" + charId);
                            var responseData = client.UploadData(url, "POST", postData);
                            var friendIdList = JsonConvert.DeserializeObject<List<int>>(encoding.GetString(responseData));

                            if (friendIdList == null)
                            {
                                Console.WriteLine("好友列表为空");
                            }
                            else
                            {
                                var sb = new StringBuilder();
                                foreach (var id in friendIdList)
                                    sb.Append(id + ",");

                                Console.WriteLine("已获取好友列表，好友Id为 " + sb);
                            }
                        }
                            break;
                        case "pullrecommandteams":
                        {
                            var url = baseurl + "/PullRecommendTeams";
                            byte[] postData = {};
                            var responseData = client.UploadData(url, "POST", postData);

                            var teamList =
                                JsonConvert.DeserializeObject<List<RecommendTeamModel>>(encoding.GetString(responseData));

                            if (teamList == null)
                            {
                                Console.WriteLine("没有推荐组队");
                            }
                            else
                            {
                                var sb = new StringBuilder();
                                foreach (var model in teamList)
                                    sb.AppendFormat("队伍Id：{0}，队长名字：{1}，队长等级{2}，队长头像Id：{3}\n", model.TeamId,
                                        model.CaptainName, model.CaptainLevel, model.CaptainFaceId);

                                Console.WriteLine("已组队推荐列表，内容 " + sb);
                            }
                        }
                            break;
                        case "invite":
                        {
                            Console.WriteLine("请输入要邀请的角色的Id：");
                            var invitecharId = Console.ReadLine();

                            var url = baseurl + "/InviteCharacter";
                            var postData = encoding.GetBytes("CharId=" + charId + "&InvitedCharId=" + invitecharId);
                            var responseData = client.UploadData(url, "POST", postData);

                            var result = JsonConvert.DeserializeObject<bool>(encoding.GetString(responseData));
                            Console.WriteLine(result ? "发出邀请成功" : "发出邀请失败");
                        }
                            break;
                        case "queryinvitation":
                        {
                            var url = baseurl + "/QueryInvitation";
                            var postData = encoding.GetBytes("CharId=" + charId);
                            var responseData = client.UploadData(url, "POST", postData);

                            inviteTeamId = JsonConvert.DeserializeObject<Guid>(encoding.GetString(responseData));
                            Console.WriteLine(inviteTeamId != Guid.Empty ? "有组队邀请，队伍Id " + inviteTeamId : "无组队邀请");
                        }
                            break;
                    }
                }

                input = Console.ReadLine();
            }
        }
    }
}