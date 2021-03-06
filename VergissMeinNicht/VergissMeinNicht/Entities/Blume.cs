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

namespace VergissMeinNicht.Entities
{
	public partial class Blume
	{
        public float thisStartPosY;
        public float BlumeVelocity = 10;

		private void CustomInitialize()
		{
            this.YVelocity = BlumeVelocity;
            Manager.FlowerDestroyed = false;
		}

		private void CustomActivity()
		{
            // Floating (Vertikale Bewegung)
            if (this.Y >= thisStartPosY + 15) this.YVelocity = -BlumeVelocity;
            else if (this.Y <= thisStartPosY - 15) this.YVelocity = BlumeVelocity;

            // Visibility
            if (PlatformerCharacterBase.isChild()) this.Visible = true;
            else this.Visible = false;
            this.Collision.Visible = false;
		}

		private void CustomDestroy()
		{
            Manager.CheckPointX = this.X;
            Manager.FlowerDestroyed = true;

		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public void PositionBlume(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
	}
}
