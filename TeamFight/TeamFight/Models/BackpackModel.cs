using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamFight.Models
{
    public class BackpackModel
    {
        public int Id { get; set; }

        public int CharacterId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}