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

        public int CurrentLayer = 1;
        public bool isSwitching = false;
        public bool DisableLayers = false;
        public bool DisableLayer3 = false;

        bool DebuggerOn = true;

        float CollisionHeightLayer1;
        float CollisionHeightLayer2;
        float CollisionHeightLayer3;

        float CollisionWidthLayer1;
        float CollisionWidthLayer2;
        float CollisionWidthLayer3;
       

		public virtual void CustomInitialize()
		{
            CollisionsVisible = false;  // Collision Visibility An/Aus

            DisableLayers = false;      // Erstmal Layer aktivieren
            DisableLayer3 = false;      // Layer 3 aktivieren

            //Theodor erstellen
            TheodorChild Temp = new VergissMeinNicht.Entities.TheodorChild(ContentManagerName, false);
            Temp.Name = "TheodorChildInstance";
            PlatformerCharacterBase.updateinstance(Temp);
            Temp.AddToManagers(mLayer);
             
            CollisionHeightLayer1 = PlatformerCharacterBase.getInstance().Collision.Height * 1.1f;
            CollisionHeightLayer2 = PlatformerCharacterBase.getInstance().Collision.Height * 1.0f;
            CollisionHeightLayer3 = PlatformerCharacterBase.getInstance().Collision.Height * 0.9f;
            PlatformerCharacterBase.getInstance().Collision.Height = CollisionHeightLayer1;

            CollisionWidthLayer1 = PlatformerCharacterBase.getInstance().Collision.Width * 1.1f;
            CollisionWidthLayer2 = PlatformerCharacterBase.getInstance().Collision.Width * 1.0f;
            CollisionWidthLayer3 = PlatformerCharacterBase.getInstance().Collision.Width * 0.9f;
            PlatformerCharacterBase.getInstance().Collision.Width = CollisionWidthLayer1;

            // Create a rectangle
            Boden = new AxisAlignedRectangle();
            Boden.ScaleX = 1800;
            Boden.ScaleY = 50;
            Boden.Y = -50;
            this.SolidCollisions.AxisAlignedRectangles.Add(Boden);  // Add it to the ShapeCollection so the player can collide against it
            

            PlatformerCharacterBase.getInstance().Y = 75;       // Make the character appear on top of the rectangle


            if (CollisionsVisible) CollisionVisibilityOn();
		}

		public virtual void CustomActivity(bool firstTimeCalled)
		{
            //Theodor Movement      
            PlatformerCharacterBase.getInstance().DetermineMovementValues();        
            CameraMovement();         

            //Theodor Collisions
            PlatformerCharacterBase.getInstance().CollideAgainst(SolidCollisions);

            LayerOn();
            LayerManagement();


            if (!IsPaused) PlatformerCharacterBase.getInstance().Activity();            
            
            PauseGame();
  

            // "Cheats"
            DeveloperActivity();
		}

        public virtual void CustomDestroy()
        {
            PlatformerCharacterBase.updateinstance(null);
		}

        public void CollisionVisibilityOn() 
        {
            SolidCollisions.AddToManagers(); // Add the ShapeColleciton to the ShapeManager so it's visible

            PlatformerCharacterBase.getInstance().Collision.Visible = true;
        }

        public virtual void DeveloperActivity()
        {
            // Z  -- Zoolevel
            if (InputManager.Keyboard.KeyPushed(Keys.Z)) MoveToScreen(typeof(ZooLevel).FullName);

            // R  -- Reset Character
            if (InputManager.Keyboard.KeyPushed(Keys.R))
            {
                DisableLayers = true;
                Boden.Y = -50;
                CurrentLayer = 1;
                PlatformerCharacterBase.getInstance().Y = 75;
                PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale = 1.1F;
                //resetCollisionSize();
                if (!PlatformerCharacterBase.isChild())
                {
                    DisableLayers = false;
                    TheodorChild Temp = new TheodorChild();
                    Temp.X = PlatformerCharacterBase.getInstance().X;
                    Temp.Y = PlatformerCharacterBase.getInstance().Y;
                    PlatformerCharacterBase.updateinstance(Temp);
                }
            }

            //------------------------------------------------------
            //Zeigt Debugger Werte an 
            if (InputManager.Keyboard.KeyPushed(Keys.F3))
            {
                if (!DebuggerOn) DebuggerOn = true;
            }

            if (InputManager.Keyboard.KeyPushed(Keys.F4))
            {
                if (DebuggerOn) DebuggerOn = false;
            }

            if (!DebuggerOn)
            {
                FlatRedBall.Debugging.Debugger.Write("");
            }
            if ( DebuggerOn)
            {
                string resultStringY = "Character Y: " + PlatformerCharacterBase.getInstance().Y;
                string resultStringX = "Character X: " + PlatformerCharacterBase.getInstance().X;
                string resultStringCollisionH = "Collision H: " + PlatformerCharacterBase.getInstance().Collision.Height;
                string resultStringCollisionW = "Collision W: " + PlatformerCharacterBase.getInstance().Collision.Width;
                string resultStringLayer = "CurrentLayer:" + CurrentLayer.ToString();
                string resultStringL3 = "Layer3Disable:" + DisableLayer3.ToString();
                string resultStringLa = "LayersDisable:" + DisableLayers.ToString();
                string resultStringSwitch = "isSwitching:" + isSwitching.ToString();
                FlatRedBall.Debugging.Debugger.Write(resultStringX + "\n" + resultStringY + "\n" + resultStringCollisionH + "\n" + resultStringCollisionW + "\n" + resultStringLayer + "\n" + resultStringL3 + "\n" + resultStringLa + "\n" + resultStringSwitch);
            }
        }

        /*public void resetCollisionSize()
        {
            PlatformerCharacterBase.getInstance().Collision.Height = CollisionHeightLayer1;
            PlatformerCharacterBase.getInstance().Collision.Width = CollisionWidthLayer1;
        }*/

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
            SpriteManager.Camera.Y = 250; 
        }

        void LayerOn()
        {
            if (isSwitching)
            {
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Left);
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Right);
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Space);  
            }
            
            if (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == 1.1f)
            {
                CurrentLayer = 1;
                isSwitching = false;
            }

            else if (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == SizeFirstDiff)
            {
                CurrentLayer = 2;
                isSwitching = false;
            }

            else if (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == SizeSecondDiff)
            {
                CurrentLayer = 3;
                isSwitching = false;
            }
        }
        

        void LayerManagement()
        {
            // Workaround zum Ebenenwechsel. Das komplette Tweening bedarf noch "etwas" Optimierung!


            if (DisableLayer3 && CurrentLayer == 2) DisableLayers=true; //WA

            if (!DisableLayers)
            {
                
                
                if (InputManager.Keyboard.KeyPushed(Keys.Up) && PlatformerCharacterBase.getInstance().Y < 210 && !isSwitching)
                {
                    isSwitching = true;

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

                                Boden                                   //WA
                                    .Tween("Y")
                                    .To(Boden.Y + 75)
                                    .During(1)
                                    .Using(
                                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                        FlatRedBall.Glue.StateInterpolation.Easing.Out);
                            
                            break;

                        case 2:
                            if (!DisableLayer3)
                            {
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

                                Boden                                                       //WA
                                   .Tween("Y")
                                   .To(Boden.Y + 75)
                                   .During(1)
                                   .Using(
                                       FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                       FlatRedBall.Glue.StateInterpolation.Easing.Out);
                            }
                            
                            break;

                        default:

                            break;
                    }

                    
                    
                    
                }
                
                DisableLayers = false;  //WA
            }



            if (InputManager.Keyboard.KeyPushed(Keys.Down) && PlatformerCharacterBase.getInstance().Y > 75 && !isSwitching)
            {
                isSwitching = true;
                DisableLayers = false; //WA
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
                        .To(1.1f)
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
