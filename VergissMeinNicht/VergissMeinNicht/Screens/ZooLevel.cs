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
using VergissMeinNicht.Entities;
#endif
#endregion

namespace VergissMeinNicht.Screens
{
	public partial class ZooLevel
	{

		public override void CustomInitialize()
		{
            base.CustomInitialize();

            // Create a rectangle
            AxisAlignedRectangle rectangle = new AxisAlignedRectangle();
            rectangle.ScaleX = 800;
            rectangle.ScaleY = 50;
            //rectangle.Y = -50;

            // Add it to the ShapeCollection so the player can collide against it
            this.SolidCollisions.AxisAlignedRectangles.Add(rectangle);

            // Add the ShapeColleciton to the ShapeManager so it's visible
            SolidCollisions.AddToManagers();


            // Make the character appear on top of the rectangle:
            PlatformerCharacterBase.getInstance().Y = 200;

		}

		public override void CustomActivity(bool firstTimeCalled)
		{
            base.CustomActivity(firstTimeCalled);

            //Theodor Collisions
            PlatformerCharacterBase.getInstance().CollideAgainst(SolidCollisions);

        }


        public override void CustomDestroy()
		{
            base.CustomDestroy();
		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
