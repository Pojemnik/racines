using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterOperator))]

public class EnemyOperator : MonoBehaviour
{
    [HideInInspector]
    public List<List<GameObject>> Movements;

    CharacterOperator characterComponent;
    FieldOperator fieldComponent;
    BoardOperator boardComponent;

    public void aiMakeDecision()
    {
        Movements = new List<List<GameObject>>();
        Movements = boardComponent.getPossibleMovements(characterComponent.field, new List<GameObject>(), characterComponent.moveRange, characterComponent.moveRange, new List<List<GameObject>>());
        fieldComponent = characterComponent.field.GetComponent<FieldOperator>();
        if(Movements.Count != 0)
        {
            characterComponent.declareMovement(Movements[Random.Range(0, Movements.Count)]);
        }
        characterComponent.moved = true;
    }
    void Start()
    {
        characterComponent = GetComponent<CharacterOperator>();
        fieldComponent = characterComponent.field.GetComponent<FieldOperator>();
        boardComponent = fieldComponent.board.GetComponent<BoardOperator>();
    }
    void Update()
    {
        
    }
}