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
            TeddyInstance.SetNeededLayer(2);
            



		}

        public override void CustomActivity(bool firstTimeCalled)
		{
            
            if (PlatformerCharacterBase.getInstance().X < -345) DisableLayer3 = true;
            else DisableLayer3 = false;
            base.CustomActivity(firstTimeCalled);
            CollisionActivity();
            TeddyInstance.SetCurrentLayer(LayerOn);


		}

        public override void CustomDestroy()
		{
            base.CustomDestroy();

		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public void CollisionActivity()
        {

            PlatformerCharacterBase.getInstance().Collision.CollideAgainstMove(Layer1, 0, 1);  //Kollision mit R�ndern
           

            if (LayerOn == 3) 
            {
                PlatformerCharacterBase.getInstance().Collision.CollideAgainstMove(Layer3, 0, 1);  //Kollision auf Layer 3
            }


        }




	}
}
