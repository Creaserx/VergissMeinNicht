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

namespace VergissMeinNicht.Entities.Enemies
{
	public partial class JackInTheBox
	{
		private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{
            AnimationActivity();

		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void AnimationActivity()
        {
            if (TheoInRange())
            {
                this.SpriteInstance.CurrentChainName = "JitB_Attack";
            }            
            else
            {
                this.SpriteInstance.CurrentChainName = "JitB_Idle";
            }
        }

        private bool TheoInRange()
        {
            if (PlatformerCharacterBase.getInstance().X >= this.X - 180 
                && PlatformerCharacterBase.getInstance().X <= this.X + 55
                && PlatformerCharacterBase.getInstance().Y <= this.Y + 40)
            {
                return true;
            }
            else return false;
        }
	}
}
