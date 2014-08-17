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
        public static int CurrentLevel = 0;

        public static bool CharacterFallingInHole = false;
        public static float CheckPointX = 0;
        public static float CheckPointY = 0;

        public static bool MusicOn = true;
        public static bool SoundOn = true;

        public static bool CharacterReset = false;
        
        //Keys Disablen
        public static bool EnableKey_Space = true;
        public static bool EnableKey_Down = true;
        public static bool EnableKey_Up = true;
        public static bool EnableKey_E = true;
        public static bool EnableKey_Left = true;
        public static bool EnableKey_Right = true;



        public static void SetCheckPoint(float x, float y)
        {
            CheckPointX = x;
            CheckPointY = y;
        }

        public static void GoToCheckPoint(float x, float y)
        {
            PlatformerCharacterBase.getInstance().X = x;
            PlatformerCharacterBase.getInstance().Y = y;
        }
    }
}
