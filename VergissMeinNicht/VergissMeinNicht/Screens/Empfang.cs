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
	public partial class Empfang
	{
        public static int CurrentLayerGhost = 1;        
        public bool DisableLayersGhost = false;

        public AxisAlignedRectangle GhostBoden;

        public double tempTime = Double.PositiveInfinity;
        public double tutTime = Double.PositiveInfinity;

        public int GhostMovementState = 1;

        public bool LeftStartZone = false;
        public bool LeftLayerSwitchZone = false;
        public bool LeftJumpZone = false;

        //------INITIALIZE----------------------------------------------------------------
		public override void CustomInitialize()
		{
            base.CustomInitialize();
            VisibilityInit();
        
            Manager.GhostSpawnTime = Double.PositiveInfinity;

            CameraMaximum(-305, 305);

            GhostInitialize();

            Manager.EnableKey_Down = false;
            Manager.EnableKey_Up = false;
            Manager.EnableKey_Space = false;
            
            //Objektpositionen bestimmen
            BlumeInstance.PositionBlume(-600, 80);
            RauchInstance.PositionRauch(760, 180);
            TeddyInstance.PositionTeddy(400, 200);
            UI_Button_EInstance.PositionUI_Button(790, 300, UI_Button_EInstance.Y); //E_Button Tür       

            Manager.EnableKey_Space = false;
            Manager.CharacterFallingInHole = false;
            

            tutTime = TimeManager.CurrentTime;
        }

        //------UPDATE---------------------------------------------------------------
        public override void CustomActivity(bool firstTimeCalled)
		{
            SwitchBlock();  //Blocken der Switch-Function

            base.CustomActivity(firstTimeCalled);
            CollisionActivity();

            DoorActivity(700);
            NextRoom();

            ObjectVisibilityChange(); //Aktualisiert die Visibility von Objekten je nach Child/GU

            //Löcher
            if (InputManager.Keyboard.KeyPushed(Keys.R)) ResetHoles();        
            HoleActivity();

            CameraMovement();

            if (PlatformerCharacterBase.isChild())
            {
                GhostMovement(); 
                LayerOnGhost();
            }


            TutorialButtons();
            /*
            string resultStringGhostLayer = "GhostLayer:" + CurrentLayerGhost.ToString();
            string resultStringGhostIsSwitching = "GhostSwitching: " + Manager.isSwitchingGhost.ToString();
            string resultStringLeftJumpZone = "LeftJumpZone:" + LeftJumpZone.ToString();
            string resultStringRandomNumber = "randomNumber: " + Manager.randomz;
            FlatRedBall.Debugging.Debugger.Write(resultStringGhostLayer + "\n" + resultStringGhostIsSwitching + "\n" + resultStringLeftJumpZone + "\n" + resultStringRandomNumber);
            */
            if (FlashInstance.SpriteInstanceBlack.Alpha == 1)
                MoveToScreen(typeof(LoadingScreen).FullName);  
            
        }

        public override void CustomDestroy()
		{
            base.CustomDestroy();

		}

        static void CustomLoadStaticContent(string contentManagerName)
        {

            
        }

        //----------------------------------------------------------------------------------
        //--------FUNKTIONEN----------------------------------------------------------------

        void GhostInitialize()
        {         
            TheodorGhostInstance.Collision.Height = 205;
            TheodorGhostInstance.Collision.Width = 110; // Theo Ghost Collision Size
            TheodorGhostInstance.X = 530;

            // Create Collision: GhostBoden
            GhostBoden = new AxisAlignedRectangle();
            GhostBoden.ScaleX = 1800;
            GhostBoden.ScaleY = 50;
            GhostBoden.Y = -50;

            this.GhostBodenCollision.AxisAlignedRectangles.Add(GhostBoden);  // Add it to the ShapeCollection so the ghost can collide against it
        }

        public void VisibilityInit()
        {
            Layer1.Visible = false;
            Layer3.Visible = false;

            TheodorGhostInstance.Collision.Visible = false;

            Background_creepy.Visible = true;
            if (Manager.CollisionsVisible) CollisionVisibilityEmpfang();
        }

        //Collisions Visible machen
        public void CollisionVisibilityEmpfang()
        {
            Layer1.Visible = true;
            Layer3.Visible = true;

            for (int i = HoleList.Count - 1; i > -1; i--) HoleList[i].Collision.Visible = true;   // Hole-Collision Visible machen

            // Theodor Ghost 
            TheodorGhostInstance.Collision.Visible = true;
            GhostBodenCollision.AddToManagers(); // Add the GhostBodenCollision to the ShapeManager so it's visible
        }

        // Manage Visibility in Child/GrownUp State
        void ObjectVisibilityChange()
        {
            if (PlatformerCharacterBase.isChild())
            { 
                Background_creepy.Visible = true;               // Hintergrund
                FeuerInstance.SpriteInstance.Visible = true;    // Feuer
            }
            else 
            {
                Background_creepy.Visible = false;
                FeuerInstance.SpriteInstance.Visible = false;
            }
        }
      
        public void SwitchBlock()
        {
            // Disable Layer3 when necessary
            DisableLayerBack = false;
            DisableLayers = false;
            if (PlatformerCharacterBase.getInstance().X < -345 
                || PlatformerCharacterBase.getInstance().X > 685) 
            {
                DisableLayerBack = true;
                if (CurrentLayer == 2) InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Up);         
            }
        }

        void CameraMovement()
        {
            SpriteManager.Camera.MinimumX = -305;
            SpriteManager.Camera.MaximumX = 305;
        }

        void NextRoom()
        {
            if (InputManager.Keyboard.KeyPushed(Keys.E) && DoorOpen)
            {
                FlashInstance.BlackFadeIn();
                if (Manager.SoundOn) TuerQuitschen.Play();
                Manager.LoadLevel = 1;              
            }
        }


        public void CollisionActivity()
        {
            //Collision Ränder
            PlatformerCharacterBase.getInstance().Collision.CollideAgainstMove(Layer1, 0, 1);  //Kollision mit Rändern
            if (CurrentLayer == 3) PlatformerCharacterBase.getInstance().Collision.CollideAgainstMove(Layer3, 0, 1);  //Kollision auf Layer 3

            //Collision mit Holes als Child
            if (PlatformerCharacterBase.isChild()) 
            {
                //LayerOnGhost();
                for (int i = HoleList.Count - 1; i > -1; i--)
                {
                    if (PlatformerCharacterBase.getInstance().Collision.CollideAgainst(HoleList[i].Collision) && CurrentLayer == HoleList[i].Layer)
                    {
                            OpenHole(i);
                            if (HoleList[i].SpriteInstance.CurrentFrameIndex >= 2) 
                                FallInHole();
                            else if (HoleList[i].SpriteInstance.CurrentChainName == "Idle")
                                FallInHole();
                    }
                    //Collision Ghost-Holes
                    if (TheodorGhostInstance.Collision.CollideAgainst(HoleList[i].Collision) && CurrentLayerGhost == HoleList[i].Layer)
                        HoleList[i].SpriteInstance.Visible = true; 
                }
            }

           //PlatformerCharacterBase.getInstance().SpriteInstance.

            //Theodor Ghost Collisions
            TheodorGhostInstance.CollideAgainst(GhostBodenCollision);
  
            // LevelReset WA
            //if (PlatformerCharacterBase.getInstance().Y < 85 && Boden.Y == -300) MoveToScreen(typeof(Empfang).FullName);
        }

        //----BUTTONS----
        void TutorialButtons()
        {

                    MakeUiInvisible();

                    //--Show Left/Right at the Beginning
                    if (PlatformerCharacterBase.getInstance().X < -450 && !PlatformerCharacterBase.isChild()
                        && TimeManager.CurrentTime - tutTime > 1 && !LeftStartZone)
                    {
                        //Left Button
                        UI_Button_LeftInstance.X = PlatformerCharacterBase.getInstance().X - 60;
                        UI_Button_LeftInstance.Y = PlatformerCharacterBase.getInstance().Y + 150;
                        UI_Button_LeftInstance.SpriteInstanceVisible = true;

                        //Right Button
                        UI_Button_RightInstance.X = PlatformerCharacterBase.getInstance().X + 60;
                        UI_Button_RightInstance.Y = PlatformerCharacterBase.getInstance().Y + 150;
                        UI_Button_RightInstance.SpriteInstanceVisible = true;
                    }
                    if (PlatformerCharacterBase.getInstance().X > -450 && !LeftStartZone) LeftStartZone = true;

                    //--Show Up/Down after GhostSpawn
                    if (CurrentLayerGhost == 2 && !LeftLayerSwitchZone && PlatformerCharacterBase.isChild())
                    {
                      //Up Button
                        UI_Button_UpInstance.X = PlatformerCharacterBase.getInstance().X + 60;
                        UI_Button_UpInstance.Y = PlatformerCharacterBase.getInstance().Y + 140;
                        UI_Button_UpInstance.SpriteInstanceVisible = true;
                        Manager.EnableKey_Up = true;

                        //Down Button
                        UI_Button_DownInstance.X = PlatformerCharacterBase.getInstance().X + 60;
                        UI_Button_DownInstance.Y = PlatformerCharacterBase.getInstance().Y + 100;
                        UI_Button_DownInstance.SpriteInstanceVisible = true;
                        Manager.EnableKey_Down = true;
                    }
                    if (CurrentLayer >= 2 && !LeftLayerSwitchZone) LeftLayerSwitchZone = true;

                //--Show Space after GhostSpawn
                if (PlatformerCharacterBase.isChild() && Manager.FlowerDestroyed)
                {
                    Manager.EnableKey_Space = true;
                    if (!LeftJumpZone)
                    {
                        //Space Button
                        UI_Button_SpaceInstance.X = PlatformerCharacterBase.getInstance().X;
                        UI_Button_SpaceInstance.Y = PlatformerCharacterBase.getInstance().Y + 140;
                        UI_Button_SpaceInstance.SpriteInstanceVisible = true;
                    }
                    if (PlatformerCharacterBase.getInstance().SpriteInstance.CurrentChainName == "JumpRight" ||
                    PlatformerCharacterBase.getInstance().SpriteInstance.CurrentChainName == "JumpRLeft")
                        LeftJumpZone = true;
                }
                //if (PlatformerCharacterBase.getInstance().SpriteInstance.CurrentChainName == "JumpRight" ||
                //    PlatformerCharacterBase.getInstance().SpriteInstance.CurrentChainName == "JumpRLeft") 
                //        LeftJumpZone = true;
        }

        void MakeUiInvisible()
        {
            UI_Button_LeftInstance.SpriteInstanceVisible = false;
            UI_Button_RightInstance.SpriteInstanceVisible = false;
            UI_Button_UpInstance.SpriteInstanceVisible = false;
            UI_Button_DownInstance.SpriteInstanceVisible = false;
            UI_Button_SpaceInstance.SpriteInstanceVisible = false;
        }

        //----LÖCHER----

      
        void HoleActivity()
        {
            for (int i = HoleList.Count - 1; i > -1; i--)
                if (HoleList[i].SpriteInstance.CurrentFrameIndex >= 4 && HoleList[i].Open)
                {
                    //HoleList[i].Open = true;
                    HoleList[i].SpriteInstance.CurrentChainName = "Idle";
                }
            if (Manager.FlowerDestroyed) OpenAllHoles(); 
        }

        void ResetHoles()
        {
            for (int i = HoleList.Count - 1; i > -1; i--)
            {
                HoleList[i].Open = false;
                HoleList[i].SpriteInstance.Visible = false;
            }
            Manager.CharacterFallingInHole = false;
        }

        void OpenHole(int i)
        {
                if (!HoleList[i].Open)
                {
                    HoleList[i].SpriteInstance.Visible = true;
                    HoleList[i].SpriteInstance.CurrentChainName = "Break";
                    HoleList[i].Open = true;                   
                }                
        }

        void OpenAllHoles()
        {
            for (int i = HoleList.Count - 1; i > -1; i--)
             if (!HoleList[i].Open)
             {
                HoleList[i].SpriteInstance.Visible = true;
                HoleList[i].SpriteInstance.CurrentChainName = "Break";
                HoleList[i].Open = true;
             }


        }  

        //----GHOST----
        void GhostMovement()  // Movement-Script Ghost
        {
            // STATE 1:
            if (TimeManager.SecondsSince(Manager.GhostSpawnTime) >= 4 && GhostMovementState == 1)
            {
                LayerManagementGhost(1);
                GhostMovementState = 2;
                
            }

            // STATE 2:
            if (TimeManager.SecondsSince(Manager.GhostSpawnTime) >= 8 && GhostMovementState == 2)
            {
                
                if (TheodorGhostInstance.X >= 175)
                    TheodorGhostInstance.moveleft = true;
                else
                {
                    tempTime = TimeManager.CurrentTime;
                    TheodorGhostInstance.moveleft = false;
                    GhostMovementState = 3;
                }
            }

             // STATE 3:
             if (GhostMovementState == 3 && TimeManager.SecondsSince(tempTime) >= 3)
             {
                 LayerManagementGhost(-1);
                 GhostMovementState = 4;
             }

             // STATE 4:
             if (GhostMovementState == 4 && PlatformerCharacterBase.getInstance().X <= 300 && CurrentLayerGhost == 1)
             {
                 TheodorGhostInstance.moveleft = true;          
             }

             if (TheodorGhostInstance.X <= 50 && GhostMovementState == 4)
             {
                 TheodorGhostInstance.moveleft = false;
                 tempTime = TimeManager.CurrentTime;
                 GhostMovementState = 5;
             }

             // STATE 5:
             if (GhostMovementState == 5 && /*OBERAFFENTITTENGEIL*/ TimeManager.SecondsSince(tempTime) >= 1)
             {
                 LayerManagementGhost(1);      
             }
             
             if (GhostMovementState == 5 && CurrentLayerGhost == 3)
             {
                 GhostMovementState = 6;
             }

             // STATE 6:
             if (GhostMovementState == 6 && PlatformerCharacterBase.getInstance().X >= 10 && PlatformerCharacterBase.getInstance().X <= 80 && CurrentLayer == 2)
             {
                 TheodorGhostInstance.moveleft = true;
             }
             if (GhostMovementState == 6 && TheodorGhostInstance.X <= -220)
             {
                 TheodorGhostInstance.moveleft = false;
                 GhostMovementState = 7;
             }

             // STATE 7:
             if (GhostMovementState == 7 && PlatformerCharacterBase.getInstance().X <= -100 && CurrentLayer == 3)
             {
                 LayerManagementGhost(-1); 
             }
             if (GhostMovementState == 7 && CurrentLayerGhost == 1)
             {
                 tempTime = TimeManager.CurrentTime;
                 GhostMovementState = 8;
             }
             // STATE 8:
             if (GhostMovementState == 8 && TheodorGhostInstance.X <= -100 && TimeManager.SecondsSince(tempTime) >= 1)
             {
                 TheodorGhostInstance.moveleft = true;
             }

             if (TheodorGhostInstance.X <= -450)
             {
                 TheodorGhostInstance.moveleft = false;
                 GhostMovementState = 9;
             }
        }

        void LayerOnGhost()
        {
            //CurrentLayer zuweisen
            if (TheodorGhostInstance.SpriteInstance.TextureScale == 0.5f && CurrentLayerGhost != 1)
            {
                CurrentLayerGhost = 1;
                Manager.isSwitchingGhost = false;
            }

            else if (TheodorGhostInstance.SpriteInstance.TextureScale == SizeFirstDiff && CurrentLayerGhost != 2)
            {
                CurrentLayerGhost = 2;
                Manager.isSwitchingGhost = false;
            }

            else if (TheodorGhostInstance.SpriteInstance.TextureScale == SizeSecondDiff && CurrentLayerGhost != 3)
            {
                CurrentLayerGhost = 3;
                Manager.isSwitchingGhost = false;
            }            
        }

        //--Management des Hoch-/Runterswitchens
        void LayerManagementGhost(int VerticalMovement)
        {
            if (!DisableLayersGhost && !IsPaused)
            {
                // --Hochswitchen
                if (VerticalMovement == 1 && CurrentLayerGhost != 3 && !Manager.isSwitchingGhost)
                {
                    Manager.isSwitchingGhost = true;

                    // Direction Left/Right bestimmen
                    if (TheodorGhostInstance.DirectionFacing == PlatformerCharacterBase.LeftOrRight.Left)
                        TheodorGhostInstance.SpriteInstance.CurrentChainName = "IdleLeft";
                    else
                        TheodorGhostInstance.SpriteInstance.CurrentChainName = "IdleRight";

                    switch (CurrentLayerGhost)
                    {
                        case 1:
                            TheodorGhostInstance.SpriteInstance
                                .Tween("TextureScale")
                                .To(SizeFirstDiff)
                                .During(0.75f)
                                .Using(
                                    FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                    FlatRedBall.Glue.StateInterpolation.Easing.Out);

                            TheodorGhostInstance.Collision
                                .Tween("Height")
                                .To(CollisionHeightLayerMid)
                                .During(0.75f)
                                .Using(
                                    FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                    FlatRedBall.Glue.StateInterpolation.Easing.Out);

                            TheodorGhostInstance.Collision
                                .Tween("Width")
                                .To(CollisionWidthLayerMid)
                                .During(0.75f)
                                .Using(
                                    FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                    FlatRedBall.Glue.StateInterpolation.Easing.Out);

                            GhostBoden
                                .Tween("Y")
                                .To(GhostBoden.Y + 75)
                                .During(0.75f)
                                .Using(
                                    FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                    FlatRedBall.Glue.StateInterpolation.Easing.Out);

                            TheodorGhostInstance.MoveToLayer(LayerMid);

                            break;

                        case 2:
                                TheodorGhostInstance.SpriteInstance
                                .Tween("TextureScale")
                                .To(SizeSecondDiff)
                                .During(0.75f)
                                .Using(
                                    FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                    FlatRedBall.Glue.StateInterpolation.Easing.Out);

                                TheodorGhostInstance.Collision
                                    .Tween("Height")
                                    .To(CollisionHeightLayerBack)
                                    .During(0.75f)
                                    .Using(
                                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                        FlatRedBall.Glue.StateInterpolation.Easing.Out);

                                TheodorGhostInstance.Collision
                                    .Tween("Width")
                                    .To(CollisionWidthLayerBack)
                                    .During(0.75f)
                                    .Using(
                                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                        FlatRedBall.Glue.StateInterpolation.Easing.Out);

                                GhostBoden                                                       
                                   .Tween("Y")
                                   .To(GhostBoden.Y + 75)
                                   .During(0.75f)
                                   .Using(
                                       FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                       FlatRedBall.Glue.StateInterpolation.Easing.Out);

                                TheodorGhostInstance.MoveToLayer(LayerBack);                           
                            break;

                        default:
                            break;
                    }
                }
                DisableLayersGhost = false;
            }


            // --Runterswitchen
            if (VerticalMovement == -1 && CurrentLayerGhost != 1 && !Manager.isSwitchingGhost)
            {
                Manager.isSwitchingGhost = true;

                // Direction Left/Right bestimmen
                if (TheodorGhostInstance.DirectionFacing == PlatformerCharacterBase.LeftOrRight.Left)
                    TheodorGhostInstance.SpriteInstance.CurrentChainName = "IdleLeft";
                else
                    TheodorGhostInstance.SpriteInstance.CurrentChainName = "IdleRight";


                switch (CurrentLayerGhost)
                {
                    case 2:
                        TheodorGhostInstance.SpriteInstance
                        .Tween("TextureScale")
                        .To(0.5f)
                        .During(0.75f)
                        .Using(
                            FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                            FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        TheodorGhostInstance.Collision
                            .Tween("Height")
                            .To(CollisionHeightLayerFront)
                            .During(0.75f)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        TheodorGhostInstance.Collision
                            .Tween("Width")
                            .To(CollisionWidthLayerFront)
                            .During(0.75f)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        TheodorGhostInstance.MoveToLayer(LayerFront);

                        break;

                    case 3:
                        TheodorGhostInstance.SpriteInstance
                            .Tween("TextureScale")
                            .To(SizeFirstDiff)
                            .During(0.75f)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        TheodorGhostInstance.Collision
                            .Tween("Height")
                            .To(CollisionHeightLayerMid)
                            .During(0.75f)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        TheodorGhostInstance.Collision
                            .Tween("Width")
                            .To(CollisionWidthLayerMid)
                            .During(0.75f)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        TheodorGhostInstance.MoveToLayer(LayerMid);

                        break;

                    default:

                        break;
                }


                GhostBoden
                    .Tween("Y")
                    .To(GhostBoden.Y - 75)
                    .During(0.75f)
                    .Using(
                        FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                        FlatRedBall.Glue.StateInterpolation.Easing.Out);
            }
        }
        //----/GHOST----
	}
}
