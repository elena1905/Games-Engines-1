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
    class Bullet : Entity
    {
        public override void Initialize()
        {
            pos.X = 100;
            pos.Y = 100;
            rotation = 0.0f;
            look.X = 0;
            look.Y = -1;
            Alive = true;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            sprite = Game1.Instance.Content.Load<Texture2D>("bullet");
        }

        public override void Update(GameTime gameTime) 
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //KeyboardState keyState = Keyboard.GetState();
            float speed = 85.0f;

            pos += look * speed * timeDelta;

            // Bounds checking
            if (pos.X < sprite.Width / 2 ||
                pos.Y < sprite.Height / 2||
                pos.X > Game1.Instance.Window.ClientBounds.Width - sprite.Width / 2 ||
                pos.Y > Game1.Instance.Window.ClientBounds.Height - sprite.Height / 2)
            {
                Alive = false;
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
    }
}