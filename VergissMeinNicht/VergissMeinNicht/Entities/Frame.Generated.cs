
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
using FlatRedBall.ManagedSpriteGroups;
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
	public partial class Frame : PositionedObject, IDestroyable, IVisible
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
		public enum VariableState
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			RoundedRed = 2, 
			SquareGray = 3, 
			RoundedGray = 4, 
			SingleRoundedGray = 5
		}
		protected int mCurrentState = 0;
		public Entities.Frame.VariableState CurrentState
		{
			get
			{
				if (Enum.IsDefined(typeof(VariableState), mCurrentState))
				{
					return (VariableState)mCurrentState;
				}
				else
				{
					return VariableState.Unknown;
				}
			}
			set
			{
				mCurrentState = (int)value;
				switch(CurrentState)
				{
					case  VariableState.Uninitialized:
						break;
					case  VariableState.Unknown:
						break;
					case  VariableState.RoundedRed:
						CurrentChain = "RoundedRed";
						break;
					case  VariableState.SquareGray:
						CurrentChain = "SquareGray";
						break;
					case  VariableState.RoundedGray:
						CurrentChain = "RoundedGray";
						break;
					case  VariableState.SingleRoundedGray:
						CurrentChain = "SingleRoundedGray";
						break;
				}
			}
		}
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		protected static FlatRedBall.Graphics.Animation.AnimationChainList AnimationChainListFile;
		protected static FlatRedBall.Scene SceneFile;
		
		private FlatRedBall.ManagedSpriteGroups.SpriteFrame SpriteFrameInstance;
		public string CurrentChain
		{
			get
			{
				return SpriteFrameInstance.CurrentChainName;
			}
			set
			{
				SpriteFrameInstance.CurrentChainName = value;
			}
		}
		public float ScaleX
		{
			get
			{
				return SpriteFrameInstance.ScaleX;
			}
			set
			{
				SpriteFrameInstance.ScaleX = value;
			}
		}
		public float ScaleY
		{
			get
			{
				return SpriteFrameInstance.ScaleY;
			}
			set
			{
				SpriteFrameInstance.ScaleY = value;
			}
		}
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

        public Frame()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public Frame(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Frame(string contentManagerName, bool addToManagers) :
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
			SpriteFrameInstance = SceneFile.SpriteFrames.FindByName("AnimationChainListFile").Clone();
			
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
			SpriteManager.AddToLayer(SpriteFrameInstance, LayerProvidedByContainer);
		}
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			SpriteManager.AddToLayer(SpriteFrameInstance, LayerProvidedByContainer);
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
			
			if (SpriteFrameInstance != null)
			{
				SpriteManager.RemoveSpriteFrame(SpriteFrameInstance);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (SpriteFrameInstance.Parent == null)
			{
				SpriteFrameInstance.CopyAbsoluteToRelative();
				SpriteFrameInstance.AttachTo(this, false);
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
			if (SpriteFrameInstance != null)
			{
				SpriteManager.RemoveSpriteFrame(SpriteFrameInstance);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
			CurrentChain = "RoundedRed";
			ScaleX = 32f;
			ScaleY = 32f;
			if (Parent == null)
			{
				X = 0f;
			}
			else
			{
				RelativeX = 0f;
			}
			if (Parent == null)
			{
				Y = 0f;
			}
			else
			{
				RelativeY = 0f;
			}
			if (Parent == null)
			{
				Z = 0f;
			}
			else if (Parent is Camera)
			{
				RelativeZ = 0f - 40.0f;
			}
			else
			{
				RelativeZ = 0f;
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			SpriteManager.ConvertToManuallyUpdated(SpriteFrameInstance);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("FrameStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/frame/animationchainlistfile.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				AnimationChainListFile = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/frame/animationchainlistfile.achx", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/entities/frame/scenefile.scnx", ContentManagerName))
				{
					registerUnload = true;
				}
				SceneFile = FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/entities/frame/scenefile.scnx", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("FrameStaticUnload", UnloadStaticContent);
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
				if (AnimationChainListFile != null)
				{
					AnimationChainListFile= null;
				}
				if (SceneFile != null)
				{
					SceneFile.RemoveFromManagers(ContentManagerName != "Global");
					SceneFile= null;
				}
			}
		}
		static VariableState mLoadingState = VariableState.Uninitialized;
		public static VariableState LoadingState
		{
			get
			{
				return mLoadingState;
			}
			set
			{
				mLoadingState = value;
			}
		}
		public FlatRedBall.Instructions.Instruction InterpolateToState (VariableState stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case  VariableState.RoundedRed:
					break;
				case  VariableState.SquareGray:
					break;
				case  VariableState.RoundedGray:
					break;
				case  VariableState.SingleRoundedGray:
					break;
			}
			var instruction = new FlatRedBall.Instructions.DelegateInstruction<VariableState>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + secondsToTake;
			this.Instructions.Add(instruction);
			return instruction;
		}
		public void StopStateInterpolation (VariableState stateToStop)
		{
			switch(stateToStop)
			{
				case  VariableState.RoundedRed:
					break;
				case  VariableState.SquareGray:
					break;
				case  VariableState.RoundedGray:
					break;
				case  VariableState.SingleRoundedGray:
					break;
			}
			CurrentState = stateToStop;
		}
		public void InterpolateBetween (VariableState firstState, VariableState secondState, float interpolationValue)
		{
			#if DEBUG
			if (float.IsNaN(interpolationValue))
			{
				throw new Exception("interpolationValue cannot be NaN");
			}
			#endif
			switch(firstState)
			{
				case  VariableState.RoundedRed:
					if (interpolationValue < 1)
					{
						this.CurrentChain = "RoundedRed";
					}
					break;
				case  VariableState.SquareGray:
					if (interpolationValue < 1)
					{
						this.CurrentChain = "SquareGray";
					}
					break;
				case  VariableState.RoundedGray:
					if (interpolationValue < 1)
					{
						this.CurrentChain = "RoundedGray";
					}
					break;
				case  VariableState.SingleRoundedGray:
					if (interpolationValue < 1)
					{
						this.CurrentChain = "SingleRoundedGray";
					}
					break;
			}
			switch(secondState)
			{
				case  VariableState.RoundedRed:
					if (interpolationValue >= 1)
					{
						this.CurrentChain = "RoundedRed";
					}
					break;
				case  VariableState.SquareGray:
					if (interpolationValue >= 1)
					{
						this.CurrentChain = "SquareGray";
					}
					break;
				case  VariableState.RoundedGray:
					if (interpolationValue >= 1)
					{
						this.CurrentChain = "RoundedGray";
					}
					break;
				case  VariableState.SingleRoundedGray:
					if (interpolationValue >= 1)
					{
						this.CurrentChain = "SingleRoundedGray";
					}
					break;
			}
			if (interpolationValue < 1)
			{
				mCurrentState = (int)firstState;
			}
			else
			{
				mCurrentState = (int)secondState;
			}
		}
		public static void PreloadStateContent (VariableState state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			switch(state)
			{
				case  VariableState.RoundedRed:
					{
						object throwaway = "RoundedRed";
					}
					break;
				case  VariableState.SquareGray:
					{
						object throwaway = "SquareGray";
					}
					break;
				case  VariableState.RoundedGray:
					{
						object throwaway = "RoundedGray";
					}
					break;
				case  VariableState.SingleRoundedGray:
					{
						object throwaway = "SingleRoundedGray";
					}
					break;
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimationChainListFile":
					return AnimationChainListFile;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "AnimationChainListFile":
					return AnimationChainListFile;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimationChainListFile":
					return AnimationChainListFile;
				case  "SceneFile":
					return SceneFile;
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
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(SpriteFrameInstance);
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(SpriteFrameInstance);
			}
			SpriteManager.AddToLayer(SpriteFrameInstance, layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
		public static class FrameExtensionMethods
	{
		public static void SetVisible (this PositionedObjectList<Frame> list, bool value)
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i].Visible = value;
			}
		}
	}

	
}
