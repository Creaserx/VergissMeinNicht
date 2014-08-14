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
        bool LastDirectionRight;
        public static bool DisableTeddy = false;

        private void CustomInitialize()
		{

            if (!PlatformerCharacterBase.isChild()) this.Sprite.Texture = creepyteddy;
		}

		private void CustomActivity()
		{
            if (!DisableTeddy) TeddySwitch();
            DisableInput();
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

        void DisableInput()
        {
            if (FlashInstance.Alpha != 0) 
            {      
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Up);
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Down);
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Left);
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Right);
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Space);
                InputManager.Keyboard.IgnoreKeyForOneFrame(Keys.Escape);
            }
        }

        public void PositionTeddy(float x, float y)
        {
            this.X = x;
            this.Y = y;
            UI_Button_EInstance.PositionUI_Button(this.X + 50, this.Y + 50, UI_Button_EInstance.Y);           
        }
        void TeddySwitch()
        {
            //Makes Button Visible / Invisible in front of a Teddy
            if (PlatformerCharacterBase.getInstance().X < (Sprite.X + 75) && PlatformerCharacterBase.getInstance().X > (Sprite.X - 75) && FlashInstance.Alpha == 0)
            {
                //--Transform Character
                if (InputManager.Keyboard.KeyPushed(Keys.E))
                {
                    // aktuelle Blickrichtung speichern
                    if (PlatformerCharacterBase.getInstance().DirectionFacing == PlatformerCharacterBase.LeftOrRight.Right)
                        LastDirectionRight = true;
                    else
                        LastDirectionRight = false;
                 
                    tempLayer = LevelBase.CurrentLayer;

                    if (PlatformerCharacterBase.isChild() && Manager.LoadLevel != 0)                            
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

                        if (LastDirectionRight) PlatformerCharacterBase.getInstance().DirectionFacing = PlatformerCharacterBase.LeftOrRight.Right;  //Blickrichtung auf Rechts setzen, falls nötig

                        this.Sprite.Texture = creepyteddy;  
                        
                    }
                    else if (!PlatformerCharacterBase.isChild())
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
                        Temp.Y = PlatformerCharacterBase.getInstance().Y - 140;
                        PlatformerCharacterBase.updateinstance(Temp);
                        PlatformerCharacterBase.UpdateCharacterValues();
                        UpdateCharacterScale();

                        if (LastDirectionRight) PlatformerCharacterBase.getInstance().DirectionFacing = PlatformerCharacterBase.LeftOrRight.Right; //Blickrichtung auf Rechts setzen, falls nötig

                        this.Sprite.Texture = teddy;
                    }                          
                }

                if (PlatformerCharacterBase.isChild() && Manager.LoadLevel == 0) UI_Button_EInstance.SpriteInstanceVisible = false;
                else UI_Button_EInstance.SpriteInstanceVisible = true;   // E-Button einblenden                   
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
