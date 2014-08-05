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
        public bool isSwitchingGhost = false;
        public bool DisableLayersGhost = false;

        public AxisAlignedRectangle GhostBoden;

		public override void CustomInitialize()
		{
            base.CustomInitialize();
            VisibilityInit();


            // Position Blume & Rauch
            BlumeInstance.PositionBlume(-600, 80);
            RauchInstance.PositionRauch(760, 180);
            UI_Button_EInstance.PositionE_Button(790, 300, UI_Button_EInstance.Y);
            //UI_Button_EInstance.thisStartPosY = UI_Button_EInstance.Y;

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

        public override void CustomActivity(bool firstTimeCalled)
		{
            SwitchBlock();  //Blocken der Switch-Function

            base.CustomActivity(firstTimeCalled);
            CollisionActivity();
            DoorActivity(700, "Flur");

            ObjectVisibilityChange(); //Aktualisiert die Visibility von Objekten je nach Child/GU

            // reset Holes
            if (InputManager.Keyboard.KeyPushed(Keys.R)) for (int i = HoleList.Count - 1; i > -1; i--) HoleList[i].SpriteInstance.Visible = false;

            CameraMovement();

            if (PlatformerCharacterBase.isChild())
            {
                //GhostMovement();
            }

            NextRoom();
        }

        public override void CustomDestroy()
		{
            base.CustomDestroy();

		}

        static void CustomLoadStaticContent(string contentManagerName)
        {

            
        }

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

            PlatformerCharacterBase.getInstance().Collision.CollideAgainstMove(Layer1, 0, 1);  //Kollision mit Rändern
            if (CurrentLayer == 3) PlatformerCharacterBase.getInstance().Collision.CollideAgainstMove(Layer3, 0, 1);  //Kollision auf Layer 3

            //Collision mit Holes
            for (int i = HoleList.Count - 1; i > -1; i--)
            {
                if (PlatformerCharacterBase.getInstance().Collision.CollideAgainst(HoleList[i].Collision) && CurrentLayer == HoleList[i].Layer)
                {
                    HoleList[i].SpriteInstance.Visible = true;
                    FallInHole();
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

        //Initialize Visibility
        public void VisibilityInit()
        {
            Layer1.Visible = false;
            Layer3.Visible = false;

            TheodorGhostInstance.Collision.Visible = true;

            Background_creepy.Visible = true;
            if (CollisionsVisible) CollisionVisibilityEmpfang();
            GhostBodenCollision.AddToManagers(); // Add the GhostBodenCollision to the ShapeManager so it's visible
        }
    
        //Collisions Visible machen
        public void CollisionVisibilityEmpfang()
        {
            Layer1.Visible = true;
            Layer3.Visible = true;

            for (int i = HoleList.Count - 1; i > -1; i--) HoleList[i].Collision.Visible = true;   // Hole-Collision Visible machen


        }

        void GhostMovement()
        {
            LayerManagementGhost(1);
            
            LayerOnGhost();
            LayerManagementGhost(-1);
           
        }

        void LayerOnGhost()
        {
            //CurrentLayer zuweisen
            if (TheodorGhostInstance.SpriteInstance.TextureScale == 0.5f)
            {
                CurrentLayerGhost = 1;
                isSwitchingGhost = false;
            }

            else if (TheodorGhostInstance.SpriteInstance.TextureScale == SizeFirstDiff)
            {
                CurrentLayerGhost = 2;
                isSwitchingGhost = false;
            }

            else if (TheodorGhostInstance.SpriteInstance.TextureScale == SizeSecondDiff)
            {
                CurrentLayerGhost = 3;
                isSwitchingGhost = false;
            }            
        }

        //--Management des Hoch-/Runterswitchens
        void LayerManagementGhost(int VerticalMovement)
        {
            if (!DisableLayersGhost && !IsPaused)
            {
                // --Hochswitchen
                if (VerticalMovement == 1 && CurrentLayerGhost != 3 && !isSwitchingGhost)
                {
                    isSwitchingGhost = true;

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
            if (VerticalMovement == -1 && CurrentLayerGhost != 1 && !isSwitchingGhost)
            {
                isSwitchingGhost = true;

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

	}
}
