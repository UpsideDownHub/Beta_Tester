using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Shared;

namespace Assets.Scripts.Shared
{
    class PositionDiffernce
    {
        public Positions Positions { get; set; }
        public int Diference { get; set; }
    }

    static class PositionsExtentions
    {
        static public Vector3 CorrectPositions(this Vector3 position)
        {
            var yPosition = Mathf.RoundToInt(position.y);
            var diference = new List<PositionDiffernce>
            {
                new PositionDiffernce
                {
                    Positions = Positions.Top,
                    Diference = Mathf.Abs((int)Positions.Top - yPosition),
                },
                new PositionDiffernce
                {
                    Positions = Positions.Middle,
                    Diference = Mathf.Abs((int)Positions.Middle - yPosition)
                },
                new PositionDiffernce
                {
                    Positions = Positions.Bottom,
                    Diference = Mathf.Abs((int)Positions.Bottom - yPosition)
                },
            };
            var a = (int)diference.OrderBy(x => x.Diference).First().Positions;
            return new Vector3(position.x, a);
        }
        static public Positions SearchNewPath(this Positions position)
        {
            switch (position)
            {
                case Positions.Top:
                    return Positions.Middle;
                case Positions.Bottom:
                    return Positions.Middle;
                default:
                    var val = UnityEngine.Random.Range(0,2);
                    return val % 2 == 0 ? Positions.Top : Positions.Bottom;
            }
        }
    }
}
