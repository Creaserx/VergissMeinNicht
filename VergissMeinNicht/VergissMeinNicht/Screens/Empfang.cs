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
	public partial class Empfang
	{
        float BlumeStartPositionY;
        float BlumeVelocity = 10;

		public override void CustomInitialize()
		{
            base.CustomInitialize();
            VisibilityInit();

            BlumeStartPositionY = BlumeInstance.Y;
            BlumeInstance.YVelocity = BlumeVelocity;
            Blume_SchattenInstance.Y = BlumeStartPositionY - 40;

		}

        public override void CustomActivity(bool firstTimeCalled)
		{
            SwitchBlock();  //Blocken der Switch-Function

            base.CustomActivity(firstTimeCalled);
            CollisionActivity();

            // Background Change
            if (PlatformerCharacterBase.isChild()) Background_creepy.Visible = true;
            else Background_creepy.Visible = false;

            // reset Holes
            if (InputManager.Keyboard.KeyPushed(Keys.R)) for (int i = HoleList.Count - 1; i > -1; i--) HoleList[i].SpriteInstance.Visible = false;

            ControllFlower();
            CameraMovement();   
        }

        public override void CustomDestroy()
		{
            base.CustomDestroy();

		}

        static void CustomLoadStaticContent(string contentManagerName)
        {

            
        }

        public void SwitchBlock()
        {
            DisableLayerBack = false;
            DisableLayers = false;
            if (PlatformerCharacterBase.getInstance().X < -345) DisableLayerBack = true;
            else if (PlatformerCharacterBase.getInstance().X > 685) DisableLayerBack = true;
        }

        void CameraMovement()
        {
            SpriteManager.Camera.MinimumX = -310;
            SpriteManager.Camera.MaximumX = 310;
        }

        void ControllFlower()
        {
            if (BlumeInstance.Y >= BlumeStartPositionY + 15) BlumeInstance.YVelocity = -BlumeVelocity;
            else if (BlumeInstance.Y <= BlumeStartPositionY - 15) BlumeInstance.YVelocity = BlumeVelocity;

            if (PlatformerCharacterBase.isChild()) BlumeInstance.Visible = true ;
            else BlumeInstance.Visible = false;
        }

        public void CollisionActivity()
        {

            PlatformerCharacterBase.getInstance().Collision.CollideAgainstMove(Layer1, 0, 1);  //Kollision mit Rändern
            if (CurrentLayer == 3) PlatformerCharacterBase.getInstance().Collision.CollideAgainstMove(Layer3, 0, 1);  //Kollision auf Layer 3

            //Collision mit Holes
            for (int i = HoleList.Count - 1; i > -1; i--)
            {
                if (PlatformerCharacterBase.getInstance().Collision.CollideAgainst(HoleList[i].Collision) && CurrentLayer == HoleList[i].Layer)
                {
                    HoleList[i].SpriteInstance.Visible = true;
                    FallInHole();
                }
            }
  
            // LevelReset WA
            //if (PlatformerCharacterBase.getInstance().Y < 85 && Boden.Y == -300) MoveToScreen(typeof(Empfang).FullName);
        }



        void FallInHole()
        {
            //Boden.Y = -300;
            // SolidCollisions.AxisAlignedRectangles.Remove(Boden);   --Funktioniert theoretisch, aber ein "Boden" ist immer noch da.

        }

        //Initialize Visibility
        public void VisibilityInit()
        {
            Layer1.Visible = false;
            Layer3.Visible = false;

            Background_creepy.Visible = true;
            if (CollisionsVisible) CollisionVisibilityEmpfang();
   
        }
    
        //Collisions Visible machen
        public void CollisionVisibilityEmpfang()
        {
            LayerFront.Visible = true;
            LayerBack.Visible = true;

            for (int i = HoleList.Count - 1; i > -1; i--) HoleList[i].Collision.Visible = true;   // Hole-Collision Visible machen


        }


	}
}
