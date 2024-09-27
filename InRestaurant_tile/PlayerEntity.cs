using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using System;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace InRestaurant_tile
{
    internal class PlayerEntity : IEntity, ICollisionActor
    {
        private readonly Game1 _game;
        public int Velocity = 4;
        private Texture2D Player;
        Texture2D playerPos;
        Vector2 move;
        public IShapeF Bounds { get; }
        private KeyboardState ks;
        private KeyboardState _oldKey;


        public PlayerEntity(Game1 game, IShapeF circleF)
        {
            _game = game;
            Bounds = circleF;

        }
        public void LoadContent(ContentManager content)
        {
            Player = content.Load<Texture2D>("Player-Sheet");
        }
        public virtual void Update(GameTime gameTime)
        {
            // KeyboardState ks = Keyboard.GetState();
            ks = Keyboard.GetState();
            Vector2 move = Vector2.Zero;

            if (ks.IsKeyDown(Keys.D) && Bounds.Position.X < _game.GetMapWidth() - ((RectangleF)Bounds).Width)
            {
                move = new Vector2(Velocity, 0) * gameTime.GetElapsedSeconds() * 50;
                Bounds.Position += move;
            }
            if (ks.IsKeyDown(Keys.A) && Bounds.Position.X > 0)
            {
                move = new Vector2(-Velocity, 0) * gameTime.GetElapsedSeconds() * 50;
                Bounds.Position += move;
            }
            if (ks.IsKeyDown(Keys.S) && Bounds.Position.Y < _game.GetMapHeight() - ((RectangleF)Bounds).Height)
            {
                move = new Vector2(0, 4) * gameTime.GetElapsedSeconds() * 50;
                Bounds.Position += move;
            }
            if (ks.IsKeyDown(Keys.W) && Bounds.Position.Y > 0)
            {
                move = new Vector2(0, -4) * gameTime.GetElapsedSeconds() * 50;
                Bounds.Position += move;
            }

        }
        public virtual void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3f);





        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            if (collisionInfo.Other.ToString().Contains("PlatformEntity"))
            {
                Bounds.Position -= collisionInfo.PenetrationVector;
            }
        }
    }
}
