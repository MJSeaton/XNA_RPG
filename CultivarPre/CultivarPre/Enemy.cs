using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CultivarPre
{
    class Enemy
    {
        public int EnemyID;
        private int CURHP;
        private int MAXHP;
        private int DODGE;
   
        public int[] location = new int[2];

        public void ChangeHP(int NewHP)
        {
            MAXHP = NewHP;
            CURHP = MAXHP;
        }
        
        public int getCURHP(){

            return (CURHP);
        }
    }
}
