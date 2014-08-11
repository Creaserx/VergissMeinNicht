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
using FlatRedBall.Graphics;

#endif
#endregion

namespace VergissMeinNicht.Screens
{
	public partial class LevelBase
	{
        bool Waiter = false;
        public AxisAlignedRectangle Boden;     

        public static int CurrentLayer = 1;
        //public static bool isSwitching = false;
        public bool DisableLayers = false;
        public bool DisableLayerBack = false;

        bool DebuggerOn = true;

        public static float CollisionHeightLayerFront;
        public static float CollisionHeightLayerMid;
        public static float CollisionHeightLayerBack;

        public static float CollisionWidthLayerFront;
        public static float CollisionWidthLayerMid;
        public static float CollisionWidthLayerBack;

        public static Layer LayerBack  = SpriteManager.AddLayer();
        public static Layer LayerMid   = SpriteManager.AddLayer();
        public static Layer LayerFront = SpriteManager.AddLayer();

        public bool DoorOpen = false;



        

        //------INITIALIZE----------------------------------------------------------------
		public virtual void CustomInitialize()
		{
            EnableKeys();    
            FixLayers();


            Manager.LevelStartTime = TimeManager.CurrentTime;

            Manager.CollisionsVisible = false;  // Collision Visibility An/Aus
            DisableLayers = false;      // Erstmal Layer aktivieren
            DisableLayerBack = false;   // LayerBack aktivieren

            //TheodorInstance erstellen --"Child" -- "GrownUp"--
            StartCharacterState("GrownUp", -550);

            CollisionHeightLayerFront = PlatformerCharacterBase.getInstance().Collision.Height * 0.5f;
            CollisionHeightLayerMid = PlatformerCharacterBase.getInstance().Collision.Height * 0.45f;
            CollisionHeightLayerBack = PlatformerCharacterBase.getInstance().Collision.Height * 0.4f;
            PlatformerCharacterBase.getInstance().Collision.Height = CollisionHeightLayerFront;

            CollisionWidthLayerFront = PlatformerCharacterBase.getInstance().Collision.Width * 0.5f;
            CollisionWidthLayerMid = PlatformerCharacterBase.getInstance().Collision.Width * 0.45f;
            CollisionWidthLayerBack = PlatformerCharacterBase.getInstance().Collision.Width * 0.4f;
            PlatformerCharacterBase.getInstance().Collision.Width = CollisionWidthLayerFront;

            // Create Collision: Boden
            if (Boden == null)
            {
                Boden = new AxisAlignedRectangle();
                Boden.ScaleX = 2200;
                Boden.ScaleY = 50;
                Boden.Y = -50;
                this.SolidCollisions.AxisAlignedRectangles.Add(Boden);  // Add it to the ShapeCollection so the player can collide against it
                PlatformerCharacterBase.getInstance().Y = 75;       // Make the character appear on top of the rectangle
            }
            InitializeFlower();

            if (Manager.CollisionsVisible) CollisionVisibilityOn();
		}

        //------UPDATE---------------------------------------------------------------
		public virtual void CustomActivity(bool firstTimeCalled)
		{
            //Theodor Movement      
            PlatformerCharacterBase.getInstance().DetermineMovementValues();        
            CameraMovement();         

            //Theodor Collisions
            PlatformerCharacterBase.getInstance().CollideAgainst(SolidCollisions);

            KeyControl();

            LayerOn();
            LayerManagement();
            
            if (!IsPaused) PlatformerCharacterBase.getInstance().Activity();            
            
            PauseGame();

            ControllFlower();

            // "Cheats"
            DeveloperActivity();
		}

        public virtual void CustomDestroy()
        {
            PlatformerCharacterBase.updateinstance(null);
            SpriteManager.RemoveLayer(LayerBack);
            SpriteManager.RemoveLayer(LayerMid);
            SpriteManager.RemoveLayer(LayerFront);
		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        //----------------------------------------------------------------------------------
        //--------FUNKTIONEN----------------------------------------------------------------

        public void EnableKeys()
        {
            Manager.EnableKey_Space = true;
            Manager.EnableKey_Down = true;
            Manager.EnableKey_Up = true;
            Manager.EnableKey_E = true;
        }

        public void KeyControl()
        {
            if (!Manager.EnableKey_Up) InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Up);
            if (!Manager.EnableKey_Down) InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Down);
            if (!Manager.EnableKey_Space) InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Space);
            if (!Manager.EnableKey_E) InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.E);
        }

        public void StartCharacterState(String state, int x)
        {
            if (state == "Child")
            {
                //--Als Kind starten
                TheodorChild Temp = new VergissMeinNicht.Entities.TheodorChild(ContentManagerName, false);
                Temp.Name = "TheodorChildInstance";
                PlatformerCharacterBase.updateinstance(Temp);
                Temp.AddToManagers(LayerFront);
            }

            else //if (state == "GrownUp")
            {
                //  --Als GrownUp starten  ("Standard")
                TheodorGrownUp Temp = new VergissMeinNicht.Entities.TheodorGrownUp(ContentManagerName, false);
                Temp.Name = "TheodorGrownUpInstance";
                PlatformerCharacterBase.updateinstance(Temp);
                Temp.AddToManagers(LayerFront);
            }

            PlatformerCharacterBase.getInstance().X = x;
            SpriteManager.Camera.X = x;
        }

        //----FLOWER
        void InitializeFlower()
        {
            BlumeInstance.thisStartPosY = BlumeInstance.Y;            
            Blume_SchattenInstance.Y = BlumeInstance.thisStartPosY - 40;
            Blume_SchattenInstance.X = BlumeInstance.X;
        }

        void ControllFlower()
        {
            //--Collision
            if (PlatformerCharacterBase.getInstance().Collision.CollideAgainst(BlumeInstance.Collision) && PlatformerCharacterBase.isChild())
            {
                BlumeInstance.Y = -500;
                BlumeInstance.Destroy();
                Blume_SchattenInstance.Destroy();
                RauchInstance.Y = -500;             // WA: Ansonsten bleibt die Collision da, obwohl sie nicht mehr da ist
                RauchInstance.Destroy();
                Blume.Blop.Play();
            }
        }
        //---/Flower

        public void DoorActivity(int x)
        {

            if (PlatformerCharacterBase.getInstance().X >= x && PlatformerCharacterBase.isChild())
            {
                UI_Button_EInstance.SpriteInstanceVisible = true;
                DoorOpen = true;
            }
            else
            {
                UI_Button_EInstance.SpriteInstanceVisible = false;
                DoorOpen = false;
            }
            

        }
        
        public void CollisionVisibilityOn()
        {
            SolidCollisions.AddToManagers(); // Add the ShapeColleciton to the ShapeManager so it's visible
            PlatformerCharacterBase.getInstance().Collision.Visible = true;
            BlumeInstance.Collision.Visible = true;
            RauchInstance.Visible = true;
        }

        public virtual void DeveloperActivity()
        {
            // Z  -- Zoolevel
            if (InputManager.Keyboard.KeyPushed(Keys.Z)) MoveToScreen(typeof(ZooLevel).FullName);

            // R  -- Reset Character
            if (InputManager.Keyboard.KeyPushed(Keys.R))
            {
                EnableKeys();
                DisableLayers = true;
                Boden.Y = -50;
                CurrentLayer = 1;
                PlatformerCharacterBase.getInstance().Y = 75;
                PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale = 0.5f;
                if (!PlatformerCharacterBase.isChild())
                {
                    DisableLayers = false;
                    TheodorChild Temp = new TheodorChild();
                    Temp.X = PlatformerCharacterBase.getInstance().X;
                    Temp.Y = PlatformerCharacterBase.getInstance().Y;
                    PlatformerCharacterBase.updateinstance(Temp);
                }
            }

            // Numbers  -- GoTo: 1-Empfang, 2-Flur         
            if (InputManager.Keyboard.KeyPushed(Keys.D1)) MoveToScreen(typeof(Empfang).FullName);
            if (InputManager.Keyboard.KeyPushed(Keys.D2)) MoveToScreen(typeof(Flur).FullName);
            
            // F3/F4 -- Zeigt Debugger Werte an 
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
            if (DebuggerOn)
            {
                string resultStringY = "Character Y: " + PlatformerCharacterBase.getInstance().Y;
                string resultStringX = "Character X: " + PlatformerCharacterBase.getInstance().X;
                string resultStringCollisionH = "Collision H: " + PlatformerCharacterBase.getInstance().Collision.Height;
                string resultStringCollisionW = "Collision W: " + PlatformerCharacterBase.getInstance().Collision.Width;
                string resultStringLayer = "CurrentLayer:" + CurrentLayer.ToString();
                string resultStringL3 = "LayerBackDisable:" + DisableLayerBack.ToString();
                string resultStringLa = "LayersDisable:" + DisableLayers.ToString();
                string resultStringSwitch = "isSwitching:" + Manager.isSwitching.ToString();
                string resultStringCameraX = "CameraX:" + SpriteManager.Camera.X.ToString();
                string resultStringLayerCount = "LayerCount:" + SpriteManager.LayerCount.ToString();
                FlatRedBall.Debugging.Debugger.Write(resultStringX + "\n" + resultStringY + "\n" + resultStringCollisionH + "\n" + resultStringCollisionW +
                    "\n" + resultStringLayer + "\n" + resultStringL3 + "\n" + resultStringLa + "\n" + resultStringSwitch + "\n" + resultStringCameraX +
                    "\n" + resultStringLayerCount);
            }
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

        void FixLayers() //notwendig um Layer zu kontrollieren
        {
            // Erst Layer Removen
            SpriteManager.RemoveLayer(LayerBack);
            SpriteManager.RemoveLayer(LayerMid);
            SpriteManager.RemoveLayer(LayerFront);
            // dann wieder hinzufügen
            SpriteManager.AddLayer(LayerBack);
            SpriteManager.AddLayer(LayerMid);
            SpriteManager.AddLayer(LayerFront);
        }

        void CameraMovement()
        {
            //Camera Movement following Theodor
            if (!IsPaused)
            {
                SpriteManager.Camera.XVelocity = PlatformerCharacterBase.getInstance().X - SpriteManager.Camera.X;
                SpriteManager.Camera.Y = 275;
            }

        }

        public void CameraMaximum(float LeftValue, float RightValue) 
        {
            SpriteManager.Camera.MinimumX = LeftValue; //-305
            SpriteManager.Camera.MaximumX = RightValue;  // 305     
        }

        void LayerOn()
        {
            // Switching: ignore Keys _up, _down, _Space, _Esc
            if (Manager.isSwitching)
            {
                Teddy.DisableTeddy = true;
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Left);
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Right);
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Space);
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Escape);
            }
            
            //CurrentLayer zuweisen
            if (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == 0.5f)
            {
                CurrentLayer = 1;
                Manager.isSwitching = false;
            }

            else if (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == SizeFirstDiff)
            {
                CurrentLayer = 2;
                Manager.isSwitching = false;
            }

            else if (PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale == SizeSecondDiff)
            {
                CurrentLayer = 3;
                Manager.isSwitching = false;
            }
            if (!Manager.isSwitching) Teddy.DisableTeddy = false;
        }
        
        //-------
        //--Management des Hoch-/Runterswitchens
        void LayerManagement()
        {
            if (!DisableLayers && !IsPaused)
            {
                // --Hochswitchen
                if (InputManager.Keyboard.KeyPushed(Keys.Up) && CurrentLayer != 3 && !Manager.isSwitching)
                {
                    Manager.isSwitching = true;                                 

                    switch (CurrentLayer)
                    {
                        case 1:
                                PlatformerCharacterBase.getInstance().SpriteInstance
                                    .Tween("TextureScale")
                                    .To(SizeFirstDiff)
                                    .During(0.75f)
                                    .Using(
                                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                        FlatRedBall.Glue.StateInterpolation.Easing.Out);

                                PlatformerCharacterBase.getInstance().Collision
                                    .Tween("Height")
                                    .To(CollisionHeightLayerMid)
                                    .During(0.75f)
                                    .Using(
                                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                        FlatRedBall.Glue.StateInterpolation.Easing.Out);

                                PlatformerCharacterBase.getInstance().Collision
                                    .Tween("Width")
                                    .To(CollisionWidthLayerMid)
                                    .During(0.75f)
                                    .Using(
                                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                        FlatRedBall.Glue.StateInterpolation.Easing.Out);

                                Boden                                   
                                    .Tween("Y")
                                    .To(Boden.Y + 75)
                                    .During(0.75f)
                                    .Using(
                                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                        FlatRedBall.Glue.StateInterpolation.Easing.Out);

                                PlatformerCharacterBase.getInstance().MoveToLayer(LayerMid);
                            
                            break;

                        case 2:
                            if (!DisableLayerBack)
                            {
                                PlatformerCharacterBase.getInstance().SpriteInstance
                                .Tween("TextureScale")
                                .To(SizeSecondDiff)
                                .During(0.75f)
                                .Using(
                                    FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                    FlatRedBall.Glue.StateInterpolation.Easing.Out);

                                PlatformerCharacterBase.getInstance().Collision
                                    .Tween("Height")
                                    .To(CollisionHeightLayerBack)
                                    .During(0.75f)
                                    .Using(
                                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                        FlatRedBall.Glue.StateInterpolation.Easing.Out);

                                PlatformerCharacterBase.getInstance().Collision
                                    .Tween("Width")
                                    .To(CollisionWidthLayerBack)
                                    .During(0.75f)
                                    .Using(
                                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                        FlatRedBall.Glue.StateInterpolation.Easing.Out);

                                Boden                                                       //WA
                                   .Tween("Y")
                                   .To(Boden.Y + 75)
                                   .During(0.75f)
                                   .Using(
                                       FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                       FlatRedBall.Glue.StateInterpolation.Easing.Out);

                                PlatformerCharacterBase.getInstance().MoveToLayer(LayerBack);
                            }
                            break;

                        default:
                            break;
                    }                   
                }                
                DisableLayers = false;  
            }


            // --Runterswitchen
            if (InputManager.Keyboard.KeyPushed(Keys.Down) && CurrentLayer != 1 && !Manager.isSwitching)
            {
                Manager.isSwitching = true;

                // Direction Left/Right bestimmen
                if (PlatformerCharacterBase.getInstance().DirectionFacing == PlatformerCharacterBase.LeftOrRight.Left)
                    PlatformerCharacterBase.getInstance().SpriteInstance.CurrentChainName = "IdleLeft";
                else           
                    PlatformerCharacterBase.getInstance().SpriteInstance.CurrentChainName = "IdleRight";
                

                switch (CurrentLayer)
                {
                    case 2:
                        PlatformerCharacterBase.getInstance().SpriteInstance
                        .Tween("TextureScale")
                        .To(0.5f)
                        .During(0.75f)
                        .Using(
                            FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                            FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().Collision
                            .Tween("Height")
                            .To(CollisionHeightLayerFront)
                            .During(0.75f)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().Collision
                            .Tween("Width")
                            .To(CollisionWidthLayerFront)
                            .During(0.75f)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().MoveToLayer(LayerFront);

                        break;

                    case 3:
                        PlatformerCharacterBase.getInstance().SpriteInstance
                            .Tween("TextureScale")
                            .To(SizeFirstDiff)
                            .During(0.75f)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().Collision
                            .Tween("Height")
                            .To(CollisionHeightLayerMid)
                            .During(0.75f)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().Collision
                            .Tween("Width")
                            .To(CollisionWidthLayerMid)
                            .During(0.75f)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        PlatformerCharacterBase.getInstance().MoveToLayer(LayerMid);

                        break;

                    default:

                        break;
                }             
               
                
                Boden
                    .Tween("Y")
                    .To(Boden.Y - 75)
                    .During(0.75f)
                    .Using(
                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                        FlatRedBall.Glue.StateInterpolation.Easing.Out);                
            }
        }

	}
}

/*@FacePalm Studios 
............................................________
....................................,.-‘”..................``~.,
.............................,.-”.................................“-.,
.........................,/..........................................”:,
.....................,?................................................\,
.................../...................................................,}
................./...............................................,:`^`..}
.............../...........................................,:”........./
..............?.....__...................................:`.........../
............./__.(.....“~-,_...........................,:`........../
.........../(_....”~,_........“~,_....................,:`........_/
..........{.._$;_......”=,_.......“-,_.......,.-~-,},.~”;/....}
...........((.....*~_.......”=-._......“;,,./`..../”............../
...,,,___.\`~,......“~.,....................`.....}............../
............(....`=-,,.......`........................(......;_,,-”
............/.`~,......`-...............................\....../\
.............\`~.*-,.....................................|,./.....\,__
,,_..........}.>-._\...................................|..............`=~-,
.....`=~-,_\_......`\,.................................\
...................`=~-,,.\,...............................\
................................`:,,...........................`\..............__
.....................................`=-,...................,%`>--==``
........................................_\..........._,-%.......`\
...................................,<`.._|_,-&``................`\
*/