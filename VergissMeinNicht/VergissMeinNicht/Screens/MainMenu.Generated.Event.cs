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
using VergissMeinNicht.Entities.Räume_Inhalt.Empfang;
using VergissMeinNicht.Entities.Räume_Inhalt.Schlafraum;
using VergissMeinNicht.Screens;
namespace VergissMeinNicht.Screens
{
	public partial class MainMenu
	{
		void OnExitGameButtonClickTunnel (FlatRedBall.Gui.IWindow callingWindow)
		{
			if (this.ExitGameButtonClick != null)
			{
				ExitGameButtonClick(callingWindow);
			}
		}
		void OnOptionsButtonClickTunnel (FlatRedBall.Gui.IWindow callingWindow)
		{
			if (this.OptionsButtonClick != null)
			{
				OptionsButtonClick(callingWindow);
			}
		}
		void OnNewGameButtonClickTunnel (FlatRedBall.Gui.IWindow callingWindow)
		{
			if (this.NewGameButtonClick != null)
			{
				NewGameButtonClick(callingWindow);
			}
		}
		void OnLoadGameButtonClickTunnel (FlatRedBall.Gui.IWindow callingWindow)
		{
			if (this.LoadGameButtonClick != null)
			{
				LoadGameButtonClick(callingWindow);
			}
		}
		void OnOkCancelInstanceOkClickTunnel (FlatRedBall.Gui.IWindow callingWindow)
		{
			if (this.OkCancelInstanceOkClick != null)
			{
				OkCancelInstanceOkClick(callingWindow);
			}
		}
		void OnOkCancelInstanceCancelClickTunnel (FlatRedBall.Gui.IWindow callingWindow)
		{
			if (this.OkCancelInstanceCancelClick != null)
			{
				OkCancelInstanceCancelClick(callingWindow);
			}
		}
		void OnZooLevelButtonClickTunnel (FlatRedBall.Gui.IWindow callingWindow)
		{
			if (this.ZooLevelButtonClick != null)
			{
				ZooLevelButtonClick(callingWindow);
			}
		}
	}
}
