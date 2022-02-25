﻿using System;
using SFML.System;
using SandBoxSFML.Materials;

namespace SandBoxSFML
{
    public class CellularMatrix
    {
        private readonly Material[,] _matrix;
        private readonly bool[,] _isUpdatedThisFrame;
        private readonly bool[,] _fillValue;

        public CellularMatrix(int width, int height)
        {
            Width = width;
            Height = height;

            _matrix = new Material[Width, Height];
            _isUpdatedThisFrame = new bool[Width, Height];

            _fillValue = new[,] { { false }, { false } };
        }

        public event EventHandler<MatrixUpdatedEventArgs> MatrixUpdated;

        public int Width { get; }
        public int Height { get; }

        public Material this[int i, int j]
        {
            get => _matrix[i, j];
            private set => _matrix[i, j] = value;
        }

        public bool IsUpdatedThisFrame(int i, int j)
        {
            return _isUpdatedThisFrame[i, j];
        }

        public void UpdateCell(int i, int j)
        {
            _isUpdatedThisFrame[i, j] = true;
        }

        public void Initialize()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _matrix[i, j] = new Material(MaterialType.Empty);
                    _isUpdatedThisFrame[i, j] = false;
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _matrix[i, j].ChangeType(MaterialType.Empty);
                    _isUpdatedThisFrame[i, j] = false;
                }
            }
        }

        public bool IsWithihBounds(int i, int j)
        {
            return i >= 0 && i < Width && j >= 0 && j < Height;
        }

        public bool IsFree(Material cell)
        {
            return cell.Type == MaterialType.Empty;
        }

        public bool IsFree(int i, int j)
        {
            return IsWithihBounds(i, j) && IsFree(_matrix[i, j]);
        }

        public bool IsLiquid(int i, int j)
        {
            return IsWithihBounds(i, j) && (_matrix[i, j].Type == MaterialType.Water);
        }

        public void Swap(int i1, int j1, int i2, int j2)
        {
            var t = _matrix[i1, j1];
            _matrix[i1, j1] = _matrix[i2, j2];
            _matrix[i2, j2] = t;

            _isUpdatedThisFrame[i1, j1] = true;
            _isUpdatedThisFrame[i2, j2] = true;

            MatrixUpdated?.Invoke(this, new MatrixUpdatedEventArgs(i1, j1, _matrix[i1, j1].Color));
            MatrixUpdated?.Invoke(this, new MatrixUpdatedEventArgs(i2, j2, _matrix[i2, j2].Color));
        }

        public void StepAll()
        {
            for (int j = Height - 1; j >= 0; j--)
            {
                for (int i = 0; i < Width; i++)
                {
                    _matrix[i, j].Step(this, i, j);
                }
            }
        }

        public void Add(MaterialType type, Point location, Vector2i velocity)
        {
            if (type == MaterialType.Empty)
            {
                return;
            }

            var i = location.X;
            var j = location.Y;

            if (IsFree(i, j))
            {
                _matrix[i, j].ChangeType(type);
                _matrix[i, j].Velocity = velocity;

                MatrixUpdated?.Invoke(this, new MatrixUpdatedEventArgs(i, j, _matrix[i, j].Color));
            }
        }

        public void Erase(int i, int j)
        {
            if (IsWithihBounds(i, j))
            {
                _matrix[i, j].ChangeType(MaterialType.Empty);

                MatrixUpdated?.Invoke(this, new MatrixUpdatedEventArgs(i, j, _matrix[i, j].Color));
            }
        }

        public void ToggleFrameUpdate()
        {
            _isUpdatedThisFrame.Fill(_fillValue);
        }
    }
}
