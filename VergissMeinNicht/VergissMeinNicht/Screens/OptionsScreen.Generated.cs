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
	public partial class OptionsScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private VergissMeinNicht.Entities.CheckBox SoundEffectCheckBox;
		private VergissMeinNicht.Entities.CheckBox SoundCheckBox;
		public event FlatRedBall.Gui.WindowEvent SoundEffectCheckBoxClick;
		public event FlatRedBall.Gui.WindowEvent SoundCheckBoxClick;

		public OptionsScreen()
			: base("OptionsScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			SoundEffectCheckBox = new VergissMeinNicht.Entities.CheckBox(ContentManagerName, false);
			SoundEffectCheckBox.Name = "SoundEffectCheckBox";
			SoundCheckBox = new VergissMeinNicht.Entities.CheckBox(ContentManagerName, false);
			SoundCheckBox.Name = "SoundCheckBox";
			
			
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
			SoundEffectCheckBox.AddToManagers(mLayer);
			SoundCheckBox.AddToManagers(mLayer);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				SoundEffectCheckBox.Activity();
				SoundCheckBox.Activity();
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
			
			if (SoundEffectCheckBox != null)
			{
				SoundEffectCheckBox.Destroy();
				SoundEffectCheckBox.Detach();
			}
			if (SoundCheckBox != null)
			{
				SoundCheckBox.Destroy();
				SoundCheckBox.Detach();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			SoundEffectCheckBox.Click += OnSoundEffectCheckBoxClick;
			SoundEffectCheckBox.Click += OnSoundEffectCheckBoxClickTunnel;
			SoundCheckBox.Click += OnSoundCheckBoxClick;
			SoundCheckBox.Click += OnSoundCheckBoxClickTunnel;
			if (SoundEffectCheckBox.Parent == null)
			{
				SoundEffectCheckBox.Y = -40f;
			}
			else
			{
				SoundEffectCheckBox.RelativeY = -40f;
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
			SoundEffectCheckBox.RemoveFromManagers();
			SoundCheckBox.RemoveFromManagers();
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				SoundEffectCheckBox.AssignCustomVariables(true);
				SoundCheckBox.AssignCustomVariables(true);
			}
			if (SoundEffectCheckBox.Parent == null)
			{
				SoundEffectCheckBox.Y = -40f;
			}
			else
			{
				SoundEffectCheckBox.RelativeY = -40f;
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			SoundEffectCheckBox.ConvertToManuallyUpdated();
			SoundCheckBox.ConvertToManuallyUpdated();
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
			VergissMeinNicht.Entities.CheckBox.LoadStaticContent(contentManagerName);
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
