using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOperator : MonoBehaviour
{
    [Header("Gameplay Data")]
    [SerializeField]
    public int moveRange;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public float height;
    [SerializeField]
    public int type = 0;
    [HideInInspector]
    public GameObject field;
    [HideInInspector]
    public List<GameObject> destinationField;
    [HideInInspector]
    public int pathStep;
    [HideInInspector]
    public bool moved = false;

    Vector3 destination;

    public void move()
    {
        FieldOperator field1Component = field.GetComponent<FieldOperator>();
        BoardOperator boardComponent = field1Component.board.GetComponent<BoardOperator>();
        FieldOperator field2Component;
        PlayerOperator playerComponent;
        GameObject spice;
        bool change = false;
        Vector3 movement = new Vector3();

        if(destination.magnitude > 0)
        {
            if (Time.deltaTime * moveSpeed >= destination.magnitude)
            {
                movement = destination.normalized * destination.magnitude;
                field = destinationField[pathStep];
                pathStep++;
                if(pathStep < destinationField.Count)
                {
                    change = true;
                }
            }
            else
            {
                movement = destination.normalized * Time.deltaTime * moveSpeed;
            }
            transform.SetPositionAndRotation(transform.position + movement, new Quaternion());
            transform.forward = destination;
            if(change)
            {
                field1Component = destinationField[pathStep-1].GetComponent<FieldOperator>();
                field2Component = destinationField[pathStep].GetComponent<FieldOperator>();
                destination = new Vector3((field2Component.positionX - field1Component.positionX) * boardComponent.FieldSize, field2Component.FieldHeight - field1Component.FieldHeight, (field2Component.positionY - field1Component.positionY) * boardComponent.FieldSize);
            }
            else
            {
                destination -= movement;
            }
        }
        if(pathStep >= destinationField.Count)
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
            spice = boardComponent.checkSpice(field);
            if (spice != null)
            {
                spicePickup(spice);
            }
            moved = true;
            if(boardComponent.checkTurnEnd())
            {
                boardComponent.nextTurn();
            }
            playerComponent = gameObject.GetComponent<PlayerOperator>();
            field2Component = field.GetComponent<FieldOperator>();
            if (playerComponent != null && playerComponent.carriedSpice != null && field2Component.SpiceDrop)
            {
                SpiceOperator spiceController = playerComponent.carriedSpice.GetComponent<SpiceOperator>();
                spiceController.score();
            }
        }
    }
    public void declareMovement(List<GameObject> path)
    {
        FieldOperator field1Component = field.GetComponent<FieldOperator>();
        BoardOperator boardComponent = field1Component.board.GetComponent<BoardOperator>();
        FieldOperator field2Component = path[path.Count - 1].GetComponent<FieldOperator>();
        destinationField = path;
        field1Component.character = null;
        field2Component.character = gameObject;
        field2Component = path[0].GetComponent<FieldOperator>();
        pathStep = 0;

        destination = new Vector3((field2Component.positionX - field1Component.positionX) * boardComponent.FieldSize, field2Component.FieldHeight - field1Component.FieldHeight, (field2Component.positionY - field1Component.positionY) * boardComponent.FieldSize);
    }
    public void setField(GameObject input)
    {
        FieldOperator fieldComponent = input.GetComponent<FieldOperator>();
        field = input;
        destinationField = new List<GameObject>();
        fieldComponent.character = gameObject;
        destinationField.Add(input);
    }
    public void spicePickup(GameObject spice)
    {
        PlayerOperator playerController = gameObject.GetComponent<PlayerOperator>();
        EnemyOperator enemyController = gameObject.GetComponent<EnemyOperator>();

        if (playerController != null)
        {
            playerController.spicePickup(spice);
        }
        if (enemyController != null)
        {
            enemyController.spicePickup(spice);
        }
    }
    public void kill()
    {
        FieldOperator fieldComponent = field.GetComponent<FieldOperator>();
        BoardOperator boardComponent = fieldComponent.board.GetComponent<BoardOperator>();
        PlayerOperator playerComponent = gameObject.GetComponent<PlayerOperator>();
        SpiceOperator spiceComponent;

        fieldComponent.character = null;
        if(playerComponent != null)
        {
            if(playerComponent.carriedSpice != null)
            {
                spiceComponent = playerComponent.carriedSpice.GetComponent<SpiceOperator>();
                spiceComponent.drop();
            }
        }
        for(int i = 0; i < boardComponent.characters.Count; i++)
        {
            if (boardComponent.characters[i] == gameObject)
            {
                boardComponent.characters.RemoveAt(i);
                i--;
            }
        }
        Destroy(gameObject);
    }
    private void Awake()
    {
        BoardOperator boardController = FindObjectOfType<BoardOperator>();
        boardController.setCharacter(gameObject);
    }
    void Start()
    {

    }

    void Update()
    {
        if(field != destinationField[destinationField.Count - 1])
        {
            move();
        }
    }
}