// ****************************************
// FileName:Program.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-15
// Revision History:
// ****************************************

using System;
using TeamFight.Test.Proxy;

namespace TeamFight.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int playerId;
            Guid teamId = Guid.Empty;
            //Guid inviteTeamId = Guid.Empty;

            Console.WriteLine("请输入一个角色Id，后续流程将会使用该Id的角色");
            Console.WriteLine(int.TryParse(Console.ReadLine(), out playerId) ? "记录成功，请输入后续指令" : "角色Id错误，请关闭程序重试");

            var accountMessagerProxy = new AccountMessagerProxy();
            var teamMessagerProxy = new TeamMessagerProxy();
            var invitationMessagerProxy = new InvitationMessagerProxy();

            var input = Console.ReadLine();
            while (input != "exit")
            {

                switch (input)
                {
                    case "login":
                        Console.WriteLine(accountMessagerProxy.Login(playerId) != null);
                        break;
                    case "create":
                        teamId = teamMessagerProxy.Create(playerId);
                        Console.WriteLine(teamId != Guid.Empty);
                        break;
                    case "getfriend":
                        Console.WriteLine(accountMessagerProxy.GetFriends(playerId) != null);
                        break;
                    case "getrecommandteams":
                        Console.WriteLine(teamMessagerProxy.PullRecommendTeams() != null);
                        break;
                    case "invite":
                        Console.WriteLine("请输入要邀请的角色的Id：");

                        int invitecharId;

                        while (!int.TryParse(Console.ReadLine(), out invitecharId))
                        {
                            Console.WriteLine("角色Id输入错误，请重新输入");
                        }

                        Console.WriteLine(teamMessagerProxy.InviteCharacter(playerId, invitecharId));
                        break;
                    case "join":
                        Console.WriteLine(teamMessagerProxy.JoinTeam(teamId, playerId));
                        break;
                    case "quit":
                        Console.WriteLine(teamMessagerProxy.QuitTeam(playerId));
                        break;
                    case "beginfight":
                        Console.WriteLine(teamMessagerProxy.SendFightInvitation(playerId));
                        break;
                    case "vote":
                        Console.WriteLine("请输入要投票的结果（true或false）：");

                        bool confirmResult;

                        while (!bool.TryParse(Console.ReadLine(), out confirmResult))
                        {
                            Console.WriteLine("角色Id输入错误，请重新输入");
                        }

                        Console.WriteLine(teamMessagerProxy.VoteFightInvitation(teamId, confirmResult));
                        break;
                    case "queryinvitation":
                        Console.WriteLine(invitationMessagerProxy.Query(playerId));
                        break;
                }


                input = Console.ReadLine();
            }
        }
    }
}