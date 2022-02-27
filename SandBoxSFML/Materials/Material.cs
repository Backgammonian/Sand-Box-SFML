using System;
using System.Collections.Generic;
using SFML.Graphics;

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
            LifeTime = 0;
            Color = MaterialColor.GetColor(Type, LifeTime);
            Velocity = new Vector2(0, 0);
            IsMovable = false;
            SpreadRate = 0;
            FallRate = 0;

            switch (Type)
            {
                case MaterialType.Empty:
                    break;

                case MaterialType.Stone:
                    break;

                case MaterialType.Wood:
                    break;

                case MaterialType.Sand:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    break;

                case MaterialType.Water:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = Constants.WaterSpreadRate;
                    FallRate = Constants.WaterFallRate;
                    break;

                case MaterialType.Oil:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = Constants.OilSpreadRate;
                    FallRate = Constants.OilFallRate;
                    break;

                case MaterialType.Fire:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = Constants.FireSpreadRate;
                    break;

                case MaterialType.Steam:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    break;

                case MaterialType.Smoke:
                    Velocity = new Vector2(0, -Constants.Gravity);
                    IsMovable = true;
                    break;

                case MaterialType.Ember:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    break;

                case MaterialType.Coal:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    break;
            }
        }

        public CellularMatrix Matrix { get; }
        public MaterialType Type { get; private set; }
        public int LifeTime { get; private set; }
        public Color Color { get; private set; }
        public Vector2 Velocity { get; private set; }
        public bool IsMovable { get; private set; }
        public int SpreadRate { get; private set; }
        public int FallRate { get; private set; }

        public void ChangeType(MaterialType newType)
        {
            Type = newType;
            LifeTime = 0;
            Color = MaterialColor.GetColor(Type, LifeTime);
            switch (Type)
            {
                case MaterialType.Empty:
                    Velocity = new Vector2(0, 0);
                    IsMovable = false;
                    SpreadRate = 0;
                    FallRate = 0;
                    break;

                case MaterialType.Sand:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = 0;
                    FallRate = 0;
                    break;

                case MaterialType.Water:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = Constants.WaterSpreadRate;
                    FallRate = Constants.WaterFallRate;
                    break;

                case MaterialType.Oil:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = Constants.OilSpreadRate;
                    FallRate = Constants.OilFallRate;
                    break;

                case MaterialType.Stone:
                    Velocity = new Vector2(0, 0);
                    IsMovable = false;
                    SpreadRate = 0;
                    FallRate = 0;
                    break;

                case MaterialType.Fire:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = Constants.FireSpreadRate;
                    FallRate = 0;
                    break;

                case MaterialType.Steam:
                    Velocity = new Vector2(0, -Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = 0;
                    FallRate = 0;
                    break;

                case MaterialType.Smoke:
                    Velocity = new Vector2(0, -Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = 0;
                    FallRate = 0;
                    break;

                case MaterialType.Ember:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = 0;
                    FallRate = 0;
                    break;

                case MaterialType.Coal:
                    Velocity = new Vector2(0, Constants.Gravity);
                    IsMovable = true;
                    SpreadRate = 0;
                    FallRate = 0;
                    break;

                case MaterialType.Wood:
                    Velocity = new Vector2(0, 0);
                    IsMovable = false;
                    SpreadRate = 0;
                    FallRate = 0;
                    break;
            }
        }

        public void Step(int i, int j)
        {
            if (Type == MaterialType.Empty)
            {
                return;
            }

            LifeTime += 1;
            if (LifeTime == int.MaxValue)
            {
                LifeTime = 0;
            }

            if (Matrix.IsUpdatedThisFrame(i, j))
            {
                return;
            }

            if (IsMovable)
            {
                Velocity.X *= 0.99f;

                if (Type == MaterialType.Steam ||
                    Type == MaterialType.Smoke)
                {
                    Velocity.Y -= Constants.Gravity;
                }
                else
                {
                    Velocity.Y += Constants.Gravity;
                }
            }

            switch (Type)
            {
                case MaterialType.Sand:
                    UpdateSand(i, j);
                    break;

                case MaterialType.Water:
                    UpdateWater(i, j);
                    break;

                case MaterialType.Oil:
                    UpdateOil(i, j);
                    break;

                case MaterialType.Fire:
                    UpdateFire(i, j);
                    break;

                case MaterialType.Steam:
                    UpdateSteam(i, j);
                    break;

                case MaterialType.Smoke:
                    UpdateSmoke(i, j);
                    break;

                case MaterialType.Ember:
                    UpdateEmber(i, j);
                    break;

                case MaterialType.Coal:
                    UpdateCoal(i, j);
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

        private void UpdateSand(int i, int j)
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

            if (Matrix.IsLiquidNearby(i, j, out int iLiquid, out int jLiquid) &&
                Utils.RandomValue(0, 10) == 0)
            {
                Matrix.Swap(iLiquid, jLiquid, i, j);

                return;
            }

            Velocity.Y /= 2.0f;
        }

        private void UpdateWater(int i, int j)
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

            if (Matrix.IsLiquidNearby(i, j, out int iLiquid, out int jLiquid) &&
                Utils.RandomValue(0, 10) == 0)
            {
                Matrix.Swap(iLiquid, jLiquid, i, j);

                return;
            }

            Velocity.Y /= 2.0f;
        }

        private void UpdateOil(int i, int j)
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

                if (Matrix.IsFree(point.X, point.Y))
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

                if (Matrix.IsFree(point.X, point.Y))
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

                if (Matrix.IsFree(point.X, point.Y))
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

            Velocity.Y /= 2.0f;
        }

        private void UpdateFire(int i, int j)
        {
            Color = MaterialColor.GetColor(Type, LifeTime);

            if (LifeTime > Constants.FireLifeTime)
            {
                Matrix.Erase(i, j);

                return;
            }

            if (Matrix.IsElementNearby(i, j, MaterialType.Water, out int iNew, out int jNew))
            {
                Matrix[iNew, jNew].ChangeType(MaterialType.Steam);

                var steamRegionHeight = Utils.RandomValue(-Constants.SteamRegionHeight, Constants.SteamRegionHeight);
                var steamRegionWidth = Utils.RandomValue(-Constants.SteamRegionWidth, Constants.SteamRegionWidth);
                var r1 = Utils.NextBoolean();
                var r2 = Utils.NextBoolean();
                for (int n = r1 ? steamRegionHeight : -steamRegionHeight; r1 ? n < Constants.SteamRegionHeight : n > Constants.SteamRegionHeight; n += r1 ? 1 : -1)
                {
                    for (int m = r2 ? steamRegionWidth : -steamRegionWidth; r2 ? m < Constants.SteamRegionWidth : m > Constants.SteamRegionWidth; m += r2 ? 1 : -1)
                    {
                        Matrix.Add(MaterialType.Steam, i + m, j + n);
                    }
                }

                Matrix.Erase(i, j);
                Matrix.Erase(iNew, jNew);

                return;
            }

            if (Matrix.IsFire(i, j + 1) &&
                Matrix.IsFree(i, j - 1))
            {
                if (Utils.RandomValue(0, 10) == 0)
                {
                    var r = Utils.NextBoolean();
                    var randomHorizontal = Utils.RandomValue(-10, -1);
                    for (int n = randomHorizontal; n < 0; n++)
                    {
                        for (int m = r ? -SpreadRate : SpreadRate; r ? m < SpreadRate : m > -SpreadRate; m += r ? 1 : -1)
                        {
                            if (Matrix.IsFree(i + m, j + n))
                            {
                                Matrix.Swap(i + m, j + n, i, j);

                                return;
                            }
                        }
                    }
                }
            }

            if (Utils.RandomValue(0, Constants.SmokeSpawnChance) == 0)
            {
                if (Matrix.IsFree(i, j - 1))
                {
                    Matrix.Add(MaterialType.Smoke, i, j - 1);
                }
            }

            if (Utils.RandomValue(0, Constants.SmokeSpawnChance) == 0)
            {
                if (Matrix.IsFree(i + 1, j - 1))
                {
                    Matrix.Add(MaterialType.Smoke, i + 1, j - 1);
                }
            }

            if (Utils.RandomValue(0, Constants.SmokeSpawnChance) == 0)
            {
                if (Matrix.IsFree(i - 1, j - 1))
                {
                    Matrix.Add(MaterialType.Smoke, i - 1, j - 1);
                }
            }

            /*if (Utils.RandomValue(0, Constants.EmberSpawnChance) == 0 &&
                LifeTime < Constants.FireLifeTime / 3)
            {
                if (Matrix.IsFree(i, j - 1))
                {
                    Matrix.Add(MaterialType.Ember, i, j - 1, new Vector2(Utils.RandomValue(-2, 2), Utils.RandomValue(-5, 0)));
                }
            }

            if (Utils.RandomValue(0, Constants.EmberSpawnChance) == 0 &&
                LifeTime < Constants.FireLifeTime / 3)
            {
                if (Matrix.IsFree(i + 1, j - 1))
                {
                    Matrix.Add(MaterialType.Ember, i + 1, j - 1, new Vector2(Utils.RandomValue(-2, 2), Utils.RandomValue(-5, 0)));
                }
            }

            if (Utils.RandomValue(0, Constants.EmberSpawnChance) == 0 &&
                LifeTime < Constants.FireLifeTime / 3)
            {
                if (Matrix.IsFree(i - 1, j - 1))
                {
                    Matrix.Add(MaterialType.Ember, i - 1, j - 1, new Vector2(Utils.RandomValue(-2, 2), Utils.RandomValue(-5, 0)));
                }
            }*/

            if (Matrix.IsElementNearby(i, j, MaterialType.Oil, out int iOil, out int jOil) &&
                Utils.RandomValue(0, Constants.OilIgnitionChance) == 0)
            {
                //Matrix[iOil, jOil].ChangeType(MaterialType.Fire);
                Matrix.Erase(iOil, jOil);
                Matrix[iOil, jOil].ChangeType(MaterialType.Fire);

                System.Diagnostics.Debug.WriteLine("oil is near fire! " + iOil + " " + jOil);
                System.Diagnostics.Debug.WriteLine("(iOil, jOil) now is " + Matrix[iOil, jOil].Type);

                if (Utils.RandomValue(0, Constants.FireSpreadChance) == 0)
                {
                    var r = Utils.NextBoolean();
                    for (var n = -3; n < 2; n++)
                    {
                        for (var m = r ? -3 : 2; r ? m < 2 : m > -3; m += r ? 1 : -1)
                        {
                            if (Matrix.IsFree(i + m, j + n))
                            {
                                LifeTime += 5;
                                Matrix.Swap(i + m, j + n, i, j);

                                break;
                            }
                        }
                    }
                }
            }

            if (Matrix.IsElementNearby(i, j, MaterialType.Coal, out int iCoal, out int jCoal) &&
                Utils.RandomValue(0, Constants.CoalIgnitionChance) == 0)
            {
                Matrix[iCoal, jCoal].ChangeType(MaterialType.Fire);

                if (Utils.RandomValue(0, Constants.FireSpreadChance) == 0)
                {
                    var r = Utils.NextBoolean();
                    for (var n = -3; n < 2; n++)
                    {
                        for (var m = r ? -3 : 2; r ? m < 2 : m > -3; m += r ? 1 : -1)
                        {
                            if (Matrix.IsFree(i + m, j + n))
                            {
                                LifeTime += 5;
                                Matrix.Swap(i + m, j + n, i, j);

                                break;
                            }
                        }
                    }
                }
            }

            if (Matrix.IsElementNearby(i, j, MaterialType.Wood, out int iWood, out int jWood) &&
                Utils.RandomValue(0, Constants.WoodIgnitionChance) == 0)
            {
                Matrix[iWood, jWood].ChangeType(MaterialType.Fire);

                if (Utils.RandomValue(0, Constants.FireSpreadChance) == 0)
                {
                    var r = Utils.NextBoolean();
                    for (var n = -3; n < 2; n++)
                    {
                        for (var m = r ? -3 : 2; r ? m < 2 : m > -3; m += r ? 1 : -1)
                        {
                            if (Matrix.IsFree(i + m, j + n))
                            {
                                LifeTime += 5;
                                Matrix.Swap(i + m, j + n, i, j);

                                break;
                            }
                        }
                    }
                }
            }

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
                    Matrix.IsFire(point.X, point.Y) ||
                    Matrix.IsSmoke(point.X, point.Y) ||
                    Matrix.IsSteam(point.X, point.Y))
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
            var horizontalDirection = random < 50 ? Direction.Right : Direction.Left;
            random = Utils.Next(0, 100);
            var verticalDirection = random < 50 ? Direction.Up : Direction.Down;

            var neighborCell = new Point(
                horizontalDirection == Direction.Right ? (i + 1) : horizontalDirection == Direction.Left ? (i - 1) : i,
                verticalDirection == Direction.Down ? (j + 1) : verticalDirection == Direction.Up ? (j - 1) : j);

            if (Matrix.IsFree(neighborCell.X, neighborCell.Y) ||
                Matrix.IsSmoke(neighborCell.X, neighborCell.Y) ||
                Matrix.IsWater(neighborCell.X, neighborCell.Y) ||
                Matrix.IsSteam(neighborCell.X, neighborCell.Y))
            {
                Matrix.Swap(neighborCell.X, neighborCell.Y, i, j);

                return;
            }

            Velocity.Y /= 2.0f;
        }

        private void UpdateSteam(int i, int j)
        {
            Color = MaterialColor.GetColor(Type, LifeTime);

            if (LifeTime > Constants.SteamLifeTime)
            {
                Matrix.Erase(i, j);

                return;
            }

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
                    Matrix.IsWater(point.X, point.Y) ||
                    Matrix.IsOil(point.X, point.Y) ||
                    Matrix.IsFire(point.X, point.Y))
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
                Velocity.X += Utils.NextBoolean() ? -0.3f : 0.3f;
                Matrix.Swap(validPoint.Value.X, validPoint.Value.Y, i, j);

                return;
            }

            var random = Utils.Next(0, 100);
            var direction = random < 50 ? Direction.Right : Direction.Left;
            var cellAbove = new Point(direction == Direction.Right ? (i + 1) : direction == Direction.Left ? (i - 1) : i, j - 1);

            if (Matrix.IsFree(cellAbove.X, cellAbove.Y) ||
                Matrix.IsWater(cellAbove.X, cellAbove.Y) ||
                Matrix.IsOil(cellAbove.X, cellAbove.Y) ||
                Matrix.IsFire(cellAbove.X, cellAbove.Y))
            {
                Velocity.X += direction == Direction.Left ? -0.3f : 0.3f;
                Matrix.Swap(cellAbove.X, cellAbove.Y, i, j);

                return;
            }

            Velocity.Y /= 2.0f;
        }

        private void UpdateSmoke(int i, int j)
        {
            Color = MaterialColor.GetColor(Type, LifeTime);

            if (LifeTime > Constants.SmokeLifeTime)
            {
                Matrix.Erase(i, j);

                return;
            }

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
                    Matrix.IsWater(point.X, point.Y) ||
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
                Velocity.X += Utils.NextBoolean() ? -0.3f : 0.3f;
                Matrix.Swap(validPoint.Value.X, validPoint.Value.Y, i, j);

                return;
            }

            var random = Utils.Next(0, 100);
            var direction = random < 50 ? Direction.Right : Direction.Left;
            var cellAbove = new Point(direction == Direction.Right ? (i + 1) : direction == Direction.Left ? (i - 1) : i, j - 1);

            if (Matrix.IsWithihBounds(cellAbove.X, cellAbove.Y) &&
                Matrix[cellAbove.X, cellAbove.Y].Type != MaterialType.Stone &&
                Matrix[cellAbove.X, cellAbove.Y].Type != MaterialType.Wood &&
                Matrix[cellAbove.X, cellAbove.Y].Type != MaterialType.Titan &&
                Matrix[cellAbove.X, cellAbove.Y].Type != MaterialType.Smoke)
            {
                Velocity.X += direction == Direction.Left ? -0.3f : 0.3f;
                Matrix.Swap(cellAbove.X, cellAbove.Y, i, j);

                return;
            }

            Velocity.Y /= 2.0f;
        }

        private void UpdateEmber(int i, int j)
        {
            if (LifeTime > Constants.EmberLifeTime)
            {
                Matrix.Erase(i, j);

                return;
            }

            if (Matrix.IsElementNearby(i, j, MaterialType.Wood, out int iWood, out int jWood) &&
                Utils.RandomValue(0, Constants.WoodIgnitionChance) == 0)
            {
                Matrix[iWood, jWood].ChangeType(MaterialType.Fire);
            }

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
                    Matrix.IsWater(point.X, point.Y) ||
                    Matrix.IsFire(point.X, point.Y) ||
                    Matrix.IsSmoke(point.X, point.Y) ||
                    Matrix.IsEmber(point.X, point.Y))
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
            var cellAbove = new Point(direction == Direction.Right ? (i + 1) : direction == Direction.Left ? (i - 1) : i, j - 1);

            if (Matrix.IsFree(cellAbove.X, cellAbove.Y) ||
                Matrix.IsWater(cellAbove.X, cellAbove.Y) ||
                Matrix.IsFire(cellAbove.X, cellAbove.Y) ||
                Matrix.IsSmoke(cellAbove.X, cellAbove.Y) ||
                Matrix.IsEmber(cellAbove.X, cellAbove.Y))
            {
                Matrix.Swap(cellAbove.X, cellAbove.Y, i, j);

                return;
            }

            Velocity.Y /= 2.0f;
        }

        private void UpdateCoal(int i, int j)
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

            if (Matrix.IsLiquidNearby(i, j, out int iLiquid, out int jLiquid) &&
                Utils.RandomValue(0, 10) == 0)
            {
                Matrix.Swap(iLiquid, jLiquid, i, j);

                return;
            }

            Velocity.Y /= 2.0f;
        }
    }
}
