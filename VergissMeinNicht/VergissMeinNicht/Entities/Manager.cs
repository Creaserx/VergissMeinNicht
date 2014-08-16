using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VergissMeinNicht.Entities
{
    public static class Manager
    {
        public static bool isSwitching = false;
        public static bool isSwitchingGhost = false;
        public static double GhostSpawnTime = Double.PositiveInfinity;
        public static double LevelStartTime;
        public static bool FlowerDestroyed;
        public static bool CollisionsVisible;

        public static double randomz;

        public static int LoadLevel;

        public static bool CharacterFallingInHole = false;

        public static bool MusicOn = true;
        public static bool SoundOn = true;
        

        //Keys Disablen
        public static bool EnableKey_Space = true;
        public static bool EnableKey_Down = true;
        public static bool EnableKey_Up = true;
        public static bool EnableKey_E = true;
        public static bool EnableKey_Left = true;
        public static bool EnableKey_Right = true;
    }
}
