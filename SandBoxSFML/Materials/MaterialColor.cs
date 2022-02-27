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

            var voidColor = new ColorSamples();
            voidColor.Add(Color.Transparent);
            _colors.Add(MaterialType.Empty, voidColor);

            var sandColors = new ColorSamples();
            sandColors.Add(new Color(234, 191, 125));
            sandColors.Add(new Color(255, 170, 114));
            sandColors.Add(new Color(219, 154, 89));
            _colors.Add(MaterialType.Sand, sandColors);

            var waterColors = new ColorSamples();
            waterColors.Add(new Color(35, 137, 218));
            waterColors.Add(new Color(33, 125, 196));
            waterColors.Add(new Color(34, 130, 204));
            _colors.Add(MaterialType.Water, waterColors);

            var stoneColors = new ColorSamples();
            stoneColors.Add(new Color(136, 140, 141));
            stoneColors.Add(new Color(123, 126, 127));
            stoneColors.Add(new Color(128, 131, 132));
            _colors.Add(MaterialType.Stone, stoneColors);

            var oilColors = new ColorSamples();
            oilColors.Add(new Color(20, 16, 14));
            oilColors.Add(new Color(22, 18, 16));
            oilColors.Add(new Color(15, 12, 10));
            _colors.Add(MaterialType.Oil, oilColors);

            var fireColors = new ColorSamples();
            fireColors.Add(new Color(242, 220, 8));
            fireColors.Add(new Color(242, 195, 0));
            fireColors.Add(new Color(242, 168, 0));
            fireColors.Add(new Color(242, 140, 0));
            fireColors.Add(new Color(242, 90, 0));
            fireColors.Add(new Color(242, 30, 0));
            fireColors.Add(new Color(242, 0, 0));
            fireColors.Add(new Color(173, 0, 0));
            fireColors.Add(new Color(114, 0, 0));
            fireColors.Add(new Color(66, 0, 0));
            _colors.Add(MaterialType.Fire, fireColors);

            var steamColors = new ColorSamples();
            steamColors.Add(new Color(206, 206, 206, 255));
            steamColors.Add(new Color(206, 206, 206, 230));
            steamColors.Add(new Color(206, 206, 206, 210));
            steamColors.Add(new Color(206, 206, 206, 180));
            steamColors.Add(new Color(206, 206, 206, 150));
            steamColors.Add(new Color(206, 206, 206, 130));
            steamColors.Add(new Color(206, 206, 206, 90));
            steamColors.Add(new Color(206, 206, 206, 50));
            steamColors.Add(new Color(206, 206, 206, 30));
            steamColors.Add(new Color(206, 206, 206, 1));
            _colors.Add(MaterialType.Steam, steamColors);

            var smokeColors = new ColorSamples();
            smokeColors.Add(new Color(112, 106, 92, 255));
            smokeColors.Add(new Color(112, 106, 92, 230));
            smokeColors.Add(new Color(112, 106, 92, 210));
            smokeColors.Add(new Color(112, 106, 92, 180));
            smokeColors.Add(new Color(112, 106, 92, 150));
            smokeColors.Add(new Color(112, 106, 92, 130));
            smokeColors.Add(new Color(112, 106, 92, 90));
            smokeColors.Add(new Color(112, 106, 92, 50));
            smokeColors.Add(new Color(112, 106, 92, 30));
            smokeColors.Add(new Color(112, 106, 92, 1));
            _colors.Add(MaterialType.Smoke, smokeColors);


            var emberColors = new ColorSamples();
            emberColors.Add(new Color(214, 148, 23));
            emberColors.Add(new Color(189, 54, 20));
            emberColors.Add(new Color(146, 43, 24));
            _colors.Add(MaterialType.Ember, emberColors);

            var coalColors = new ColorSamples();
            coalColors.Add(new Color(74, 71, 71));
            coalColors.Add(new Color(56, 53, 53));
            coalColors.Add(new Color(40, 38, 38));
            _colors.Add(MaterialType.Coal, coalColors);

            var woodColors = new ColorSamples();
            woodColors.Add(new Color(105, 64, 40));
            woodColors.Add(new Color(122, 75, 47));
            woodColors.Add(new Color(114, 70, 44));
            _colors.Add(MaterialType.Wood, woodColors);
        }

        public static Color GetColor(MaterialType type, int lifeTime)
        {
            if (type == MaterialType.Fire)
            {
                var value = Utils.Clamp(lifeTime * _colors[type].Count / Constants.FireLifeTime, 0, _colors[type].Count - 1);
                return _colors[type][value];
            }
            else
            if (type == MaterialType.Steam)
            {
                var value = Utils.Clamp(lifeTime * _colors[type].Count / Constants.SteamLifeTime, 0, _colors[type].Count - 1);
                return _colors[type][value];
            }
            else
            if (type == MaterialType.Smoke)
            {
                var value = Utils.Clamp(lifeTime * _colors[type].Count / Constants.SmokeLifeTime, 0, _colors[type].Count - 1);
                return _colors[type][value];
            }

            return _colors[type][_r.Next(0, _colors[type].Count)];
        }
    }
}
