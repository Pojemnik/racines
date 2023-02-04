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
    [HideInInspector]
    public GameObject field;

    Vector3 destination;
    GameObject destinationField;

    public void move()
    {
        FieldOperator fieldComponent = field.GetComponent<FieldOperator>();
        BoardOperator boardComponent = fieldComponent.board.GetComponent<BoardOperator>();
        Vector3 movement = new Vector3();

        if(destination.magnitude > 0)
        {
            if (Time.deltaTime * moveSpeed >= destination.magnitude)
            {
                movement = destination.normalized * destination.magnitude;
                field = destinationField;
                fieldComponent.character = null;
                fieldComponent = field.GetComponent<FieldOperator>();
                fieldComponent.character = gameObject;
            }
            else
            {
                movement = destination.normalized * Time.deltaTime * moveSpeed;
            }
            transform.SetPositionAndRotation(transform.position + movement, new Quaternion());
            destination -= movement;
        }
    }
    public void declareMovement(int X, int Y)
    {
        FieldOperator field1Component = field.GetComponent<FieldOperator>();
        BoardOperator boardComponent = field1Component.board.GetComponent<BoardOperator>();
        FieldOperator field2Component = boardComponent.getField(field1Component.positionX + X, field1Component.positionY + Y).GetComponent<FieldOperator>();
        destinationField = boardComponent.getField(field1Component.positionX + X, field1Component.positionY + Y);

        destination = new Vector3(X * boardComponent.FieldSize, field1Component.FieldHeight - field2Component.FieldHeight, Y * boardComponent.FieldSize);
    }
    public void setField(GameObject input)
    {
        field = input;
    }
    void Start()
    {

    }

    void Update()
    {
        move();
    }
}