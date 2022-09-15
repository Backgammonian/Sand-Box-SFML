using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;
using SFML.Graphics;
using SandBoxSFML.Materials;

namespace SandBoxSFML.UI
{
    public sealed class SimpleUI : Drawable
    {
        private const int _controlButtonWidth = 70;
        private const int _controlButtonHeight = 30;
        private const int _buttonWidth = 70;
        private const int _buttonHeight = 30;

        private readonly Font _font;
        private readonly ClickableRectangle _main;
        private readonly List<ClickableRectangle> _materialButtons;
        private readonly List<Text> _materialIcons;
        private readonly ClickableRectangle _controlsMenu;
        private readonly List<ClickableRectangle> _controlsButtons;
        private readonly List<Text> _controlsLabels;
        private readonly CircleShape _circleShape;
        private readonly Text _materialPreview;
        private readonly Text _controlText;
        private int? _selectedButtonIndex;
        private int? _previousButtonIndex;
        private int? _selectedControlIndex;
        private float _selectionRadius;
        private MaterialType _selectedMaterial;
        
        public SimpleUI(Vector2f position, Vector2f size, Color color, Font font, Vector2f previewPosition, Vector2f controlsPosition, Vector2f controlsSize, Vector2f controlTextPosition)
        {
            Position = position;            
            Size = size;

            _font = font;
            _main = new ClickableRectangle(Position, Size, color);
            _materialButtons = new List<ClickableRectangle>();
            _materialIcons = new List<Text>();
            AddButton(MaterialType.Sand);
            AddButton(MaterialType.Water);
            AddButton(MaterialType.Stone);
            AddButton(MaterialType.Oil);
            AddButton(MaterialType.Fire);
            AddButton(MaterialType.Steam);
            AddButton(MaterialType.Smoke);
            AddButton(MaterialType.Coal);
            AddButton(MaterialType.Wood);
            AddButton(MaterialType.Acid);
            AddButton(MaterialType.Titan);
            AddButton(MaterialType.Lava);
            AddButton(MaterialType.Ash);
            AddButton(MaterialType.Methane);
            AddButton(MaterialType.BurningGas);
            AddButton(MaterialType.Ice);
            AddButton(MaterialType.Dirt);
            AddButton(MaterialType.Seed);
            AddButton(MaterialType.Virus);

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
            _materialPreview.OutlineColor = Color.Black;
            _materialPreview.OutlineThickness = 1;
            _materialPreview.Position = previewPosition;

            SelectionRadius = 10;
            SelectedMaterial = MaterialType.Sand;

            ControlsPosition = controlsPosition;
            ControlsSize = controlsSize;
 
            _controlsMenu = new ClickableRectangle(controlsPosition, controlsSize, color);
            _controlsButtons = new List<ClickableRectangle>();
            _controlsLabels = new List<Text>();

            AddControl("Resume");
            _controlsButtons[0].Selected += OnResumeSelected;
            AddControl("Pause");
            _controlsButtons[1].Selected += OnPauseSelected;
            AddControl("Clear");
            _controlsButtons[2].Selected += OnClearSelected;
            AddControl("Save");
            _controlsButtons[3].Selected += OnSaveSelected;
            AddControl("Load");
            _controlsButtons[4].Selected += OnLoadSelected;

            _controlText = new Text();
            _controlText.DisplayedString = "";
            _controlText.Font = font;
            _controlText.CharacterSize = 16;
            _controlText.FillColor = Color.White;
            _controlText.OutlineColor = Color.Black;
            _controlText.OutlineThickness = 1;
            _controlText.Position = controlTextPosition;
        }

        public event EventHandler<EventArgs> MaterialChanged;
        public event EventHandler<EventArgs> SelectionRadiusChanged;
        public event EventHandler<EventArgs> ResumeSelected;
        public event EventHandler<EventArgs> PauseSelected;
        public event EventHandler<EventArgs> ClearSelected;
        public event EventHandler<EventArgs> SaveSelected;
        public event EventHandler<EventArgs> LoadSelected;

        public Vector2f Position { get; private set; }
        public Vector2f Size { get; private set; }
        public Vector2f ControlsPosition { get; private set; }
        public Vector2f ControlsSize { get; private set; }

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

        private void AddButton(MaterialType material)
        {
            var x = Position.X + 5;
            var y = Position.Y + (_materialButtons.Count + 1) * 5 + _buttonHeight * _materialButtons.Count;
            var position = new Vector2f(x, y);

            _materialButtons.Add(new ClickableRectangle(position,
                new Vector2f(_buttonWidth, _buttonHeight),
                MaterialColor.GetColor(material, 0),
                material));

            var materialIcon = new Text();
            var name = material.ToString();
            materialIcon.DisplayedString = name.Length > 8 ? name.Substring(0, 7) + "." : name;
            materialIcon.Font = _font;
            materialIcon.CharacterSize = 16;
            materialIcon.FillColor = new Color(240, 240, 240);
            materialIcon.OutlineColor = Color.Black;
            materialIcon.OutlineThickness = 1;
            materialIcon.Position = new Vector2f(position.X + 2, position.Y + 2);

            _materialIcons.Add(materialIcon);
        }

        private void AddControl(string label)
        {
            var x = ControlsPosition.X + 5;
            var y = ControlsPosition.Y + (_controlsButtons.Count + 1) * 5 + _controlButtonHeight * _controlsButtons.Count;
            var position = new Vector2f(x, y);

            _controlsButtons.Add(new ClickableRectangle(position,
                new Vector2f(_controlButtonWidth, _controlButtonHeight),
                new Color(120, 120, 120)));

            var controlLabel = new Text();
            controlLabel.DisplayedString = label;
            controlLabel.Font = _font;
            controlLabel.CharacterSize = 16;
            controlLabel.FillColor = new Color(240, 240, 240);
            controlLabel.OutlineColor = Color.Black;
            controlLabel.OutlineThickness = 1;
            controlLabel.Position = new Vector2f(position.X + 2, position.Y + 2);

            _controlsLabels.Add(controlLabel);
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
            for (int i = 0; i < _materialIcons.Count; i++)
            {
                target.Draw(_materialIcons[i], states);
            }

            target.Draw(_controlsMenu, states);
            for (int i = 0; i < _controlsButtons.Count; i++)
            {
                target.Draw(_controlsButtons[i], states);
            }
            for (int i = 0; i < _controlsLabels.Count; i++)
            {
                target.Draw(_controlsLabels[i], states);
            }

            target.Draw(_materialPreview, states);
            target.Draw(_controlText, states);
            target.Draw(_circleShape, states);
        }

        public bool IsMouseInside(int x, int y)
        {
            return _main.IsMouseInside(x, y) || _controlsMenu.IsMouseInside(x, y);
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

        public void ControlInput(int x, int y)
        {
            var isControlSelectionMade = false;
            for (int i = 0; i < _controlsButtons.Count; i++)
            {
                if (_controlsButtons[i].IsMouseInside(x, y))
                {
                    _selectedControlIndex = i;
                    isControlSelectionMade = true;

                    break;
                }
            }

            if (!isControlSelectionMade)
            {
                return;
            }

            if (_selectedControlIndex.HasValue)
            {
                _controlsButtons[_selectedControlIndex.Value].Select();
            }
        }

        public void UnselectControls()
        {
            if (_selectedControlIndex.HasValue)
            {
                _controlsButtons[_selectedControlIndex.Value].Unselect();
            }

            _selectedControlIndex = null;
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
                _materialPreview.DisplayedString = _materialButtons[buttonIndex.Value].AssignedMaterial + string.Empty;
            }
            else
            {
                _materialPreview.DisplayedString = string.Empty;
            }
        }

        public void ToggleControlText(bool isSimulating)
        {
            _controlText.DisplayedString = isSimulating ? string.Empty : "Paused";
        }

        private void OnResumeSelected(object sender, MaterialSelectedEventArgs e)
        {
            _controlText.DisplayedString = string.Empty;

            ResumeSelected?.Invoke(this, EventArgs.Empty);
        }

        private void OnPauseSelected(object sender, MaterialSelectedEventArgs e)
        {
            _controlText.DisplayedString = "Paused";

            PauseSelected?.Invoke(this, EventArgs.Empty);
        }

        private void OnClearSelected(object sender, MaterialSelectedEventArgs e)
        {
            ClearSelected?.Invoke(this, EventArgs.Empty);
        }

        private void OnSaveSelected(object sender, MaterialSelectedEventArgs e)
        {
            SaveSelected?.Invoke(this, EventArgs.Empty);
        }

        private void OnLoadSelected(object sender, MaterialSelectedEventArgs e)
        {
            LoadSelected?.Invoke(this, EventArgs.Empty);
        }
    }
}