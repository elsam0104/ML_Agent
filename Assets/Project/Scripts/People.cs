using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Color
{
    Black,
    White,
    Yellow,
}
public enum Sex
{
    Womyn,
    Man,
    Other,
}
public enum Age
{
    Young,
    Old
}
public class People : MonoBehaviour
{
    public Color Color;
    public Sex Sex;
    public Age Age;
    public bool isCriminal = false;
}
