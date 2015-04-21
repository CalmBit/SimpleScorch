using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimpleScorch.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleScorch
{
    /// <summary>
    /// Instance container for types of ManagerBase.
    /// </summary>
    public class ManagerContainer
    {
        /// <summary>
        /// Holds every manager registered.
        /// </summary>
        public Dictionary<string, ManagerBase> managerDictionary;
        public int managerID = 0;
        public ManagerContainer()
        {
            managerDictionary = new Dictionary<string, ManagerBase>();
        }

        /// <summary>
        /// Adds a manager to managerDictionary - adding it to:
        ///     - Tick Queue
        ///     - Render Queue (if enabled)
        /// </summary>
        /// <param name="name">Name to regeister manager under - duplicates dissalowed.</param>
        /// <param name="manager">Manager to register.</param>
        /// <returns>Registration Successful?</returns>
        public bool RegisterManager(string name, ManagerBase manager)
        {
            if (managerDictionary.ContainsKey(name)) return false;
            manager.id = managerID;
            managerID++;
            managerDictionary.Add(name, manager);
            return true;
        }

        /// <summary>
        /// Update every registered manager.
        /// </summary>
        /// <param name="gameTime">Instance of GameTime to read from.</param>
        public void UpdateAll(GameTime gameTime)
        {
            foreach(KeyValuePair<string, ManagerBase> managerObject in managerDictionary)
            {
                managerObject.Value.UpdateManager(gameTime);
            }
        }

        /// <summary>
        /// Render those registered managers that wish to be rendered.
        /// </summary>
        /// <param name="spriteBatch">Instance of SpriteBatch to render to.</param>
        public void RenderAll(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<string, ManagerBase> managerObject in managerDictionary)
            {
                if(managerObject.Value.rendersEachTick) managerObject.Value.RenderManager(spriteBatch);
            }
        }
        /// <summary>
        /// Update a specific registered manager - will throw exception if non-existant.
        /// </summary>
        /// <param name="name">Name of the registered manager to update.</param>
        /// <param name="gameTime">Instance of GameTime to read from.</param>
        public void UpdateOne(string name, GameTime gameTime)
        {
            if (!managerDictionary.ContainsKey(name)) throw new Exception("Tried to update non-existant manager from the ManagerContainer!");
            else managerDictionary[name].UpdateManager(gameTime);
        }
        /// <summary>
        /// Render a specific registered manager - will throw exceptions if non-existant or non-renderable.
        /// </summary>
        /// <param name="name">Name of the registered manager to render.</param>
        /// <param name="spriteBatch">Instance of SpriteBatch to render to.</param>
        public void RenderOne(string name, SpriteBatch spriteBatch)
        {
            if (!managerDictionary.ContainsKey(name)) throw new Exception("Tried to render non-existant manager from the ManagerContainer!");
            else if (!managerDictionary[name].rendersEachTick) throw new Exception("Tried to render non-rendering manager from the ManagerContainer!");
            else managerDictionary[name].RenderManager(spriteBatch);
        }

        /// <summary>
        /// Deletes a specific registered manager - will throw exceptions if non-existant.
        /// </summary>
        /// <param name="name">Name of the registered manager to delete.</param>
        public void PurgeOne(string name)
        {
            if (!managerDictionary.ContainsKey(name)) throw new Exception("Tried to purge non-existant manager from the ManagerContainer!");
            else managerDictionary.Remove(name);
        }
    }
}
