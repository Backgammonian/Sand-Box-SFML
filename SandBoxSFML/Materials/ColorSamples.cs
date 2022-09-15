using System.Collections.Generic;
using SFML.Graphics;

namespace SandBoxSFML.Materials
{
    public sealed class ColorSamples
    {
        private readonly List<Color> _colors;

        public ColorSamples()
        {
            _colors = new List<Color>();
        }

        public int Count => _colors.Count;

        public Color this[int index]
        {
            get => _colors[index];
            private set => _colors[index] = value;
        }

        public void Add(Color color)
        {
            _colors.Add(color);
        }

        public void Remove(int index)
        {
            if (index >= 0 &&
                index < _colors.Count)
            {
                _colors.RemoveAt(index);
            }
        }
    }
}
