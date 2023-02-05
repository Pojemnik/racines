using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterOperator))]

public class EnemyOperator : MonoBehaviour
{
    [Header("Gameplay Data")]
    [SerializeField]
    public int SpottRange;
    [HideInInspector]
    public List<List<GameObject>> Movements;

    CharacterOperator characterComponent;
    FieldOperator fieldComponent;
    BoardOperator boardComponent;

    public void aiMakeDecision()
    {
        GameObject detectedPlayer = null;
        int value = -1;
        GameObject checkField = null;
        List<GameObject> attackPath = new List<GameObject>();
        PlayerOperator playerComponent;
        FieldOperator playerFieldComponent;

        Movements = new List<List<GameObject>>();
        Movements = boardComponent.getPossibleMovements(characterComponent.field, new List<GameObject>(), SpottRange, SpottRange, new List<List<GameObject>>(), true);
        fieldComponent = characterComponent.field.GetComponent<FieldOperator>();
        for (int i = 0; i < Movements.Count; i++)
        {
            playerFieldComponent = Movements[i][Movements[i].Count - 1].GetComponent<FieldOperator>();
            if(playerFieldComponent.character != null)
            {
                playerComponent = playerFieldComponent.character.GetComponent<PlayerOperator>();
                if (playerComponent != null && (value < 0 || Movements[value].Count > Movements[i].Count))
                {
                    detectedPlayer = playerComponent.gameObject;
                    value = i;
                }
            }
        }
        if(detectedPlayer!=null)
        {
            playerComponent = detectedPlayer.GetComponent<PlayerOperator>();
            if (Movements.Count != 0 && Movements[value].Count > 1)
            {
                if (characterComponent.moveRange >= Movements[value].Count)
                {
                    attackPath = Movements[value].GetRange(0, Movements[value].Count - 1);
                    playerComponent.kill();
                }
                else
                {
                    attackPath = Movements[value].GetRange(0, characterComponent.moveRange);
                }
                characterComponent.declareMovement(attackPath);
            }
            else
            {
                characterComponent.moved = true;
                playerComponent.kill();
            }
        }
        else
        {
            if (Movements.Count != 0)
            {
                characterComponent.declareMovement(Movements[Random.Range(0, Movements.Count)]);
            }
            else
            {
                characterComponent.moved = true;
            }
        }
    }
    public void spicePickup(GameObject spice)
    {
        for(int i = 0; i < boardComponent.rootSpice.Count; i++)
        {
            if(boardComponent.rootSpice[i] == spice)
            {
                boardComponent.rootSpice.RemoveAt(i);
                i--;
            }
        }
        Destroy(spice);
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
