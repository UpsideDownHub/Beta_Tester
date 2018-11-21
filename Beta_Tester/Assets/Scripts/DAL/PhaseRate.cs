using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DAL
{
    public class PhaseRate
    {
        [DisplayName("Key")]
        public int PhaseRateId { get; set; }
        public int PhaseId { get; set; }
        public int UserId { get; set; }
        public int Rate { get; set; }
    }
}
