
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
using VergissMeinNicht.DataTypes;
using FlatRedBall.IO.Csv;
using FlatRedBall.Math.Geometry;

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
	public partial class PlatformerCharacterBase : PositionedObject, IDestroyable
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
		public static Dictionary<string, VergissMeinNicht.DataTypes.MovementValues> MovementValues;
		
		protected FlatRedBall.Math.Geometry.AxisAlignedRectangle mCollision;
		public FlatRedBall.Math.Geometry.AxisAlignedRectangle Collision
		{
			get
			{
				return mCollision;
			}
			protected set
			{
				mCollision = value;
			}
		}
		protected FlatRedBall.Sprite mSpriteInstance;
		public FlatRedBall.Sprite SpriteInstance
		{
			get
			{
				return mSpriteInstance;
			}
			protected set
			{
				mSpriteInstance = value;
			}
		}
		public event EventHandler BeforeGroundMovementSet;
		public event EventHandler AfterGroundMovementSet;
		VergissMeinNicht.DataTypes.MovementValues mGroundMovement;
		public virtual VergissMeinNicht.DataTypes.MovementValues GroundMovement
		{
			set
			{
				if (BeforeGroundMovementSet != null)
				{
					BeforeGroundMovementSet(this, null);
				}
				mGroundMovement = value;
				if (AfterGroundMovementSet != null)
				{
					AfterGroundMovementSet(this, null);
				}
			}
			get
			{
				return mGroundMovement;
			}
		}
		public event EventHandler BeforeAirMovementSet;
		public event EventHandler AfterAirMovementSet;
		VergissMeinNicht.DataTypes.MovementValues mAirMovement;
		public virtual VergissMeinNicht.DataTypes.MovementValues AirMovement
		{
			set
			{
				if (BeforeAirMovementSet != null)
				{
					BeforeAirMovementSet(this, null);
				}
				mAirMovement = value;
				if (AfterAirMovementSet != null)
				{
					AfterAirMovementSet(this, null);
				}
			}
			get
			{
				return mAirMovement;
			}
		}
		public event EventHandler BeforeAfterDoubleJumpSet;
		public event EventHandler AfterAfterDoubleJumpSet;
		VergissMeinNicht.DataTypes.MovementValues mAfterDoubleJump;
		public virtual VergissMeinNicht.DataTypes.MovementValues AfterDoubleJump
		{
			set
			{
				if (BeforeAfterDoubleJumpSet != null)
				{
					BeforeAfterDoubleJumpSet(this, null);
				}
				mAfterDoubleJump = value;
				if (AfterAfterDoubleJumpSet != null)
				{
					AfterAfterDoubleJumpSet(this, null);
				}
			}
			get
			{
				return mAfterDoubleJump;
			}
		}
		protected Layer LayerProvidedByContainer = null;

        public PlatformerCharacterBase()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public PlatformerCharacterBase(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public PlatformerCharacterBase(string contentManagerName, bool addToManagers) :
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
		}
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();
		}

		public virtual void Activity()
		{
			// Generated Activity
			
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			this.AfterGroundMovementSet += OnAfterGroundMovementSet;
			this.AfterAirMovementSet += OnAfterAirMovementSet;
			this.AfterAfterDoubleJumpSet += OnAfterAfterDoubleJumpSet;
			if (Collision!= null)
			{
				if (mCollision.Parent == null)
				{
					mCollision.CopyAbsoluteToRelative();
					mCollision.AttachTo(this, false);
				}
				Collision.Height = 48f;
			Collision.Visible = true;
				Collision.Width = 32f;
			}
			if (SpriteInstance!= null)
			{
				if (mSpriteInstance.Parent == null)
				{
					mSpriteInstance.CopyAbsoluteToRelative();
					mSpriteInstance.AttachTo(this, false);
				}
				SpriteInstance.TextureScale = 1f;
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
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
			mCollision.Visible = true;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			if (Collision != null)
			{
			}
			if (SpriteInstance != null)
			{
				SpriteManager.ConvertToManuallyUpdated(SpriteInstance);
			}
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("PlatformerCharacterBaseStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (MovementValues == null)
				{
					{
						// We put the { and } to limit the scope of oldDelimiter
						char oldDelimiter = CsvFileManager.Delimiter;
						CsvFileManager.Delimiter = ',';
						Dictionary<string, VergissMeinNicht.DataTypes.MovementValues> temporaryCsvObject = new Dictionary<string, VergissMeinNicht.DataTypes.MovementValues>();
						CsvFileManager.CsvDeserializeDictionary<string, VergissMeinNicht.DataTypes.MovementValues>("content/entities/platformercharacterbase/movementvalues.csv", temporaryCsvObject);
						CsvFileManager.Delimiter = oldDelimiter;
						MovementValues = temporaryCsvObject;
					}
				}
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("PlatformerCharacterBaseStaticUnload", UnloadStaticContent);
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
				if (MovementValues != null)
				{
					MovementValues= null;
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "MovementValues":
					return MovementValues;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "MovementValues":
					return MovementValues;
			}
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
			if (Collision != null)
			{
				FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Collision);
			}
			if (SpriteInstance != null)
			{
				FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(SpriteInstance);
			}
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(SpriteInstance);
			}
			SpriteManager.AddToLayer(SpriteInstance, layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	
}
