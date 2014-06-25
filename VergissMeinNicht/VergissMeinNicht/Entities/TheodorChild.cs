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
	public partial class TheodorChild
	{
        private void AnimationActivity()
        {
            // check facing direction
            if (HorizontalRatio > 0)
            {
                this.DirectionFacing = LeftOrRight.Right;
            }
            else if (HorizontalRatio < 0)
            {
                this.DirectionFacing = LeftOrRight.Left;
            }
            // don't do anything if it's 0, just leave it to what it was before

            // check whether in air or on ground
            if (IsOnGround)
              {
                  if (HorizontalRatio > 0)
                  {
                      this.SpriteInstance.CurrentChainName = "WalkRight";
                  }
                  else if (HorizontalRatio < 0)
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

        public enum LeftOrRight
        {
            Left,
            Right
        }

        public LeftOrRight DirectionFacing
        {
            get;
            private set;
        }

		private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{
            AnimationActivity();
            //SwitchLayer();
		}        

        /*private void SwitchLayer()
        {
            float SpriteDiff = 0.001f;
            float CollisionHeightDiff = 0.15f;
            float CollisionWidthDiff = 0.08f;

            if (this.SpriteInstance.TextureScale > 0.8)
            {
                switch (DirectionFacing)
                {
                    case LeftOrRight.Left:
                        this.SpriteInstance.CurrentChainName = "IdleLeft";
                        this.SpriteInstance.AnimationSpeed -= SpriteDiff;
                        this.SpriteInstance.TextureScale -= SpriteDiff;
                        base.Collision.Height -= CollisionHeightDiff;
                        base.Collision.Width -= CollisionWidthDiff;
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Up);
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Down);
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Left);
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Right);
                        break;

                    case LeftOrRight.Right:
                        this.SpriteInstance.CurrentChainName = "IdleRight";
                        this.SpriteInstance.AnimationSpeed -= SpriteDiff;
                        this.SpriteInstance.TextureScale -= SpriteDiff;
                        base.Collision.Height -= CollisionHeightDiff;
                        base.Collision.Width -= CollisionWidthDiff;
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Up);
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Down);
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Left);
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Right);
                        break;
                }
            }
            
        }*/

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
