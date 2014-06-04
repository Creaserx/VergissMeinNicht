
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
using FlatRedBall.Math.Geometry;
using FlatRedBall.Graphics.Animation;

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
	public partial class Theodor : VergissMeinNicht.Entities.PlatformerCharacterBase, IDestroyable
	{
        // This is made global so that static lazy-loaded content can access it.
        public static new string ContentManagerName
        {
            get{ return Entities.PlatformerCharacterBase.ContentManagerName;}
            set{ Entities.PlatformerCharacterBase.ContentManagerName = value;}
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		protected static FlatRedBall.Graphics.Animation.AnimationChainList AnimationChainListFile;
		
		private FlatRedBall.Sprite SpriteInstance;
		public event EventHandler BeforeGroundMovementSet;
		public event EventHandler AfterGroundMovementSet;
		public override VergissMeinNicht.DataTypes.MovementValues GroundMovement
		{
			set
			{
				if (BeforeGroundMovementSet != null)
				{
					BeforeGroundMovementSet(this, null);
				}
				base.GroundMovement = value;
				if (AfterGroundMovementSet != null)
				{
					AfterGroundMovementSet(this, null);
				}
			}
			get
			{
				return base.GroundMovement;
			}
		}
		public event EventHandler BeforeAirMovementSet;
		public event EventHandler AfterAirMovementSet;
		public override VergissMeinNicht.DataTypes.MovementValues AirMovement
		{
			set
			{
				if (BeforeAirMovementSet != null)
				{
					BeforeAirMovementSet(this, null);
				}
				base.AirMovement = value;
				if (AfterAirMovementSet != null)
				{
					AfterAirMovementSet(this, null);
				}
			}
			get
			{
				return base.AirMovement;
			}
		}
		public event EventHandler BeforeAfterDoubleJumpSet;
		public event EventHandler AfterAfterDoubleJumpSet;
		public override VergissMeinNicht.DataTypes.MovementValues AfterDoubleJump
		{
			set
			{
				if (BeforeAfterDoubleJumpSet != null)
				{
					BeforeAfterDoubleJumpSet(this, null);
				}
				base.AfterDoubleJump = value;
				if (AfterAfterDoubleJumpSet != null)
				{
					AfterAfterDoubleJumpSet(this, null);
				}
			}
			get
			{
				return base.AfterDoubleJump;
			}
		}

        public Theodor()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public Theodor(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Theodor(string contentManagerName, bool addToManagers) :
			base(contentManagerName, addToManagers)
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
           

		}

		protected override void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			SpriteInstance = new FlatRedBall.Sprite();
			SpriteInstance.Name = "SpriteInstance";
			
			base.InitializeEntity(addToManagers);


		}

// Generated AddToManagers
		public override void ReAddToManagers (Layer layerToAddTo)
		{
			base.ReAddToManagers(layerToAddTo);
			SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
		}
		public override void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
			base.AddToManagers(layerToAddTo);
			CustomInitialize();
		}

		public override void Activity()
		{
			// Generated Activity
			base.Activity();
			
			CustomActivity();
			
			// After Custom Activity
		}

		public override void Destroy()
		{
			// Generated Destroy
			base.Destroy();
			
			if (SpriteInstance != null)
			{
				SpriteManager.RemoveSprite(SpriteInstance);
			}


			CustomDestroy();
		}

		// Generated Methods
		public override void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			base.PostInitialize();
			if (mCollision.Parent == null)
			{
				mCollision.CopyAbsoluteToRelative();
				mCollision.AttachTo(this, false);
			}
			Collision.Height = 120f;
			Collision.Width = 32f;
			if (SpriteInstance.Parent == null)
			{
				SpriteInstance.CopyAbsoluteToRelative();
				SpriteInstance.AttachTo(this, false);
			}
			SpriteInstance.AnimationChains = AnimationChainListFile;
			SpriteInstance.TextureScale = 0.2f;
			SpriteInstance.CurrentChainName = "Idle_Right";
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public override void AddToManagersBottomUp (Layer layerToAddTo)
		{
			base.AddToManagersBottomUp(layerToAddTo);
		}
		public override void RemoveFromManagers ()
		{
			base.RemoveFromManagers();
			base.RemoveFromManagers();
			if (SpriteInstance != null)
			{
				SpriteManager.RemoveSpriteOneWay(SpriteInstance);
			}
		}
		public override void AssignCustomVariables (bool callOnContainedElements)
		{
			base.AssignCustomVariables(callOnContainedElements);
			if (callOnContainedElements)
			{
			}
			mCollision.Height = 120f;
			mCollision.Width = 32f;
			SpriteInstance.AnimationChains = AnimationChainListFile;
			SpriteInstance.TextureScale = 0.2f;
			SpriteInstance.CurrentChainName = "Idle_Right";
			GroundMovement = Theodor.MovementValues["ImmediateVelocityOnGround"];
			AirMovement = Theodor.MovementValues["ImmediateVelocityBeforeDoubleJump"];
			AfterDoubleJump = Theodor.MovementValues["ImmediateVelocityInAir"];
		}
		public override void ConvertToManuallyUpdated ()
		{
			base.ConvertToManuallyUpdated();
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			SpriteManager.ConvertToManuallyUpdated(SpriteInstance);
		}
		public static new void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
			PlatformerCharacterBase.LoadStaticContent(contentManagerName);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("TheodorStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/theodor/animationchainlistfile.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				AnimationChainListFile = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/theodor/animationchainlistfile.achx", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("TheodorStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static new void UnloadStaticContent ()
		{
			if (LoadedContentManagers.Count != 0)
			{
				LoadedContentManagers.RemoveAt(0);
				mRegisteredUnloads.RemoveAt(0);
			}
			if (LoadedContentManagers.Count == 0)
			{
				if (AnimationChainListFile != null)
				{
					AnimationChainListFile= null;
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static new object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimationChainListFile":
					return AnimationChainListFile;
			}
			return null;
		}
		public static new object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "AnimationChainListFile":
					return AnimationChainListFile;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimationChainListFile":
					return AnimationChainListFile;
			}
			return null;
		}
		public override void SetToIgnorePausing ()
		{
			base.SetToIgnorePausing();
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Collision);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(SpriteInstance);
		}
		public override void MoveToLayer (Layer layerToMoveTo)
		{
			base.MoveToLayer(layerToMoveTo);
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
