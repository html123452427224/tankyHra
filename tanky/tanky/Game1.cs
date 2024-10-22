using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace tanky
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D Texture;
        private Vector2 Position;
        private Rectangle Rectangle;
        private Color Color;
        private Rectangle projectile;
        private Color projectileColor;
        private Vector2 projectilePosition;
        private Texture2D projectileTexture;
        private Boolean shootProjectile;
        private Vector2 projectileDireciton;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Color = Color.Green;
            Rectangle = new Rectangle(50, 50, 50, 50);
            Position = new Vector2(10, 10);
            Texture = new Texture2D(GraphicsDevice, 1, 1);
            Texture.SetData<Color>(new Color[] { Color });
            projectile = new Rectangle(0,0,10,10);
            projectileColor = Color.Brown;
            projectileTexture = new Texture2D(GraphicsDevice,1,1);
            projectileTexture.SetData<Color>(new Color[] { projectileColor });


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var keyboardState = Keyboard.GetState();
            

            if (keyboardState.IsKeyDown(Keys.W) && Position.Y > 0)
            {
                Position.Y = Position.Y - 1;
            }
            if (keyboardState.IsKeyDown(Keys.A) && Position.X > 0)
            {
                Position.X = Position.X - 1;
            }

            if (keyboardState.IsKeyDown(Keys.S) && Position.Y < _graphics.PreferredBackBufferHeight-Rectangle.Height)
            {
                Position.Y = Position.Y + 1;
            }

            if (keyboardState.IsKeyDown(Keys.D) && Position.X < _graphics.PreferredBackBufferWidth - Rectangle.Width)
            {
                Position.X = Position.X + 1;
            }
            // TODO: Add your update logic here
         
            var mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            var direction = mousePosition - Position;
            direction.Normalize();
            if (keyboardState.IsKeyDown(Keys.Q) && !shootProjectile )
            {
                
                shootProjectile = true;
               
                projectilePosition = Position;
                projectileDireciton = direction;
               



            }
           

            if (shootProjectile)
            {
                projectilePosition += projectileDireciton * 5f;
                if (projectilePosition.X < 0 || projectilePosition.X > _graphics.PreferredBackBufferWidth || projectilePosition.Y < 0 || projectilePosition.Y > _graphics.PreferredBackBufferHeight)
                {
                    shootProjectile = false;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(Texture, Position, Rectangle, Color);
            _spriteBatch.End();

            if (shootProjectile)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(projectileTexture, projectilePosition, projectile, projectileColor);
                _spriteBatch.End();
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
