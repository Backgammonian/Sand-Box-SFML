﻿using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace SandBoxSFML.Materials
{
    public struct Material
    {
        private readonly List<Point> _trajectory;

        public Material(CellularMatrix matrix, MaterialType type)
        {
            _trajectory = new List<Point>();
            Matrix = matrix;
            Type = type;
            Color = MaterialColor.GetColor(Type);
            Velocity = new Vector2f(0, 0);
            IsMovable = false;
            SpreadRate = 0;
            FallRate = 0;

            switch (Type)
            {
                case MaterialType.Sand:
                    Velocity = new Vector2f(0, Constants.Gravity);
                    IsMovable = true;
                    break;

                case MaterialType.Water:
                    Velocity = new Vector2f(0, Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = Constants.WaterSpreadRate;
                    FallRate = Constants.WaterFallRate;
                    break;

                case MaterialType.Stone:
                    break;

                case MaterialType.Empty:
                    break;
            }
        }

        public CellularMatrix Matrix { get; }
        public MaterialType Type { get; private set; }
        public Color Color { get; private set; }
        public Vector2f Velocity { get; private set; }
        public bool IsMovable { get; private set; }
        public int SpreadRate { get; private set; }
        public int FallRate { get; private set; }

        public void ChangeType(MaterialType newType)
        {
            Type = newType;
            Color = MaterialColor.GetColor(Type);
            switch (Type)
            {
                case MaterialType.Sand:
                    Velocity = new Vector2f(0, Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = 0;
                    FallRate = 0;
                    break;

                case MaterialType.Water:
                    Velocity = new Vector2f(0, Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = Constants.WaterSpreadRate;
                    FallRate = Constants.WaterFallRate;
                    break;

                case MaterialType.Stone:
                    Velocity = new Vector2f(0, 0);
                    IsMovable = false;
                    SpreadRate = 0;
                    FallRate = 0;
                    break;

                case MaterialType.Empty:
                    break;
            }
        }

        public void ChangeVelocity(Vector2f newVelocity)
        {
            Velocity = newVelocity;
        }

        public void Step(int i, int j)
        {
            if (Type == MaterialType.Empty)
            {
                return;
            }

            if (Matrix.IsUpdatedThisFrame(i, j))
            {
                return;
            }

            if (IsMovable)
            {
                ChangeVelocity(new Vector2f((int)(Velocity.X * 0.99f), Velocity.Y + Constants.Gravity));
            }

            switch (Type)
            {
                case MaterialType.Stone:
                    break;

                case MaterialType.Sand:
                {
                    var vX = (int)(i + Velocity.X);
                    var vY = (int)(j + Velocity.Y);

                    Point? validPoint = null;

                    CalculateTrajectory(i, j, vX, vY);
                    for (int number = 0; number < _trajectory.Count; number++)
                    {
                        var point = _trajectory[number];

                        if (point.X == i &&
                            point.Y == j)
                        {
                            continue;
                        }

                        if (Matrix.IsFree(point.X, point.Y) ||
                            Matrix.IsWater(point.X, point.Y))
                        {
                            validPoint = point;
                        }
                        else
                        {
                            break;
                        }
                    }
        
                    if (validPoint.HasValue)
                    {
                        Matrix.Swap(validPoint.Value.X, validPoint.Value.Y, i, j);

                        return;
                    }

                    var random = Utils.Next(0, 100);
                    var direction = random < 50 ? Direction.Right : Direction.Left;
                    var cellBelow = new Point(direction == Direction.Right ? (i + 1) : direction == Direction.Left ? (i - 1) : i, j + 1);

                    if (Matrix.IsFree(cellBelow.X, cellBelow.Y) ||
                        Matrix.IsWater(cellBelow.X, cellBelow.Y))
                    {
                        Matrix.Swap(cellBelow.X, cellBelow.Y, i, j);

                        return;
                    }

                    ChangeVelocity(new Vector2f(Velocity.X, Velocity.Y / 2.0f));
                }
                break;

                case MaterialType.Water:
                {
                    var vX = (int)(i + Velocity.X);
                    var vY = (int)(j + Velocity.Y);

                    Point? validPoint = null;

                    CalculateTrajectory(i, j, vX, vY);
                    for (int number = 0; number < _trajectory.Count; number++)
                    {
                        var point = _trajectory[number];

                        if (point.X == i &&
                            point.Y == j)
                        {
                            continue;
                        }

                        if (Matrix.IsFree(point.X, point.Y) ||
                            Matrix.IsOil(point.X, point.Y))
                        {
                            validPoint = point;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (validPoint.HasValue)
                    {
                        Matrix.Swap(validPoint.Value.X, validPoint.Value.Y, i, j);

                        return;
                    }

                    CalculateTrajectory(i, j, i, j + FallRate);
                    for (int number = 0; number < _trajectory.Count; number++)
                    {
                        var point = _trajectory[number];

                        if (point.X == i &&
                            point.Y == j)
                        {
                            continue;
                        }

                        if (Matrix.IsFree(point.X, point.Y) ||
                            Matrix.IsOil(point.X, point.Y))
                        {
                            validPoint = point;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (validPoint.HasValue)
                    {
                        Matrix.Swap(validPoint.Value.X, validPoint.Value.Y, i, j);

                        return;
                    }

                    var spreadRate = Utils.NextBoolean() ? -SpreadRate : SpreadRate;
                    
                    CalculateTrajectory(i, j, i + spreadRate, j);
                    for (int number = 0; number < _trajectory.Count; number++)
                    {
                        var point = _trajectory[number];

                        if (point.X == i &&
                            point.Y == j)
                        {
                            continue;
                        }

                        if (Matrix.IsFree(point.X, point.Y) ||
                            Matrix.IsOil(point.X, point.Y))
                        {
                            validPoint = point;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (validPoint.HasValue)
                    {
                        Matrix.Swap(validPoint.Value.X, validPoint.Value.Y, i, j);

                        return;
                    }

                    ChangeVelocity(new Vector2f(Velocity.X, Velocity.Y / 2.0f));
                }
                break;
            }

            Matrix.UpdateCell(i, j);
        }

        private void CalculateTrajectory(int x0, int y0, int x1, int y1)
        {
            _trajectory.Clear();

            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);
            var sx = (x0 < x1) ? 1 : -1;
            var sy = (y0 < y1) ? 1 : -1;
            var err = dx - dy;

            while (true)
            {
                _trajectory.Add(new Point(x0, y0));

                if ((x0 == x1) && (y0 == y1))
                {
                    break;
                }

                var e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy; 
                    x0 += sx;
                }

                if (e2 < dx)
                {
                    err += dx; 
                    y0 += sy;
                }
            }
        }
    }
}