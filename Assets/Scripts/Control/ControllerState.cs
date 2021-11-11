using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Control
{    
    public class ControllerState : Singleton<ControllerState>
    {
        // Includes threshold
        public float stickPositionX;
        public float stickPositionY;
        
        public float stickPositionXRaw;
        public float stickPositionYRaw;
        
        public ControllerMode currentMode = ControllerMode.PLAYER;

        public bool IsInNeutralPosition()
        {
            return (Math.Abs(stickPositionXRaw) + Math.Abs(stickPositionYRaw)) < 0.00001f;
        }
    }
   

    public enum ControllerMode
    {
        PLAYER,
        TURRET
    }
}
