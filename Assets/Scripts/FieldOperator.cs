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
    [HideInInspector]
    public GameObject board;
    [HideInInspector]
    public int positionX;
    [HideInInspector]
    public int positionY;
    [HideInInspector]
    public GameObject character = null;

    public bool canMove()
    {
        if (Walkable && character == null)
        {
            return (true);
        }
        return (false);
    }
    public void setBoard(GameObject input, int x, int y)
    {
        board = input;
        positionX = x;
        positionY = y;
        character = null;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
