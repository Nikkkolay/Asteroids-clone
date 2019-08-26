using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AsteroidsCore;

namespace AsteroidsClone
{   /// <summary>
    /// Корабль со спрайтовой отрисовкой и прямоугольным хитбоксом
    /// </summary>
    class SpriteSpaceship: Spaceship
    {
        /// <value>Текстура со всеми изображениями корабля.</value>
        public Texture2D Texture { get; set; }
        /// <value>Ширина хитбокса корабля.</value>
        public int Width { get; set; }
        /// <value>Высота хитбокса корабля.</value>
        public int Height { get; set; }
        /// <value>Свойство с информацией работает ли двигатель корабля.</value>
        public bool IsEngineWorking { get; set; }

        public SpriteSpaceship() { }
        /// <param name="texture">Текстура со всеми изображениями корабля.</param>
        /// <param name="height">Высота корабля.</param>
        /// <param name="width">Ширина корабля.</param>
        /// <param name="angle">Угол в радианах, на который повернут корабль.</param>
        /// <param name="impulseAngle">Угол в радианах, на который был повернут корабль последний раз с включенным двигателем.</param>
        /// <param name="acceleration">Скорость, куторую набирает корабль за единицу времени при работающем двигателе.</param>
        /// <param name="deceleration">Скорость, куторую теряет корабль за единицу времени при выключенном двигателе.</param>
        /// <param name="speedRotate">Угол в радианах на который может повернуть корабль за единицу времени.</param>
        /// <param name="numLasers">Текущее количество лазерных зарядов.</param>
        /// <param name="numMaxLasers">Максимальное количество лазерных зарядов.</param>
        /// <param name="timeRecoveryLasers">Количество единиц времени, за которе востанавливается один лазерный заряд.</param>
        /// <param name="centerX">Центр объекта по оси Х.</param>
        /// <param name="centerY">Центр объекта по оси Y.</param>
        /// <param name="speedX">Растояние, преодолеваемое за единицу времени, объекта по оси Х.</param>
        /// <param name="speedY">Растояние, преодолеваемое за единицу времени, объекта по оси Y.</param>
        /// <param name="maxSpeed">Максимальное растояние, которое объект может преодолеть за единицу времени.</param>
        public SpriteSpaceship(Texture2D texture,
            int height,
            int width,
            bool isEngineWorking,
            float angle,
            float impulseAngle,
            float acceleration,
            float deceleration,
            float speedRotate,
            byte numLasers,
            byte numMaxLasers,
            short timeRecoveryLasers,
            int centerX,
            int centerY,
            float speedX,
            float speedY,
            float maxSpeed) : base(angle, impulseAngle, acceleration,
                deceleration, speedRotate, numLasers, numMaxLasers,
                timeRecoveryLasers, centerX, centerY, speedX, speedY, maxSpeed)
        {
            Texture = texture;
            Width = width;
            Height = height;
            IsEngineWorking = isEngineWorking;
        }

        /// <summary>
        /// Метод отрисовывающий объект.
        /// </summary>
        /// <param name="list">
        /// Первым параметром надо передать объект SpriteBatch,
        /// Вторым - объект Rectangle выделяющий нужную область на текстуре
        /// </param>
        public override void Drow(params object[] list)
        {

            SpriteBatch spriteBatch;
            //если переданы верные параметры
            if (list.Length == 2 && (spriteBatch = list[0] as SpriteBatch) != null && list[1] is Rectangle)
            {
                spriteBatch.Draw(//отрисовываем текстуру
                    texture:         Texture,
                    position:        new Vector2(CenterX, CenterY),
                    sourceRectangle: (Rectangle)list[1],
                    color:           Color.White,
                    rotation:        Angle,
                    origin:          new Vector2(Width / 2, Height / 2),
                    scale:           1f,
                    effects:         SpriteEffects.None,
                    layerDepth:      0);
            }
            else
                throw new Exception("Переданные параметры не соответствуют ожидаемым. " +
                    "Ожидалось получить объекты классов SpriteBatch и Rectangle.");
        }

        /// <summary>
        /// Метод проверяющий находится ли переданная точка (х, у) внутри корабля.
        /// Используется для проверки на попадание в корабль.
        /// </summary>
        /// <param name="x">Координата точки по оси Х</param>
        /// <param name="y">Координата точки по оси Y</param>
        /// <returns>Возвращает true, если точка внутри корабля, иначе возвращает false</returns>
        public override bool IsInside(int x, int y)
        {
            if (x > CenterX - 0.5f * Width && x < CenterX + 0.5f * Width &&
                y > CenterY - 0.5f * Height && y < CenterY + 0.5f * Height)
                return true;
            return false;
        }
    }
}
