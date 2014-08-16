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
	public partial class OptionsScreen
	{

		void CustomInitialize()
		{  


            // Size the sprite to match the size of the camera
            SpriteInstance.ScaleX = FlatRedBall.SpriteManager.Camera.RelativeXEdgeAt(SpriteInstance.Z);
            SpriteInstance.ScaleY = FlatRedBall.SpriteManager.Camera.RelativeYEdgeAt(SpriteInstance.Z);
		}

		void CustomActivity(bool firstTimeCalled)
		{
            if (this.SoundEffectCheckBox.SpriteInstanceCurrentChainName == "Checked") Manager.MusicOn = true;
            else Manager.MusicOn = false;

            if (this.SoundEffectCheckBox.SpriteInstanceCurrentChainName == "Checked") Manager.SoundOn = true;
            else Manager.SoundOn = false;

            if (!Manager.MusicOn) Microsoft.Xna.Framework.Media.MediaPlayer.Volume = 0;
            else Microsoft.Xna.Framework.Media.MediaPlayer.Volume = 1;

		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
