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
using StateInterpolationPlugin;
using FlatRedBall.Screens;


#endif
#endregion

namespace VergissMeinNicht.Entities
{
	public partial class TheodorChild
	{
        double tempTime = Double.PositiveInfinity;
        double randomNumber;

		private void CustomInitialize()
		{
            if (Manager.CollisionsVisible) this.Collision.Visible = true;
            else this.Collision.Visible = false;

            FlatRedBall.Audio.AudioManager.PlaySong(Kind, true, false);
            SongStartTime = TimeManager.CurrentTime;
		}

		private void CustomActivity()
		{            
            AnimationActivity();
            if (this.SpriteInstance.CurrentChainName != "IdleRightPocketIn" && this.SpriteInstance.CurrentChainName != "IdleRightPocket" && this.SpriteInstance.CurrentChainName != "IdleRight"
                && this.SpriteInstance.CurrentChainName != "IdleLeftPocketIn" && this.SpriteInstance.CurrentChainName != "IdleLeftPocket" && this.SpriteInstance.CurrentChainName != "IdleLeft")
            {
                tempTime = Double.PositiveInfinity;
            }

            //--Music Management
            Music_FadeIn(1);
            if (TimeManager.SecondsSince(SongStartTime) >= 28)
            {
                Microsoft.Xna.Framework.Media.MediaPlayer.Volume = 1 - ((float)TimeManager.SecondsSince(SongStartTime) - 28f) / 5f;
                if (Microsoft.Xna.Framework.Media.MediaPlayer.Volume == 0)
                {
                    SongStartTime = TimeManager.CurrentTime;
                    OneTimeFade = true;
                }
            }

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
            if (HorizontalRatio > 0)
            {
                base.DirectionFacing = LeftOrRight.Right;
            }
            else if (HorizontalRatio < 0)
            {
                base.DirectionFacing = LeftOrRight.Left;
            }
            // don't do anything if it's 0, just leave it to what it was before

            // check whether in air or on ground
            if (IsOnGround)
            {
                if (!Manager.isSwitching)
                {
                    switch (Manager.CurrentLayer)
                    {
                        case 1:
                            this.SpriteInstance.TextureScale = 0.5f;
                            break;

                        case 2:
                            this.SpriteInstance.TextureScale = Manager.SizeFirstDiff;
                            break;

                        case 3:
                            this.SpriteInstance.TextureScale = Manager.SizeSecondDiff;
                            break;

                        default:
                            break;
                    }
                }
                if (HorizontalRatio > 0 || (Manager.isSwitching && base.DirectionFacing == LeftOrRight.Right))
                {
                    this.SpriteInstance.CurrentChainName = "WalkRight";
                }
                else if (HorizontalRatio < 0 || (Manager.isSwitching && base.DirectionFacing == LeftOrRight.Left))
                {
                    this.SpriteInstance.CurrentChainName = "WalkLeft";
                }
                else
                {
                    if (DirectionFacing == LeftOrRight.Right)
                    {
                        IdleAnimationRight();               
                        
                    }
                    else
                    {
                        IdleAnimationLeft();
                    }
                }
            }
            else // in the air
            {
                if (!Manager.isSwitching)
                {
                    switch (Manager.CurrentLayer)
                    {
                        case 1:
                            this.SpriteInstance.TextureScale = 0.5f * 1.2f;
                            break;

                        case 2:
                            this.SpriteInstance.TextureScale = Manager.SizeFirstDiff * 1.2f;
                            break;

                        case 3:
                            this.SpriteInstance.TextureScale = Manager.SizeSecondDiff * 1.2f;
                            break;

                        default:
                            break;
                    }
                }
                if (DirectionFacing == LeftOrRight.Left && Manager.isSwitching)
                {
                    this.SpriteInstance.CurrentChainName = "WalkLeft";
                }
                else if (DirectionFacing == LeftOrRight.Right && Manager.isSwitching)
                {
                    this.SpriteInstance.CurrentChainName = "WalkRight";
                }
                else if (DirectionFacing == LeftOrRight.Left && !Manager.CharacterFallingInHole)
                {

                    this.SpriteInstance.CurrentChainName = "JumpLeft";
                    
                }
                else if (DirectionFacing == LeftOrRight.Right && !Manager.CharacterFallingInHole)
                {
                    this.SpriteInstance.CurrentChainName = "JumpRight";                  

                }
                
            }
        }

        private void IdleAnimationRight()
        {            
            if (Double.IsPositiveInfinity(tempTime))
            {
                this.SpriteInstance.CurrentChainName = "IdleRight";
                tempTime = TimeManager.CurrentTime;
                randomNumber = FlatRedBallServices.Random.Next(2, 5);
                Manager.randomz = randomNumber;
            }

            if (this.SpriteInstance.CurrentChainName == "IdleRightPocketIn" && this.SpriteInstance.CurrentFrameIndex == 6)
            {
                this.SpriteInstance.CurrentChainName = "IdleRightPocket";
            }
            else if (TimeManager.SecondsSince(tempTime) >= randomNumber && this.SpriteInstance.CurrentChainName != "IdleRightPocket")
            {
                this.SpriteInstance.CurrentChainName = "IdleRightPocketIn";
            }
        }

        private void IdleAnimationLeft()
        {
            if (Double.IsPositiveInfinity(tempTime))
            {
                this.SpriteInstance.CurrentChainName = "IdleLeft";
                tempTime = TimeManager.CurrentTime;
                randomNumber = FlatRedBallServices.Random.Next(2, 5);
                Manager.randomz = randomNumber;
            }

            if (this.SpriteInstance.CurrentChainName == "IdleLeftPocketIn" && this.SpriteInstance.CurrentFrameIndex == 6)
            {
                this.SpriteInstance.CurrentChainName = "IdleLeftPocket";
            }
            else if (TimeManager.SecondsSince(tempTime) >= randomNumber && this.SpriteInstance.CurrentChainName != "IdleLeftPocket")
            {
                this.SpriteInstance.CurrentChainName = "IdleLeftPocketIn";
            }
        }
	}
}
