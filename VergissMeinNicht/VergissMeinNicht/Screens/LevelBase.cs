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
using StateInterpolationPlugin;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using VergissMeinNicht.Entities;

#endif
#endregion

namespace VergissMeinNicht.Screens
{
	public partial class LevelBase
	{
        bool Waiter = false;
        public AxisAlignedRectangle Boden;

		public virtual void CustomInitialize()
		{          
            TheodorChild Temp = new VergissMeinNicht.Entities.TheodorChild(ContentManagerName, false);
            Temp.Name = "TheodorChildInstance";
            PlatformerCharacterBase.updateinstance(Temp);
            Temp.AddToManagers(mLayer);

            // Create a rectangle
            Boden = new AxisAlignedRectangle();
            Boden.ScaleX = 800;
            Boden.ScaleY = 50;
            Boden.Y = -50; 

            // Add it to the ShapeCollection so the player can collide against it
            this.SolidCollisions.AxisAlignedRectangles.Add(Boden);
            

            // Add the ShapeColleciton to the ShapeManager so it's visible
            SolidCollisions.AddToManagers();


            // Make the character appear on top of the rectangle:
            PlatformerCharacterBase.getInstance().Y = 75;
		}

		public virtual void CustomActivity(bool firstTimeCalled)
		{
            //Theodor Movement      
            PlatformerCharacterBase.getInstance().DetermineMovementValues();        //Collision muss extra aufgerufen werden
            CameraMovement();         

            //Theodor Collisions
            PlatformerCharacterBase.getInstance().CollideAgainst(SolidCollisions);

            LayerManagement();            

            if (!IsPaused)
            {
                PlatformerCharacterBase.getInstance().Activity();
            }
            PauseGame();
            
            //Zeigt character Y an
            string resultString = "Character Y: " + PlatformerCharacterBase.getInstance().Y;
            FlatRedBall.Debugging.Debugger.Write(resultString);
                    
		}

        public virtual void CustomDestroy()
        {
            PlatformerCharacterBase.updateinstance(null);
		}

        void PauseGame()
        {
            if (InputManager.Keyboard.KeyReleased(Keys.Escape))
            {
                Waiter = false;
            }

            if (Waiter) return;

            if (InputManager.Keyboard.KeyPushed(Keys.Escape))
            {
                Waiter = true;

                if (IsPaused)
                {
                    UnpauseThisScreen();
                    PauseButton.Visible = false;
                }
                else
                {
                    PauseThisScreen();
                    PauseButton.Visible = true;
                }
            }
        }

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        void CameraMovement()
        {
            //Camera Movement following Theodor
            SpriteManager.Camera.XVelocity = PlatformerCharacterBase.getInstance().X - SpriteManager.Camera.X;
            SpriteManager.Camera.YVelocity = PlatformerCharacterBase.getInstance().Y - SpriteManager.Camera.Y;

        }

        void LayerManagement()
        {
            float SpriteDiff = 0.001f;
            float CollisionHeightDiff = 0.15f;
            float CollisionWidthDiff = 0.08f;

            if (InputManager.Keyboard.KeyPushed(Keys.Up) && PlatformerCharacterBase.getInstance().Y < 225 && !IsPaused)
            {
                switch (PlatformerCharacterBase.getInstance().DirectionFacing)
                {                        
                    case PlatformerCharacterBase.LeftOrRight.Left:
                        PlatformerCharacterBase.getInstance().SpriteInstance.CurrentChainName = "IdleLeft";
                        PlatformerCharacterBase.getInstance().SpriteInstance.AnimationSpeed -= SpriteDiff;
                        PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale -= SpriteDiff;
                        PlatformerCharacterBase.getInstance().Collision.Height -= CollisionHeightDiff;
                        PlatformerCharacterBase.getInstance().Collision.Width -= CollisionWidthDiff;
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Up);
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Down);
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Left);
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Right);
                        break;

                    case PlatformerCharacterBase.LeftOrRight.Right:
                        PlatformerCharacterBase.getInstance().SpriteInstance.CurrentChainName = "IdleRight";
                        PlatformerCharacterBase.getInstance().SpriteInstance.AnimationSpeed -= SpriteDiff;
                        PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale -= SpriteDiff;
                        PlatformerCharacterBase.getInstance().Collision.Height -= CollisionHeightDiff;
                        PlatformerCharacterBase.getInstance().Collision.Width -= CollisionWidthDiff;
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Up);
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Down);
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Left);
                        InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Right);
                        break;
                }

                Boden
                    .Tween("Y")
                    .To(Boden.Y + 75)
                    .During(1)
                    .Using(
                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                        FlatRedBall.Glue.StateInterpolation.Easing.Out);               
            }

            if (InputManager.Keyboard.KeyPushed(Keys.Down) && PlatformerCharacterBase.getInstance().Y > 75 && !IsPaused)
            {
                Boden
                    .Tween("Y")
                    .To(Boden.Y - 75)
                    .During(1)
                    .Using(
                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                        FlatRedBall.Glue.StateInterpolation.Easing.Out);

                PlatformerCharacterBase.getInstance()
                    .Tween("Y")
                    .To(PlatformerCharacterBase.getInstance().Y - 75)
                    .During(1)
                    .Using(
                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                        FlatRedBall.Glue.StateInterpolation.Easing.Out);            
            }
        }

	}
}
