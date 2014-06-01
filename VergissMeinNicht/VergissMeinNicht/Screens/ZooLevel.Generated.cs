using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4 || WINDOWS_8
using Color = Microsoft.Xna.Framework.Color;
#elif FRB_MDX
using Color = System.Drawing.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using VergissMeinNicht.Entities;
using VergissMeinNicht.Entities.Enemies;
using VergissMeinNicht.Entities.Räume_Inhalt.Schlafraum;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Graphics;
using FlatRedBall.Math.Geometry;

namespace VergissMeinNicht.Screens
{
	public partial class ZooLevel : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private VergissMeinNicht.Entities.Button MainMenuButton;
		private FlatRedBall.Graphics.Layer Layer2D;
		private VergissMeinNicht.Entities.Theodor TheodorInstance;
		private FlatRedBall.Math.Geometry.ShapeCollection SolidCollisions;
		public event FlatRedBall.Gui.WindowEvent MainMenuButtonClick;

		public ZooLevel()
			: base("ZooLevel")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			MainMenuButton = new VergissMeinNicht.Entities.Button(ContentManagerName, false);
			MainMenuButton.Name = "MainMenuButton";
			Layer2D = new FlatRedBall.Graphics.Layer();
			Layer2D.Name = "Layer2D";
			TheodorInstance = new VergissMeinNicht.Entities.Theodor(ContentManagerName, false);
			TheodorInstance.Name = "TheodorInstance";
			SolidCollisions = new FlatRedBall.Math.Geometry.ShapeCollection();
			SolidCollisions.Name = "SolidCollisions";
			
			
			PostInitialize();
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			SpriteManager.AddLayer(Layer2D);
			Layer2D.UsePixelCoordinates();
			if (SpriteManager.Camera.Orthogonal)
			{
				Layer2D.LayerCameraSettings.OrthogonalWidth = FlatRedBall.SpriteManager.Camera.OrthogonalWidth;
				Layer2D.LayerCameraSettings.OrthogonalHeight = FlatRedBall.SpriteManager.Camera.OrthogonalHeight;
			}
			MainMenuButton.AddToManagers(Layer2D);
			TheodorInstance.AddToManagers(mLayer);
			SolidCollisions.AddToManagers();
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				MainMenuButton.Activity();
				TheodorInstance.Activity();
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			
			if (MainMenuButton != null)
			{
				MainMenuButton.Destroy();
				MainMenuButton.Detach();
			}
			if (Layer2D != null)
			{
				SpriteManager.RemoveLayer(Layer2D);
			}
			if (TheodorInstance != null)
			{
				TheodorInstance.Destroy();
				TheodorInstance.Detach();
			}
			if (SolidCollisions != null)
			{
				SolidCollisions.RemoveFromManagers(ContentManagerName != "Global");
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			MainMenuButton.Click += OnMainMenuButtonClick;
			MainMenuButton.Click += OnMainMenuButtonClickTunnel;
			MainMenuButton.DisplayText = "MainMenu";
			if (MainMenuButton.Parent == null)
			{
				MainMenuButton.Y = 260f;
			}
			else
			{
				MainMenuButton.RelativeY = 260f;
			}
			if (MainMenuButton.Parent == null)
			{
				MainMenuButton.X = 300f;
			}
			else
			{
				MainMenuButton.RelativeX = 300f;
			}
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			CameraSetup.ResetCamera(SpriteManager.Camera);
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			MainMenuButton.RemoveFromManagers();
			if (Layer2D != null)
			{
				SpriteManager.RemoveLayer(Layer2D);
			}
			TheodorInstance.RemoveFromManagers();
			if (SolidCollisions != null)
			{
				SolidCollisions.RemoveFromManagers(false);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				MainMenuButton.AssignCustomVariables(true);
				TheodorInstance.AssignCustomVariables(true);
			}
			MainMenuButton.DisplayText = "MainMenu";
			if (MainMenuButton.Parent == null)
			{
				MainMenuButton.Y = 260f;
			}
			else
			{
				MainMenuButton.RelativeY = 260f;
			}
			if (MainMenuButton.Parent == null)
			{
				MainMenuButton.X = 300f;
			}
			else
			{
				MainMenuButton.RelativeX = 300f;
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			MainMenuButton.ConvertToManuallyUpdated();
			TheodorInstance.ConvertToManuallyUpdated();
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			#if DEBUG
			if (contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			VergissMeinNicht.Entities.Button.LoadStaticContent(contentManagerName);
			VergissMeinNicht.Entities.Theodor.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			return null;
		}
		public static object GetFile (string memberName)
		{
			return null;
		}
		object GetMember (string memberName)
		{
			return null;
		}


	}
}