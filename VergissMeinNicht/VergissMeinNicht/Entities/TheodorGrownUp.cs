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
	public partial class TheodorGrownUp
	{
        double tempTime = Double.PositiveInfinity;
        double randomNumber;
        

		private void CustomInitialize()
		{
            if (Manager.CollisionsVisible) this.Collision.Visible = true;
            else this.Collision.Visible = false;
            base.DirectionFacing = LeftOrRight.Right;


            Microsoft.Xna.Framework.Media.MediaPlayer.Volume = 0;
            FlatRedBall.Audio.AudioManager.PlaySong(Erwachsen, true, false);
            SongStartTime = TimeManager.CurrentTime;
		}

		private void CustomActivity()
		{
            AnimationActivity();
            if (this.SpriteInstance.CurrentChainName != "GU_IdleRightPocketIn" && this.SpriteInstance.CurrentChainName != "GU_IdleRightPocket" && this.SpriteInstance.CurrentChainName != "GU_IdleRight"
                && this.SpriteInstance.CurrentChainName != "GU_IdleLeftPocketIn" && this.SpriteInstance.CurrentChainName != "GU_IdleLeftPocket" && this.SpriteInstance.CurrentChainName != "GU_IdleLeft")
            {
                tempTime = Double.PositiveInfinity;
            }

            //--Music Management
            Music_FadeIn(1);
            if (TimeManager.SecondsSince(SongStartTime) >= 40)
            {
                Microsoft.Xna.Framework.Media.MediaPlayer.Volume = 1 - ((float)TimeManager.SecondsSince(SongStartTime) - 40f) / 5f;
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
                if (HorizontalRatio > 0 || (Manager.isSwitching && base.DirectionFacing == LeftOrRight.Right))
                {
                    this.SpriteInstance.CurrentChainName = "GU_WalkRight";
                }
                else if (HorizontalRatio < 0 || (Manager.isSwitching && base.DirectionFacing == LeftOrRight.Left))
                {
                    this.SpriteInstance.CurrentChainName = "GU_WalkLeft";
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
            
        }

        private void IdleAnimationRight()
        {
            if (Double.IsPositiveInfinity(tempTime))
            {
                this.SpriteInstance.CurrentChainName = "GU_IdleRight";
                tempTime = TimeManager.CurrentTime;
                randomNumber = FlatRedBallServices.Random.Next(2, 5);
                Manager.randomz = randomNumber;
            }

            if (this.SpriteInstance.CurrentChainName == "GU_IdleRightPocketIn" && this.SpriteInstance.CurrentFrameIndex == 5)
            {
                this.SpriteInstance.CurrentChainName = "GU_IdleRightPocket";
            }
            else if (TimeManager.SecondsSince(tempTime) >= randomNumber && this.SpriteInstance.CurrentChainName != "GU_IdleRightPocket")
            {
                this.SpriteInstance.CurrentChainName = "GU_IdleRightPocketIn";
            }
        }

        private void IdleAnimationLeft()
        {
            if (Double.IsPositiveInfinity(tempTime))
            {
                this.SpriteInstance.CurrentChainName = "GU_IdleLeft";
                tempTime = TimeManager.CurrentTime;
                randomNumber = FlatRedBallServices.Random.Next(2, 5);
                Manager.randomz = randomNumber;
            }

            if (this.SpriteInstance.CurrentChainName == "GU_IdleLeftPocketIn" && this.SpriteInstance.CurrentFrameIndex == 5)
            {
                this.SpriteInstance.CurrentChainName = "GU_IdleLeftPocket";
            }
            else if (TimeManager.SecondsSince(tempTime) >= randomNumber && this.SpriteInstance.CurrentChainName != "GU_IdleLeftPocket")
            {
                this.SpriteInstance.CurrentChainName = "GU_IdleLeftPocketIn";
            }
        }

	}
}
