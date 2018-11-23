using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Vecteur2D
    {
        public double x, y;


        public Vecteur2D(double x =0.0, double y=0.0) 
        {
            this.x = x;
            this.y = y;
        }

        public double Norme
        {
            get
            {
                return Math.Sqrt((x * x) + (y * y));

            }
        }

        public static Vecteur2D operator +(Vecteur2D v1, Vecteur2D v2)
        { return new Vecteur2D(v1.x + v2.x, v1.y + v2.y); }

        public static Vecteur2D operator -(Vecteur2D v1, Vecteur2D v2)
        { return new Vecteur2D(v1.x - v2.x, v1.y - v2.y); }

        public static Vecteur2D operator -(Vecteur2D v)
        { return new Vecteur2D(-v.x, -v.y); }

        public static Vecteur2D operator *(Vecteur2D v, double d)
        { return new Vecteur2D(v.x*d, v.y*d); }

        public static Vecteur2D operator *(double d, Vecteur2D v)
        { return new Vecteur2D(v.x * d, v.y * d); }

        public static Vecteur2D operator /(Vecteur2D v, double d)
        { return new Vecteur2D(v.x / d, v.y / d); }
    }
}
