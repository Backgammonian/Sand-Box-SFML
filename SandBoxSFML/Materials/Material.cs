using SFML.Graphics;
using SFML.System;

namespace SandBoxSFML.Materials
{
    public struct Material
    {
        public Material(MaterialType type)
        {
            Type = type;
            Color = MaterialColor.GetColor(Type);
            Velocity = new Vector2i(0, 0);
            IsUpdatedThisFrame = false;
            IsMovable = false;

            switch (Type)
            {
                case MaterialType.Sand:
                    Velocity = new Vector2i(Constants.Gravity.X, Constants.Gravity.Y);
                    IsMovable = true;
                    break;

                case MaterialType.Empty:
                    break;
            }
        }

        public MaterialType Type { get; private set; }
        public Color Color { get; private set; }
        public Vector2i Velocity { get; set; }
        public bool IsUpdatedThisFrame { get; set; }
        public bool IsMovable { get; private set; }

        public void ChangeType(MaterialType newType)
        {
            Type = newType;
            Color = MaterialColor.GetColor(Type);
            switch (Type)
            {
                case MaterialType.Sand:
                    Velocity = new Vector2i(Constants.Gravity.X, Constants.Gravity.Y);
                    IsMovable = true;
                    break;

                case MaterialType.Empty:
                    break;
            }
        }

        public void Step(CellularMatrix matrix, int i, int j)
        {
            if (IsUpdatedThisFrame)
            {
                return;
            }

            switch (Type)
            {
                case MaterialType.Empty:
                    break;

                case MaterialType.Sand:
                    {
                        Velocity = new Vector2i(Utils.Clamp(Velocity.X + Constants.Gravity.X, -10, 10), Utils.Clamp(Velocity.Y + Constants.Gravity.Y, -10, 10));

                        var vX = i + Velocity.X;
                        var vY = j + Velocity.Y;

                        var random = Utils.Next(0, 100);
                        var direction = random < 33 ? Direction.Right : random > 66 ? Direction.Left : Direction.None;
                        var cellBelow = new Point(direction == Direction.Right ? (i + 1) : direction == Direction.Left ? (i - 1) : i, j + 1);

                        if (matrix.IsFree(vX, vY))
                        {
                            matrix.Swap(vX, vY, i, j);
                        }
                        else
                        if (matrix.IsFree(cellBelow.X, cellBelow.Y))
                        {
                            matrix.Swap(cellBelow.X, cellBelow.Y, i, j);
                        }
                    }
                    break;
            }

            IsUpdatedThisFrame = true;
        }
    }
}
