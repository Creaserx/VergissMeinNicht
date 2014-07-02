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
            if (PlatformerCharacterBase.getInstance().X < (Sprite.X + 75) && PlatformerCharacterBase.getInstance().X > (Sprite.X - 75))
            {
                //--Transform Character
                if (InputManager.Keyboard.KeyPushed(Keys.E))
                {
                    if (PlatformerCharacterBase.isChild())                            
                    {
                        TheodorGrownUp Temp = new TheodorGrownUp();
                        Temp.X = PlatformerCharacterBase.getInstance().X;
                        Temp.Y = PlatformerCharacterBase.getInstance().Y;
                        PlatformerCharacterBase.updateinstance(Temp);
                    }
                    else
                    {
                        TheodorChild Temp = new TheodorChild();
                        Temp.X = PlatformerCharacterBase.getInstance().X;
                        Temp.Y = PlatformerCharacterBase.getInstance().Y;
                        PlatformerCharacterBase.updateinstance(Temp);
                    }
                }

                if (InputManager.Keyboard.KeyPushed(Keys.S))
                {

                }

                SaveText.Visible = true;
                InteractText.Visible = true;
                SaveText.Position.X = PlatformerCharacterBase.getInstance().Position.X;
                SaveText.Position.Y = PlatformerCharacterBase.getInstance().Y + 80;
                InteractText.Position.X = PlatformerCharacterBase.getInstance().Position.X;
                InteractText.Position.Y = PlatformerCharacterBase.getInstance().Y + 100;
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
