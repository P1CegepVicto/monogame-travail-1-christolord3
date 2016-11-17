using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Projet1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject heros;
        GameObject Background;
        GameObject missile;
        GameObject ennemi;
        Random random = new Random();
        SoundEffect son;
        SoundEffectInstance missileSon;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ToggleFullScreen();
            fenetre = new Rectangle(0,0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            heros = new GameObject();
            Background = new GameObject();
            heros.estVivant = true;
            Background.estVivant = true;
            Background.sprite = Content.Load<Texture2D>("Background.jpg");
            heros.vitesse = 10;
            heros.hauteur = 48;
            heros.longueur = 160;
            heros.sprite = Content.Load<Texture2D>("Vaisseau.png");
            heros.position = heros.sprite.Bounds;
            heros.position.X = fenetre.Width / 2;
            heros.position.Y = fenetre.Height - heros.hauteur;
            Background.position = Background.sprite.Bounds;
            Background.hauteur = 1920;
            Background.longueur = 1080;
            missile = new GameObject();
            missile.estVivant = false;
            missile.sprite = Content.Load<Texture2D>("Missile.png");
            missile.position = missile.sprite.Bounds;
            missile.vitesse = 15;
            ennemi = new GameObject();
            ennemi.estVivant = true;
            ennemi.sprite = Content.Load<Texture2D>("ennemi.png");
            ennemi.position = ennemi.sprite.Bounds;
            ennemi.hauteur = 112;
            ennemi.longueur = 220;
            ennemi.position.X = random.Next(0,fenetre.Width - ennemi.longueur);
            ennemi.position.Y = 20;
            ennemi.vitesse = 10;
            son = Content.Load<SoundEffect>("Sounds\\Missile");
            missileSon = son.CreateInstance();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            clavier();
            mouvementNonJoueur();
            collision();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        public void mouvementNonJoueur()
        {
            if(missile.estVivant == true)
            {
                missile.position.Y -= missile.vitesse;
            }
            if(ennemi.estVivant == true)
            {
                ennemi.vitesse = random.Next(-90, 90);
                ennemi.position.X += ennemi.vitesse;
            }
        }
        public void collision()
        {
            if(heros.position.X < 0)
            {
                heros.position.X = 0;
            }
            if(heros.position.Y < (fenetre.Height / 4) * 3)
            {
                heros.position.Y = (fenetre.Height / 4) * 3;
            }
            if (heros.position.Y > fenetre.Height - heros.hauteur)
            {
                heros.position.Y = fenetre.Height - heros.hauteur;
            }
            if (heros.position.X > fenetre.Width - heros.longueur)
            {
                heros.position.X = fenetre.Width - heros.longueur;
            }
            if(missile.position.Y < 0)
            {
                missile.estVivant = false;
            }
            if(ennemi.position.X < 0)
            {
                ennemi.position.X = 0;
            }
            if (ennemi.position.X > fenetre.Width - ennemi.longueur)
            {
                ennemi.position.X = fenetre.Width - ennemi.longueur;
            }
        }
        public void clavier()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                heros.position.Y -= heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                heros.position.X -= heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                heros.position.Y += heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                heros.position.X += heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                heros.position.X += heros.vitesse;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (missile.estVivant != true)
                {
                    missileSon.Play();
                    missile.estVivant = true;
                    missile.position.X = (heros.position.Width / 2) + heros.position.X;
                    missile.position.Y = (heros.position.Height / 2) + heros.position.Y;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);
            spriteBatch.Begin();
            spriteBatch.Draw(Background.sprite, Background.position, Color.White);
            spriteBatch.Draw(ennemi.sprite, ennemi.position, Color.White);
            if (missile.estVivant == true)
            {
                spriteBatch.Draw(missile.sprite, missile.position, Color.White);
            }
            spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
