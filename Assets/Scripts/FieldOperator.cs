using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOperator : MonoBehaviour
{
    [Header("Gameplay Data")]
    [SerializeField]
    public bool Walkable = true;
    [SerializeField]
    public bool SpiceDrop = false;
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

    public bool canMove(bool ignorePlayers)
    {
        PlayerOperator playerController = null;
        if (character != null && ignorePlayers)
        {
            playerController = character.GetComponent<PlayerOperator>();
        }
        if (Walkable && (character == null || playerController != null))
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
    private void Awake()
    {
        BoardOperator boardController = FindObjectOfType<BoardOperator>();
        board = boardController.gameObject;
        positionX = (int)Mathf.Round(transform.position.x);
        positionY = (int)Mathf.Round(transform.position.z);
        boardController.setField(gameObject);
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
