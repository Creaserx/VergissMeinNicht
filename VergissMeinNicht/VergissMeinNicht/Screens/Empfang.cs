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

        public int GhostMovementState = 1;


        //------INITIALIZE----------------------------------------------------------------
		public override void CustomInitialize()
		{
            base.CustomInitialize();
            VisibilityInit();

            CameraMaximum(-305, 305);

            GhostInitialize();

            //Objektpositionen bestimmen
            BlumeInstance.PositionBlume(-600, 80);
            RauchInstance.PositionRauch(760, 180);
            TeddyInstance.PositionTeddy(400, 200);
            UI_Button_EInstance.PositionE_Button(790, 300, UI_Button_EInstance.Y); //E_Button Tür       

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

            // reset Holes
            if (InputManager.Keyboard.KeyPushed(Keys.R)) for (int i = HoleList.Count - 1; i > -1; i--) HoleList[i].SpriteInstance.Visible = false;

            CameraMovement();

            if (PlatformerCharacterBase.isChild())
            {
                LayerOnGhost();
                GhostMovement();
            }

            string resultStringGhostLayer = "GhostLayer:" + CurrentLayerGhost.ToString();
                FlatRedBall.Debugging.Debugger.Write(resultStringGhostLayer);
            
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
            if (CollisionsVisible) CollisionVisibilityEmpfang();

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
            if (PlatformerCharacterBase.getInstance().X < -345) DisableLayerBack = true;
            else if (PlatformerCharacterBase.getInstance().X > 685) DisableLayerBack = true;
        }

        void CameraMovement()
        {
            SpriteManager.Camera.MinimumX = -305;
            SpriteManager.Camera.MaximumX = 305;
        }

        void NextRoom()
        {
            if (InputManager.Keyboard.KeyPushed(Keys.E) && DoorOpen) MoveToScreen(typeof(Flur).FullName); 
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
                        HoleList[i].SpriteInstance.Visible = true;
                        FallInHole();
                    }
                    //Collision Ghost-Holes
                    if (TheodorGhostInstance.Collision.CollideAgainst(HoleList[i].Collision) && CurrentLayerGhost == HoleList[i].Layer)
                        HoleList[i].SpriteInstance.Visible = true; 
                }
            }

           

            //Theodor Ghost Collisions
            TheodorGhostInstance.CollideAgainst(GhostBodenCollision);

            //if (PlatformerCharacterBase.getInstance().X >= 689) InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Right);
  
            // LevelReset WA
            //if (PlatformerCharacterBase.getInstance().Y < 85 && Boden.Y == -300) MoveToScreen(typeof(Empfang).FullName);
        }


        void FallInHole()
        {
            //Boden.Y = -300;
            // SolidCollisions.AxisAlignedRectangles.Remove(Boden);   --Funktioniert theoretisch, aber ein "Boden" ist immer noch da.

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


        //----GHOST----
        void GhostMovement()
        {
            // STATE 1:
            if (TimeManager.SecondsSince(Manager.GhostSpawnTime) >= 5 && GhostMovementState == 1)
            {
                LayerManagementGhost(1);
                GhostMovementState = 2;
                
            }

            // STATE 2:
            if (TimeManager.SecondsSince(Manager.GhostSpawnTime) >= 10 && GhostMovementState == 2)
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
             if (GhostMovementState == 3 && TimeManager.SecondsSince(tempTime) >= 5)
             {
                 LayerManagementGhost(-1);
                 GhostMovementState = 4;
             } 

                       
            
        }

        void LayerOnGhost()
        {
            //CurrentLayer zuweisen
            if (TheodorGhostInstance.SpriteInstance.TextureScale == 0.5f)
            {
                CurrentLayerGhost = 1;
                Manager.isSwitchingGhost = false;
            }

            else if (TheodorGhostInstance.SpriteInstance.TextureScale == SizeFirstDiff)
            {
                CurrentLayerGhost = 2;
                Manager.isSwitchingGhost = false;
            }

            else if (TheodorGhostInstance.SpriteInstance.TextureScale == SizeSecondDiff)
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
