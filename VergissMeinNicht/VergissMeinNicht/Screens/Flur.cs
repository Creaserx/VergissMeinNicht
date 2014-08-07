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

            CameraMaximum(-505, 505);

            //Objektpositionen bestimmen
            BlumeInstance.PositionBlume(-600, 80);
            RauchInstance.PositionRauch(960, 180);
            TeddyInstance.PositionTeddy(400, 200);

            //StartCharacterState("Child", 0);
		}

        //------UPDATE---------------------------------------------------------------
        public override void CustomActivity(bool firstTimeCalled)
		{
            base.CustomActivity(firstTimeCalled);

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

	}
}
