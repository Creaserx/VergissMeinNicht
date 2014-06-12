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
	public partial class LevelBase
	{

		public virtual void CustomInitialize()
		{
            TheodorChild Temp = new VergissMeinNicht.Entities.TheodorChild(ContentManagerName, false);
            Temp.Name = "TheodorChildInstance";
            PlatformerCharacterBase.updateinstance(Temp);
            Temp.AddToManagers(mLayer);

		}

		public virtual void CustomActivity(bool firstTimeCalled)
		{
            //Theodor Movement      
            PlatformerCharacterBase.getInstance().DetermineMovementValues();        //Collision muss extra aufgerufen werden

            if (!IsPaused)
            {
                PlatformerCharacterBase.getInstance().Activity();
            }

            CameraMovement();


		}

        public virtual void CustomDestroy()
        {
            PlatformerCharacterBase.updateinstance(null);

		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        void CameraMovement()
        {
            //Camera Movement following Theodor
            SpriteManager.Camera.XVelocity = PlatformerCharacterBase.getInstance().X - SpriteManager.Camera.X;
            SpriteManager.Camera.YVelocity = PlatformerCharacterBase.getInstance().Y - SpriteManager.Camera.Y;

        }

	}
}
