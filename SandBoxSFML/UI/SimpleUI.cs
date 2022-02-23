using System;
using System.Collections.Generic;
using System.Drawing;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
using SandBoxSFML.Materials;

namespace SandBoxSFML.UI
{
    public class SimpleUI : Drawable
    {
        private readonly ClickableRectangle _main;
        private readonly List<ClickableRectangle> _materialButtons;
        private readonly CircleShape _circleShape;
        private int? _selectedButtonIndex;
        private int? _previousButtonIndex;
        private float _selectionRadius;
        private MaterialType _selectedMaterial;
        private const int _buttonWidth = 30;
        private const int _buttonHeight = 30;

        public SimpleUI(Vector2f position, Vector2f size, SFML.Graphics.Color color)
        {
            Position = position;            
            Size = size;

            _main = new ClickableRectangle(position, size, color);

            _materialButtons = new List<ClickableRectangle>();
            _materialButtons.Add(new ClickableRectangle(
                new Vector2f(Position.X + 5, Position.Y + 5 + _buttonHeight * _materialButtons.Count), 
                new Vector2f(_buttonWidth, _buttonHeight), 
                MaterialColor.GetColor(MaterialType.Sand), 
                MaterialType.Sand));
            _materialButtons.Add(new ClickableRectangle(
                new Vector2f(Position.X + 5, Position.Y + 10 + _buttonHeight * _materialButtons.Count),
                new Vector2f(_buttonWidth, _buttonHeight),
                MaterialColor.GetColor(MaterialType.Water),
                MaterialType.Water));

            for (int i = 0; i < _materialButtons.Count; i++)
            {
                _materialButtons[i].Selected += OnMaterialSelected;
            }

            _selectedButtonIndex = 0;
            _materialButtons[0].Select();

            _circleShape = new CircleShape();
            _circleShape.FillColor = SFML.Graphics.Color.Transparent;
            _circleShape.OutlineThickness = 2;
            _circleShape.OutlineColor = color;
            _circleShape.Radius = SelectionRadius;

            SelectionRadius = 10;
            SelectedMaterial = MaterialType.Sand;
        }

        public event EventHandler<EventArgs> MaterialChanged;
        public event EventHandler<EventArgs> SelectionRadiusChanged;

        public Vector2f Position { get; private set; }
        public Vector2f Size { get; private set; }

        public MaterialType SelectedMaterial 
        {
            get => _selectedMaterial;
            private set
            {
                _selectedMaterial = value;

                MaterialChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public float SelectionRadius
        {
            get => _selectionRadius;
            private set
            {
                if (value > 0 &&
                    value <= Constants.MaxRadius)
                {
                    _selectionRadius = value;
                    _circleShape.Radius = value;

                    SelectionRadiusChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void SetMaterial(MaterialType material)
        {
            SelectedMaterial = material;
        }

        public void SetRadius(float radius)
        {
            SelectionRadius = radius;
        }

        public void AddSelectionRadius(float increment)
        {
            SelectionRadius += increment;
        }

        private void OnMaterialSelected(object sender, MaterialSelectedEventArgs e)
        {
            SelectedMaterial = e.Material;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.BlendMode = BlendMode.Alpha;
            target.Draw(_main, states);
            for (int i = 0; i < _materialButtons.Count; i++)
            {
                target.Draw(_materialButtons[i], states);
            }
            target.Draw(_circleShape, states);
        }

        public bool IsMouseInside(int x, int y)
        {
            return _main.IsMouseInside(x, y);
        }

        public void Input(int x, int y)
        {
            _previousButtonIndex = _selectedButtonIndex;

            var isSelectionMade = false;
            for (int i = 0; i < _materialButtons.Count; i++)
            {
                if (_materialButtons[i].IsMouseInside(x, y))
                {
                    _selectedButtonIndex = i;
                    isSelectionMade = true;
                    break;
                }
            }

            if (!isSelectionMade)
            {
                return;
            }

            if (_previousButtonIndex.HasValue &&
                _selectedButtonIndex.HasValue &&
                _selectedButtonIndex.Value == _previousButtonIndex.Value)
            {
                return;
            }

            if (_selectedButtonIndex.HasValue)
            {
                _materialButtons[_selectedButtonIndex.Value].Select();
            }

            if (_previousButtonIndex.HasValue)
            {
                _materialButtons[_previousButtonIndex.Value].Unselect();
            }
        }

        public void UpdateCirclePosition(int x, int y)
        {
            _circleShape.Position = new Vector2f(x - _circleShape.Radius, y - _circleShape.Radius);
        }
    }
}
