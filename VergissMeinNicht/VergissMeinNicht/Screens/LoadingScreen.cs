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
	public partial class LoadingScreen
	{
        double startTime = TimeManager.CurrentTime;

		void CustomInitialize()
		{
            // Size the sprite to match the size of the camera
            SpriteInstance.ScaleX = FlatRedBall.SpriteManager.Camera.RelativeXEdgeAt(SpriteInstance.Z);
            SpriteInstance.ScaleY = FlatRedBall.SpriteManager.Camera.RelativeYEdgeAt(SpriteInstance.Z);  

		}


		void CustomActivity(bool firstTimeCalled)
		{
            Microsoft.Xna.Framework.Media.MediaPlayer.Volume = 1 - ((float)TimeManager.SecondsSince(startTime) / 2f);

            if (this.AsyncLoadingState == FlatRedBall.Screens.AsyncLoadingState.NotStarted)
            {
                switch(Manager.LoadLevel)
                {
                    case 0:      StartAsyncLoad(typeof(Empfang).FullName);
                        break;
                    case 1:      StartAsyncLoad(typeof(Flur).FullName);
                        break;
                        
                }      
            }
            else if (this.AsyncLoadingState == FlatRedBall.Screens.AsyncLoadingState.Done)
            {
                IsActivityFinished = true;
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
