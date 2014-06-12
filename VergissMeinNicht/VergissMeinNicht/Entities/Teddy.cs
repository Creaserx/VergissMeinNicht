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

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

#endif
#endregion

namespace VergissMeinNicht.Entities
{
	public partial class Teddy
	{
        public TheodorChild TheodorChildInstance = null;
        public TheodorGrownUp TheodorGrownUpInstance = null;
        
        private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{

            TeddyText();
		}

        void TeddyText()
        {
            //Makes Text Visible / Invisible in front of a Teddy
            if (TheodorChildInstance.X < (Sprite.X + 75) && TheodorChildInstance.X > (Sprite.X - 75) || TheodorGrownUpInstance.X < (Sprite.X + 75) && TheodorGrownUpInstance.X > (Sprite.X - 75))
            {
                if (InputManager.Keyboard.KeyPushed(Keys.E))
                {
                    TheodorGrownUpInstance = new TheodorGrownUp();
                    this.TheodorGrownUpInstance.X = TheodorChildInstance.X;
                    this.TheodorGrownUpInstance.Y = TheodorChildInstance.Y;                    
                    TheodorChildInstance.Destroy();                    
                    TheodorChildInstance = null;
                }

                if (InputManager.Keyboard.KeyPushed(Keys.S))
                {

                }

                SaveText.Visible = true;
                InteractText.Visible = true;
            }
            else
            {
                SaveText.Visible = false;
                InteractText.Visible = false;
            }

        }

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
