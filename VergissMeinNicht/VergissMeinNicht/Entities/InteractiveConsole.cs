using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

using FlatRedBall.Graphics;
using System.Linq;
//using Noesis.Javascript;
//using KallSoft.Console;

namespace VergissMeinNicht.Entities
{
    public partial class InteractiveConsole
	{
        #region Constants and Variables

        protected const string ECHOED_INPUT_PREPEND = "---> ";

        protected List<Text> _displayedText;
        protected double _lastScrollTime = 0;
        protected int _displayStartIndex;
        protected string _currentCategory;

        /// <summary>
        /// Stored strings is a round-robin array, so once filled the earliest
        /// stored string is the first to be overwritten
        /// </summary>
        protected string[] _storedStrings;

        /// <summary>
        /// Points to the index of the latest stored string
        /// </summary>
        protected int _currentStringIndex;

        #endregion

        #region Properties

        /// <summary>
        /// Determines if the console is open or not
        /// </summary>
        public bool IsOpen { get { return UILayer.Visible; } }

        public Keys ConsoleToggleKey { get; set; }
        public Keys ConsoleScrollUpKey { get; set; }
        public Keys ConsoleScrollDownKey { get; set; }

        #endregion

        #region Public Methods

        public void ToggleConsole()
        {
            if (IsOpen)
                HideConsole();
            else
                ShowConsole();
        }

        public void ChangeSubscribedConsoleCategory(string newCategory)
        {
            ConsoleManager.Instance.UnregisterOutputHandler(AddTextToConsole);
            ConsoleManager.Instance.RegisterOutputUpdateHandler(AddTextToConsole, newCategory);
            _currentCategory = newCategory;
        }

        #endregion

        #region FRB Methods

        private void CustomInitialize()
		{
            InitConsoleDisplay();

            UILayer.Visible = false;
            Window.ScaleX = UILayer.LayerCameraSettings.OrthogonalWidth / 2f;
            Window.ScaleY = ((UILayer.LayerCameraSettings.OrthogonalHeight / 2f) * PercentOfScreen);
            Window.RelativeY = UILayer.LayerCameraSettings.OrthogonalHeight * (1 - PercentOfScreen);
            Window.AttachTo(this, false);

            _storedStrings = new string[MaxStoredLines];
            _currentCategory = StartingConsoleCategory;
            ChangeSubscribedConsoleCategory(StartingConsoleCategory);
            ConsoleManager.Instance.RegisterObjectToConsole(this, "ConsoleUI", this);
		}

		private void CustomActivity()
		{
            ProcessInput();
		}

		private void CustomDestroy()
		{
            ConsoleManager.Instance.UnregisterOutputHandler(AddTextToConsole);
            ConsoleManager.Instance.UnregisterObjectsFromSource(this);
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        #endregion

        
    }
}
