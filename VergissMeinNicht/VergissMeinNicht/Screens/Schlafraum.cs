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
using VergissMeinNicht.Entities;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif
#endregion

namespace VergissMeinNicht.Screens
{
	public partial class Schlafraum
	{

        public override void CustomInitialize()
		{
            base.CustomInitialize();

            this.SpriteInstance.Y = 270;
            this.SpriteInstance.TextureScale *= 0.9f;
            CameraMaximum(-280, 280);

            Manager.CurrentLevel = 3;
            

            //Objektpositionen bestimmen
            BlumeInstance.PositionBlume(-600, 80);
            RauchInstance.PositionRauch(960, 180);
            TeddyInstance.PositionTeddy(-400, 100);
            PlatformerCharacterBase.getInstance().X = 550;
            PlatformerCharacterBase.getInstance().DirectionFacing = PlatformerCharacterBase.LeftOrRight.Left;
            
            
		}

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

	}
}
