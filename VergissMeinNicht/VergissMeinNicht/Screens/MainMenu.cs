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
	public partial class MainMenu
	{

		void CustomInitialize()
		{
            // Size the sprite to match the size of the camera
            SpriteInstance.ScaleX = FlatRedBall.SpriteManager.Camera.RelativeXEdgeAt(SpriteInstance.Z);
            SpriteInstance.ScaleY = FlatRedBall.SpriteManager.Camera.RelativeYEdgeAt(SpriteInstance.Z);            
		}

		void CustomActivity(bool firstTimeCalled)
		{
            // Z  -- Zoolevel
            if (InputManager.Keyboard.KeyPushed(Keys.Z))
            {
                
                MoveToScreen(typeof(ZooLevel).FullName);
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
