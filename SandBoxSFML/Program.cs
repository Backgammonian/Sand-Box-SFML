using System;
using System.Threading;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SandBoxSFML.Materials;
using SandBoxSFML.UI;

namespace SandBoxSFML
{
    class Program
    {
        private static uint _width;
        private static uint _height;
        private static World _world;
        private static SimpleUI _ui;
        private static float _delta;
        private static long _ticks;

        public static RenderWindow Window { get; private set; }

        private static void Main(string[] args)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            if (args.Length == 2 &&
                uint.TryParse(args[0], out uint width) &&
                uint.TryParse(args[1], out uint height))
            {
                _width = width;
                _height = height;
            }
            else
            {
                _width = 800;
                _height = 450;
            }

            _world = new World((int)_width, (int)_height);

            var font = new Font(Properties.Resources.Minecraft);
            _ui = new SimpleUI(new Vector2f(5, 25), new Vector2f(30, 200), new Color(255, 255, 255, 50), font, new Vector2f(5, 5));
            _ui.MaterialChanged += OnUIMaterialChanged;
            _ui.SelectionRadiusChanged += OnUISelectionRadiusChanged;

            _ui.SetMaterial(MaterialType.Sand);
            _ui.SetRadius(10);

            Window = new RenderWindow(new VideoMode(_width, _height), "SandBox", Styles.Close);
            Window.SetVerticalSyncEnabled(true);
            Window.Closed += (_, __) => Window.Close();
            Window.MouseButtonPressed += OnMouseButtonPressed;
            Window.MouseButtonReleased += OnMouseButtonReleased;
            Window.MouseMoved += OnMouseMoved;
            Window.MouseWheelScrolled += OnMouseWheelScrolled;
            Window.KeyPressed += OnKeyPressed;

            var clock = new Clock();
            while (Window.IsOpen)
            {
                _ticks += 1;
                if (_ticks == long.MaxValue)
                {
                    _ticks = 0;
                }

                _delta = clock.Restart().AsSeconds();

                Window.DispatchEvents();

                _world.Update();

                Window.Clear(Color.Black);
                Window.Draw(_world);
                Window.Draw(_ui);

                Window.Display();

                if (_ticks % 20 == 0)
                {
                    Console.WriteLine("FPS: " + _world.FPS);
                    Console.WriteLine(Math.Round(_delta * 1000, 2) + " ms per frame");
                }
            }
        }

        private static void OnUISelectionRadiusChanged(object sender, EventArgs e)
        {
            _world.SetRadius(_ui.SelectionRadius);
        }

        private static void OnUIMaterialChanged(object sender, EventArgs e)
        {
            _world.SetMaterial(_ui.SelectedMaterial);
        }

        private static void OnMouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            _ui.AddSelectionRadius((int)e.Delta);
            _ui.UpdatePosition(e.X, e.Y);
        }

        private static void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            _world.UpdateMousePosition(e.X, e.Y);
            _ui.UpdatePosition(e.X, e.Y);
        }

        private static void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (_ui.IsMouseInside(e.X, e.Y))
            {
                _ui.Input(e.X, e.Y);
            }
            else
            {
                _world.StartInput(e.Button, e.X, e.Y);
            }
        }

        private static void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (_world.IsUsed)
            {
                _world.StopInput();
            }
        }

        private static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                Window.Close();
            }
        }
    }
}
