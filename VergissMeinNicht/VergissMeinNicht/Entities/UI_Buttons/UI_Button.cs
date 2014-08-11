#region Usings

using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

#endif
#endregion

namespace VergissMeinNicht.Entities.UI_Buttons
{
	public partial class UI_Button
	{
        public float thisStartPosY;


        public void CustomInitialize()
        {
            this.YVelocity = 10;

        }

        public void CustomActivity()
        {
            if (this.Y >= thisStartPosY + 7) this.YVelocity = -10;
            else if (this.Y <= thisStartPosY - 7) this.YVelocity = 10; // Hovern

        }

        public void CustomDestroy()
        {


        }

        public static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public void PositionUI_Button(float x, float y, float StartPos)
        {
            this.X = x;
            this.Y = y;
            thisStartPosY = StartPos;
        }
	}
}
