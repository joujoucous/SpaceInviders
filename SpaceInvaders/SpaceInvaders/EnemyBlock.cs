using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders
{
    class EnemyBlock : GameObject
    {
        public HashSet<SpaceShip> enemyShips;
        int baseWidth;
        Size size;
        Vecteur2D Position { get; set; }
        public double Vitesse = 30;
        public double direction = 1;

        public EnemyBlock(Vecteur2D p, int w)
        {
            this.Position = p;
            this.baseWidth = w;
            enemyShips = new HashSet<SpaceShip>();
            size = new Size(w, 0);
        }

        public void AddLine(int nbShips, int nbLives, Bitmap shipImage)
        {
            int diviseur = nbShips - 1;
            if (diviseur == 0) diviseur = 1;
            double space = (baseWidth-(nbShips*shipImage.Width))/diviseur;

            foreach (SpaceShip sp in enemyShips)
                sp.Position.y += shipImage.Size.Height + 10;

            for (int i = 0; i < nbShips; i++)
            {
                Vecteur2D p = new Vecteur2D(Position.x, Position.y);
                enemyShips.Add(new SpaceShip(p, nbLives, shipImage));
                p.x= (shipImage.Width + space)*i;
            }
        }
        void UpdateSize()
        {
            double xMax = 0;
            double yMax=0;
            double xMin = 1000000;
            double yMin = 1000000;
            foreach (SpaceShip sp in enemyShips){
                xMax = Math.Max(xMax, sp.Position.x + sp.Image.Size.Width);
                yMax = Math.Max(yMax, sp.Position.y + sp.Image.Size.Height);
                xMin = Math.Min(xMin, sp.Position.x);
                yMin = Math.Min(yMin, sp.Position.y);

            }
            size.Width= (int)xMax - (int)xMin;
            size.Height=(int)yMax - (int)yMin;

        }

        public override void Collision(Missile m)
        {
            foreach(SpaceShip sp in enemyShips){
                sp.Collision(m);
            }
           this.UpdateSize();//ou quand un ennemie est retiré de la liste?
        }

        /// <summary>
        /// Render the game object
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            foreach (SpaceShip sp in enemyShips)
            {
                graphics.DrawImage(sp.Image, (float)sp.Position.x, (float)sp.Position.y, sp.Image.Width, sp.Image.Height);
            }
        }

        public override bool IsAlive()
        {
            enemyShips.RemoveWhere(SpaceShip => !SpaceShip.IsAlive());//cad ici
            return enemyShips.Count > 0 ? true : false;
        }

        /// <summary>
        /// Update the state of a game objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public override void Update(Game gameInstance, double deltaT)
        {

            if (Position.x + size.Width + (Vitesse * deltaT * direction) >= gameInstance.gameSize.Width || Position.x + (Vitesse * deltaT * direction) <= 0)
            {
                Console.WriteLine(Position.x + size.Width + (Vitesse * deltaT * direction) + "----" + gameInstance.gameSize.Width);

                direction *= -1;
                Vitesse += 10;
                foreach (SpaceShip sp in enemyShips)
                {
                    sp.Position.y += 10;
                }

            }
            else{
                foreach (SpaceShip sp in enemyShips)
                {
                    sp.Position.x += Vitesse * deltaT * direction;
                }
            }

            Position.x += Vitesse * deltaT * direction;

        }
    }
}
