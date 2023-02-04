using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardOperator : MonoBehaviour
{
    [Header("Setup")]
    [HideInInspector]
    public GameObject[][] fields;
    [SerializeField]
    public List<GameObject> characters = new List<GameObject>();
    [SerializeField]
    public int BoardSizeX = 2;
    [SerializeField]
    public int BoardSizeY = 2;
    [SerializeField]
    public GameObject FieldPrefab;
    [SerializeField]
    public float FieldSize;
    [HideInInspector]
    public GameObject selectedCharacter = null;

    public void characterSelected(GameObject character)
    {
        PlayerOperator playerControler;
        if (selectedCharacter!=null)
        {
            playerControler = selectedCharacter.GetComponent<PlayerOperator>();
            playerControler.deselect();
        }
        selectedCharacter = character;
        playerControler = selectedCharacter.GetComponent<PlayerOperator>();
    }
    public GameObject getField(int X, int Y)
    {
        return (fields[X][Y]);
    }
    public List<GameObject> checkAndMove(GameObject start, int length, List<GameObject> used)
    {
        FieldOperator fieldController = start.GetComponent<FieldOperator>();
        bool possible = true;
        for (int i = 0; i < used.Count; i++)
        {
            if (used[i] == start)
            {
                possible = false;
                break;
            }
        }
        if (possible)
        {
            fieldController = start.GetComponent<FieldOperator>();
            if (fieldController.canMove())
            {
                used.Add(start);
                used = getPossibleMovements(start, length - 1, used);
            }
        }
        return (used);
    }
    public List<GameObject> getPossibleMovements(GameObject start, int length, List<GameObject> used)
    {
        if(length != 0)
        {
            FieldOperator fieldController = start.GetComponent<FieldOperator>();
            GameObject newField = null;
            int X = fieldController.positionX;
            int Y = fieldController.positionY;
            if (X - 1 >= 0)
            {
                newField = fields[X - 1][Y];
                used = checkAndMove(newField, length, used);
            }
            if (Y - 1 >= 0)
            {
                newField = fields[X][Y - 1];
                used = checkAndMove(newField, length, used);
            }
            if (X + 1 < BoardSizeX)
            {
                newField = fields[X + 1][Y];
                used = checkAndMove(newField, length, used);
            }
            if (Y + 1 < BoardSizeY)
            {
                newField = fields[X][Y + 1];
                used = checkAndMove(newField, length, used);
            }
        }
        return (used);
    }
    void createBoard()
    {
        fields = new GameObject[BoardSizeX][];
        FieldOperator fieldComponent;

        for(int i=0; i<BoardSizeX; i++)
        {
            fields[i] = new GameObject[BoardSizeY];
            for (int j=0; j<BoardSizeY; j++)
            {
                fields[i][j] = FieldPrefab;
                fields[i][j] = Instantiate(fields[i][j]);
                fieldComponent = fields[i][j].GetComponent<FieldOperator>();
                fieldComponent.setBoard(gameObject, i, j);
                fields[i][j].transform.SetPositionAndRotation(new Vector3(FieldSize * i, 0, FieldSize * j), new Quaternion());
            }
        }
    }
    public void addCharacter(int type, int X, int Y)
    {
        CharacterOperator characterComponent;
        FieldOperator fieldComponent = fields[X][Y].GetComponent<FieldOperator>();

        characters[type] = Instantiate(characters[type]);
        characters[type].transform.SetPositionAndRotation(new Vector3(FieldSize * X, fieldComponent.FieldHeight, FieldSize * Y), new Quaternion());
        characterComponent = characters[type].GetComponent<CharacterOperator>();
        characterComponent.setField(fields[X][Y]);
    }
    void Start()
    {
        CharacterOperator characterComponent;

        createBoard();
        addCharacter(0, 0, 0);
        characterComponent = characters[characters.Count - 1].GetComponent<CharacterOperator>();
    }
    void Update()
    {
        
    }
}
