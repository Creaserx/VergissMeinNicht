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
using VergissMeinNicht.Entities.Räume_Inhalt.Empfang;
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
	public partial class Empfang : LevelBase
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		public static FlatRedBall.Scene Background;
		protected static FlatRedBall.Math.Geometry.ShapeCollection Layer3;
		protected static FlatRedBall.Math.Geometry.ShapeCollection Layer1;
		
		private VergissMeinNicht.Entities.Teddy TeddyInstance;
		private PositionedObjectList<VergissMeinNicht.Entities.Räume_Inhalt.Empfang.Loch> HolesList;
		private VergissMeinNicht.Entities.Räume_Inhalt.Empfang.Loch Loch1;
		private VergissMeinNicht.Entities.Räume_Inhalt.Empfang.Loch Loch2;

		public Empfang()
			: base()
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			TeddyInstance = new VergissMeinNicht.Entities.Teddy(ContentManagerName, false);
			TeddyInstance.Name = "TeddyInstance";
			HolesList = new PositionedObjectList<VergissMeinNicht.Entities.Räume_Inhalt.Empfang.Loch>();
			HolesList.Name = "HolesList";
			Loch1 = new VergissMeinNicht.Entities.Räume_Inhalt.Empfang.Loch(ContentManagerName, false);
			Loch1.Name = "Loch1";
			Loch2 = new VergissMeinNicht.Entities.Räume_Inhalt.Empfang.Loch(ContentManagerName, false);
			Loch2.Name = "Loch2";
			
			
			base.Initialize(addToManagers);

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			Background.AddToManagers(mLayer);
			Layer3.AddToManagers(mLayer);
			Layer1.AddToManagers(mLayer);
			TeddyInstance.AddToManagers(mLayer);
			Loch1.AddToManagers(mLayer);
			Loch2.AddToManagers(mLayer);
			base.AddToManagers();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				TeddyInstance.Activity();
				for (int i = HolesList.Count - 1; i > -1; i--)
				{
					if (i < HolesList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						HolesList[i].Activity();
					}
				}
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
				Background.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				Background.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				Background = null;
			}
			else
			{
				Background.MakeOneWay();
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				Layer3.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				Layer3.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				Layer3 = null;
			}
			else
			{
				Layer3.MakeOneWay();
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				Layer1.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				Layer1.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				Layer1 = null;
			}
			else
			{
				Layer1.MakeOneWay();
			}
			
			HolesList.MakeOneWay();
			if (TeddyInstance != null)
			{
				TeddyInstance.Destroy();
				TeddyInstance.Detach();
			}
			for (int i = HolesList.Count - 1; i > -1; i--)
			{
				HolesList[i].Destroy();
			}
			HolesList.MakeTwoWay();

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public override void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			base.PostInitialize();
			if (TeddyInstance.Parent == null)
			{
				TeddyInstance.X = 500f;
			}
			else
			{
				TeddyInstance.RelativeX = 500f;
			}
			if (TeddyInstance.Parent == null)
			{
				TeddyInstance.Y = 150f;
			}
			else
			{
				TeddyInstance.RelativeY = 150f;
			}
			HolesList.Add(Loch1);
			if (Loch1.Parent == null)
			{
				Loch1.X = 50f;
			}
			else
			{
				Loch1.RelativeX = 50f;
			}
			if (Loch1.Parent == null)
			{
				Loch1.Y = 100f;
			}
			else
			{
				Loch1.RelativeY = 100f;
			}
			HolesList.Add(Loch2);
			if (Loch2.Parent == null)
			{
				Loch2.X = 120f;
			}
			else
			{
				Loch2.RelativeX = 120f;
			}
			if (Loch2.Parent == null)
			{
				Loch2.Y = 20f;
			}
			else
			{
				Loch2.RelativeY = 20f;
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
			TeddyInstance.RemoveFromManagers();
			for (int i = HolesList.Count - 1; i > -1; i--)
			{
				HolesList[i].Destroy();
			}
		}
		public override void AssignCustomVariables (bool callOnContainedElements)
		{
			base.AssignCustomVariables(callOnContainedElements);
			if (callOnContainedElements)
			{
				TeddyInstance.AssignCustomVariables(true);
				Loch1.AssignCustomVariables(true);
				Loch2.AssignCustomVariables(true);
			}
			if (TeddyInstance.Parent == null)
			{
				TeddyInstance.X = 500f;
			}
			else
			{
				TeddyInstance.RelativeX = 500f;
			}
			if (TeddyInstance.Parent == null)
			{
				TeddyInstance.Y = 150f;
			}
			else
			{
				TeddyInstance.RelativeY = 150f;
			}
			if (Loch1.Parent == null)
			{
				Loch1.X = 50f;
			}
			else
			{
				Loch1.RelativeX = 50f;
			}
			if (Loch1.Parent == null)
			{
				Loch1.Y = 100f;
			}
			else
			{
				Loch1.RelativeY = 100f;
			}
			if (Loch2.Parent == null)
			{
				Loch2.X = 120f;
			}
			else
			{
				Loch2.RelativeX = 120f;
			}
			if (Loch2.Parent == null)
			{
				Loch2.Y = 20f;
			}
			else
			{
				Loch2.RelativeY = 20f;
			}
		}
		public override void ConvertToManuallyUpdated ()
		{
			base.ConvertToManuallyUpdated();
			TeddyInstance.ConvertToManuallyUpdated();
			for (int i = 0; i < HolesList.Count; i++)
			{
				HolesList[i].ConvertToManuallyUpdated();
			}
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
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/empfang/background.scnx", contentManagerName))
			{
			}
			Background = FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/empfang/background.scnx", contentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/empfang/layer3.shcx", contentManagerName))
			{
			}
			Layer3 = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/empfang/layer3.shcx", contentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/empfang/layer1.shcx", contentManagerName))
			{
			}
			Layer1 = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/empfang/layer1.shcx", contentManagerName);
			VergissMeinNicht.Entities.Teddy.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static new object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "Background":
					return Background;
				case  "Layer3":
					return Layer3;
				case  "Layer1":
					return Layer1;
			}
			return null;
		}
		public static new object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "Background":
					return Background;
				case  "Layer3":
					return Layer3;
				case  "Layer1":
					return Layer1;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "Background":
					return Background;
				case  "Layer3":
					return Layer3;
				case  "Layer1":
					return Layer1;
			}
			return null;
		}


	}
}
