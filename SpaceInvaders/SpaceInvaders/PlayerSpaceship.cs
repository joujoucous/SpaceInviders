using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SpaceInvaders
{
    class PlayerSpaceship :  SpaceShip
    {
        public PlayerSpaceship(Vecteur2D position, int lives, Bitmap image) : base(position, lives, image)
        {
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            if (gameInstance.keyPressed.Contains(Keys.Left) && (base.Position.x - speedPixelPerSecond * deltaT) > 0)
            {
                base.Position.x -= base.speedPixelPerSecond * deltaT;
            }
            if (gameInstance.keyPressed.Contains(Keys.Right) && (base.Position.x + speedPixelPerSecond * deltaT) <= gameInstance.gameSize.Width - base.Image.Width)
            {
                base.Position.x += speedPixelPerSecond * deltaT;
            }
            if (gameInstance.keyPressed.Contains(Keys.Space))
            {
                Shoot(gameInstance);
            }
        }
    }
}
