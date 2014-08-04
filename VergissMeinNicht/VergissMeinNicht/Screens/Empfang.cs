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


		public override void CustomInitialize()
		{
            base.CustomInitialize();
            VisibilityInit();          

            // Position Blume & Rauch
            BlumeInstance.PositionBlume(-600, 80);
            RauchInstance.PositionRauch(760, 180);
            UI_Button_EInstance.PositionE_Button(790, 300, UI_Button_EInstance.Y);
            //UI_Button_EInstance.thisStartPosY = UI_Button_EInstance.Y;
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

            Background_creepy.Visible = true;
            if (CollisionsVisible) CollisionVisibilityEmpfang();
   
        }
    
        //Collisions Visible machen
        public void CollisionVisibilityEmpfang()
        {
            Layer1.Visible = true;
            Layer3.Visible = true;

            for (int i = HoleList.Count - 1; i > -1; i--) HoleList[i].Collision.Visible = true;   // Hole-Collision Visible machen


        }


	}
}
