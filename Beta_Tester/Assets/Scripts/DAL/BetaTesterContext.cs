using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DAL
{
    public class BetaTesterContext
    {
        public static Sql<User> User = new Sql<User>();
        public static Sql<Phase> Phase = new Sql<Phase>();
    }
}
