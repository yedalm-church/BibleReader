using System;
using UnityEngine;

[Serializable]
public class BibleBookData
{
    [SerializeField] private int _index;
    [SerializeField] private string _name;

    public static object OldTestament { get; internal set; }

    public int Index => _index;
    public string Name => _name;
}