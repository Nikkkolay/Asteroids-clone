namespace AsteroidsCore
{
    /// <summary>
    /// Базовый класс для объектов игры Asteroids.
    /// </summary>
    public abstract class GameObject
    {
        /// <value>Центр объекта по оси Х.</value>  
        public int CenterX { get; set; }
        /// <value>Центр объекта по оси Y.</value>  
        public int CenterY { get; set; }
        /// <value>Растояние, преодолеваемое за единицу времени, объекта по оси Х.</value>  
        public float SpeedX { get; set; }
        /// <value>Растояние, преодолеваемое за единицу времени, объекта по оси Y.</value> 
        public float SpeedY { get; set; }
        /// <value>Максимальное растояние, которое объект может преодолеть за единицу времени.</value> 
        public float MaxSpeed { get; set; }

        /// <summary>Пустой конструктор.</summary>
        public GameObject() { }
        /// <param name="centerX">Центр объекта по оси Х.</param>
        /// <param name="centerY">Центр объекта по оси Y.</param>
        /// <param name="speedX">Растояние, преодолеваемое за единицу времени, объекта по оси Х.</param>
        /// <param name="speedY">Растояние, преодолеваемое за единицу времени, объекта по оси Y.</param>
        /// <param name="maxSpeed">Максимальное растояние, которое объект может преодолеть за единицу времени.</param>
        public GameObject(int centerX, int centerY, float speedX, float speedY, float maxSpeed)
        {
            CenterX = centerX;
            CenterY = centerY;
            SpeedX = speedX;
            SpeedY = speedY;
            MaxSpeed = maxSpeed;
        }

        /// <summary>
        /// Метод отрисовывающий объект.
        /// </summary>
        /// <param name="list">Набор параметров необходимых для отрисовки</param>
        public abstract void Drow(params object[] list);

        /// <summary>
        /// Метод проверяющий находится ли переданная точка (х, у) внутри объекта.
        /// Используется для проверки на попадание в объект.
        /// </summary>
        /// <param name="x">Координата точки по оси Х</param>
        /// <param name="y">Координата точки по оси Y</param>
        /// <returns>Возвращает true, если точка внутри объекта, иначе возвращает false</returns>
        public abstract bool IsInside(int x, int y);
    }
}
