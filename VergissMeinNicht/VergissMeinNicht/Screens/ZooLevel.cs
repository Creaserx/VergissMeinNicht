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

            

            TheodorChild Temp = new VergissMeinNicht.Entities.TheodorChild(ContentManagerName, false);
            Temp.Name = "TheodorChildInstance";
            PlatformerCharacterBase.updateinstance(Temp);
            Temp.AddToManagers(mLayer);
            



            // Let's make the character appear on top of the rectangle:
            PlatformerCharacterBase.getInstance().Y = 200;

		}

		void CustomActivity(bool firstTimeCalled)
		{
            //Theodor Collisions & Movement
            PlatformerCharacterBase.getInstance().CollideAgainst(SolidCollisions);
            PlatformerCharacterBase.getInstance().DetermineMovementValues();

            if (!IsPaused)
            {
                PlatformerCharacterBase.getInstance().Activity();
            }


            //Camera Movement following Theodor
            SpriteManager.Camera.XVelocity = PlatformerCharacterBase.getInstance().X - SpriteManager.Camera.X;
            SpriteManager.Camera.YVelocity = PlatformerCharacterBase.getInstance().Y - SpriteManager.Camera.Y;
		}


		void CustomDestroy()
		{
            PlatformerCharacterBase.updateinstance(null);
		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
