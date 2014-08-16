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
	public partial class OptionsScreen
	{
        void OnSoundCheckBoxClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            if(this.MusicCheckBox.CurrentState == CheckBox.VariableState.Checked)
            {
            
            }
        }
        void OnSoundEffectCheckBoxClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            if(this.SoundEffectCheckBox.CurrentState == CheckBox.VariableState.Checked)
            {
              
            }
            
            
        }
        void OnBackButtonClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            this.MoveToScreen(typeof(MainMenu).FullName);
        }

	}
}
