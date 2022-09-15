using System;
using System.Drawing;
using SFML.System;
using SFML.Graphics;
using SandBoxSFML.Materials;

namespace SandBoxSFML.UI
{
    public sealed class ClickableRectangle : Drawable
    {
        private readonly RectangleShape _rectangleShape;
        private readonly Rectangle _rectangle;
        private bool _isSelected;

        public ClickableRectangle(Vector2f position, Vector2f size, SFML.Graphics.Color color, MaterialType material = MaterialType.Empty)
        {
            Position = position;
            Size = size;

            _rectangleShape = new RectangleShape();
            _rectangleShape.Size = Size;
            _rectangleShape.Position = Position;
            _rectangleShape.OutlineThickness = 2;
            _rectangleShape.FillColor = color;

            AssignedMaterial = material;

            _rectangle = new Rectangle();
            _rectangle.Location = new System.Drawing.Point((int)Position.X, (int)Position.Y);
            _rectangle.Size = new Size((int)Size.X, (int)Size.Y);

            IsSelected = false;
        }

        public event EventHandler<MaterialSelectedEventArgs> Selected;

        public Vector2f Position { get; private set; }
        public Vector2f Size { get; private set; }
        public MaterialType AssignedMaterial { get; private set; }
        public bool IsSelected
        {
            get => _isSelected;
            private set
            {
                _isSelected = value;
                _rectangleShape.OutlineColor = _isSelected ? SFML.Graphics.Color.White : SFML.Graphics.Color.Transparent;

                if (value)
                {
                    Selected?.Invoke(this, new MaterialSelectedEventArgs(AssignedMaterial));
                }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.BlendMode = BlendMode.Alpha;
            target.Draw(_rectangleShape, states);
        }

        public bool IsMouseInside(int x, int y)
        {
            return _rectangle.Contains(x, y);
        }

        public void Select()
        {
            if (IsSelected)
            {
                return;
            }

            IsSelected = true;
        }

        public void Unselect()
        {
            if (!IsSelected)
            {
                return;
            }

            IsSelected = false;
        }
    }
}
