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
	public partial class OptionsScreen
	{
		void OnSoundEffectCheckBoxClickTunnel (FlatRedBall.Gui.IWindow callingWindow)
		{
			if (this.SoundEffectCheckBoxClick != null)
			{
				SoundEffectCheckBoxClick(callingWindow);
			}
		}
		void OnSoundCheckBoxClickTunnel (FlatRedBall.Gui.IWindow callingWindow)
		{
			if (this.SoundCheckBoxClick != null)
			{
				SoundCheckBoxClick(callingWindow);
			}
		}
		void OnBackButtonClickTunnel (FlatRedBall.Gui.IWindow callingWindow)
		{
			if (this.BackButtonClick != null)
			{
				BackButtonClick(callingWindow);
			}
		}
	}
}
