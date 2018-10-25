using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Jump
{
    public class Jump
    {
        public Vector3 Position { get; set; }
        public Vector3 Force { get; set; }
        public float JumpSpeed { get; set; }
        //public bool Direction { get; set; } // true = right | false = left
        //public bool Sense { get; set; }
        public bool Up { get; set; }
    }
}
