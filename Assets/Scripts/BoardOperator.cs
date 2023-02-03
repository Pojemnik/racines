using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardOperator : MonoBehaviour
{
    [Header("Setup")]
    [HideInInspector]
    public GameObject[][] fields;
    [SerializeField]
    int BoardSizeX = 2;
    [SerializeField]
    int BoardSizeY = 2;
    [SerializeField]
    GameObject FieldPrefab;
    [SerializeField]
    float FieldSize;

    void createBoard()
    {
        fields = new GameObject[BoardSizeX][];
        for(int i=0; i<BoardSizeX; i++)
        {
            fields[i] = new GameObject[BoardSizeY];
            for (int j=0; j<BoardSizeY; j++)
            {
                fields[i][j] = FieldPrefab;
                Instantiate(fields[i][j]);
                fields[i][j].transform.SetPositionAndRotation(new Vector3(FieldSize * i, 0, FieldSize * j), new Quaternion());
            }
        }
    }
    void Start()
    {
        createBoard();
    }
    void Update()
    {
        
    }
}
