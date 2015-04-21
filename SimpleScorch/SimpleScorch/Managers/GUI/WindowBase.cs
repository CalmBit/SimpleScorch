using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleScorch.Managers.GUI
{
    //TEXT WINDOW RENDER START: 4,2
    public abstract class WindowBase
    {
        protected int zOrder;
        protected Vector2 at;
        public WindowBase(Vector2 at)
        {
            this.at = at;
        }
        
        public virtual void UpdateWindow(GameTime gameTime)
        {

        }

        public virtual void RenderWindow(SpriteBatch spriteBatch)
        {
            
        }
    }
}
