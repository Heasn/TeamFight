using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TeamFight.Core.Cache;
using TeamFight.Core.Character;
using TeamFight.Core.Database;

namespace TeamFight.WebApi.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        public Models.PlayerPropertiesModel Login(int charId)
        {
            if (OnlinePlayersCache.Instance.IsPlayerOnline(charId))
                return null;

            var player = DatabaseHelper.LoadPlayer(charId);

            if (OnlinePlayersCache.Instance.AddPlayer(player))
            {
                return new Models.PlayerPropertiesModel
                {
                    Id = player.Id,
                    Name = player.Name,
                    Gender = player.Gender != GameGender.Female,
                    Level = player.Level,
                    PhysicalStrength = player.PhysicalStrength,
                    Endurance = player.Endurance
                };
            }

            return null;
        }
    }
}