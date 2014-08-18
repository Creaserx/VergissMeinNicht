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
	public partial class StartUp
	{

		void CustomInitialize()
		{
            SpriteInstance.ScaleX = FlatRedBall.SpriteManager.Camera.RelativeXEdgeAt(SpriteInstance.Z);
            SpriteInstance.ScaleY = FlatRedBall.SpriteManager.Camera.RelativeYEdgeAt(SpriteInstance.Z);
            IsPaused = true;
            RauchInstance.X = 1600;
            TeddyInstance.X = 1600;
            BlumeInstance.X = 1600;
            SpriteInstance.X = -550;
            PlatformerCharacterBase.getInstance().SpriteInstance.Visible = false;

            
		}

		void CustomActivity(bool firstTimeCalled)
		{
            if (TimeManager.CurrentTime >= 3)
            {
                MoveToScreen(typeof(MainMenu).FullName);
            }

		}

		void CustomDestroy()
		{
                        
		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
