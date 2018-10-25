using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Jump
{
    static class JumpExtensions
    {
        public static void Set(this List<Jump> me, Vector3 position, Vector3 force, float jumpSpeed, bool up = false) //bool direction, bool sense,
        {
            me.Add(new Jump
            {
                Position = position,
                Force = force,
                JumpSpeed = jumpSpeed,
                //Direction = direction,
                //Sense = sense,
                Up = up
            });
        }
    }
}
