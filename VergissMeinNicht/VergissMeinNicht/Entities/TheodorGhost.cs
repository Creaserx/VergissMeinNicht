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
	public partial class TheodorGhost
	{    
		private void CustomInitialize()
		{
            InputEnabled = false;

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
            // check facing direction
            if (moveright)
            {
                base.DirectionFacing = LeftOrRight.Right;
            }
            else if (moveleft)
            {
                base.DirectionFacing = LeftOrRight.Left;
            }
            // don't do anything if it's 0, just leave it to what it was before

            // check whether in air or on ground
            if (IsOnGround)
            {
                if (moveright)
                {
                    this.SpriteInstance.CurrentChainName = "WalkRight";
                }
                else if (moveleft)
                {
                    this.SpriteInstance.CurrentChainName = "WalkLeft";
                }
                else
                {
                    if (DirectionFacing == LeftOrRight.Right)
                    {
                        this.SpriteInstance.CurrentChainName = "IdleRight";
                    }
                    else
                    {
                        this.SpriteInstance.CurrentChainName = "IdleLeft";
                    }
                }
            }
            else // in the air
            {
                if (DirectionFacing == LeftOrRight.Left)
                {
                    this.SpriteInstance.CurrentChainName = "JumpLeft";
                }
                else
                {
                    this.SpriteInstance.CurrentChainName = "JumpRight";
                }
            }
        }
	}
}
