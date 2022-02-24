using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace SandBoxSFML.Materials
{
    public static class MaterialColor
    {
        private readonly static Dictionary<MaterialType, ColorSamples> _colors;
        private readonly static Random _r;

        static MaterialColor()
        {
            _r = new Random();
            _colors = new Dictionary<MaterialType, ColorSamples>();

            var sandColors = new ColorSamples();
            sandColors.Add(new Color(234, 191, 125));
            sandColors.Add(new Color(255, 170, 114));
            sandColors.Add(new Color(219, 154, 89));
            _colors.Add(MaterialType.Sand, sandColors);

            var waterColors = new ColorSamples();
            waterColors.Add(new Color(35, 137, 218));
            _colors.Add(MaterialType.Water, waterColors);

            var voidColor = new ColorSamples();
            voidColor.Add(Color.Transparent);
            _colors.Add(MaterialType.Empty, voidColor);
        }

        public static Color GetColor(MaterialType type)
        {
            return _colors[type][_r.Next(0, _colors[type].Count)];
        }
    }
}
