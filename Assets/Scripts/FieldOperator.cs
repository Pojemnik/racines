using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOperator : MonoBehaviour
{
    [Header("Gameplay Data")]
    [SerializeField]
    public bool Walkable = true;
    [SerializeField]
    public float FieldHeight = 0;

    public GameObject board;
    public int positionX;
    public int positionY;

    public void setBoard(GameObject input, int x, int y)
    {
        board = input;
        positionX = x;
        positionY = y;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
