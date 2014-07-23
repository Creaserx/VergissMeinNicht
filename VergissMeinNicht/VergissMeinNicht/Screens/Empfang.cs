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

            
		}

        public override void CustomActivity(bool firstTimeCalled)
		{
            SwitchBlock();  //Blocken der Switch-Function

            base.CustomActivity(firstTimeCalled);
            CollisionActivity();

            // Background Change
            if (PlatformerCharacterBase.isChild()) Background_creepy.Visible = true;
            else Background_creepy.Visible = false;

            // reset Holes
            if (InputManager.Keyboard.KeyPushed(Keys.R)) for (int i = HoleList.Count - 1; i > -1; i--) HoleList[i].SpriteInstance.Visible = false;

        
        }

        public override void CustomDestroy()
		{
            base.CustomDestroy();

		}

        static void CustomLoadStaticContent(string contentManagerName)
        {

            
        }

        public void SwitchBlock()
        {
            DisableLayer3 = false;
            DisableLayers = false;
            if (PlatformerCharacterBase.getInstance().X < -345) DisableLayer3 = true;
            else if (PlatformerCharacterBase.getInstance().X > 685) DisableLayer3 = true;
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
  
            // LevelReset WA
            //if (PlatformerCharacterBase.getInstance().Y < 85 && Boden.Y == -300) MoveToScreen(typeof(Empfang).FullName);
        }



        void FallInHole()
        {
            //Boden.Y = -300;
            // SolidCollisions.AxisAlignedRectangles.Remove(Boden);   --Funktioniert theoretisch, aber ein "Boden" ist immer noch da.

        }

        public void VisibilityInit()
        {
            Layer1.Visible = false;
            Layer3.Visible = false;

            Background_creepy.Visible = true;
            if (CollisionsVisible) CollisionVisibilityEmpfang();
   
        }
    
        public void CollisionVisibilityEmpfang()
        {
            Layer1.Visible = true;
            Layer3.Visible = true;

            for (int i = HoleList.Count - 1; i > -1; i--) HoleList[i].Collision.Visible = true;   // Hole-Collision Visible machen


        }


	}
}
