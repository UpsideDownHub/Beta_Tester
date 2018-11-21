using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DAL
{
    public class UserPhaseFav
    {
        [DisplayName("Key")]
        public int UserPhaseFavId { get; set; }
        public int PhaseId { get; set; }
        public int UserId { get; set; }
    }
}
