using System;
using System.Collections.Generic;
using System.Linq;
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
        private const int _buttonWidth = 70;
        private const int _buttonHeight = 30;
        private readonly Text _materialPreview;
        private readonly List<Text> _materialIcons;

        public SimpleUI(Vector2f position, Vector2f size, Color color, Font font, Vector2f fontPosition)
        {
            Position = position;            
            Size = size;

            _main = new ClickableRectangle(position, size, color);

            _materialButtons = new List<ClickableRectangle>();
            _materialIcons = new List<Text>();
            AddButton(MaterialType.Sand, font);
            AddButton(MaterialType.Water, font);
            AddButton(MaterialType.Stone, font);
            AddButton(MaterialType.Oil, font);
            AddButton(MaterialType.Fire, font);
            AddButton(MaterialType.Steam, font);
            AddButton(MaterialType.Smoke, font);
            AddButton(MaterialType.Coal, font);
            AddButton(MaterialType.Wood, font);
            AddButton(MaterialType.Acid, font);
            AddButton(MaterialType.Titan, font);
            AddButton(MaterialType.Lava, font);
            AddButton(MaterialType.Ash, font);
            AddButton(MaterialType.Methane, font);
            AddButton(MaterialType.BurningGas, font);
            AddButton(MaterialType.Ice, font);
            AddButton(MaterialType.Dirt, font);
            AddButton(MaterialType.Seed, font);
            AddButton(MaterialType.Virus, font);

            for (int i = 0; i < _materialButtons.Count; i++)
            {
                _materialButtons[i].Selected += OnMaterialSelected;
            }

            _selectedButtonIndex = 0;
            _materialButtons[0].Select();

            _circleShape = new CircleShape();
            _circleShape.FillColor = Color.Transparent;
            _circleShape.OutlineThickness = 2;
            _circleShape.OutlineColor = color;
            _circleShape.Radius = SelectionRadius;

            _materialPreview = new Text();
            _materialPreview.DisplayedString = "";
            _materialPreview.Font = font;
            _materialPreview.CharacterSize = 16;
            _materialPreview.FillColor = Color.White;
            _materialPreview.Position = fontPosition;

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

        private void AddButton(MaterialType material, Font font)
        {
            var x = Position.X + 5;
            var y = Position.Y + (_materialButtons.Count + 1) * 5 + _buttonHeight * _materialButtons.Count;
            var position = new Vector2f(x, y);

            _materialButtons.Add(new ClickableRectangle(
                position,
                new Vector2f(_buttonWidth, _buttonHeight),
                MaterialColor.GetColor(material, 0),
                material));

            var materialIcon = new Text();
            var name = material.ToString();
            materialIcon.DisplayedString = name.Length > 8 ? name.Substring(0, 7) + "." : name;
            materialIcon.Font = font;
            materialIcon.CharacterSize = 16;
            //materialIcon.FillColor = Utils.InvertColor(MaterialColor.GetColor(material, 0));
            materialIcon.FillColor = new Color(240, 240, 240);
            materialIcon.OutlineColor = Color.Black;
            materialIcon.OutlineThickness = 1;
            materialIcon.Position = new Vector2f(position.X + 2, position.Y + 2);

            _materialIcons.Add(materialIcon);
        }

        public void SetMaterial(MaterialType material)
        {
            SelectedMaterial = material;
            for (int i = 0; i < _materialButtons.Count; i++)
            {
                _materialButtons[i].Unselect();
            }

            var button = _materialButtons.Single(button => button.AssignedMaterial == SelectedMaterial);
            button.Select();
            _previousButtonIndex = _materialButtons.IndexOf(button);
            _selectedButtonIndex = _materialButtons.IndexOf(button);
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
            target.Draw(_materialPreview, states);
            for (int i = 0; i < _materialIcons.Count; i++)
            {
                target.Draw(_materialIcons[i], states);
            }
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

        public void UpdatePosition(int x, int y)
        {
            _circleShape.Position = new Vector2f(x - _circleShape.Radius, y - _circleShape.Radius);

            int? buttonIndex = null;
            for (int i = 0; i < _materialButtons.Count; i++)
            {
                if (_materialButtons[i].IsMouseInside(x, y))
                {
                    buttonIndex = i;
                    break;
                }
            }

            if (buttonIndex.HasValue)
            {
                _materialPreview.DisplayedString = _materialButtons[buttonIndex.Value].AssignedMaterial + "";
            }
            else
            {
                _materialPreview.DisplayedString = "";
            }
        }
    }
}