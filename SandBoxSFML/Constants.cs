using SFML.System;

namespace SandBoxSFML
{
    public class Constants
    {
        //it is convinient to present a gravity as unit vector multiplied by some value
        public static readonly Vector2i Gravity = new Vector2i(0, 1) * 1;
        public const int MaxRadius = 100;
    }
}
