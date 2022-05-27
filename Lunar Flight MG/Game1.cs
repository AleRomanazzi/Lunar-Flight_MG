using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace Lunar_Flight_MG
{
    public class Game1 : Game
    {
        private int coll = 0;
        GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch, spriteBatch2, spriteBatch3;
        private double movement;
        private Texture2D lvl1, lvl2, lvl3;
        private Texture2D Nave, Rock, moon, Base;
        private Rectangle RockRec, moonRec, GoodRec, GoodRec2, GoodRec3, NaveRec, BaseRec;
        private Vector2 BasePos;
        MouseState mState;
        SpriteFont texto, texto2;
        public bool start = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferWidth = 400,
                PreferredBackBufferHeight = 728
            };
            this.Window.AllowUserResizing = true;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            NaveRec = new Rectangle(200,0, 80, 80);
            BaseRec = new Rectangle(200, 380, 200, 150);
            BasePos = new Vector2(200, 300);
            //moonRec = new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            RockRec = new Rectangle(0, 200, 50, 50);
            GoodRec = new Rectangle(0, 200, 200, 200);
            GoodRec2 = new Rectangle(0, 250, 200, 200);
            GoodRec3 = new Rectangle(0, 300, 200, 200);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.Window.Title = "Lunar Flight";
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch2 = new SpriteBatch(GraphicsDevice);
            spriteBatch3 = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Nave = this.Content.Load<Texture2D>("Fuente/nave2p");
            Base = this.Content.Load<Texture2D>("Fuente/base");
            lvl1 = this.Content.Load<Texture2D>("Fuente/n1");
            lvl2 = this.Content.Load<Texture2D>("Fuente/n2");
            lvl3 = this.Content.Load<Texture2D>("Fuente/n3");
            Rock = this.Content.Load<Texture2D>("Fuente/asteroide");
            moon = this.Content.Load<Texture2D>("Fuente/luna");
            texto = this.Content.Load<SpriteFont>("Fuente/FuenteTexto");
            texto2 = this.Content.Load<SpriteFont>("Fuente/FuenteTexto2");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            /*if (mState.LeftButton == ButtonState.Pressed)
            {
                start = true;
            }
            */
            movement = NaveRec.Y + 2 + 1 / 2 * 1.6;
            NaveRec.Y = (int)movement;
            
            Random rand = new Random();
            int rockSpeed = rand.Next(5, 10);
            RockRec.X += rockSpeed;

          
            mState = Mouse.GetState();
            IsMouseVisible = false;
            NaveRec.X = mState.X;

            //Wind settings
            if (coll == 0)
            {
                Random rd = new Random();
                float wind = rd.Next(-5, 5);
                NaveRec.X += (int)wind;
            }
            if (coll == 1)
            {
                Random rd = new Random();
                float wind = rd.Next(-8, 8);
                NaveRec.X += (int)wind;

            }
            if (coll == 2)
            {
                Random rd = new Random();
                float wind = rd.Next(-12, 12);
                NaveRec.X += (int)wind;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                NaveRec.X += 6;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                NaveRec.X -= 6;
            }
            if (mState.LeftButton == ButtonState.Pressed)
            {
                NaveRec.Y -= 5;
                Nave = this.Content.Load<Texture2D>("Fuente/nave2");
            }
            if (mState.LeftButton == ButtonState.Released)
            {
                Nave = this.Content.Load<Texture2D>("Fuente/nave2p");
            }

            //Collision settings
            /*if (RockRec.Intersects(NaveRec))
            {
                Exit(); //Cambiar por texto y pausar juego o resetear.
            }
            */
            if (NaveRec.Intersects(BaseRec))
            {
                coll += 1;
                Random rd = new Random();
                int rect = rd.Next(0, 400);
                BaseRec = new Rectangle(rect, 380, 200, 150);
                NaveRec = new Rectangle(rect, 0, 80, 80);
                RockRec = new Rectangle(rect, rect, 50, 50);

                /*if (coll == 3)
                {
                    Exit();
                }
                */
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            /*
            spriteBatch3.Begin();
            spriteBatch3.Draw(moon, moonRec, Color.White);
            spriteBatch3.End();
            */
            spriteBatch2.Begin();
            spriteBatch2.DrawString(texto, "Lunar Flight", new Vector2(200, 200), Color.DarkGreen);
            spriteBatch2.DrawString(texto2, "Presiona Click Derecho para inciar el juego", new Vector2(200, 280), Color.White);
            spriteBatch2.End();
            spriteBatch.Begin();
            spriteBatch.Draw(Nave, NaveRec, Color.White);
            spriteBatch.Draw(Base, BaseRec, Color.White);
                if (coll == 0)
                {
                    spriteBatch.Draw(lvl1, GoodRec, Color.White);
                }
                if (coll == 1)
                {
                    spriteBatch.Draw(lvl2, GoodRec2, Color.White);
                    spriteBatch.Draw(Rock, RockRec, Color.White);

                }
                if (coll == 2)
                {
                    spriteBatch.Draw(lvl3, GoodRec3, Color.White);
                    spriteBatch.Draw(Rock, RockRec, Color.White);
                }
                spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
