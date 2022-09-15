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
            oilColors.Add(new Color(0, 90, 101));
            oilColors.Add(new Color(0, 104, 109));
            oilColors.Add(new Color(0, 82, 90));
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

            var acidColors = new ColorSamples();
            acidColors.Add(new Color(136, 241, 9));
            acidColors.Add(new Color(130, 229, 9));
            acidColors.Add(new Color(167, 201, 25));
            _colors.Add(MaterialType.Acid, acidColors);

            var lavaColors = new ColorSamples();
            lavaColors.Add(new Color(234, 99, 8));
            lavaColors.Add(new Color(216, 91, 8)); 
            lavaColors.Add(new Color(233, 24, 0));
            lavaColors.Add(new Color(235, 55, 11));
            lavaColors.Add(new Color(196, 15, 30));
            _colors.Add(MaterialType.Lava, lavaColors);

            var titanColors = new ColorSamples();
            titanColors.Add(new Color(210, 203, 214));
            titanColors.Add(new Color(200, 193, 204));
            _colors.Add(MaterialType.Titan, titanColors);

            var plantColors = new ColorSamples();
            plantColors.Add(new Color(85, 162, 92));
            plantColors.Add(new Color(93, 179, 100));
            plantColors.Add(new Color(46, 100, 71));
            plantColors.Add(new Color(50, 129, 82));
            _colors.Add(MaterialType.Plant, plantColors);

            var methaneColors = new ColorSamples();
            methaneColors.Add(new Color(242, 214, 60, 70));
            methaneColors.Add(new Color(242, 214, 60, 60));
            methaneColors.Add(new Color(242, 214, 60, 80));
            _colors.Add(MaterialType.Methane, methaneColors);

            var burningGasColors = new ColorSamples();
            burningGasColors.Add(new Color(221, 245, 253));
            burningGasColors.Add(new Color(194, 227, 242));
            burningGasColors.Add(new Color(178, 220, 236));
            burningGasColors.Add(new Color(158, 228, 254));
            burningGasColors.Add(new Color(122, 214, 251));
            burningGasColors.Add(new Color(93, 199, 247));
            burningGasColors.Add(new Color(72, 189, 243));
            burningGasColors.Add(new Color(58, 157, 215));
            burningGasColors.Add(new Color(47, 133, 194));
            burningGasColors.Add(new Color(33, 95, 158));
            burningGasColors.Add(new Color(7, 30, 80));
            _colors.Add(MaterialType.BurningGas, burningGasColors);

            var ashColors = new ColorSamples();
            ashColors.Add(new Color(133, 130, 117));
            ashColors.Add(new Color(114, 112, 100));
            ashColors.Add(new Color(95, 93, 84));
            ashColors.Add(new Color(152, 149, 134));
            _colors.Add(MaterialType.Ash, ashColors);

            var iceColors = new ColorSamples();
            iceColors.Add(new Color(231, 213, 222));
            iceColors.Add(new Color(176, 220, 222));
            iceColors.Add(new Color(127, 203, 205));
            iceColors.Add(new Color(137, 220, 221));
            _colors.Add(MaterialType.Ice, iceColors);

            var obsidianColors = new ColorSamples();
            obsidianColors.Add(new Color(37, 0, 35));
            obsidianColors.Add(new Color(31, 18, 53));
            obsidianColors.Add(new Color(2, 9, 15));
            obsidianColors.Add(new Color(58, 50, 71));
            obsidianColors.Add(new Color(3, 1, 33));
            _colors.Add(MaterialType.Obsidian, obsidianColors);

            var dirtColors = new ColorSamples();
            dirtColors.Add(new Color(123, 85, 56));
            dirtColors.Add(new Color(152, 108, 72));
            dirtColors.Add(new Color(123, 85, 56));
            _colors.Add(MaterialType.Dirt, dirtColors);

            var seedColors = new ColorSamples();
            seedColors.Add(new Color(222, 115, 203));
            seedColors.Add(new Color(194, 112, 204));
            seedColors.Add(new Color(179, 112, 204));
            seedColors.Add(new Color(157, 111, 205));
            seedColors.Add(new Color(111, 109, 206));
            seedColors.Add(new Color(98, 96, 181));
            _colors.Add(MaterialType.Seed, seedColors);

            var virusColors = new ColorSamples();
            virusColors.Add(new Color(255, 0, 35));
            virusColors.Add(new Color(210, 0, 35));
            virusColors.Add(new Color(180, 0, 35));
            virusColors.Add(new Color(150, 0, 35));
            virusColors.Add(new Color(120, 0, 35));
            virusColors.Add(new Color(90, 0, 35));
            virusColors.Add(new Color(60, 0, 35));
            virusColors.Add(new Color(30, 0, 35));
            virusColors.Add(new Color(10, 0, 35));
            virusColors.Add(new Color(0, 0, 35));
            _colors.Add(MaterialType.Virus, virusColors);
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
            else
            if (type == MaterialType.BurningGas)
            {
                var value = Utils.Clamp(lifeTime * _colors[type].Count / Constants.BurningGasLifeTime, 0, _colors[type].Count - 1);
                return _colors[type][value];
            }
            else
            if (type == MaterialType.Virus)
            {
                var value = Utils.Clamp(lifeTime * _colors[type].Count / Constants.VirusLifeTime, 0, _colors[type].Count - 1);
                return _colors[type][value];
            }

            return _colors[type][_r.Next(0, _colors[type].Count)];
        }

        public static MaterialType MatchColor(byte r, byte g, byte b, byte a)
        {
            foreach (var palette in _colors)
            {
                var firstColor = palette.Value[0];

                if (firstColor.R == r &&
                    firstColor.G == g &&
                    firstColor.B == b &&
                    firstColor.A == a)
                {
                    return palette.Key;
                }
            }

            return MaterialType.Empty;
        }

        public static Color GetFirstTone(MaterialType type)
        {
            return _colors[type][0];
        }
    }
}
