using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Missile : SimpleObject
    {
        public double Vitesse = 100;

        public Missile(Vecteur2D position)
        {
            base.Position = position;
            base.Lives = 1;
            base.Image = SpaceInvaders.Properties.Resources.shoot1;
        }


        public override void Update(Game gameInstance, double deltaT)
        {
            base.Position.y -= Vitesse * deltaT;
            if (base.Position.y < 0)
                base.Lives = 0;

            foreach(GameObject obj in gameInstance.gameObjects)
            {
                obj.Collision(this);
            }
            
        }

        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
                this.Lives = m.Lives = 0;

        }
    }
}
