using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CultivarPre
{
    class GameItem
    {
       public char Category;
       public GameItem()
        {
        }

    }
    class Potion : GameItem
    {
        public Potion() : base()
        {
            Category = 'a';
        }
    }



    class Weapon : GameItem
    {
        public int SpriteSheetLocation;
        public int Base;
        public int Plus;
        public int[] Reqs;
        public float Overdrive;
        public float AttackDelay;
        public char WeaponType;
        public char[] OnWield;
        public char[] OnAttack;
        public Weapon()  : base()
        {
            Category = 'b';
        }

    }
}
