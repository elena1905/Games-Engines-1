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
    class AITank : Tank
    {
        float targetRotation;
        bool attack;

        Vector2 basis;

        public override void Initialize()
        {
            base.Initialize();

            pos.X = centre.X;
            pos.Y = 400;

            speed = 20.0f;

            attack = false;

            basis = new Vector2(0, -1);
        }

        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Tank enemyTank = Game1.Instance.Tank;
            Vector2 toTarget = enemyTank.pos - pos;
            float distance = toTarget.Length();

            if (distance < 150.0f)
            {
                if (toTarget.X < 0)
                {
                    targetRotation = (float)-Math.Acos(Vector2.Dot(basis, Vector2.Normalize(toTarget)));
                }
                else
                {
                    targetRotation = (float)Math.Acos(Vector2.Dot(basis, Vector2.Normalize(toTarget)));
                }
                attack = true;
            }
            else
            {
                attack = false;
            }

            float rotAmount = 1.0f;

            if (!attack)
            {   // Not attacked: AI movement
                look.X = (float)-Math.Cos(rotation);
                look.Y = (float)Math.Sin(rotation);

                pos += look * speed * timeDelta;

                // Window boundaries detect
                if (pos.X < sprite.Width / 2)
                {
                    rotation = MathHelper.Pi;
                }
                if (pos.X > Game1.Instance.Window.ClientBounds.Width - sprite.Width / 2)
                {
                    rotation = 0.0f;
                }
                if (pos.Y < sprite.Height / 2)
                {
                    pos.Y += speed * timeDelta;
                }
                if (pos.Y > Game1.Instance.Window.ClientBounds.Height - sprite.Height / 2)
                {
                    pos.Y -= speed * timeDelta;
                }
            }
            else
            {   // Attacked: move towards playerTank and fire bullets
                float direction = targetRotation - rotation;

                if (direction < 0)
                {
                    rotation -= rotAmount * timeDelta;
                }
                else
                {
                    rotation += rotAmount * timeDelta;
                }

                look.X = (float)Math.Sin(rotation);
                look.Y = (float)-Math.Cos(rotation);

                pos += look * speed * timeDelta;

                if (elapsedTime > (1.0f / fireRate))
                {
                    fireBullet();
                    elapsedTime = 0;
                }

                elapsedTime += timeDelta;

                if (elapsedTime >= 100)
                {
                    elapsedTime = 100;
                }
            }

        }

        // Method to fire a bullet
        /*private void fireBullet()
        {
            Bullet bullet = new Bullet();

            // Why load content explicitly???
            bullet.LoadContent();

            // Set bullet position where it should be fired
            bullet.pos = pos + look * centre.Y;

            // Set direction at which bullet should be fired
            bullet.look = look;

            Game1.Instance.children.Add(bullet);
        }*/
    }
}
