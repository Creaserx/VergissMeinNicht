﻿using System;
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
    }
}
