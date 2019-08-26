namespace AsteroidsCore
{
    /// <summary>
    /// Базовый класс космического корабля без отрисовки и проверки на попадание
    /// </summary>
    public abstract class Spaceship : GameObject
    {
        /// <value>Угол в радианах, на который повернут корабль.</value>
        public float Angle { get; set; }
        /// <value>Угол в радианах, на который был повернут корабль
        /// последний раз с включенным двигателем.
        /// </value>
        public float ImpulseAngle { get; set; }
        /// <value>Скорость, куторую набирает корабль за единицу времени при работающем двигателе.</value>
        public float Acceleration { get; set; }
        /// <value>Скорость, куторую теряет корабль за единицу времени при выключенном двигателе.</value>
        public float Deceleration { get; set; }
        /// <value>Угол в радианах на который может повернуть корабль за единицу времени.</value>
        public float SpeedRotate { get; set; }
        /// <value>Текущее количество лазерных зарядов.</value>
        public byte NumLasers { get; set; }
        /// <value>Максимальное количество лазерных зарядов.</value>
        public byte NumMaxLasers { get; set; }
        /// <value>Количество кадров, за которе востанавливается один лазерный заряд.</value>
        public short TimeRecoveryLasers { get; set; }

        /// <summary>Пустой конструктор.</summary>
        public Spaceship() { }
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
        public Spaceship(
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
            float maxSpeed)
            : base(centerX,
                  centerY,
                  speedX,
                  speedY,
                  maxSpeed)
        {
            this.Angle = angle;
            this.ImpulseAngle = impulseAngle;
            this.Acceleration = acceleration;
            this.Deceleration = deceleration;
            this.SpeedRotate = speedRotate;
            this.NumLasers = numLasers;
            this.NumMaxLasers = numMaxLasers;
            this.TimeRecoveryLasers = timeRecoveryLasers;
        }
    }
}
