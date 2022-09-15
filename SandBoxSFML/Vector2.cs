namespace SandBoxSFML
{
    public sealed class Vector2
    {
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public static Vector2 Lerp(Vector2 start, Vector2 end, float amount)
        {
            var x = Interpolate(start.X, end.X, amount);
            var y = Interpolate(start.Y, end.Y, amount);

            return new Vector2(x, y);
        }

        private static float Interpolate(float from, float to, float amount)
        {
            return (1.0f - amount) * from + amount * to;
        }
    }
}
