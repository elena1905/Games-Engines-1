﻿using System;
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
        protected float fireRate;
        protected float elapsedTime;

        protected Vector2 centre;

        public override void LoadContent()
        {
            base.LoadContent();

            Alive = true;

            // Fire 10 bullets per second
            fireRate = 10.0f;
            elapsedTime = 100.0f;

            // Speed of the sprite
            speed = 100.0f;

            sprite = Game1.Instance.Content.Load<Texture2D>("smalltank");

            // Set the centre of the sprite
            centre.X = sprite.Width / 2;
            centre.Y = sprite.Height / 2;
        }

        public override void Update(GameTime gameTime) 
        {
            // Get elapsed game time in seconds
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // get keyboard key state
            KeyboardState keyState = Keyboard.GetState();

            // Calculate the orientation vector for the tank
            look.X = (float) Math.Sin(rotation);
            look.Y = (float)-Math.Cos(rotation);

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
                pos -= look * speed * timeDelta;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                pos.X += (speed * timeDelta);
            }

            // Window boundaries detect
            if (pos.X < centre.X)
            {
                pos.X += speed * timeDelta;
            }
            if (pos.X > Game1.Instance.ScreenWidth - centre.X)
            {
                pos.X -= speed * timeDelta;
            }
            if (pos.Y < centre.Y)
            {
                pos.Y += speed * timeDelta;
            }
            if (pos.Y > Game1.Instance.ScreenHeight - centre.Y)
            {
                pos.Y -= speed * timeDelta;
            }

            // Fire a bullet at most ten times in a second
            if (keyState.IsKeyDown(Keys.Space))
            {
                if (elapsedTime > (1.0f / fireRate))
                {
                    fireBullet();
                    elapsedTime = 0;
                }
            }
            
            elapsedTime += timeDelta;

            if (elapsedTime >= 100)
            {
                elapsedTime = 100;
            }

            // Exit the game when Escape key pressed
            if (keyState.IsKeyDown(Keys.Escape))
            {
                Game1.Instance.Exit();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(sprite, pos, null, Color.White, rotation, centre, Vector2.One, SpriteEffects.None, 1);
        }

        // Method to fire a bullet
        protected void fireBullet()
        {
            Bullet bullet = new Bullet();

            // Why load content explicitly???
            bullet.LoadContent();

            // Set bullet position where it should be fired
            bullet.pos = pos + look * centre.Y;

            // Set direction at which bullet should be fired
            bullet.look = look;

            Game1.Instance.children.Add(bullet);
        }
    }
}
