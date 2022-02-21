using System;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
using SandBoxSFML.Materials;

namespace SandBoxSFML
{
    public class World
    {
        private readonly ImageArray _canvas;
        private readonly CellularMatrix _matrix;
        private MaterialType _selectedMaterial;
        private int _selectionRadius;
        private DateTime _lastTime;
        private long _framesRendered;

        public World(int width, int height)
        {
            Width = width;
            Height = height;

            _canvas = new ImageArray(Width, Height);
            _matrix = new CellularMatrix(Width, Height);
            _matrix.MatrixUpdated += OnMatrixUpdated;

            Sprite = new Sprite();
            Sprite.Texture = _canvas.Bitmap;

            Clear();
        }

        public event EventHandler<EventArgs> MaterialChanged;
        public event EventHandler<EventArgs> SelectionRadiusChanged;

        public int Width { get; }
        public int Height { get; }
        public long FPS { get; private set; }
        public Sprite Sprite { get; }

        public MaterialType SelectedMaterial
        {
            get => _selectedMaterial;
            private set
            {
                _selectedMaterial = value;

                MaterialChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int SelectionRadius
        {
            get => _selectionRadius;
            private set
            {
                if (value > 0)
                {
                    _selectionRadius = value;

                    SelectionRadiusChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void OnMatrixUpdated(object sender, MatrixUpdatedEventArgs e)
        {
            _canvas.DrawPoint(e.X, e.Y, e.Color);
        }

        private void Clear()
        {
            _canvas.Clear(Color.Black);
            _matrix.Clear();
        }

        public void Update()
        {
            Input();

            _matrix.StepAll();
            _matrix.ToggleFrameUpdate();

            _canvas.Update();

            CountFPS();
        }

        private void Input()
        {
            var position = Mouse.GetPosition(Program.Window);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                switch (SelectedMaterial)
                {
                    case MaterialType.Sand:
                        AddMaterialToWorld(position);
                        break;
                }
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                EraseMaterial(position);
            }
        }

        private void CountFPS()
        {
            _framesRendered++;
            if ((DateTime.Now - _lastTime).TotalSeconds >= 1)
            {
                FPS = _framesRendered;
                _framesRendered = 0;
                _lastTime = DateTime.Now;
            }
        }

        private void AddMaterialToWorld(Vector2i mousePosition)
        {
            var count = Utils.RandomValue(1, 100);
            for (int i = 0; i < count; i++)
            {
                var r = SelectionRadius * Utils.NextDouble();
                var theta = 2.0 * Math.PI * Utils.NextDouble();
                var rx = Math.Cos(theta) * r;
                var ry = Math.Sin(theta) * r;

                var position = new Point(mousePosition.X + (int)rx, mousePosition.Y + (int)ry);
                var deviation = Utils.Next(0, 100) > 50 ? -1 : 1;
                var velocity = new Vector2i(deviation, Utils.RandomValue(-2, 5));

                _matrix.Add(SelectedMaterial, position, velocity);
            }
        }

        private void EraseMaterial(Vector2i mousePosition)
        {
            var R = SelectionRadius;
            for (var i = -R; i < R; i++)
            {
                for (var j = -R; j < R; j++)
                {
                    var rx = mousePosition.X + i;
                    var ry = mousePosition.Y + j;
                    var r = new Vector2i(rx, ry);
                    var origin = new Vector2i(mousePosition.X, mousePosition.Y);
                    var l = new Vector2i(origin.X - r.X, origin.Y - r.Y);

                    if ((l.X * l.X + l.Y + l.Y) <= R * R)
                    {
                        _matrix.Erase(rx, ry);
                    }
                }
            }
        }

        public void SetMaterial(MaterialType material)
        {
            SelectedMaterial = material;
        }

        public void SetRadius(int radius)
        {
            SelectionRadius = radius;
        }
    }
}