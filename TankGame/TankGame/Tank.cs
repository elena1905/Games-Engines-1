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

namespace TankGame
{
    class Tank : Entity
    {

        public override void LoadContent()
        {
            base.LoadContent();

            sprite = Game1.Instance.Content.Load<Texture2D>("smalltank");
        }

        public override void Update(GameTime gameTime) 
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyState = Keyboard.GetState();
            float speed = 85.0f;

            // Calculate the correct look vector for the tank
            look.X = (float) Math.Sin(rotation);
            look.Y = (float)-Math.Cos(rotation);

            // Exit the game when Escape key pressed
            if (keyState.IsKeyDown(Keys.Escape))
            {
                Game1.Instance.Exit();
            }

            // Rotation
            if (keyState.IsKeyDown(Keys.Left))
            {
                rotation -= (5.0f * timeDelta);
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                rotation += (5.0f * timeDelta);
            }

            // Movement
            if (keyState.IsKeyDown(Keys.W))
            {
                pos += look * speed * timeDelta;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                pos.X -= (speed * timeDelta);
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                pos.Y += (speed * timeDelta);
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                pos.X += (speed * timeDelta);
            }

            // Fire bullet
            if (keyState.IsKeyDown(Keys.Space))
            {
                fireBullet();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 origin;
            origin.X = sprite.Width / 2;
            origin.Y = sprite.Height / 2;

            Game1.Instance.spriteBatch.Draw(sprite, pos, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 1);
            //Game1.Instance.spriteBatch.Draw(sprite, pos, Color.White);
        }

        // Method to fire a bullet
        private void fireBullet()
        {
            Bullet bullet = new Bullet();

            // Initialize bullet: set Alive true
            bullet.Initialize();

            // Why load content explicitly???
            bullet.LoadContent();

            // Set bullet position where it should be fired
            bullet.pos = pos + look * (sprite.Height / 2);

            // Set direction at which bullet should be fired
            bullet.look = look;

            Game1.Instance.children.Add(bullet);
        }
    }
}
