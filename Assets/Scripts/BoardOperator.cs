using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardOperator : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    public List<SerializationArrayWrapper<GameObject>> fields;
    [SerializeField]
    public List<GameObject> characters = new List<GameObject>();
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
    public bool fieldExist(int X, int Y)
    {
        if (X >= 0 && X < fields.Count)
        {
            if (Y >= 0 && Y < fields[X].Count)
            {
                return (true);
            }
        }
        return (false);
    }
    public List<List<GameObject>> checkAndMove(GameObject start, List<GameObject> path, int length, List<List<GameObject>> used)
    {
        FieldOperator fieldController = start.GetComponent<FieldOperator>();
        bool possible = true;
        List<GameObject> newPath = new List<GameObject>(path);
        for (int i = 0; i < used.Count; i++)
        {
            if (used[i][used[i].Count - 1] == start)
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
                newPath.Add(start);
                used.Add(newPath);
                used = getPossibleMovements(start, newPath, length - 1, used);
            }
        }
        return (used);
    }
    public List<List<GameObject>> getPossibleMovements(GameObject start, List<GameObject> path, int length, List<List<GameObject>> used)
    {
        if(length != 0)
        {
            FieldOperator fieldController = start.GetComponent<FieldOperator>();
            GameObject newField = null;
            int X = fieldController.positionX;
            int Y = fieldController.positionY;
            if (fieldExist(X - 1, Y))
            {
                newField = fields[X - 1][Y];
                used = checkAndMove(newField, path, length, used);
            }
            if (fieldExist(X, Y - 1))
            {
                newField = fields[X][Y - 1];
                used = checkAndMove(newField, path, length, used);
            }
            if (fieldExist(X + 1, Y))
            {
                newField = fields[X + 1][Y];
                used = checkAndMove(newField, path, length, used);
            }
            if (fieldExist(X, Y + 1))
            {
                newField = fields[X][Y + 1];
                used = checkAndMove(newField, path, length, used);
            }
        }
        return (used);
    }
    void createBoard()
    {
        FieldOperator fieldComponent;

        for(int i=0; i<fields.Count; i++)
        {
            for (int j=0; j<fields[i].Count; j++)
            {
                fieldComponent = fields[i][j].GetComponent<FieldOperator>();
                fieldComponent.setBoard(gameObject, i, j);
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
        addCharacter(0, 1, 1);
        characterComponent = characters[characters.Count - 1].GetComponent<CharacterOperator>();
    }
    void Update()
    {
        
    }
}
