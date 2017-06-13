using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamFight.Models
{
    public class CharacterModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public uint Fatigue { get; set; }
    }
}