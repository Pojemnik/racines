using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardOperator : MonoBehaviour
{
    [Header("Setup")]
    [HideInInspector]
    public List<SerializationArrayWrapper<GameObject>> fields = new List<SerializationArrayWrapper<GameObject>>();
    [HideInInspector]
    public List<GameObject> characters = new List<GameObject>();
    [SerializeField]
    public float FieldSize;
    [SerializeField]
    public int Turns;
    [HideInInspector]
    public GameObject selectedCharacter = null;
    [HideInInspector]
    public List<GameObject> rootSpice = new List<GameObject>();

    int turn = 0;

    public GameObject checkSpice(GameObject input)
    {
        FieldOperator fieldController = input.GetComponent<FieldOperator>();
        SpiceOperator spiceController;

        for (int i = 0; i < rootSpice.Count; i++)
        {
            spiceController = rootSpice[i].GetComponent<SpiceOperator>();
            if(fieldController.positionX == spiceController.positionX && fieldController.positionY == spiceController.positionY && spiceController.caryingCharacter == null)
            {
                return (rootSpice[i]);
            }
        }
        return (null);
    }
    public void enemyTurnMovements()
    {
        CharacterOperator characterController;
        EnemyOperator enemyController;

        for (int i = 0; i < characters.Count; i++)
        {
            characterController = characters[i].GetComponent<CharacterOperator>();
            if(turn == characterController.type)
            {
                enemyController = characters[i].GetComponent<EnemyOperator>();
                if (enemyController != null)
                {
                    enemyController.aiMakeDecision();
                }
            }
        }
    }
    public bool checkTurnEnd()
    {
        CharacterOperator characterController;

        for(int i=0;i<characters.Count;i++)
        {
            characterController = characters[i].GetComponent<CharacterOperator>();
            if(characterController.type==turn && !characterController.moved)
            {
                return (false);
            }
        }
        return (true);
    }
    public void nextTurn()
    {
        CharacterOperator characterController;
        bool anyCharacter = false;

        turn++;
        if(turn>=Turns)
        {
            for(int i = 0; i < characters.Count; i++)
            {
                characterController = characters[i].GetComponent<CharacterOperator>();
                characterController.moved = false;
            }
            turn = 0;
        }
        for(int i = 0; i < characters.Count; i++)
        {
            characterController = characters[i].GetComponent<CharacterOperator>();
            if (characterController.type == turn)
            {
                anyCharacter = true;
                break;
            }
        }
        if(!anyCharacter)
        {
            nextTurn();
        }
        enemyTurnMovements();
    }
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
    public void setField(GameObject input)
    {
        FieldOperator fieldControler = input.GetComponent<FieldOperator>();

        for(int i = fields.Count; i <= fieldControler.positionX; i++)
        {
            fields.Add(new SerializationArrayWrapper<GameObject>());
        }
        for (int i = fields[fieldControler.positionX].Count; i <= fieldControler.positionY; i++)
        {
            fields[fieldControler.positionX].Add(new GameObject());
            fields[fieldControler.positionX][i] = null;
        }
        fields[fieldControler.positionX][fieldControler.positionY] = input;
    }
    public void setCharacter(GameObject input)
    {
        CharacterOperator characterControler = input.GetComponent<CharacterOperator>();

        characters.Add(input);
    }
    public void setSpice(GameObject input)
    {
        rootSpice.Add(input);
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
                if(fields[X][Y]!=null)
                {
                    return (true);
                }
            }
        }
        return (false);
    }
    public List<List<GameObject>> checkAndMove(GameObject start, List<GameObject> path, int length, int total, List<List<GameObject>> used, bool ignorePlayers)
    {
        FieldOperator fieldController = start.GetComponent<FieldOperator>();
        bool possible = true;
        List<int> others = new List<int>();
        List<GameObject> newPath = new List<GameObject>(path);
        for (int i = 0; i < used.Count; i++)
        {
            if (used[i][used[i].Count - 1] == start)
            {
                if(used[i].Count <= total - length)
                {
                    possible = false;
                    break;
                }
                else
                {
                    others.Add(i);
                }
            }
        }
        if (possible)
        {
            fieldController = start.GetComponent<FieldOperator>();
            if (fieldController.canMove(ignorePlayers))
            {
                for(int i=0;i<others.Count;i++)
                {
                    used.RemoveAt(others[i]);
                }
                newPath.Add(start);
                used.Add(newPath);
                used = getPossibleMovements(start, newPath, length - 1, total, used, ignorePlayers);
            }
        }
        return (used);
    }
    public List<List<GameObject>> getPossibleMovements(GameObject start, List<GameObject> path, int length, int total, List<List<GameObject>> used, bool ignorePlayers)
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
                used = checkAndMove(newField, path, length, total, used, ignorePlayers);
            }
            if (fieldExist(X, Y - 1))
            {
                newField = fields[X][Y - 1];
                used = checkAndMove(newField, path, length, total, used, ignorePlayers);
            }
            if (fieldExist(X + 1, Y))
            {
                newField = fields[X + 1][Y];
                used = checkAndMove(newField, path, length, total, used, ignorePlayers);
            }
            if (fieldExist(X, Y + 1))
            {
                newField = fields[X][Y + 1];
                used = checkAndMove(newField, path, length, total, used, ignorePlayers);
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
        characterComponent.transform.SetPositionAndRotation(new Vector3((int)Mathf.Round(gameObject.transform.position.x), 0, (int)Mathf.Round(gameObject.transform.position.z)), transform.rotation);
    }
    public void setupCharacter(GameObject input)
    {
        CharacterOperator characterComponent = input.GetComponent<CharacterOperator>();
        FieldOperator fieldComponent = fields[(int)Mathf.Round(input.transform.position.x)][(int)Mathf.Round(input.transform.position.z)].GetComponent<FieldOperator>();
        PlayerOperator playerComponent = characterComponent.GetComponent<PlayerOperator>();
        characterComponent.setField(fieldComponent.gameObject);
        if(playerComponent != null)
        {
            playerComponent.fieldComponent = fieldComponent;
        }
    }
    private void Awake()
    {

    }
    void Start()
    {
        for(int i = 0; i < characters.Count; i++)
        {
            setupCharacter(characters[i]);
        }
    }
    void Update()
    {
        
    }
}
