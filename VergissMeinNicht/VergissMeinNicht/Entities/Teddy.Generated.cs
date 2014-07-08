
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using VergissMeinNicht.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using VergissMeinNicht.Entities;
using VergissMeinNicht.Entities.Enemies;
using VergissMeinNicht.Entities.Räume_Inhalt.Schlafraum;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

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
	public partial class Teddy : PositionedObject, IDestroyable
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
		protected static Microsoft.Xna.Framework.Graphics.Texture2D teddy_normal;
		protected static Microsoft.Xna.Framework.Graphics.Texture2D teddy_fucked;
		
		private FlatRedBall.Sprite Sprite;
		private VergissMeinNicht.Entities.TextArial SaveText;
		private VergissMeinNicht.Entities.TextArial InteractText;
		private VergissMeinNicht.Entities.Flash FlashInstance;
		public bool SpriteVisible
		{
			get
			{
				return Sprite.Visible;
			}
			set
			{
				Sprite.Visible = value;
			}
		}
		protected Layer LayerProvidedByContainer = null;

        public Teddy()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public Teddy(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Teddy(string contentManagerName, bool addToManagers) :
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
			Sprite = new FlatRedBall.Sprite();
			Sprite.Name = "Sprite";
			SaveText = new VergissMeinNicht.Entities.TextArial(ContentManagerName, false);
			SaveText.Name = "SaveText";
			InteractText = new VergissMeinNicht.Entities.TextArial(ContentManagerName, false);
			InteractText.Name = "InteractText";
			FlashInstance = new VergissMeinNicht.Entities.Flash(ContentManagerName, false);
			FlashInstance.Name = "FlashInstance";
			
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
			SpriteManager.AddToLayer(Sprite, LayerProvidedByContainer);
			SaveText.ReAddToManagers(LayerProvidedByContainer);
			InteractText.ReAddToManagers(LayerProvidedByContainer);
			FlashInstance.ReAddToManagers(LayerProvidedByContainer);
		}
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			SpriteManager.AddToLayer(Sprite, LayerProvidedByContainer);
			SaveText.AddToManagers(LayerProvidedByContainer);
			InteractText.AddToManagers(LayerProvidedByContainer);
			FlashInstance.AddToManagers(LayerProvidedByContainer);
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();
		}

		public virtual void Activity()
		{
			// Generated Activity
			
			SaveText.Activity();
			InteractText.Activity();
			FlashInstance.Activity();
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			
			if (Sprite != null)
			{
				SpriteManager.RemoveSprite(Sprite);
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
			if (FlashInstance != null)
			{
				FlashInstance.Destroy();
				FlashInstance.Detach();
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (Sprite.Parent == null)
			{
				Sprite.CopyAbsoluteToRelative();
				Sprite.AttachTo(this, false);
			}
			Sprite.Texture = teddy_normal;
			Sprite.TextureScale = 1f;
			SaveText.DisplayText = "Press S To Save";
			if (SaveText.Parent == null)
			{
				SaveText.X = -150f;
			}
			else
			{
				SaveText.RelativeX = -150f;
			}
			if (SaveText.Parent == null)
			{
				SaveText.Y = -300f;
			}
			else
			{
				SaveText.RelativeY = -300f;
			}
			InteractText.DisplayText = "Press E to Interact";
			if (InteractText.Parent == null)
			{
				InteractText.X = -150f;
			}
			else
			{
				InteractText.RelativeX = -150f;
			}
			if (InteractText.Parent == null)
			{
				InteractText.Y = -350f;
			}
			else
			{
				InteractText.RelativeY = -350f;
			}
			if (FlashInstance.Parent == null)
			{
				FlashInstance.CopyAbsoluteToRelative();
				FlashInstance.AttachTo(this, false);
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
			if (Sprite != null)
			{
				SpriteManager.RemoveSpriteOneWay(Sprite);
			}
			SaveText.RemoveFromManagers();
			InteractText.RemoveFromManagers();
			FlashInstance.RemoveFromManagers();
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				SaveText.AssignCustomVariables(true);
				InteractText.AssignCustomVariables(true);
				FlashInstance.AssignCustomVariables(true);
			}
			Sprite.Texture = teddy_normal;
			Sprite.TextureScale = 1f;
			SaveText.DisplayText = "Press S To Save";
			if (SaveText.Parent == null)
			{
				SaveText.X = -150f;
			}
			else
			{
				SaveText.RelativeX = -150f;
			}
			if (SaveText.Parent == null)
			{
				SaveText.Y = -300f;
			}
			else
			{
				SaveText.RelativeY = -300f;
			}
			InteractText.DisplayText = "Press E to Interact";
			if (InteractText.Parent == null)
			{
				InteractText.X = -150f;
			}
			else
			{
				InteractText.RelativeX = -150f;
			}
			if (InteractText.Parent == null)
			{
				InteractText.Y = -350f;
			}
			else
			{
				InteractText.RelativeY = -350f;
			}
			SpriteVisible = true;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			SpriteManager.ConvertToManuallyUpdated(Sprite);
			SaveText.ConvertToManuallyUpdated();
			InteractText.ConvertToManuallyUpdated();
			FlashInstance.ConvertToManuallyUpdated();
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("TeddyStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/teddynormal/teddy_normal.png", ContentManagerName))
				{
					registerUnload = true;
				}
				teddy_normal = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/teddynormal/teddy_normal.png", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/teddyfuckedup/teddy_fucked.png", ContentManagerName))
				{
					registerUnload = true;
				}
				teddy_fucked = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/teddyfuckedup/teddy_fucked.png", ContentManagerName);
			}
			VergissMeinNicht.Entities.TextArial.LoadStaticContent(contentManagerName);
			VergissMeinNicht.Entities.Flash.LoadStaticContent(contentManagerName);
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("TeddyStaticUnload", UnloadStaticContent);
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
				if (teddy_normal != null)
				{
					teddy_normal= null;
				}
				if (teddy_fucked != null)
				{
					teddy_fucked= null;
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "teddy_normal":
					return teddy_normal;
				case  "teddy_fucked":
					return teddy_fucked;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "teddy_normal":
					return teddy_normal;
				case  "teddy_fucked":
					return teddy_fucked;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "teddy_normal":
					return teddy_normal;
				case  "teddy_fucked":
					return teddy_fucked;
			}
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
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Sprite);
			SaveText.SetToIgnorePausing();
			InteractText.SetToIgnorePausing();
			FlashInstance.SetToIgnorePausing();
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(Sprite);
			}
			SpriteManager.AddToLayer(Sprite, layerToMoveTo);
			SaveText.MoveToLayer(layerToMoveTo);
			InteractText.MoveToLayer(layerToMoveTo);
			FlashInstance.MoveToLayer(layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	
}
