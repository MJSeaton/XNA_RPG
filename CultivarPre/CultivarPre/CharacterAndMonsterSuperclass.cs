using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CultivarPre
{
    class CharacterAndMonsterSuperclass
    {
        public int[] stats;// 0 = EVADE (dodge chance is .10 * EV -Incoming attack level, 1= VIG (STR and provides a %HP boost at several levels, 2=DEX, wielding quick or complex objects, % boost to dex, 3=WIS,4=MAXHP,5=CURHP, 6=ATTACKCOOLDOWN 
        public int speciesID;
        public int typeID;
        public Vector2 location;
        public CharacterAndMonsterSuperclass(int posX, int posY)
        {
            stats = new int[7];
            location.X = posX;
            location.Y = posY;
        }
    }
    ////
    /////
    class PlayerCharacter : CharacterAndMonsterSuperclass
    {
        public int[] skills; //0 Swords & Daggers, 1 Axes, 2 Polearms, 3 Heavy Weapons, 4 Projectile Weapons, 5 Shapeshifting, 6 Elemental Magic, 7 Nature Magic,
        PlayerCharacter(int choice, int POS_X, int POS_Y)
            : base(POS_X, POS_Y)
        {
            skills = new int[8];
            for (int i = 0; i < 8; i++)
            {
                skills[i] = 5;
            }
            if (choice > 7)
            {
                choice = 7;
            }
            skills[choice] += 10;
        }

    }
    
}
