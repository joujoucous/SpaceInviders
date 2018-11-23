using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class SpaceShip : SimpleObject
    {
        public double speedPixelPerSecond=100;

        Missile missile;

        public SpaceShip(Vecteur2D position, int lives, Bitmap image)
        {
            base.Position = position;
            base.Lives = lives;
            base.Image = image;
            this.missile = null;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            //do nothing
        }
        public void Shoot(Game gameInstance)
        {
            if(missile==null || !missile.IsAlive()){
                this.missile = new Missile(new Vecteur2D(base.Position.x + (base.Image.Width) / 2, base.Position.y - base.Image.Height));
                gameInstance.AddNewGameObject(missile);
            }
                      
        }

        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            int degas = Math.Min(m.Lives, this.Lives);
            m.Lives -= degas;
            this.Lives -= degas;
            if (this.Lives < 0) this.Lives = 0;
        }
    }
}
