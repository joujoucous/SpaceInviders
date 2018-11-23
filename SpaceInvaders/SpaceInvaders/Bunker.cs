using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Bunker : SimpleObject
    {

        public Bunker(Vecteur2D position)
        {
            base.Position = position;
            base.Lives = 1;
            base.Image = SpaceInvaders.Properties.Resources.bunker;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            //Do nothing
        }

        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            m.Lives -= numberOfPixelsInCollision;
        }
    }
}
