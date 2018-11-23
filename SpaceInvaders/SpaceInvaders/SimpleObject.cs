using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    abstract class SimpleObject:GameObject
    {
        public Vecteur2D Position { get; set; }
        public int Lives { get; set; }
        public Bitmap Image { get; set; }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(this.Image, (float)Position.x, (float)Position.y, Image.Width, Image.Height);
        }

        public override bool IsAlive()
        {
            return (Lives > 0);
        }
        override public void Collision(Missile m)
        {
            if (this != m)
            {
                if (IsRectangleEnglobantIntersect(m))
                {
                    IsCollisionPixelToPixel(m);

                }
            }

        }

        public bool IsRectangleEnglobantIntersect(Missile m)
        {

            double x1 = this.Position.x;
            double y1 = this.Position.y;
            double lx1 = this.Image.Width;
            double ly1 = this.Image.Height;

            double x2 = m.Position.x;
            double y2 = m.Position.y;
            double lx2 = m.Image.Width;
            double ly2 = m.Image.Height;

            return x2 <= (x1 + lx1) && y2 <= (y1 + ly1) && x1 <= (x2 + lx2) && y1 <= (y2 + ly2);
        }

        private void IsCollisionPixelToPixel(Missile m)
        {
            int cpt = 0;

            for (int i = 0; i < m.Image.Width; i++)
            {
                for (int j = 0; j < m.Image.Height; j++)
                {
                    //position du pixel dans le bumker
                    int xPix = i + (int)m.Position.x - (int)Position.x;
                    int yPix = j + (int)m.Position.y - (int)Position.y;

                    if (xPix >= 0 && yPix >= 0 && xPix < Image.Width && yPix < Image.Height)
                    {
                        if (Image.GetPixel(xPix, yPix).GetBrightness() < 0.01)
                        {
                            Image.SetPixel(xPix, yPix, Color.White);
                            cpt++;
                        }
                    }
                }


            }
            if (cpt > 0) OnCollision(m, cpt);
            //m.Lives -= cpt;
        }
        protected abstract void OnCollision(Missile m, int numberOfPixelsInCollision);

    }
}
