
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using VergissMeinNicht.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using VergissMeinNicht.Entities;
using VergissMeinNicht.Entities.Enemies;
using VergissMeinNicht.Entities.Räume_Inhalt.Empfang;
using VergissMeinNicht.Entities.Räume_Inhalt.Schlafraum;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;

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
#endif

#if FRB_XNA && !MONODROID
using Model = Microsoft.Xna.Framework.Graphics.Model;
#endif

namespace VergissMeinNicht.Entities
{
	public partial class OkCancel : PositionedObject, IDestroyable, IVisible
	{
        // This is made global so that static lazy-loaded content can access it.
        public static string ContentManagerName
        {
            get;
            set;
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		
		private VergissMeinNicht.Entities.Frame FrameInstance;
		private VergissMeinNicht.Entities.Button OkButton;
		private VergissMeinNicht.Entities.Button CancelButton;
		private VergissMeinNicht.Entities.TextArial TextArialInstance;
		public event FlatRedBall.Gui.WindowEvent OkClick;
		public event FlatRedBall.Gui.WindowEvent CancelClick;
		public event EventHandler BeforeVisibleSet;
		public event EventHandler AfterVisibleSet;
		protected bool mVisible = true;
		public virtual bool Visible
		{
			get
			{
				return mVisible;
			}
			set
			{
				if (BeforeVisibleSet != null)
				{
					BeforeVisibleSet(this, null);
				}
				mVisible = value;
				if (AfterVisibleSet != null)
				{
					AfterVisibleSet(this, null);
				}
			}
		}
		public bool IgnoresParentVisibility { get; set; }
		public bool AbsoluteVisible
		{
			get
			{
				return Visible && (Parent == null || IgnoresParentVisibility || Parent is IVisible == false || (Parent as IVisible).AbsoluteVisible);
			}
		}
		IVisible IVisible.Parent
		{
			get
			{
				if (this.Parent != null && this.Parent is IVisible)
				{
					return this.Parent as IVisible;
				}
				else
				{
					return null;
				}
			}
		}
		protected Layer LayerProvidedByContainer = null;

        public OkCancel()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public OkCancel(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public OkCancel(string contentManagerName, bool addToManagers) :
			base()
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);

		}

		protected virtual void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			FrameInstance = new VergissMeinNicht.Entities.Frame(ContentManagerName, false);
			FrameInstance.Name = "FrameInstance";
			OkButton = new VergissMeinNicht.Entities.Button(ContentManagerName, false);
			OkButton.Name = "OkButton";
			CancelButton = new VergissMeinNicht.Entities.Button(ContentManagerName, false);
			CancelButton.Name = "CancelButton";
			TextArialInstance = new VergissMeinNicht.Entities.TextArial(ContentManagerName, false);
			TextArialInstance.Name = "TextArialInstance";
			
			PostInitialize();
			if (addToManagers)
			{
				AddToManagers(null);
			}


		}

// Generated AddToManagers
		public virtual void ReAddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			FrameInstance.ReAddToManagers(LayerProvidedByContainer);
			OkButton.ReAddToManagers(LayerProvidedByContainer);
			CancelButton.ReAddToManagers(LayerProvidedByContainer);
			TextArialInstance.ReAddToManagers(LayerProvidedByContainer);
		}
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			FrameInstance.AddToManagers(LayerProvidedByContainer);
			OkButton.AddToManagers(LayerProvidedByContainer);
			CancelButton.AddToManagers(LayerProvidedByContainer);
			TextArialInstance.AddToManagers(LayerProvidedByContainer);
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();
		}

		public virtual void Activity()
		{
			// Generated Activity
			
			FrameInstance.Activity();
			OkButton.Activity();
			CancelButton.Activity();
			TextArialInstance.Activity();
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			
			if (FrameInstance != null)
			{
				FrameInstance.Destroy();
				FrameInstance.Detach();
			}
			if (OkButton != null)
			{
				OkButton.Destroy();
				OkButton.Detach();
			}
			if (CancelButton != null)
			{
				CancelButton.Destroy();
				CancelButton.Detach();
			}
			if (TextArialInstance != null)
			{
				TextArialInstance.Destroy();
				TextArialInstance.Detach();
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			OkButton.Click += OnOkClick;
			OkButton.Click += OnOkClickTunnel;
			CancelButton.Click += OnCancelClick;
			CancelButton.Click += OnCancelClickTunnel;
			if (FrameInstance.Parent == null)
			{
				FrameInstance.CopyAbsoluteToRelative();
				FrameInstance.AttachTo(this, false);
			}
			FrameInstance.ScaleX = 96f;
			FrameInstance.ScaleY = 64f;
			if (OkButton.Parent == null)
			{
				OkButton.CopyAbsoluteToRelative();
				OkButton.AttachTo(this, false);
			}
			OkButton.DisplayText = "OK";
			OkButton.ScaleX = 65f;
			if (OkButton.Parent == null)
			{
				OkButton.Z = 1f;
			}
			else
			{
				OkButton.RelativeZ = 1f;
			}
			if (CancelButton.Parent == null)
			{
				CancelButton.CopyAbsoluteToRelative();
				CancelButton.AttachTo(this, false);
			}
			CancelButton.DisplayText = "Cancel";
			CancelButton.ScaleX = 65f;
			if (CancelButton.Parent == null)
			{
				CancelButton.Y = -40f;
			}
			else
			{
				CancelButton.RelativeY = -40f;
			}
			if (CancelButton.Parent == null)
			{
				CancelButton.Z = 1f;
			}
			else
			{
				CancelButton.RelativeZ = 1f;
			}
			if (TextArialInstance.Parent == null)
			{
				TextArialInstance.CopyAbsoluteToRelative();
				TextArialInstance.AttachTo(this, false);
			}
			TextArialInstance.DisplayText = "Do you really want to exit?";
			if (TextArialInstance.Parent == null)
			{
				TextArialInstance.Y = 40f;
			}
			else
			{
				TextArialInstance.RelativeY = 40f;
			}
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp (Layer layerToAddTo)
		{
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			SpriteManager.ConvertToManuallyUpdated(this);
			FrameInstance.RemoveFromManagers();
			OkButton.RemoveFromManagers();
			CancelButton.RemoveFromManagers();
			TextArialInstance.RemoveFromManagers();
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				FrameInstance.AssignCustomVariables(true);
				OkButton.AssignCustomVariables(true);
				CancelButton.AssignCustomVariables(true);
				TextArialInstance.AssignCustomVariables(true);
			}
			FrameInstance.ScaleX = 96f;
			FrameInstance.ScaleY = 64f;
			OkButton.DisplayText = "OK";
			OkButton.ScaleX = 65f;
			if (OkButton.Parent == null)
			{
				OkButton.Z = 1f;
			}
			else
			{
				OkButton.RelativeZ = 1f;
			}
			CancelButton.DisplayText = "Cancel";
			CancelButton.ScaleX = 65f;
			if (CancelButton.Parent == null)
			{
				CancelButton.Y = -40f;
			}
			else
			{
				CancelButton.RelativeY = -40f;
			}
			if (CancelButton.Parent == null)
			{
				CancelButton.Z = 1f;
			}
			else
			{
				CancelButton.RelativeZ = 1f;
			}
			TextArialInstance.DisplayText = "Do you really want to exit?";
			if (TextArialInstance.Parent == null)
			{
				TextArialInstance.Y = 40f;
			}
			else
			{
				TextArialInstance.RelativeY = 40f;
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			FrameInstance.ConvertToManuallyUpdated();
			OkButton.ConvertToManuallyUpdated();
			CancelButton.ConvertToManuallyUpdated();
			TextArialInstance.ConvertToManuallyUpdated();
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
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
			bool registerUnload = false;
			if (LoadedContentManagers.Contains(contentManagerName) == false)
			{
				LoadedContentManagers.Add(contentManagerName);
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("OkCancelStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			VergissMeinNicht.Entities.Frame.LoadStaticContent(contentManagerName);
			VergissMeinNicht.Entities.Button.LoadStaticContent(contentManagerName);
			VergissMeinNicht.Entities.TextArial.LoadStaticContent(contentManagerName);
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("OkCancelStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static void UnloadStaticContent ()
		{
			if (LoadedContentManagers.Count != 0)
			{
				LoadedContentManagers.RemoveAt(0);
				mRegisteredUnloads.RemoveAt(0);
			}
			if (LoadedContentManagers.Count == 0)
			{
			}
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
		protected bool mIsPaused;
		public override void Pause (FlatRedBall.Instructions.InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(this);
			FrameInstance.SetToIgnorePausing();
			OkButton.SetToIgnorePausing();
			CancelButton.SetToIgnorePausing();
			TextArialInstance.SetToIgnorePausing();
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			FrameInstance.MoveToLayer(layerToMoveTo);
			OkButton.MoveToLayer(layerToMoveTo);
			CancelButton.MoveToLayer(layerToMoveTo);
			TextArialInstance.MoveToLayer(layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
		public static class OkCancelExtensionMethods
	{
		public static void SetVisible (this PositionedObjectList<OkCancel> list, bool value)
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i].Visible = value;
			}
		}
	}

	
}
