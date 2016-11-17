using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1
{
    class GameObject
    {
        public bool estVivant;
        public int vitesse;
        public int hauteur;
        public int longueur;
        public Texture2D sprite;
        public Rectangle position;
        public int vie;

        public Rectangle GetRect() //1 Pour sauver plusieurs lignes de code
        {
            Rectangle rectColision;
            rectColision.X = (int)this.position.X;
            rectColision.Y = (int)this.position.Y;
            rectColision.Height = this.sprite.Height;
            rectColision.Width = this.sprite.Width;

            return rectColision;
        }
    }
}
