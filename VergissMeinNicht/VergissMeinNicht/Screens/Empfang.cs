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
            Layer1.Visible = false;
            Layer3.Visible = false;

            Background_creepy.Visible = true;
            if (CollisionsVisible) CollisionVisibilityEmpfang();
		}

        public override void CustomActivity(bool firstTimeCalled)
		{
            
            SwitchBlock();
            base.CustomActivity(firstTimeCalled);
            CollisionActivity();
            if (PlatformerCharacterBase.isChild()) Background_creepy.Visible = true;
            else Background_creepy.Visible = false;

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


            if (CurrentLayer == 2 && PlatformerCharacterBase.getInstance().Y == 150 && PlatformerCharacterBase.getInstance().X > 25 && PlatformerCharacterBase.getInstance().X < 80)
            {
                FallInHole();
                
            }
            if (PlatformerCharacterBase.getInstance().Y < 85 && Boden.Y == -300) MoveToScreen(typeof(Empfang).FullName);
        }

        void FallInHole()
        {
            Boden.Y = -300;

            //PlatformerCharacterBase.getInstance().Collision.CollideAgainst();

            //if (PlatformerCharacterBase.getInstance().Y < 85 && Boden.Y == -300) MoveToScreen(typeof(Empfang).FullName);
        }


        public void CollisionVisibilityEmpfang()
        {
            Layer1.Visible = true;
            Layer3.Visible = true;

            for (int i = HoleList.Count - 1; i > -1; i--) HoleList[i].Collision.Visible = true;   // Holes Visible machen


        }


	}
}
