using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleScorch.Managers
{
    /// <summary>
    /// Abstract base class for all other Managers.
    /// Defines basic manager behaviour and expectations.
    /// </summary>
    public abstract class ManagerBase
    {
        /// <summary>
        /// Does this manager have anything to render?
        /// </summary>
        public bool rendersEachTick;
        /// <summary>
        /// Container assigned ID number - completely arbitrary.
        /// </summary>
        public int id;
        public ManagerBase(bool render)
        {
            rendersEachTick = render;
        }
        public virtual void OnLoad(ContentManager content)
        {

        }

        /// <summary>
        /// Run through a tick.
        /// </summary>
        /// <param name="gameTime">Instance of GameTime to read from.</param>
        public virtual void UpdateManager(GameTime gameTime)
        {

        }

        /// <summary>
        /// Render through a tick.
        /// </summary>
        /// <param name="spriteBatch">Instance of SpriteBatch to render to.</param>
        public virtual void RenderManager(SpriteBatch spriteBatch)
        {
            if (!rendersEachTick) throw new Exception("Non-Rendering manager tried to render itself!");
        }
    }
}
