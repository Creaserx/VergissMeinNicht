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
	public partial class ZooLevel
	{

		void CustomInitialize()
		{
            // Create a rectnagle
            AxisAlignedRectangle rectangle = new AxisAlignedRectangle();
            rectangle.ScaleX = 800;
            rectangle.ScaleY = 50;
            //rectangle.Y = -50;

            // Add it to the ShapeCollection so the player can collide against it
            this.SolidCollisions.AxisAlignedRectangles.Add(rectangle);

            // Add the ShapeColleciton to the ShapeManager so it's visible
            SolidCollisions.AddToManagers();

            // Let's make the character appear on top of the rectangle:
            this.TheodorChildInstance.Y = 200;

            this.TeddyNormalInstance.TheodorChildInstance = this.TheodorChildInstance;
		}

		void CustomActivity(bool firstTimeCalled)
		{
            //Theodor Collisions & Movement
            TheodorChildInstance.CollideAgainst(SolidCollisions);
            TheodorChildInstance.DetermineMovementValues();

            //TeddyText();

            //Camera Movement following Theodor
            SpriteManager.Camera.XVelocity = TheodorChildInstance.X - SpriteManager.Camera.X;
            SpriteManager.Camera.YVelocity = TheodorChildInstance.Y - SpriteManager.Camera.Y;
		}

        /*void TeddyText()
        {
            //Makes Text Visible / Invisible in front of a Teddy
            if (TheodorChildInstance.X < (TeddyNormalInstance.X + 75) && TheodorChildInstance.X > (TeddyNormalInstance.X - 75))
            {
                SaveText.Visible = true;
                InteractText.Visible = true;
            }
            else
            {
                SaveText.Visible = false;
                InteractText.Visible = false;
            }

        }*/

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
