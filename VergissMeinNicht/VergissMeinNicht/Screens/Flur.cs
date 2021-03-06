#region Usings

using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
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
	public partial class Flur
	{
        bool HighJump = false;
        double jumpTime = Double.NegativeInfinity;

        //------INITIALIZE----------------------------------------------------------------
		public override void CustomInitialize()
		{
            base.CustomInitialize();
            DisableLayers = true;

            Manager.CurrentLevel = 2;

            CameraMaximum(-305, 1400);

            //Objektpositionen bestimmen
            BlumeInstance.PositionBlume(-600, -180);
            RauchInstance.PositionRauch(1860, 180);
            TeddyInstance.PositionTeddy(-400, 100);
            DoorInstance_Schlafraum.X = 1260;
            UI_Button_EInstance.PositionUI_Button(DoorInstance_Schlafraum.X + 90, DoorInstance_Schlafraum.Y + 90, UI_Button_EInstance.Y);

            //StartCharacterState("Child", 0);

            CollisionInit();
            VisibilityInit();

            //PlatformerCharacterBase.getInstance().YVelocity = 150;
            
		}

        //------UPDATE---------------------------------------------------------------
        public override void CustomActivity(bool firstTimeCalled)
		{
            base.CustomActivity(firstTimeCalled);

            CharacterGrenzen();

            DoorActivity();

            //Lochfallen
            for (int i = HoleList.Count - 1; i > -1; i--)
                if (PlatformerCharacterBase.getInstance().Collision.CollideAgainst(HoleList[i].Collision))
                    FallInHole();

           
            TheoHighJump();

            JitBKill();
            
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

        void JitBKill()
        {
            if (JackInTheBoxInstance.SpriteInstance.CurrentChainName == "JitB_Attack" && JackInTheBoxInstance.SpriteInstance.CurrentFrameIndex == 4 && PlatformerCharacterBase.getInstance().X <= 500)
            {
                TweenCharacterInvis(0.75f);                
                FlashInstance.BlackFadeIn(1, 1);
                PlatformerCharacterBase.getInstance().X = TeddyInstance.X;
            }
            else if (FlashInstance.SpriteInstanceBlack.Alpha == 1)
            {
                TweenCharacterInvis(0.75f);
                FlashInstance.BlackFadeOut(0, 1);            
            }
        }
        

        void TheoHighJump()
        {
            if (PlatformerCharacterBase.getInstance().X >= JackInTheBoxInstance.X - 55
             && PlatformerCharacterBase.getInstance().X <= JackInTheBoxInstance.X + 55
             && PlatformerCharacterBase.getInstance().Y >= JackInTheBoxInstance.Y + 45 && PlatformerCharacterBase.getInstance().Y <= JackInTheBoxInstance.Y + 55)
                HighJump = true;

            if (HighJump)
            {
                jumpTime = TimeManager.CurrentTime;
                HighJump = false;
            }

            if (TimeManager.SecondsSince(jumpTime) < 1)
            {
                float time = (float)TimeManager.SecondsSince(jumpTime) * 300;   
                PlatformerCharacterBase.getInstance().YVelocity = 400 - time;   // merkw�rdige Funktion die einen Sprung mit abnehmender Beschleunigung simulieren soll
            }
            else if (TimeManager.SecondsSince(jumpTime) > 1) jumpTime = Double.NegativeInfinity;
        }
        
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

            this.SolidCollisions.AxisAlignedRectangles.Remove(JackInTheBoxInstance.Collision);
            this.SolidCollisions.AxisAlignedRectangles.Add(JackInTheBoxInstance.Collision);


        }

        void VisibilityInit()
        {
            for (int i = HoleList.Count - 1; i > -1; i--)
                HoleList[i].SpriteInstance.Visible = true;

            if (Manager.CollisionsVisible)
            {
                for (int i = KisteList.Count - 1; i > -1; i--)
                    KisteList[i].Collision.Visible = true;
                for (int i = KartonList.Count - 1; i > -1; i--)
                    KartonList[i].Collision.Visible = true;
                for (int i = HoleList.Count - 1; i > -1; i--)
                    HoleList[i].Collision.Visible = true;

                JackInTheBoxInstance.Collision.Visible = true;
            }
        }

        void DoorActivity()
        {
            if (PlatformerCharacterBase.getInstance().X > DoorInstance_Schlafraum.X - 60 &&
                PlatformerCharacterBase.getInstance().X < DoorInstance_Schlafraum.X + 60)
            {
                UI_Button_EInstance.SpriteInstanceVisible = true;
                if (InputManager.Keyboard.KeyPushed(Keys.E))
                    MoveToScreen(typeof(FadeOut).FullName);  
            }
            else UI_Button_EInstance.SpriteInstanceVisible = false;
        }
	}
}
