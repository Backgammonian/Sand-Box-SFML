using System.Collections.Generic;
using SFML.Graphics;

namespace SandBoxSFML.Materials
{
    public static class MaterialColor
    {
        private readonly static Dictionary<MaterialType, Color> _colors;

        static MaterialColor()
        {
            _colors = new Dictionary<MaterialType, Color>();
            _colors.Add(MaterialType.Empty, Color.Transparent);
            _colors.Add(MaterialType.Sand, new Color(194, 178, 128));
            _colors.Add(MaterialType.Water, new Color(35, 137, 218));
        }

        public static Color GetColor(MaterialType type)
        {
            return _colors[type];
        }
    }
}
