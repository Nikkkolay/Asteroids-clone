using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AsteroidsClone
{

    public class AsteroidsClone : Game
    {
        const float PI = 3.141592f;
        int currentTime = 0; // сколько времени прошло
        int period = 16; // период обновления в миллисекундах, примерно 60 кадров в секунду 

        GraphicsDeviceManager graphics;          
        SpriteBatch spriteBatch;                 //Объект для отрисовки спрайтов
        SpriteSpaceship spaceship;               //Объект корабля игрока
        Rectangle movingFrame, driftingFrame;    //Прямоугольники с текстурой летящего и дрейфующего корабля
        Song backgroundMusic;                    //Музыка на заднем фоне игры
        SoundEffect engineEffect;                //Звук работающего двигателя корабля
        SoundEffectInstance engineEffectInstance;//Объект для управления звуковым эффектом engineEffect

        public AsteroidsClone()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() => base.Initialize();

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures:
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Загрузка аудио и текстур
            engineEffect = Content.Load<SoundEffect>("spaceship_engine sound");// загружаем звук двигателя 
            engineEffectInstance = engineEffect.CreateInstance();// создаем объект для управления им
            engineEffectInstance.Volume = 0.1f;// устанавливаем громкость двигателя на 10%

            backgroundMusic = Content.Load<Song>("JOJO1");// загружаем музыку для заднего фона
            MediaPlayer.IsRepeating = true;// после завершения музыки она начнется с начала
            MediaPlayer.Volume = 0.1f;// устанавливаем громкость музыки на 10%
            MediaPlayer.Play(backgroundMusic);// включить музыку

            Texture2D spaceshipTexture = Content.Load<Texture2D>("spaceship");// загружаем текстуру с кораблями
            driftingFrame = new Rectangle(0, 0, spaceshipTexture.Width, spaceshipTexture.Height / 2);// верхняя половина текстуры
            movingFrame = new Rectangle(0, spaceshipTexture.Height / 2, spaceshipTexture.Width, spaceshipTexture.Height / 2);// нижняя половина текстуры
            #endregion

            #region создание корабля игрока
            spaceship = new SpriteSpaceship(
                texture:            spaceshipTexture,
                width:              spaceshipTexture.Width,
                height:             spaceshipTexture.Height / 2,
                isEngineWorking:    false,
                angle:              0,
                impulseAngle:       0,
                acceleration:       1.0f,
                deceleration:       0.01f,
                speedRotate:        0.06f,
                numLasers:          3,
                numMaxLasers:       3,
                timeRecoveryLasers: 180,
                centerX:            Window.ClientBounds.Width / 2,
                centerY:            Window.ClientBounds.Height / 2,
                speedX:             0,
                speedY:             0,
                maxSpeed:           3);
            #endregion
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
            KeyboardState keyboardState = Keyboard.GetState(); // получаем информацию о состоянии клавиатуры
            MouseState currentMouseState = Mouse.GetState(); // получаем информацию о состоянии мышки

            //если нажата клавиша Esc или паузавыходим из игры
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            // добавляем к текущему времени прошедшее время
            currentTime += gameTime.ElapsedGameTime.Milliseconds;
            // пока текущее время превышает период обновления
            while (currentTime > period)//
            {
                currentTime -= period;// вычитаем из текущего времени период обновления

                double spaceshipSpeed = Math.Sqrt(spaceship.SpeedX * spaceship.SpeedX
                + spaceship.SpeedY * spaceship.SpeedY);//вычисляем текущую скорость корабля

                #region обработка клавиш движения и поворотов
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    //если скорость корабля меньше максимальной увеличиваем скорость
                    spaceshipSpeed = spaceshipSpeed + spaceship.Acceleration > spaceship.MaxSpeed ? spaceship.MaxSpeed
                        : spaceshipSpeed + spaceship.Acceleration;

                    if (!spaceship.IsEngineWorking)// если статус корабля "двигатель выключен"
                        spaceship.IsEngineWorking = true;// меняем его

                    //запоминаем под каким углом был корабль со включенными двигателями
                    spaceship.ImpulseAngle = spaceship.Angle;

                    if (engineEffectInstance.State != SoundState.Playing)// если звук двигателя выключен
                        engineEffectInstance.Play();// включаем его
                }
                else if (keyboardState.IsKeyUp(Keys.Space))
                {
                    //если скорость корабля больше 0 уменьшаем ее
                    spaceshipSpeed = spaceshipSpeed - spaceship.Deceleration < 0 ? 0
                        : spaceshipSpeed - spaceship.Deceleration;

                    if (engineEffectInstance.State == SoundState.Playing)// если звук двигателя включен
                        engineEffectInstance.Stop();// выключаем его

                    if (spaceship.IsEngineWorking)// если статус корабля "двигатель включен"
                        spaceship.IsEngineWorking = false;// меняем его
                }

                if (keyboardState.IsKeyDown(Keys.A))// если нажата английская A
                    spaceship.Angle -= spaceship.SpeedRotate; // поворачиваем налево
                if (spaceship.Angle <= -2 * PI) // если угол <= -2PI
                    spaceship.Angle += 2 * PI;// увеличиваем его

                if (keyboardState.IsKeyDown(Keys.D))// если нажата английская D
                    spaceship.Angle += spaceship.SpeedRotate;// поворачиваем направо
                if (spaceship.Angle >= 2 * PI) // если угол >= 2PI
                    spaceship.Angle -= 2 * PI;// уменьшаем его
                #endregion

                #region вычисление скорости корабля
                //вычисляем какая часть скорости уйдет на ось Х и ось У
                spaceship.SpeedX = (float)(spaceshipSpeed * Math.Cos(spaceship.ImpulseAngle));
                spaceship.SpeedY = (float)(spaceshipSpeed * Math.Sin(spaceship.ImpulseAngle));
                #endregion

                #region вычисляем новые координаты корабля
                int intSpeedX = (int)spaceship.SpeedX; //количество пройденых пикселей по оси Х
                int intSpeedY = (int)spaceship.SpeedY; //количество пройденых пикселей по оси У

                if (spaceship.CenterX + intSpeedX > Window.ClientBounds.Width)//если вышли за правый край
                    spaceship.CenterX = 0;// появляемся в левом
                else if (spaceship.CenterX + intSpeedX < 0)// если вышли за левый край
                    spaceship.CenterX = Window.ClientBounds.Width;// появляемся в правом
                else
                    spaceship.CenterX += intSpeedX;// иначе просто двигаемся

                if (spaceship.CenterY + intSpeedY > Window.ClientBounds.Height)// если вышли за нижний край
                    spaceship.CenterY = 0;// появляемся в верхнем
                else if (spaceship.CenterY + intSpeedY < 0)// если вышли за верхний край
                    spaceship.CenterY = Window.ClientBounds.Height;// появляемя в нижнем
                else
                    spaceship.CenterY += intSpeedY;// иначе просто двигаемся
                #endregion

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);//очищаем экран и заливаем одним цветом

            spriteBatch.Begin();

            if (spaceship.IsEngineWorking)//если включен двигатель
                spaceship.Drow(spriteBatch, movingFrame);//используем текстуру с работающим двигателем 
            else
                spaceship.Drow(spriteBatch, driftingFrame);//иначе без работающего двигателя

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
