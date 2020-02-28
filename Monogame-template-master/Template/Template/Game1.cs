using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Template
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D xwing;
        private Texture2D xwing2;
        private Vector2 xwingPos = new Vector2(350, 300);
        private Vector2 xwingPos2 = new Vector2(350, -100);
        private List<Vector2> xwingBulletPos = new List<Vector2>();      

        KeyboardState kNewState;
        KeyboardState kOldState;

        Rectangle hitbox = new Rectangle (100,100,100,100);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            xwing = Content.Load<Texture2D>("xwing");
            xwing2 = Content.Load<Texture2D>("xwing");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            kNewState = Keyboard.GetState();

            if (kNewState.IsKeyDown(Keys.Right))
                xwingPos.X += 10;
            if (kNewState.IsKeyDown(Keys.Left))
                xwingPos.X -= 10;

            if (kNewState.IsKeyDown(Keys.Space) && kOldState.IsKeyUp(Keys.Space))
            {
                xwingBulletPos.Add(xwingPos);
                
            }

            for (int i = 0; i < xwingBulletPos.Count; i++)
            {
                xwingBulletPos[i] = xwingBulletPos[i] - new Vector2(0, 2);

            }

            List<Vector2> temp = new List<Vector2>();
            foreach(var item in xwingBulletPos)
            {
                if (item.Y >= 50)
                {
                    temp.Add(item);
                }
            }

            xwingBulletPos = temp;

            kOldState = kNewState;

            xwingPos2.Y++;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(xwing, xwingPos, Color.White);
            spriteBatch.Draw(xwing2, xwingPos2, Color.White);

            foreach (Vector2 bulletPos in xwingBulletPos)
            {
                Rectangle rec = new Rectangle();
                rec.Location = bulletPos.ToPoint();
                rec.Size = new Point(3, 100);
                spriteBatch.Draw(xwing, rec, Color.Red);
            }

             
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}


