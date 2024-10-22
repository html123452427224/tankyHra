using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            Texture.SetData<Color>(new Color[] { Color});
           
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                Position.Y = Position.Y - 1;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Position.X = Position.X - 1;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                Position.Y = Position.Y + 1;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                Position.X = Position.X + 1;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(Texture,Position,Rectangle,Color);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
