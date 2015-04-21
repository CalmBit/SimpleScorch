using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleScorch.Managers.GUI
{
    public class TextBox : WindowBase
    {
        string text;
        bool white;
        public TextBox(Vector2 at, string text, bool white) : base(at)
        {
            this.text = text;
            if (this.text == "") this.text = "ERROR: No text passed to text box!";
            this.white = white;
        }

        public override void RenderWindow(SpriteBatch spriteBatch)
        {
            base.RenderWindow(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.Draw(GUIManager.textWindow, at, Color.White);
            spriteBatch.End();
            ((GUIManager)SimpleScorch.managerContainer.managerDictionary["GUI"]).RenderStringRaw(spriteBatch, text, new Vector2(at.X + 8, at.Y + 4), white, new Vector2(8, 16));
        }
    }
}
