using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SimpleScorch.Managers.GUI;

namespace SimpleScorch
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SimpleScorch : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static ManagerContainer managerContainer;

        public SimpleScorch()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            managerContainer = new ManagerContainer();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //THIS IS WHERE MANAGERS WITH ONLOAD REQUIREMENTS SHOULD BE LOADED - IF NOT, YOU WON'T BE ABLE
            //TO SATISFY THE PARAM REQUIREMENTS. NULL REFERENCES GALORE!

            GUIManager gui = new GUIManager();
            gui.OnLoad(this.Content);
            TextBox textbox = new TextBox(new Vector2(128, 128), "A package for a... Ms.Alice?", true);
            TextBox textbox2 = new TextBox(new Vector2(128, 228), "I'm a Ms. Alice.\nWhat of it?", false);
            gui.currentWindows.Add("TestTextBox", textbox);
            gui.currentWindows.Add("TestTextBox2", textbox2);
            managerContainer.RegisterManager("GUI", gui);       
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            managerContainer.UpdateAll(gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            managerContainer.RenderAll(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
