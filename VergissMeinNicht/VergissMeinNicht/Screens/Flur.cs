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
	public partial class Flur
	{

        //------INITIALIZE----------------------------------------------------------------
		public override void CustomInitialize()
		{
            base.CustomInitialize();
            DisableLayers = true;

            CameraMaximum(-305, 1600);

            //Objektpositionen bestimmen
            BlumeInstance.PositionBlume(-600, 80);
            RauchInstance.PositionRauch(1060, 180);
            TeddyInstance.PositionTeddy(-400, 100);
            UI_Button_EInstance.PositionUI_Button(DoorInstance_Schlafraum.X + 90, DoorInstance_Schlafraum.Y + 90, UI_Button_EInstance.Y);

            //StartCharacterState("Child", 0);

            CollisionInit();
            VisibilityInit();

            
		}

        //------UPDATE---------------------------------------------------------------
        public override void CustomActivity(bool firstTimeCalled)
		{
            base.CustomActivity(firstTimeCalled);

            CharacterGrenzen();

            DoorActivity();


            //PlatformerCharacterBase.getInstance().Collision.CollideAgainst(KartonInstance.Collision); 


            if (InputManager.Keyboard.KeyPushed(Keys.G)) JackInTheBoxInstance.SpriteInstance.CurrentChainName = "Attack";
            if (InputManager.Keyboard.KeyPushed(Keys.H)) JackInTheBoxInstance.SpriteInstance.CurrentChainName = "Drehen";
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
        void CharacterGrenzen()
        {
            if (PlatformerCharacterBase.getInstance().X < -700) Manager.EnableKey_Left = false;
            else Manager.EnableKey_Left = true;
        
        }

        void CollisionInit()
        {
            //Collision
            for (int i = KartonList.Count - 1; i > -1; i--)
            {
                this.SolidCollisions.AxisAlignedRectangles.Remove(KartonList[i].Collision);
                this.SolidCollisions.AxisAlignedRectangles.Add(KartonList[i].Collision);
            }
            for (int i = KisteList.Count - 1; i > -1; i--)
            {
                this.SolidCollisions.AxisAlignedRectangles.Remove(KisteList[i].Collision);
                this.SolidCollisions.AxisAlignedRectangles.Add(KisteList[i].Collision);
            }

            //Lochfallen
            for (int i = HoleList.Count - 1; i > -1; i--)
                if (PlatformerCharacterBase.getInstance().Collision.CollideAgainst(HoleList[i].Collision))
                    FallInHole();

        }

        void VisibilityInit()
        {
            HoleInstance.SpriteInstance.Visible = true;
            if (Manager.CollisionsVisible)
            {
                for (int i = KisteList.Count - 1; i > -1; i--)
                    KisteList[i].Collision.Visible = true;
                for (int i = KartonList.Count - 1; i > -1; i--)
                    KartonList[i].Collision.Visible = true;
            }
        }

        void DoorActivity()
        {
            if (PlatformerCharacterBase.getInstance().X > DoorInstance_Schlafraum.X - 60 &&
                PlatformerCharacterBase.getInstance().X < DoorInstance_Schlafraum.X + 60)
            {
                UI_Button_EInstance.SpriteInstanceVisible = true;
                if (InputManager.Keyboard.KeyPushed(Keys.E))
                    MoveToScreen(typeof(Schlafraum).FullName);  
            }
            else UI_Button_EInstance.SpriteInstanceVisible = false;
        }
	}
}
