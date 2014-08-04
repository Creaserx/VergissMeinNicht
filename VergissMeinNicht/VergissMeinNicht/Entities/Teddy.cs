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
using StateInterpolationPlugin;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using VergissMeinNicht.Screens;
using FlatRedBall.Graphics;

#endif
#endregion

namespace VergissMeinNicht.Entities
{
	public partial class Teddy
	{
        int tempLayer;
        
        private void CustomInitialize()
		{
            UI_Button_EInstance.SpriteInstance.Position.X = this.X + 50;
            UI_Button_EInstance.SpriteInstance.Position.Y = this.Y + 50;
            UI_Button_EInstance.SpriteInstance.YVelocity = 10;  //macht irgendwie nichts
		}

		private void CustomActivity()
		{
            TeddySwitch();
            string resultStringX = "E.X:" + this.Position.X.ToString();
            string resultStringY = "E.Y:" + this.Position.Y.ToString();
            FlatRedBall.Debugging.Debugger.Write(resultStringX + "\n" + resultStringY);
		}

        public void UpdateCharacterScale()
        {
            switch (tempLayer)
            {
                case 1:
                    PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale = 0.5f;
                    PlatformerCharacterBase.getInstance().Collision.Height = LevelBase.CollisionHeightLayerFront;
                    PlatformerCharacterBase.getInstance().Collision.Width = LevelBase.CollisionWidthLayerFront;
                    break;

                case 2:
                    PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale = 0.45f;
                    PlatformerCharacterBase.getInstance().Collision.Height = LevelBase.CollisionHeightLayerMid;
                    PlatformerCharacterBase.getInstance().Collision.Width = LevelBase.CollisionWidthLayerMid;
                    break;

                case 3:
                    PlatformerCharacterBase.getInstance().SpriteInstance.TextureScale = 0.4f;
                    PlatformerCharacterBase.getInstance().Collision.Height = LevelBase.CollisionHeightLayerBack;
                    PlatformerCharacterBase.getInstance().Collision.Width = LevelBase.CollisionWidthLayerMid;
                    break;

                default:

                    break;
            }
        }

        void TeddySwitch()
        {
            //Makes Text Visible / Invisible in front of a Teddy
            if (PlatformerCharacterBase.getInstance().X < (Sprite.X + 75) && PlatformerCharacterBase.getInstance().X > (Sprite.X - 75) && FlashInstance.Alpha == 0)
            {
                //--Transform Character
                if (InputManager.Keyboard.KeyPushed(Keys.E))
                {
                    tempLayer = LevelBase.CurrentLayer;

                    if (PlatformerCharacterBase.isChild())                            
                    {
                        if (FlashInstance.Alpha == 0)
                        {
                            FlashInstance.Visible = false;
                            FlashInstance.Alpha = 1;
                        }

                        FlashInstance.Visible = true;
                        FlashInstance.SpriteInstance
                            .Tween("Alpha")
                            .To(0)
                            .During(1)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);

                        TheodorGrownUp Temp = new TheodorGrownUp();
                        Temp.X = PlatformerCharacterBase.getInstance().X;
                        Temp.Y = PlatformerCharacterBase.getInstance().Y;
                        PlatformerCharacterBase.updateinstance(Temp);
                        PlatformerCharacterBase.UpdateCharacterValues();
                        UpdateCharacterScale();

                        
                    }
                    else
                    {
                        if (FlashInstance.Alpha == 0)
                        {
                            FlashInstance.Visible = false;
                            FlashInstance.Alpha = 1;
                        }

                        FlashInstance.Visible = true;
                        FlashInstance.SpriteInstance
                            .Tween("Alpha")
                            .To(0)
                            .During(1)
                            .Using(
                                FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear,
                                FlatRedBall.Glue.StateInterpolation.Easing.Out);                        

                        TheodorChild Temp = new TheodorChild();
                        Temp.X = PlatformerCharacterBase.getInstance().X;
                        Temp.Y = PlatformerCharacterBase.getInstance().Y;
                        PlatformerCharacterBase.updateinstance(Temp);
                        PlatformerCharacterBase.UpdateCharacterValues();
                        UpdateCharacterScale();

                    }
                                                          
                                           
                }

                if (InputManager.Keyboard.KeyPushed(Keys.S))
                {

                }

                UI_Button_EInstance.SpriteInstanceVisible = true;    

                   /* SaveText.Visible = true;
                    InteractText.Visible = true;
                    SaveText.Position.X = PlatformerCharacterBase.getInstance().Position.X + 100;
                    SaveText.Position.Y = PlatformerCharacterBase.getInstance().Y + 80;
                    InteractText.Position.X = PlatformerCharacterBase.getInstance().Position.X + 100;
                    InteractText.Position.Y = PlatformerCharacterBase.getInstance().Y + 100;
                     */           

                
            }
            else
            {
                UI_Button_EInstance.SpriteInstanceVisible = false;
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
