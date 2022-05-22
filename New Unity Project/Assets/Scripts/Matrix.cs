using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Matrix<T> : MonoBehaviour
{
    public int Width { get; }
    public int Height { get; }
    public T[] Data { get; }
    public Matrix(int width, int height)
    {
        Width = width;
        Height = height;
        Data = new T[width * height];
    }
    public T this[int x, int y]
    {
        get => Data[y * Width + x];
        set => Data[y * Width + x] = value;
    }
}
