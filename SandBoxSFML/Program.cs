using System;
using SFML.Graphics;
using SFML.System;
using SandBoxSFML.Materials;

namespace SandBoxSFML
{
    class Program
    {
        private const int _width = 800;
        private const int _height = 600;
        private static World _world;
        private static float _delta;
        public static RenderWindow Window { get; private set; }

        private static void Main(string[] args)
        {
            _world = new World(_width, _height);
            _world.SetMaterial(MaterialType.Sand);
            _world.SetRadius(10);

            Window = new RenderWindow(new SFML.Window.VideoMode(_width, _height), "SandBox", SFML.Window.Styles.Close);
            Window.SetVerticalSyncEnabled(true);
            Window.Closed += OnClosed;

            var clock = new Clock();
            while (Window.IsOpen)
            {
                _delta = clock.Restart().AsSeconds();

                Window.DispatchEvents();
                _world.Update();

                Window.Clear(Color.Black);
                Window.Draw(_world.Sprite);

                Window.Display();

                Console.WriteLine(_world.FPS);
            }
        }

        private static void OnClosed(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}
