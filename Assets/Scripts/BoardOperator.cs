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
    int BoardSizeX = 2;
    [SerializeField]
    int BoardSizeY = 2;
    [SerializeField]
    GameObject FieldPrefab;
    [SerializeField]
    public float FieldSize;

    public GameObject getField(int X, int Y)
    {
        return (fields[X][Y]);
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
    public void addCharacter(GameObject type, int X, int Y)
    {
        CharacterOperator characterComponent;
        FieldOperator fieldComponent = fields[X][Y].GetComponent<FieldOperator>();

        characters.Add(type);
        characters[characters.Count - 1] = Instantiate(characters[characters.Count - 1]);
        characters[characters.Count - 1].transform.SetPositionAndRotation(new Vector3(FieldSize * X, fieldComponent.FieldHeight, FieldSize * Y), new Quaternion());
        characterComponent = characters[characters.Count - 1].GetComponent<CharacterOperator>();
        characterComponent.setField(fields[X][Y]);
    }
    void Start()
    {
        CharacterOperator characterComponent;

        createBoard();
        addCharacter(characters[0], 0, 0);
        characterComponent = characters[characters.Count - 1].GetComponent<CharacterOperator>();
        characterComponent.declareMovement(1, 0);
    }
    void Update()
    {
        
    }
}
