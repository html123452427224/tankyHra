using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace tankyHra
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
        private float rotationAngle;
        private Vector2 aiPosition;
        private List<Vector2> points;
        private Vector2 targetPoint;
        private float aiSpeed = 1f;
        private List<AI> aiEnemy;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
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
            projectile = new Rectangle(0, 0, 10, 10);
            projectileColor = Color.Brown;
            projectileTexture = new Texture2D(GraphicsDevice, 1, 1);
            projectileTexture.SetData<Color>(new Color[] { projectileColor });
            points = generatePoints(20);
            aiPosition = new Vector2(10, 10);
            targetPoint = getRandomPoint();
            aiEnemy = new List<AI>();
            
            for (int i = 0; i < 9; i++)
            {
                aiEnemy.Add(new AI(getRandomPoint(), aiSpeed));
            }



            // TODO: use this.Content to load your game content here
        }
        
        private List<Vector2> generatePoints(int spacing)
        {
            var points = new List<Vector2>();
            for (int x = 0; x < _graphics.PreferredBackBufferWidth; x += spacing)
            {
                for (int y = 0; y < _graphics.PreferredBackBufferHeight; y += spacing)
                {
                    points.Add(new Vector2(x, y));
                }
            }
            return points;
        }
        
        private Vector2 getRandomPoint()
        {
            var random = new Random();
            int index = random.Next(points.Count);
            return points[index];
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var keyboardState = Keyboard.GetState();
            Vector2 forwardDirection = new Vector2((float)Math.Cos(rotationAngle), (float)Math.Sin(rotationAngle));
            

            if (keyboardState.IsKeyDown(Keys.W))
            {
                Position += forwardDirection;
            }
            if (keyboardState.IsKeyDown(Keys.A) )
            {
                rotationAngle -= 0.05f;
            }

            if (keyboardState.IsKeyDown(Keys.S) )
            {
                Position -= forwardDirection;
            }

            if (keyboardState.IsKeyDown(Keys.D) )
            {
                rotationAngle += 0.05f;
            }
            
            Position.X = MathHelper.Clamp(Position.X, Rectangle.Width / 2, _graphics.PreferredBackBufferWidth - Rectangle.Width / 2);
            Position.Y = MathHelper.Clamp(Position.Y, Rectangle.Height / 2, _graphics.PreferredBackBufferHeight - Rectangle.Height / 2);
            // TODO: Add your update logic here
            
            foreach (var ai in aiEnemy)
            {
                ai.Update(points);
            }
            
            Vector2 direction1 = targetPoint - aiPosition;
            if (direction1.Length() < aiSpeed)
            {
                aiPosition = targetPoint;
                targetPoint = getRandomPoint();
            }
            else
            {
                direction1.Normalize();
                aiPosition += direction1  * aiSpeed;
            }

            var mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            var direction = mousePosition - Position;
            direction.Normalize();
            if (keyboardState.IsKeyDown(Keys.Q) && !shootProjectile)
            {

                shootProjectile = true;

                projectilePosition = Position;
                projectileDireciton = direction;




            }


            if (shootProjectile)
            {
                projectilePosition += projectileDireciton * 5f;
                CheckProjectileCollision();
                if (projectilePosition.X < 0 || projectilePosition.X > _graphics.PreferredBackBufferWidth || projectilePosition.Y < 0 || projectilePosition.Y > _graphics.PreferredBackBufferHeight)
                {
                    shootProjectile = false;
                }
            }

            base.Update(gameTime);
        }
        
        private void CheckProjectileCollision()
        {
            float hitRadius = 50f; 

            for (int i = 0; i < aiEnemy.Count; i++)
            {
                var ai = aiEnemy[i];
        
               
                var distance = Vector2.Distance(projectilePosition, ai.Position);

                
                if (distance < hitRadius)
                {
                   
                    aiEnemy.RemoveAt(i);
                    shootProjectile = false; 
                    break; 
                }
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(Texture, Position, null, Color, rotationAngle, new Vector2(Texture.Width/2, Texture.Height/2), new Vector2(50,50), SpriteEffects.None, 0);
            foreach (var ai in aiEnemy)
            {
                _spriteBatch.Draw(Texture, ai.Position, Rectangle, Color.Red);
            }
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