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
	public partial class MainMenu
	{
  

		void CustomInitialize()
		{
            // Size the sprite to match the size of the camera
            SpriteInstance.ScaleX = FlatRedBall.SpriteManager.Camera.RelativeXEdgeAt(SpriteInstance.Z);
            SpriteInstance.ScaleY = FlatRedBall.SpriteManager.Camera.RelativeYEdgeAt(SpriteInstance.Z);
            Manager.LoadLevel = 0;
		}

		void CustomActivity(bool firstTimeCalled)
		{   
            // Z  -- Zoolevel
            if (InputManager.Keyboard.KeyPushed(Keys.Z))
            {
                
                MoveToScreen(typeof(ZooLevel).FullName);
            }

            
            if (InputManager.Mouse.X >= (NewGameButton.X+500) && InputManager.Mouse.X < (NewGameButton.X + 900) && 
                            InputManager.Mouse.Y > (NewGameButton.Y + 275) && InputManager.Mouse.Y < (NewGameButton.Y + 360) )
            {
                SpriteInstance.Texture = HauptmenüHighlightNewGame;
                SpriteInstance.ScaleX = FlatRedBall.SpriteManager.Camera.RelativeXEdgeAt(SpriteInstance.Z);
                SpriteInstance.ScaleY = FlatRedBall.SpriteManager.Camera.RelativeYEdgeAt(SpriteInstance.Z);      
            }
            else if (InputManager.Mouse.X >= (NewGameButton.X + 500) && InputManager.Mouse.X < (NewGameButton.X + 900) &&
                            InputManager.Mouse.Y > (NewGameButton.Y + 350) && InputManager.Mouse.Y < (NewGameButton.Y + 420))
            {
                SpriteInstance.Texture = HauptmenüHighlightLoadGame;
                SpriteInstance.ScaleX = FlatRedBall.SpriteManager.Camera.RelativeXEdgeAt(SpriteInstance.Z);
                SpriteInstance.ScaleY = FlatRedBall.SpriteManager.Camera.RelativeYEdgeAt(SpriteInstance.Z);
            }
            else if (InputManager.Mouse.X >= (NewGameButton.X + 500) && InputManager.Mouse.X < (NewGameButton.X + 900) &&
                            InputManager.Mouse.Y > (NewGameButton.Y + 400) && InputManager.Mouse.Y < (NewGameButton.Y + 480))
            {
                SpriteInstance.Texture = HauptmenüHighlightOptions;
                SpriteInstance.ScaleX = FlatRedBall.SpriteManager.Camera.RelativeXEdgeAt(SpriteInstance.Z);
                SpriteInstance.ScaleY = FlatRedBall.SpriteManager.Camera.RelativeYEdgeAt(SpriteInstance.Z);
            }
            else if (InputManager.Mouse.X >= (NewGameButton.X + 500) && InputManager.Mouse.X < (NewGameButton.X + 900) &&
                            InputManager.Mouse.Y > (NewGameButton.Y + 500) && InputManager.Mouse.Y < (NewGameButton.Y + 570))
            {
                SpriteInstance.Texture = HauptmenüHighlightExit;
                SpriteInstance.ScaleX = FlatRedBall.SpriteManager.Camera.RelativeXEdgeAt(SpriteInstance.Z);
                SpriteInstance.ScaleY = FlatRedBall.SpriteManager.Camera.RelativeYEdgeAt(SpriteInstance.Z);
            }
            else
            {
                SpriteInstance.Texture = HauptmenüFinal;
                SpriteInstance.ScaleX = FlatRedBall.SpriteManager.Camera.RelativeXEdgeAt(SpriteInstance.Z);
                SpriteInstance.ScaleY = FlatRedBall.SpriteManager.Camera.RelativeYEdgeAt(SpriteInstance.Z);      
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
