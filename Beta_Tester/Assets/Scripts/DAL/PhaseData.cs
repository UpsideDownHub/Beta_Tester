using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DAL
{
    public class PhaseData
    {
         public int Block { get; set; }
         public int? Trap { get; set;}
         public bool HFlipBlock { get; set;}
         public bool VFlipBlock { get; set;}
         public bool HFlipTrap { get; set;}
         public bool VFlipTrap { get; set;}
    }
}
