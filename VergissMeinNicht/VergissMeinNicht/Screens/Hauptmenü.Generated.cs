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
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;

namespace VergissMeinNicht.Screens
{
	public partial class Hauptmenü : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		public enum VariableState
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			Options = 2
		}
		protected int mCurrentState = 0;
		public Screens.Hauptmenü.VariableState CurrentState
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
					case  VariableState.Options:
						break;
				}
			}
		}
		
		private PositionedObjectList<AxisAlignedRectangle> Button;
		private FlatRedBall.Math.Geometry.AxisAlignedRectangle mNewGame;
		public FlatRedBall.Math.Geometry.AxisAlignedRectangle NewGame
		{
			get
			{
				return mNewGame;
			}
			private set
			{
				mNewGame = value;
			}
		}
		private FlatRedBall.Math.Geometry.AxisAlignedRectangle mLoadGame;
		public FlatRedBall.Math.Geometry.AxisAlignedRectangle LoadGame
		{
			get
			{
				return mLoadGame;
			}
			private set
			{
				mLoadGame = value;
			}
		}
		private FlatRedBall.Math.Geometry.AxisAlignedRectangle mOptions;
		public FlatRedBall.Math.Geometry.AxisAlignedRectangle Options
		{
			get
			{
				return mOptions;
			}
			private set
			{
				mOptions = value;
			}
		}
		private FlatRedBall.Math.Geometry.AxisAlignedRectangle mExit;
		public FlatRedBall.Math.Geometry.AxisAlignedRectangle Exit
		{
			get
			{
				return mExit;
			}
			private set
			{
				mExit = value;
			}
		}

		public Hauptmenü()
			: base("Hauptmenü")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			Button = new PositionedObjectList<AxisAlignedRectangle>();
			Button.Name = "Button";
			mNewGame = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mNewGame.Name = "mNewGame";
			mLoadGame = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mLoadGame.Name = "mLoadGame";
			mOptions = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mOptions.Name = "mOptions";
			mExit = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mExit.Name = "mExit";
			
			
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
			ShapeManager.AddAxisAlignedRectangle(mNewGame);
			ShapeManager.AddAxisAlignedRectangle(mLoadGame);
			ShapeManager.AddAxisAlignedRectangle(mOptions);
			ShapeManager.AddAxisAlignedRectangle(mExit);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
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
			
			Button.MakeOneWay();
			for (int i = Button.Count - 1; i > -1; i--)
			{
				ShapeManager.Remove(Button[i]);
			}
			Button.MakeTwoWay();

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			Button.Add(NewGame);
			NewGame.Color = Color.Red;
			NewGame.Height = 32f;
			NewGame.Width = 64f;
			if (NewGame.Parent == null)
			{
				NewGame.Y = 200f;
			}
			else
			{
				NewGame.RelativeY = 200f;
			}
			Button.Add(LoadGame);
			LoadGame.Color = Color.Red;
			LoadGame.Height = 32f;
			LoadGame.Width = 64f;
			if (LoadGame.Parent == null)
			{
				LoadGame.Y = 100f;
			}
			else
			{
				LoadGame.RelativeY = 100f;
			}
			Button.Add(Options);
			Options.Color = Color.Red;
			Options.Height = 32f;
			Options.Width = 64f;
			if (Options.Parent == null)
			{
				Options.Y = 0f;
			}
			else
			{
				Options.RelativeY = 0f;
			}
			Button.Add(Exit);
			Exit.Color = Color.Red;
			Exit.Height = 32f;
			Exit.Width = 64f;
			if (Exit.Parent == null)
			{
				Exit.Y = -100f;
			}
			else
			{
				Exit.RelativeY = -100f;
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
			for (int i = Button.Count - 1; i > -1; i--)
			{
				ShapeManager.Remove(Button[i]);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
			mNewGame.Color = Color.Red;
			mNewGame.Height = 32f;
			mNewGame.Width = 64f;
			if (mNewGame.Parent == null)
			{
				mNewGame.Y = 200f;
			}
			else
			{
				mNewGame.RelativeY = 200f;
			}
			mLoadGame.Color = Color.Red;
			mLoadGame.Height = 32f;
			mLoadGame.Width = 64f;
			if (mLoadGame.Parent == null)
			{
				mLoadGame.Y = 100f;
			}
			else
			{
				mLoadGame.RelativeY = 100f;
			}
			mOptions.Color = Color.Red;
			mOptions.Height = 32f;
			mOptions.Width = 64f;
			if (mOptions.Parent == null)
			{
				mOptions.Y = 0f;
			}
			else
			{
				mOptions.RelativeY = 0f;
			}
			mExit.Color = Color.Red;
			mExit.Height = 32f;
			mExit.Width = 64f;
			if (mExit.Parent == null)
			{
				mExit.Y = -100f;
			}
			else
			{
				mExit.RelativeY = -100f;
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
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
			CustomLoadStaticContent(contentManagerName);
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
				case  VariableState.Options:
					break;
			}
			var instruction = new FlatRedBall.Instructions.DelegateInstruction<VariableState>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + secondsToTake;
			FlatRedBall.Instructions.InstructionManager.Add(instruction);
			return instruction;
		}
		public void StopStateInterpolation (VariableState stateToStop)
		{
			switch(stateToStop)
			{
				case  VariableState.Options:
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
				case  VariableState.Options:
					break;
			}
			switch(secondState)
			{
				case  VariableState.Options:
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
		public override void MoveToState (int state)
		{
			this.CurrentState = (VariableState)state;
		}
		
		/// <summary>Sets the current state, and pushes that state onto the back stack.</summary>
		public void PushState (VariableState state)
		{
			this.CurrentState = state;
			
			#if !MONOGAME
			ScreenManager.PushStateToStack((int)this.CurrentState);
			#endif
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
