﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Control
{    
    class ControllerState : Singleton<ControllerState>
    {
        // Includes threshold
        public float stickPositionX;
        public float stickPositionY;
        

        public float stickPositionXRaw;
        public float stickPositionYRaw;
        
        public ControllerMode currentMode = ControllerMode.PLAYER;
        
    }

    enum ControllerMode
    {
        PLAYER,
        TURRET
    }
}
