using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializationArrayWrapper<T>
{
    [SerializeField]
    public List<T> Array;

    public SerializationArrayWrapper()
    {
        Array = new List<T>();
    }

    public T this[int i]
    {
        get { return Array[i]; }
        set { Array[i] = value; }
    }
    public int Count { get { return Array.Count; } }
    public void Add(T input)
    {
        Array.Add(input);
    }
}
