using System;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using BigGustave;
using SandBoxSFML.Materials;
using SandBoxSFML.UI;

namespace SandBoxSFML
{
    class Program
    {
        private const string _appName = "SandBox";
        private static uint _width;
        private static uint _height;
        private static World _world;
        private static SimpleUI _ui;
        private static float _delta;
        private static long _ticks;
        private static bool _isSimulating;

        [STAThread]
        private static void Main()
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            _width = 800;
            _height = 700;
            
            _world = new World((int)_width, (int)_height);
            _isSimulating = true;

            var font = new Font(Properties.Resources.Minecraft);
            _ui = new SimpleUI(new Vector2f(5, 25), 
                new Vector2f(80, 670), 
                new Color(255, 255, 255, 50), 
                font, 
                new Vector2f(5, 5), 
                new Vector2f(715, 25), 
                new Vector2f(80, 180),
                new Vector2f(715, 5));
            //yes, magic numbers

            _ui.MaterialChanged += OnUIMaterialChanged;
            _ui.SelectionRadiusChanged += OnUISelectionRadiusChanged;
            _ui.ResumeSelected += OnUIResumeSelected;
            _ui.PauseSelected += OnUIPauseSelected;
            _ui.ClearSelected += OnUIClearSelected;
            _ui.SaveSelected += OnUISaveSelected;
            _ui.LoadSelected += OnUILoadSelected;

            _ui.SetMaterial(MaterialType.Sand);
            _ui.SetRadius(10);

            var colorTop = new Color(10, 10, 20);
            var colorBottom = new Color(30, 20, 10);
            var gradient = new Vertex[4];
            gradient[0] = new Vertex(new Vector2f(0, 0), colorTop);
            gradient[1] = new Vertex(new Vector2f(0, _height), colorBottom);
            gradient[2] = new Vertex(new Vector2f(_width, _height), colorBottom);
            gradient[3] = new Vertex(new Vector2f(_width, 0), colorTop);

            Window = new RenderWindow(new VideoMode(_width, _height), _appName, Styles.Close);
            Window.SetVerticalSyncEnabled(true);
            Window.Closed += (_, _) => Window.Close();
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

                if (_world.IsUsed)
                {
                    _world.Input();
                }

                if (_isSimulating)
                {
                    _world.Update();
                }

                Window.Clear(Color.Black);
                Window.Draw(gradient, PrimitiveType.Quads);
                Window.Draw(_world);
                Window.Draw(_ui);

                Window.Display();

                if (_ticks % 20 == 0)
                {
                    var milliseconds = (Math.Round(_delta * 1000, 2) + "").Replace(',', '.');
                    var title = $"{_appName} (FPS: {_world.FPS} | {milliseconds} ms per frame)";
                    Window.SetTitle(title);
                }
            }
        }

        public static RenderWindow Window { get; private set; }

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
            _world.Input();
            _ui.UpdatePosition(e.X, e.Y);
        }

        private static void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (_ui.IsMouseInside(e.X, e.Y))
            {
                _ui.Input(e.X, e.Y);
                _ui.ControlInput(e.X, e.Y);
            }
            else
            {
                _world.StartInput(e.Button, e.X, e.Y);
                _world.Input();
            }
        }

        private static void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (_world.IsUsed)
            {
                _world.StopInput();
            }

            _ui.UnselectControls();
        }

        private static void OnKeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                Window.Close();
            }
            else
            if (e.Code == Keyboard.Key.Space)
            {
                _isSimulating = !_isSimulating;
                _ui.ToggleControlText(_isSimulating);
            }
        }

        private static void OnUIResumeSelected(object sender, EventArgs e)
        {
            _isSimulating = true;
        }

        private static void OnUIPauseSelected(object sender, EventArgs e)
        {
            _isSimulating = false;
        }

        private static void OnUIClearSelected(object sender, EventArgs e)
        {
            _world.Clear();
        }

        private static void OnUISaveSelected(object sender, EventArgs e)
        {
            _ui.UnselectControls();

            var saveFile = new SaveFileDialog()
            {
                FileName = "world",
                DefaultExt = ".png",
                ValidateNames = true,
                Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*"
            };

            if (saveFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                var builder = PngBuilder.Create((int)_width, (int)_height, true);
                var matrix = _world.GetMatrix();
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        var color = MaterialColor.GetFirstTone(matrix[i, j]);
                        builder.SetPixel(new Pixel(color.R, color.G, color.B, color.A, false), i, j);
                    }
                }

                using var saveFileStream = File.OpenWrite(saveFile.FileName);
                builder.Save(saveFileStream);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Unable to save a field to image",
                    "Save error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static void OnUILoadSelected(object sender, EventArgs e)
        {
            _ui.UnselectControls();

            var openFile = new OpenFileDialog();
            if (openFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var path = openFile.FileName;
            var fileExtension = Path.GetExtension(path);

            if (fileExtension != ".png")
            {
                MessageBox.Show(
                    "Input image should have .PNG format!", 
                    "Load error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

                return;
            }

            try
            {
                using var openStream = File.OpenRead(path);
                var image = Png.Open(openStream);

                if (image.Header.Width != _width ||
                    image.Header.Height != _height)
                {
                    MessageBox.Show(
                        "Input image should have same size: width - " + _width + " px, height - " + _height + " px!",
                        "Load error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                if (!image.HasAlphaChannel)
                {
                    MessageBox.Show(
                        "Input image should have alpha channel (transparency)!",
                        "Load error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                var newWorld = new MaterialType[_width, _height];
                for (int i = 0; i < _width; i++)
                {
                    for (int j = 0; j < _height; j++)
                    {
                        var pixel = image.GetPixel(i, j);
                        newWorld[i, j] = MaterialColor.MatchColor(pixel.R, pixel.G, pixel.B, pixel.A);
                    }
                }

                _world.Load(newWorld);

                openStream.Close();
                openStream.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Unable to load image",
                    "Load error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
