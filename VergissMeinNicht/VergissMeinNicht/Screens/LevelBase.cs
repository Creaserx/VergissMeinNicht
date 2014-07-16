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

        int CurrentLayer = 1;

        float CollisionHeightLayer1 = 150;
        float CollisionHeightLayer2 = 150 * SizeFirstDiff;
        float CollisionHeightLayer3 = 150 * SizeSecondDiff;

        float CollisionWidthLayer1 = 80;
        float CollisionWidthLayer2 = 80 * SizeFirstDiff;
        float CollisionWidthLayer3 = 80 * SizeSecondDiff;

        

		public virtual void CustomInitialize()
		{          
            TheodorChild Temp = new VergissMeinNicht.Entities.TheodorChild(ContentManagerName, false);
            Temp.Name = "TheodorChildInstance";
            PlatformerCharacterBase.updateinstance(Temp);
            Temp.AddToManagers(mLayer);

            // Create a rectangle
            Boden = new AxisAlignedRectangle();
            Boden.ScaleX = 1800;
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

            if (InputManager.Keyboard.KeyPushed(Keys.Up) && PlatformerCharacterBase.getInstance().Y < 210 && (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == 1 || PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == 0.9f || PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == 0.8f))
            {
                if (PlatformerCharacterBase.getInstance().DirectionFacing == PlatformerCharacterBase.LeftOrRight.Left)
                {
                    PlatformerCharacterBase.getInstance().SpriteInstance.CurrentChainName = "IdleLeft";
                }

                else
                {
                    PlatformerCharacterBase.getInstance().SpriteInstance.CurrentChainName = "IdleRight";
                }

                switch (CurrentLayer)
                {
                    case 1:
                        PlatformerCharacterBase.getInstance().SpriteInstance
                            .Tween("TextureScale")
                            .To(SizeFirstDiff)
                            .During(1)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().Collision
                            .Tween("Height")
                            .To(CollisionHeightLayer2)
                            .During(1)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().Collision
                            .Tween("Width")
                            .To(CollisionWidthLayer2)
                            .During(1)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        break;

                    case 2:
                        PlatformerCharacterBase.getInstance().SpriteInstance
                        .Tween("TextureScale")
                        .To(SizeSecondDiff)
                        .During(1)
                        .Using(
                            FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                            FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().Collision
                            .Tween("Height")
                            .To(CollisionHeightLayer3)
                            .During(1)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().Collision
                            .Tween("Width")
                            .To(CollisionWidthLayer3)
                            .During(1)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        break;

                    default:

                        break;
                }

                if (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == 1)
                {
                    CurrentLayer = 1;
                }

                else if (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == SizeFirstDiff)
                {
                    CurrentLayer = 2;
                }

                else if (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == SizeSecondDiff)
                {
                    CurrentLayer = 3;
                }
                               
                Boden
                    .Tween("Y")
                    .To(Boden.Y + 75)
                    .During(1)
                    .Using(
                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                        FlatRedBall.Glue.StateInterpolation.Easing.Out);               
            }



            if (InputManager.Keyboard.KeyPushed(Keys.Down) && PlatformerCharacterBase.getInstance().Y > 75)
            {
                if (PlatformerCharacterBase.getInstance().DirectionFacing == PlatformerCharacterBase.LeftOrRight.Left)
                {
                    PlatformerCharacterBase.getInstance().SpriteInstance.CurrentChainName = "IdleLeft";
                }

                else
                {
                    PlatformerCharacterBase.getInstance().SpriteInstance.CurrentChainName = "IdleRight";
                }

                switch (CurrentLayer)
                {
                    case 2:
                        PlatformerCharacterBase.getInstance().SpriteInstance
                        .Tween("TextureScale")
                        .To(1)
                        .During(1)
                        .Using(
                            FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                            FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().Collision
                            .Tween("Height")
                            .To(CollisionHeightLayer1)
                            .During(1)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().Collision
                            .Tween("Width")
                            .To(CollisionWidthLayer1)
                            .During(1)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        break;

                    case 3:
                        PlatformerCharacterBase.getInstance().SpriteInstance
                            .Tween("TextureScale")
                            .To(SizeFirstDiff)
                            .During(1)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().Collision
                            .Tween("Height")
                            .To(CollisionHeightLayer2)
                            .During(1)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().Collision
                            .Tween("Width")
                            .To(CollisionWidthLayer2)
                            .During(1)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        break;

                    default:

                        break;
                }

                if (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == 1)
                {
                    CurrentLayer = 1;
                }

                else if (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == SizeFirstDiff)
                {
                    CurrentLayer = 2;
                }

                else if (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == SizeSecondDiff)
                {
                    CurrentLayer = 3;
                }

                Boden
                    .Tween("Y")
                    .To(Boden.Y - 75)
                    .During(1)
                    .Using(
                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                        FlatRedBall.Glue.StateInterpolation.Easing.Out);
            }
        }

	}
}
