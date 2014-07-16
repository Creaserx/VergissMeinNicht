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
using Microsoft.Xna.Framework.Graphics;

namespace VergissMeinNicht.Screens
{
	public partial class MainMenu : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		public enum VariableState
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			MainButtonsVisible = 2, 
			ExitOkCancelVisible = 3
		}
		protected int mCurrentState = 0;
		public Screens.MainMenu.VariableState CurrentState
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
					case  VariableState.MainButtonsVisible:
						OkCancelInstanceVisible = false;
						ExitGameButtonVisible = true;
						LoadGameButtonVisible = true;
						NewGameButtonVisible = true;
						OptionsButtonVisible = true;
						ZooLevelButtonVisible = true;
						break;
					case  VariableState.ExitOkCancelVisible:
						OkCancelInstanceVisible = true;
						ExitGameButtonVisible = false;
						LoadGameButtonVisible = false;
						NewGameButtonVisible = false;
						OptionsButtonVisible = false;
						ZooLevelButtonVisible = false;
						break;
				}
			}
		}
		protected static Microsoft.Xna.Framework.Graphics.Texture2D HauptmenüFinal;
		
		private VergissMeinNicht.Entities.Button ZooLevelButton;
		private FlatRedBall.Sprite SpriteInstance;
		private VergissMeinNicht.Entities.Button NewGameButton;
		private VergissMeinNicht.Entities.Button LoadGameButton;
		private VergissMeinNicht.Entities.Button OptionsButton;
		private VergissMeinNicht.Entities.Button ExitGameButton;
		private FlatRedBall.Graphics.Layer Layer2D;
		private VergissMeinNicht.Entities.OkCancel OkCancelInstance;
		private FlatRedBall.Graphics.Layer OkCancelLayer;
		public bool OkCancelInstanceVisible
		{
			get
			{
				return OkCancelInstance.Visible;
			}
			set
			{
				OkCancelInstance.Visible = value;
			}
		}
		public bool ExitGameButtonVisible
		{
			get
			{
				return ExitGameButton.Visible;
			}
			set
			{
				ExitGameButton.Visible = value;
			}
		}
		public bool LoadGameButtonVisible
		{
			get
			{
				return LoadGameButton.Visible;
			}
			set
			{
				LoadGameButton.Visible = value;
			}
		}
		public bool NewGameButtonVisible
		{
			get
			{
				return NewGameButton.Visible;
			}
			set
			{
				NewGameButton.Visible = value;
			}
		}
		public bool OptionsButtonVisible
		{
			get
			{
				return OptionsButton.Visible;
			}
			set
			{
				OptionsButton.Visible = value;
			}
		}
		public bool ZooLevelButtonVisible
		{
			get
			{
				return ZooLevelButton.Visible;
			}
			set
			{
				ZooLevelButton.Visible = value;
			}
		}
		public event FlatRedBall.Gui.WindowEvent ExitGameButtonClick;
		public event FlatRedBall.Gui.WindowEvent OptionsButtonClick;
		public event FlatRedBall.Gui.WindowEvent NewGameButtonClick;
		public event FlatRedBall.Gui.WindowEvent LoadGameButtonClick;
		public event FlatRedBall.Gui.WindowEvent OkCancelInstanceOkClick;
		public event FlatRedBall.Gui.WindowEvent OkCancelInstanceCancelClick;
		public event FlatRedBall.Gui.WindowEvent ZooLevelButtonClick;

		public MainMenu()
			: base("MainMenu")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			ZooLevelButton = new VergissMeinNicht.Entities.Button(ContentManagerName, false);
			ZooLevelButton.Name = "ZooLevelButton";
			SpriteInstance = new FlatRedBall.Sprite();
			SpriteInstance.Name = "SpriteInstance";
			NewGameButton = new VergissMeinNicht.Entities.Button(ContentManagerName, false);
			NewGameButton.Name = "NewGameButton";
			LoadGameButton = new VergissMeinNicht.Entities.Button(ContentManagerName, false);
			LoadGameButton.Name = "LoadGameButton";
			OptionsButton = new VergissMeinNicht.Entities.Button(ContentManagerName, false);
			OptionsButton.Name = "OptionsButton";
			ExitGameButton = new VergissMeinNicht.Entities.Button(ContentManagerName, false);
			ExitGameButton.Name = "ExitGameButton";
			Layer2D = new FlatRedBall.Graphics.Layer();
			Layer2D.Name = "Layer2D";
			OkCancelInstance = new VergissMeinNicht.Entities.OkCancel(ContentManagerName, false);
			OkCancelInstance.Name = "OkCancelInstance";
			OkCancelLayer = new FlatRedBall.Graphics.Layer();
			OkCancelLayer.Name = "OkCancelLayer";
			
			
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
			SpriteManager.AddLayer(OkCancelLayer);
			OkCancelLayer.UsePixelCoordinates();
			if (SpriteManager.Camera.Orthogonal)
			{
				OkCancelLayer.LayerCameraSettings.OrthogonalWidth = FlatRedBall.SpriteManager.Camera.OrthogonalWidth;
				OkCancelLayer.LayerCameraSettings.OrthogonalHeight = FlatRedBall.SpriteManager.Camera.OrthogonalHeight;
			}
			ZooLevelButton.AddToManagers(Layer2D);
			SpriteManager.AddSprite(SpriteInstance);
			NewGameButton.AddToManagers(SpriteManager.UnderAllDrawnLayer);
			LoadGameButton.AddToManagers(SpriteManager.UnderAllDrawnLayer);
			OptionsButton.AddToManagers(SpriteManager.UnderAllDrawnLayer);
			ExitGameButton.AddToManagers(SpriteManager.UnderAllDrawnLayer);
			OkCancelInstance.AddToManagers(OkCancelLayer);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				ZooLevelButton.Activity();
				NewGameButton.Activity();
				LoadGameButton.Activity();
				OptionsButton.Activity();
				ExitGameButton.Activity();
				OkCancelInstance.Activity();
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
			HauptmenüFinal = null;
			
			if (ZooLevelButton != null)
			{
				ZooLevelButton.Destroy();
				ZooLevelButton.Detach();
			}
			if (SpriteInstance != null)
			{
				SpriteManager.RemoveSprite(SpriteInstance);
			}
			if (NewGameButton != null)
			{
				NewGameButton.Destroy();
				NewGameButton.Detach();
			}
			if (LoadGameButton != null)
			{
				LoadGameButton.Destroy();
				LoadGameButton.Detach();
			}
			if (OptionsButton != null)
			{
				OptionsButton.Destroy();
				OptionsButton.Detach();
			}
			if (ExitGameButton != null)
			{
				ExitGameButton.Destroy();
				ExitGameButton.Detach();
			}
			if (Layer2D != null)
			{
				SpriteManager.RemoveLayer(Layer2D);
			}
			if (OkCancelInstance != null)
			{
				OkCancelInstance.Destroy();
				OkCancelInstance.Detach();
			}
			if (OkCancelLayer != null)
			{
				SpriteManager.RemoveLayer(OkCancelLayer);
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			ExitGameButton.Click += OnExitGameButtonClick;
			ExitGameButton.Click += OnExitGameButtonClickTunnel;
			OptionsButton.Click += OnOptionsButtonClick;
			OptionsButton.Click += OnOptionsButtonClickTunnel;
			NewGameButton.Click += OnNewGameButtonClick;
			NewGameButton.Click += OnNewGameButtonClickTunnel;
			LoadGameButton.Click += OnLoadGameButtonClick;
			LoadGameButton.Click += OnLoadGameButtonClickTunnel;
			OkCancelInstance.OkClick += OnOkCancelInstanceOkClick;
			OkCancelInstance.OkClick += OnOkCancelInstanceOkClickTunnel;
			OkCancelInstance.CancelClick += OnOkCancelInstanceCancelClick;
			OkCancelInstance.CancelClick += OnOkCancelInstanceCancelClickTunnel;
			ZooLevelButton.Click += OnZooLevelButtonClick;
			ZooLevelButton.Click += OnZooLevelButtonClickTunnel;
			ZooLevelButton.SpriteFrameInstanceCurrentChainName = "";
			ZooLevelButton.DisplayText = "Enter ZooLevel";
			ZooLevelButton.ScaleX = 100f;
			if (ZooLevelButton.Parent == null)
			{
				ZooLevelButton.X = 0f;
			}
			else
			{
				ZooLevelButton.RelativeX = 0f;
			}
			if (ZooLevelButton.Parent == null)
			{
				ZooLevelButton.Y = 80f;
			}
			else
			{
				ZooLevelButton.RelativeY = 80f;
			}
			ZooLevelButton.Visible = true;
			SpriteInstance.Texture = HauptmenüFinal;
			SpriteInstance.TextureScale = 1f;
			NewGameButton.SpriteFrameInstanceCurrentChainName = "";
			NewGameButton.DisplayText = "New Game";
			NewGameButton.ScaleX = 120f;
			if (NewGameButton.Parent == null)
			{
				NewGameButton.X = 200f;
			}
			else
			{
				NewGameButton.RelativeX = 200f;
			}
			if (NewGameButton.Parent == null)
			{
				NewGameButton.Y = 20f;
			}
			else
			{
				NewGameButton.RelativeY = 20f;
			}
			NewGameButton.Visible = false;
			if (NewGameButton.Parent == null)
			{
				NewGameButton.RotationZ = 0.25f;
			}
			else
			{
				NewGameButton.RelativeRotationZ = 0.25f;
			}
			LoadGameButton.DisplayText = "Load Game";
			LoadGameButton.ScaleX = 120f;
			if (LoadGameButton.Parent == null)
			{
				LoadGameButton.X = 200f;
			}
			else
			{
				LoadGameButton.RelativeX = 200f;
			}
			if (LoadGameButton.Parent == null)
			{
				LoadGameButton.Y = -40f;
			}
			else
			{
				LoadGameButton.RelativeY = -40f;
			}
			LoadGameButton.Visible = false;
			if (LoadGameButton.Parent == null)
			{
				LoadGameButton.RotationZ = 0.2f;
			}
			else
			{
				LoadGameButton.RelativeRotationZ = 0.2f;
			}
			OptionsButton.DisplayText = "Options";
			OptionsButton.ScaleX = 120f;
			if (OptionsButton.Parent == null)
			{
				OptionsButton.X = 200f;
			}
			else
			{
				OptionsButton.RelativeX = 200f;
			}
			if (OptionsButton.Parent == null)
			{
				OptionsButton.Y = -100f;
			}
			else
			{
				OptionsButton.RelativeY = -100f;
			}
			OptionsButton.Visible = false;
			if (OptionsButton.Parent == null)
			{
				OptionsButton.RotationZ = 0.1f;
			}
			else
			{
				OptionsButton.RelativeRotationZ = 0.1f;
			}
			ExitGameButton.DisplayText = "Exit Game";
			ExitGameButton.ScaleX = 120f;
			if (ExitGameButton.Parent == null)
			{
				ExitGameButton.X = 200f;
			}
			else
			{
				ExitGameButton.RelativeX = 200f;
			}
			if (ExitGameButton.Parent == null)
			{
				ExitGameButton.Y = -170f;
			}
			else
			{
				ExitGameButton.RelativeY = -170f;
			}
			ExitGameButton.Visible = false;
			if (ExitGameButton.Parent == null)
			{
				ExitGameButton.RotationZ = 0f;
			}
			else
			{
				ExitGameButton.RelativeRotationZ = 0f;
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
			ZooLevelButton.RemoveFromManagers();
			if (SpriteInstance != null)
			{
				SpriteManager.RemoveSpriteOneWay(SpriteInstance);
			}
			NewGameButton.RemoveFromManagers();
			LoadGameButton.RemoveFromManagers();
			OptionsButton.RemoveFromManagers();
			ExitGameButton.RemoveFromManagers();
			if (Layer2D != null)
			{
				SpriteManager.RemoveLayer(Layer2D);
			}
			OkCancelInstance.RemoveFromManagers();
			if (OkCancelLayer != null)
			{
				SpriteManager.RemoveLayer(OkCancelLayer);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				ZooLevelButton.AssignCustomVariables(true);
				NewGameButton.AssignCustomVariables(true);
				LoadGameButton.AssignCustomVariables(true);
				OptionsButton.AssignCustomVariables(true);
				ExitGameButton.AssignCustomVariables(true);
				OkCancelInstance.AssignCustomVariables(true);
			}
			ZooLevelButton.SpriteFrameInstanceCurrentChainName = "";
			ZooLevelButton.DisplayText = "Enter ZooLevel";
			ZooLevelButton.ScaleX = 100f;
			if (ZooLevelButton.Parent == null)
			{
				ZooLevelButton.X = 0f;
			}
			else
			{
				ZooLevelButton.RelativeX = 0f;
			}
			if (ZooLevelButton.Parent == null)
			{
				ZooLevelButton.Y = 80f;
			}
			else
			{
				ZooLevelButton.RelativeY = 80f;
			}
			ZooLevelButton.Visible = true;
			SpriteInstance.Texture = HauptmenüFinal;
			SpriteInstance.TextureScale = 1f;
			NewGameButton.SpriteFrameInstanceCurrentChainName = "";
			NewGameButton.DisplayText = "New Game";
			NewGameButton.ScaleX = 120f;
			if (NewGameButton.Parent == null)
			{
				NewGameButton.X = 200f;
			}
			else
			{
				NewGameButton.RelativeX = 200f;
			}
			if (NewGameButton.Parent == null)
			{
				NewGameButton.Y = 20f;
			}
			else
			{
				NewGameButton.RelativeY = 20f;
			}
			NewGameButton.Visible = false;
			if (NewGameButton.Parent == null)
			{
				NewGameButton.RotationZ = 0.25f;
			}
			else
			{
				NewGameButton.RelativeRotationZ = 0.25f;
			}
			LoadGameButton.DisplayText = "Load Game";
			LoadGameButton.ScaleX = 120f;
			if (LoadGameButton.Parent == null)
			{
				LoadGameButton.X = 200f;
			}
			else
			{
				LoadGameButton.RelativeX = 200f;
			}
			if (LoadGameButton.Parent == null)
			{
				LoadGameButton.Y = -40f;
			}
			else
			{
				LoadGameButton.RelativeY = -40f;
			}
			LoadGameButton.Visible = false;
			if (LoadGameButton.Parent == null)
			{
				LoadGameButton.RotationZ = 0.2f;
			}
			else
			{
				LoadGameButton.RelativeRotationZ = 0.2f;
			}
			OptionsButton.DisplayText = "Options";
			OptionsButton.ScaleX = 120f;
			if (OptionsButton.Parent == null)
			{
				OptionsButton.X = 200f;
			}
			else
			{
				OptionsButton.RelativeX = 200f;
			}
			if (OptionsButton.Parent == null)
			{
				OptionsButton.Y = -100f;
			}
			else
			{
				OptionsButton.RelativeY = -100f;
			}
			OptionsButton.Visible = false;
			if (OptionsButton.Parent == null)
			{
				OptionsButton.RotationZ = 0.1f;
			}
			else
			{
				OptionsButton.RelativeRotationZ = 0.1f;
			}
			ExitGameButton.DisplayText = "Exit Game";
			ExitGameButton.ScaleX = 120f;
			if (ExitGameButton.Parent == null)
			{
				ExitGameButton.X = 200f;
			}
			else
			{
				ExitGameButton.RelativeX = 200f;
			}
			if (ExitGameButton.Parent == null)
			{
				ExitGameButton.Y = -170f;
			}
			else
			{
				ExitGameButton.RelativeY = -170f;
			}
			ExitGameButton.Visible = false;
			if (ExitGameButton.Parent == null)
			{
				ExitGameButton.RotationZ = 0f;
			}
			else
			{
				ExitGameButton.RelativeRotationZ = 0f;
			}
			ExitGameButtonVisible = false;
			LoadGameButtonVisible = false;
			NewGameButtonVisible = false;
			OptionsButtonVisible = false;
			CurrentState = MainMenu.VariableState.MainButtonsVisible;
			ZooLevelButtonVisible = true;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			ZooLevelButton.ConvertToManuallyUpdated();
			SpriteManager.ConvertToManuallyUpdated(SpriteInstance);
			NewGameButton.ConvertToManuallyUpdated();
			LoadGameButton.ConvertToManuallyUpdated();
			OptionsButton.ConvertToManuallyUpdated();
			ExitGameButton.ConvertToManuallyUpdated();
			OkCancelInstance.ConvertToManuallyUpdated();
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
			if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/screens/mainmenu/hauptmenüfinal.jpg", contentManagerName))
			{
			}
			HauptmenüFinal = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/screens/mainmenu/hauptmenüfinal.jpg", contentManagerName);
			VergissMeinNicht.Entities.Button.LoadStaticContent(contentManagerName);
			VergissMeinNicht.Entities.OkCancel.LoadStaticContent(contentManagerName);
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
				case  VariableState.MainButtonsVisible:
					break;
				case  VariableState.ExitOkCancelVisible:
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
				case  VariableState.MainButtonsVisible:
					break;
				case  VariableState.ExitOkCancelVisible:
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
				case  VariableState.MainButtonsVisible:
					if (interpolationValue < 1)
					{
						this.OkCancelInstanceVisible = false;
					}
					if (interpolationValue < 1)
					{
						this.ExitGameButtonVisible = true;
					}
					if (interpolationValue < 1)
					{
						this.LoadGameButtonVisible = true;
					}
					if (interpolationValue < 1)
					{
						this.NewGameButtonVisible = true;
					}
					if (interpolationValue < 1)
					{
						this.OptionsButtonVisible = true;
					}
					if (interpolationValue < 1)
					{
						this.ZooLevelButtonVisible = true;
					}
					break;
				case  VariableState.ExitOkCancelVisible:
					if (interpolationValue < 1)
					{
						this.OkCancelInstanceVisible = true;
					}
					if (interpolationValue < 1)
					{
						this.ExitGameButtonVisible = false;
					}
					if (interpolationValue < 1)
					{
						this.LoadGameButtonVisible = false;
					}
					if (interpolationValue < 1)
					{
						this.NewGameButtonVisible = false;
					}
					if (interpolationValue < 1)
					{
						this.OptionsButtonVisible = false;
					}
					if (interpolationValue < 1)
					{
						this.ZooLevelButtonVisible = false;
					}
					break;
			}
			switch(secondState)
			{
				case  VariableState.MainButtonsVisible:
					if (interpolationValue >= 1)
					{
						this.OkCancelInstanceVisible = false;
					}
					if (interpolationValue >= 1)
					{
						this.ExitGameButtonVisible = true;
					}
					if (interpolationValue >= 1)
					{
						this.LoadGameButtonVisible = true;
					}
					if (interpolationValue >= 1)
					{
						this.NewGameButtonVisible = true;
					}
					if (interpolationValue >= 1)
					{
						this.OptionsButtonVisible = true;
					}
					if (interpolationValue >= 1)
					{
						this.ZooLevelButtonVisible = true;
					}
					break;
				case  VariableState.ExitOkCancelVisible:
					if (interpolationValue >= 1)
					{
						this.OkCancelInstanceVisible = true;
					}
					if (interpolationValue >= 1)
					{
						this.ExitGameButtonVisible = false;
					}
					if (interpolationValue >= 1)
					{
						this.LoadGameButtonVisible = false;
					}
					if (interpolationValue >= 1)
					{
						this.NewGameButtonVisible = false;
					}
					if (interpolationValue >= 1)
					{
						this.OptionsButtonVisible = false;
					}
					if (interpolationValue >= 1)
					{
						this.ZooLevelButtonVisible = false;
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
			switch(memberName)
			{
				case  "HauptmenüFinal":
					return HauptmenüFinal;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "HauptmenüFinal":
					return HauptmenüFinal;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "HauptmenüFinal":
					return HauptmenüFinal;
			}
			return null;
		}


	}
}
