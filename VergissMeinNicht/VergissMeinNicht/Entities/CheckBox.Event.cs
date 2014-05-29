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
namespace VergissMeinNicht.Entities
{
	public partial class CheckBox
	{
        void OnClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            //checks what state the checkbox is in and changes it on click
            if(this.CurrentState == CheckBox.VariableState.Checked)
            {
                    this.CurrentState = CheckBox.VariableState.Unchecked;
            }
            else
            {
                    this.CurrentState = CheckBox.VariableState.Checked;
            }
        }

	}
}
