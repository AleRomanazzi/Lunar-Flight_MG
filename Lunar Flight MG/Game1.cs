using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;



namespace Lunar_Flight_MG
{
    public class Game1 : Game
    {
        private int puntos = 0, wind = 0, fuel = 1000;
        GraphicsDeviceManager graphics;
        private SpriteBatch inGame, fondo, inicio, nivel;
        private double movement;
        private Texture2D lvl1, lvl2, lvl3;
        private Texture2D Nave, Rock, moon, Base;
        private Rectangle RockRec, moonRec, GoodRec, GoodRec2, GoodRec3, NaveRec, BaseRec;
        private Vector2 BasePos;
        MouseState mState;
        JoystickState joyst;
        SpriteFont texto, score, textWind, textFuel, htp;
        public bool start = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080
            };
            this.Window.AllowUserResizing = true;
            graphics.ApplyChanges();


            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            NaveRec = new Rectangle(200, 0, 50, 50);
            BaseRec = new Rectangle(200, 380, 160, 120);
            BasePos = new Vector2(200, 300);
            moonRec = new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            RockRec = new Rectangle(0, 200, 30, 30);
            GoodRec = new Rectangle(0, 200, 200, 200);
            GoodRec2 = new Rectangle(0, 250, 200, 200);
            GoodRec3 = new Rectangle(0, 300, 200, 200);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.Window.Title = "Lunar Flight";
            inGame = new SpriteBatch(GraphicsDevice);
            inicio = new SpriteBatch(GraphicsDevice);
            fondo = new SpriteBatch(GraphicsDevice);
            nivel = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here

            Nave = this.Content.Load<Texture2D>("Fuente/nave2p");
            Base = this.Content.Load<Texture2D>("Fuente/base");
            lvl1 = this.Content.Load<Texture2D>("Fuente/n1");
            lvl2 = this.Content.Load<Texture2D>("Fuente/n2");
            lvl3 = this.Content.Load<Texture2D>("Fuente/n3");
            Rock = this.Content.Load<Texture2D>("Fuente/asteroide");
            moon = this.Content.Load<Texture2D>("Fuente/luna");
            texto = this.Content.Load<SpriteFont>("Fuente/FuenteTexto");
            score = this.Content.Load<SpriteFont>("Fuente/FuenteTexto2");
            htp = this.Content.Load<SpriteFont>("Fuente/FuenteTexto2");
            textWind = this.Content.Load<SpriteFont>("Fuente/FuenteTexto2");
            textFuel = this.Content.Load<SpriteFont>("Fuente/FuenteTexto2");



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
            if (start == true)
            {
                movement = NaveRec.Y + 2 + 1 / 2 * 1.6;
                NaveRec.Y = (int)movement;
                Random rand = new Random();
                int rockSpeed = rand.Next(5, 10);
                RockRec.X += rockSpeed;
            }


            mState = Mouse.GetState();
            joyst = Joystick.GetState(1);
            IsMouseVisible = false;
            //NaveRec.X = mState.X;

            //Wind settings
            if (puntos >= 0)
            {
                Random rd = new Random();
                wind = rd.Next(-5, 5);
                NaveRec.X += wind;
            }
            if (puntos >= 600)
            {
                Random rd = new Random();
                wind = rd.Next(-8, 8);
                NaveRec.X += wind;

            }
            if (puntos >= 1000)
            {
                Random rd = new Random();
                wind = rd.Next(-12, 12);
                NaveRec.X += wind;
            }

            GamePadState state = GamePad.GetState(PlayerIndex.One);

            //Joystick and Keyboard Settings
            if (GamePad.GetCapabilities(PlayerIndex.One).HasLeftXThumbStick)
            {
                NaveRec.X += (int)(state.ThumbSticks.Left.X * 10.0f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                NaveRec.X += 6;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                NaveRec.X -= 6;
            }
            if (mState.LeftButton == ButtonState.Pressed || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                fuel -= 1;
                NaveRec.Y -= 5;
                Nave = this.Content.Load<Texture2D>("Fuente/nave2");
            }
            if (mState.LeftButton == ButtonState.Released && GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Released)
            {
                Nave = this.Content.Load<Texture2D>("Fuente/nave2p");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                start = true;
            }

            //Collision settings

            /*if (RockRec.Intersects(NaveRec))
            {
                Exit(); //Cambiar por texto y pausar juego o resetear.
            }
            */
            if (NaveRec.Intersects(BaseRec))
            {
                puntos += 200;
                Random rd = new Random();
                int rect = rd.Next(0, 400);
                BaseRec = new Rectangle(rect, 380, 160, 120);
                NaveRec = new Rectangle(rect, 0, 50, 50);
                RockRec = new Rectangle(rect, rect, 30, 30);

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

            fondo.Begin();
            fondo.Draw(moon, moonRec, Color.White);
            fondo.End();

            if (start == false)
            {
                inicio.Begin();
                inicio.DrawString(texto, "Lunar Flight", new Vector2(200, 160), Color.DarkGreen);
                inicio.DrawString(htp, "How to play:", new Vector2(200, 240), Color.Black);
                inicio.DrawString(htp, "Keyboard: A-D to move and L for propulsion.", new Vector2(200, 260), Color.Black);
                inicio.DrawString(htp, "Joystick: Stick(L) to move and X for propulsion.", new Vector2(200, 280), Color.Black);
                inicio.DrawString(htp, "Press START(Joystick) or INTRO to start landing", new Vector2(200, 350), Color.WhiteSmoke);
                inicio.End();
            }
            if (start == true)
            {
                inGame.Begin();
                inGame.DrawString(score, "Score: " + puntos, new Vector2(0, 0), Color.White);
                inGame.DrawString(textWind, "Wind: " + wind, new Vector2(0, 30), Color.White);
                inGame.DrawString(textWind, "Fuel: " + fuel + "L", new Vector2(0, 60), Color.White);
                inGame.Draw(Nave, NaveRec, Color.White);
                inGame.Draw(Base, BaseRec, Color.White);

                if (puntos >= 0 && puntos < 600)
                {
                    nivel.Begin();
                    nivel.Draw(lvl1, GoodRec, Color.White);
                    nivel.End();
                }
                if (puntos >= 600 && puntos < 1000)
                {
                    nivel.Begin();
                    nivel.Draw(lvl2, GoodRec, Color.White);
                    nivel.End();
                    inGame.Draw(Rock, RockRec, Color.White);

                }
                if (puntos >= 1000)
                {
                    nivel.Begin();
                    nivel.Draw(lvl3, GoodRec, Color.White);
                    nivel.End();
                    inGame.Draw(Rock, RockRec, Color.White);
                }
                inGame.End();
            }


            base.Draw(gameTime);
        }
    }
}
