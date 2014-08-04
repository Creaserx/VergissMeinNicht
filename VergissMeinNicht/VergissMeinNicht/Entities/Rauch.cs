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
	public partial class Rauch
	{

		private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{
            PlatformerCharacterBase.getInstance().Collision.CollideAgainstMove(this.Collision, 0, 1); //Collision Rauch-Character
            AnimationActivity();
		}

		private void CustomDestroy()
		{
            

		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public void PositionRauch(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        private void AnimationActivity()
        {
            this.SpriteInstance.CurrentChainName = "Wiggle";
        }
    }
}
