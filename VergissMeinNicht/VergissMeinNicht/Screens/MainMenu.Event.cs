using System;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using FlatRedBall.Audio;
using FlatRedBall.Screens;
using VergissMeinNicht.Entities;
using VergissMeinNicht.Entities.Enemies;
using VergissMeinNicht.Entities.Räume_Inhalt.Schlafraum;
using VergissMeinNicht.Screens;
namespace VergissMeinNicht.Screens
{
	public partial class MainMenu
	{
        void OnExitGameButtonClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            //onClick makes OkCancelInstance visible
            this.CurrentState = MainMenu.VariableState.ExitOkCancelVisible;
        }
        void OnOptionsButtonClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            //takes the player to the Options Screen
            this.MoveToScreen(typeof(OptionsScreen).FullName);
        }
        void OnNewGameButtonClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            this.MoveToScreen(typeof(Empfang).FullName);
        }
        void OnLoadGameButtonClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            this.MoveToScreen(typeof(LoadGameScreen).FullName);
        }
        void OnOkCancelInstanceOkClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            //exits the game when the player clicks OK
            FlatRedBallServices.Game.Exit();
        }
        void OnOkCancelInstanceCancelClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            //OnClick Cancel makes OkCancelInstance invisible
            this.CurrentState = MainMenu.VariableState.MainButtonsVisible;
        }
        void OnZooLevelButtonClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            this.MoveToScreen(typeof(ZooLevel).FullName);
        }

	}
}
