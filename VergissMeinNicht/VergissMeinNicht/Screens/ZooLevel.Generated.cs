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
using FlatRedBall.Math.Geometry;

namespace VergissMeinNicht.Screens
{
	public partial class ZooLevel : LevelBase
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		protected static FlatRedBall.Scene SceneFile;
		
		private VergissMeinNicht.Entities.Teddy TeddyNormalInstance;
		private FlatRedBall.Math.Geometry.ShapeCollection SolidCollisions;

		public ZooLevel()
			: base()
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			TeddyNormalInstance = new VergissMeinNicht.Entities.Teddy(ContentManagerName, false);
			TeddyNormalInstance.Name = "TeddyNormalInstance";
			SolidCollisions = new FlatRedBall.Math.Geometry.ShapeCollection();
			SolidCollisions.Name = "SolidCollisions";
			
			
			base.Initialize(addToManagers);

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			SceneFile.AddToManagers(mLayer);
			TeddyNormalInstance.AddToManagers(mLayer);
			SolidCollisions.AddToManagers();
			base.AddToManagers();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				TeddyNormalInstance.Activity();
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
			
			if (TeddyNormalInstance != null)
			{
				TeddyNormalInstance.Destroy();
				TeddyNormalInstance.Detach();
			}
			if (SolidCollisions != null)
			{
				SolidCollisions.RemoveFromManagers(ContentManagerName != "Global");
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public override void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			base.PostInitialize();
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
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public override void AddToManagersBottomUp ()
		{
			base.AddToManagersBottomUp();
		}
		public override void RemoveFromManagers ()
		{
			base.RemoveFromManagers();
			TeddyNormalInstance.RemoveFromManagers();
			if (SolidCollisions != null)
			{
				SolidCollisions.RemoveFromManagers(false);
			}
		}
		public override void AssignCustomVariables (bool callOnContainedElements)
		{
			base.AssignCustomVariables(callOnContainedElements);
			if (callOnContainedElements)
			{
				TeddyNormalInstance.AssignCustomVariables(true);
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
		}
		public override void ConvertToManuallyUpdated ()
		{
			base.ConvertToManuallyUpdated();
			TeddyNormalInstance.ConvertToManuallyUpdated();
		}
		public static new void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			LevelBase.LoadStaticContent(contentManagerName);
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
			VergissMeinNicht.Entities.Teddy.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static new object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		public static new object GetFile (string memberName)
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
