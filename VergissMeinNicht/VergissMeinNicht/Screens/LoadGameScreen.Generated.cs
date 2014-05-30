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

namespace VergissMeinNicht.Screens
{
	public partial class LoadGameScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private VergissMeinNicht.Entities.Button Save1;
		private VergissMeinNicht.Entities.Button Save2;
		private VergissMeinNicht.Entities.Button Save3;
		private VergissMeinNicht.Entities.Button BackButton;
		public event FlatRedBall.Gui.WindowEvent BackButtonClick;

		public LoadGameScreen()
			: base("LoadGameScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			Save1 = new VergissMeinNicht.Entities.Button(ContentManagerName, false);
			Save1.Name = "Save1";
			Save2 = new VergissMeinNicht.Entities.Button(ContentManagerName, false);
			Save2.Name = "Save2";
			Save3 = new VergissMeinNicht.Entities.Button(ContentManagerName, false);
			Save3.Name = "Save3";
			BackButton = new VergissMeinNicht.Entities.Button(ContentManagerName, false);
			BackButton.Name = "BackButton";
			
			
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
			Save1.AddToManagers(mLayer);
			Save2.AddToManagers(mLayer);
			Save3.AddToManagers(mLayer);
			BackButton.AddToManagers(mLayer);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				Save1.Activity();
				Save2.Activity();
				Save3.Activity();
				BackButton.Activity();
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
			
			if (Save1 != null)
			{
				Save1.Destroy();
				Save1.Detach();
			}
			if (Save2 != null)
			{
				Save2.Destroy();
				Save2.Detach();
			}
			if (Save3 != null)
			{
				Save3.Destroy();
				Save3.Detach();
			}
			if (BackButton != null)
			{
				BackButton.Destroy();
				BackButton.Detach();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			BackButton.Click += OnBackButtonClick;
			BackButton.Click += OnBackButtonClickTunnel;
			Save1.DisplayText = "Save1";
			if (Save1.Parent == null)
			{
				Save1.Y = 40f;
			}
			else
			{
				Save1.RelativeY = 40f;
			}
			Save2.DisplayText = "Save 2";
			Save3.DisplayText = "Save 3";
			if (Save3.Parent == null)
			{
				Save3.Y = -40f;
			}
			else
			{
				Save3.RelativeY = -40f;
			}
			BackButton.DisplayText = "Back";
			if (BackButton.Parent == null)
			{
				BackButton.Y = -120f;
			}
			else
			{
				BackButton.RelativeY = -120f;
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
			Save1.RemoveFromManagers();
			Save2.RemoveFromManagers();
			Save3.RemoveFromManagers();
			BackButton.RemoveFromManagers();
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				Save1.AssignCustomVariables(true);
				Save2.AssignCustomVariables(true);
				Save3.AssignCustomVariables(true);
				BackButton.AssignCustomVariables(true);
			}
			Save1.DisplayText = "Save1";
			if (Save1.Parent == null)
			{
				Save1.Y = 40f;
			}
			else
			{
				Save1.RelativeY = 40f;
			}
			Save2.DisplayText = "Save 2";
			Save3.DisplayText = "Save 3";
			if (Save3.Parent == null)
			{
				Save3.Y = -40f;
			}
			else
			{
				Save3.RelativeY = -40f;
			}
			BackButton.DisplayText = "Back";
			if (BackButton.Parent == null)
			{
				BackButton.Y = -120f;
			}
			else
			{
				BackButton.RelativeY = -120f;
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			Save1.ConvertToManuallyUpdated();
			Save2.ConvertToManuallyUpdated();
			Save3.ConvertToManuallyUpdated();
			BackButton.ConvertToManuallyUpdated();
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
