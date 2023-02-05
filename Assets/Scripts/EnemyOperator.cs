using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterOperator))]

public class EnemyOperator : MonoBehaviour
{
    [Header("Gameplay Data")]
    [SerializeField]
    public List<List<GameObject>> SpottRange;
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
    public void spicePickup(GameObject spice)
    {
        SpiceOperator spiceController = spice.GetComponent<SpiceOperator>();

        spiceController.caryingCharacter = gameObject;
    }
    private void Awake()
    {
        boardComponent = FindObjectOfType<BoardOperator>();
    }
    void Start()
    {
        characterComponent = GetComponent<CharacterOperator>();
    }
    void Update()
    {
        
    }
}
