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
		protected static FlatRedBall.Scene SceneFile;
		
		private VergissMeinNicht.Entities.Button MainMenuButton;
		private FlatRedBall.Graphics.Layer Layer2D;
		private VergissMeinNicht.Entities.TeddyNormal TeddyNormalInstance;
		private VergissMeinNicht.Entities.TeddyFuckedUp TeddyFuckedUpInstance;
		private VergissMeinNicht.Entities.TheodorChild TheodorChildInstance;
		private VergissMeinNicht.Entities.TheodorGrownUp TheodorGrownUpInstance;
		private VergissMeinNicht.Entities.TextArial SaveText;
		private VergissMeinNicht.Entities.TextArial InteractText;
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
			TeddyNormalInstance = new VergissMeinNicht.Entities.TeddyNormal(ContentManagerName, false);
			TeddyNormalInstance.Name = "TeddyNormalInstance";
			TeddyFuckedUpInstance = new VergissMeinNicht.Entities.TeddyFuckedUp(ContentManagerName, false);
			TeddyFuckedUpInstance.Name = "TeddyFuckedUpInstance";
			TheodorChildInstance = new VergissMeinNicht.Entities.TheodorChild(ContentManagerName, false);
			TheodorChildInstance.Name = "TheodorChildInstance";
			TheodorGrownUpInstance = new VergissMeinNicht.Entities.TheodorGrownUp(ContentManagerName, false);
			TheodorGrownUpInstance.Name = "TheodorGrownUpInstance";
			SaveText = new VergissMeinNicht.Entities.TextArial(ContentManagerName, false);
			SaveText.Name = "SaveText";
			InteractText = new VergissMeinNicht.Entities.TextArial(ContentManagerName, false);
			InteractText.Name = "InteractText";
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
			SceneFile.AddToManagers(mLayer);
			SpriteManager.AddLayer(Layer2D);
			Layer2D.UsePixelCoordinates();
			if (SpriteManager.Camera.Orthogonal)
			{
				Layer2D.LayerCameraSettings.OrthogonalWidth = FlatRedBall.SpriteManager.Camera.OrthogonalWidth;
				Layer2D.LayerCameraSettings.OrthogonalHeight = FlatRedBall.SpriteManager.Camera.OrthogonalHeight;
			}
			MainMenuButton.AddToManagers(Layer2D);
			TeddyNormalInstance.AddToManagers(mLayer);
			TeddyFuckedUpInstance.AddToManagers(mLayer);
			TheodorChildInstance.AddToManagers(mLayer);
			TheodorGrownUpInstance.AddToManagers(mLayer);
			SaveText.AddToManagers(mLayer);
			InteractText.AddToManagers(mLayer);
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
				TeddyNormalInstance.Activity();
				TeddyFuckedUpInstance.Activity();
				TheodorChildInstance.Activity();
				TheodorGrownUpInstance.Activity();
				SaveText.Activity();
				InteractText.Activity();
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
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				SceneFile.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				SceneFile.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				SceneFile = null;
			}
			else
			{
				SceneFile.MakeOneWay();
			}
			
			if (MainMenuButton != null)
			{
				MainMenuButton.Destroy();
				MainMenuButton.Detach();
			}
			if (Layer2D != null)
			{
				SpriteManager.RemoveLayer(Layer2D);
			}
			if (TeddyNormalInstance != null)
			{
				TeddyNormalInstance.Destroy();
				TeddyNormalInstance.Detach();
			}
			if (TeddyFuckedUpInstance != null)
			{
				TeddyFuckedUpInstance.Destroy();
				TeddyFuckedUpInstance.Detach();
			}
			if (TheodorChildInstance != null)
			{
				TheodorChildInstance.Destroy();
				TheodorChildInstance.Detach();
			}
			if (TheodorGrownUpInstance != null)
			{
				TheodorGrownUpInstance.Destroy();
				TheodorGrownUpInstance.Detach();
			}
			if (SaveText != null)
			{
				SaveText.Destroy();
				SaveText.Detach();
			}
			if (InteractText != null)
			{
				InteractText.Destroy();
				InteractText.Detach();
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
			if (MainMenuButton.Parent == null)
			{
				MainMenuButton.CopyAbsoluteToRelative();
				MainMenuButton.RelativeZ += -40;
				MainMenuButton.AttachTo(SpriteManager.Camera, false);
			}
			MainMenuButton.DisplayText = "MainMenu";
			if (MainMenuButton.Parent == null)
			{
				MainMenuButton.X = 300f;
			}
			else
			{
				MainMenuButton.RelativeX = 300f;
			}
			if (MainMenuButton.Parent == null)
			{
				MainMenuButton.Y = 260f;
			}
			else
			{
				MainMenuButton.RelativeY = 260f;
			}
			if (TeddyNormalInstance.Parent == null)
			{
				TeddyNormalInstance.X = -250f;
			}
			else
			{
				TeddyNormalInstance.RelativeX = -250f;
			}
			if (TeddyNormalInstance.Parent == null)
			{
				TeddyNormalInstance.Y = 250f;
			}
			else
			{
				TeddyNormalInstance.RelativeY = 250f;
			}
			SaveText.DisplayText = "Press S To Save";
			if (SaveText.Parent == null)
			{
				SaveText.X = -350f;
			}
			else
			{
				SaveText.RelativeX = -350f;
			}
			if (SaveText.Parent == null)
			{
				SaveText.Y = -100f;
			}
			else
			{
				SaveText.RelativeY = -100f;
			}
			InteractText.DisplayText = "Press E to Interact";
			if (InteractText.Parent == null)
			{
				InteractText.X = -350f;
			}
			else
			{
				InteractText.RelativeX = -350f;
			}
			if (InteractText.Parent == null)
			{
				InteractText.Y = -150f;
			}
			else
			{
				InteractText.RelativeY = -150f;
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
			TeddyNormalInstance.RemoveFromManagers();
			TeddyFuckedUpInstance.RemoveFromManagers();
			TheodorChildInstance.RemoveFromManagers();
			TheodorGrownUpInstance.RemoveFromManagers();
			SaveText.RemoveFromManagers();
			InteractText.RemoveFromManagers();
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
				TeddyNormalInstance.AssignCustomVariables(true);
				TeddyFuckedUpInstance.AssignCustomVariables(true);
				TheodorChildInstance.AssignCustomVariables(true);
				TheodorGrownUpInstance.AssignCustomVariables(true);
				SaveText.AssignCustomVariables(true);
				InteractText.AssignCustomVariables(true);
			}
			MainMenuButton.DisplayText = "MainMenu";
			if (MainMenuButton.Parent == null)
			{
				MainMenuButton.X = 300f;
			}
			else
			{
				MainMenuButton.RelativeX = 300f;
			}
			if (MainMenuButton.Parent == null)
			{
				MainMenuButton.Y = 260f;
			}
			else
			{
				MainMenuButton.RelativeY = 260f;
			}
			if (TeddyNormalInstance.Parent == null)
			{
				TeddyNormalInstance.X = -250f;
			}
			else
			{
				TeddyNormalInstance.RelativeX = -250f;
			}
			if (TeddyNormalInstance.Parent == null)
			{
				TeddyNormalInstance.Y = 250f;
			}
			else
			{
				TeddyNormalInstance.RelativeY = 250f;
			}
			SaveText.DisplayText = "Press S To Save";
			if (SaveText.Parent == null)
			{
				SaveText.X = -350f;
			}
			else
			{
				SaveText.RelativeX = -350f;
			}
			if (SaveText.Parent == null)
			{
				SaveText.Y = -100f;
			}
			else
			{
				SaveText.RelativeY = -100f;
			}
			InteractText.DisplayText = "Press E to Interact";
			if (InteractText.Parent == null)
			{
				InteractText.X = -350f;
			}
			else
			{
				InteractText.RelativeX = -350f;
			}
			if (InteractText.Parent == null)
			{
				InteractText.Y = -150f;
			}
			else
			{
				InteractText.RelativeY = -150f;
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			MainMenuButton.ConvertToManuallyUpdated();
			TeddyNormalInstance.ConvertToManuallyUpdated();
			TeddyFuckedUpInstance.ConvertToManuallyUpdated();
			TheodorChildInstance.ConvertToManuallyUpdated();
			TheodorGrownUpInstance.ConvertToManuallyUpdated();
			SaveText.ConvertToManuallyUpdated();
			InteractText.ConvertToManuallyUpdated();
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
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/zoolevel/scenefile.scnx", contentManagerName))
			{
			}
			SceneFile = FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/zoolevel/scenefile.scnx", contentManagerName);
			VergissMeinNicht.Entities.Button.LoadStaticContent(contentManagerName);
			VergissMeinNicht.Entities.TeddyNormal.LoadStaticContent(contentManagerName);
			VergissMeinNicht.Entities.TeddyFuckedUp.LoadStaticContent(contentManagerName);
			VergissMeinNicht.Entities.TheodorChild.LoadStaticContent(contentManagerName);
			VergissMeinNicht.Entities.TheodorGrownUp.LoadStaticContent(contentManagerName);
			VergissMeinNicht.Entities.TextArial.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}


	}
}
