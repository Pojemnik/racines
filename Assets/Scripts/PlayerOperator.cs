using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterOperator))]
[RequireComponent(typeof(AudioSource))]

public class PlayerOperator : MonoBehaviour
{
    [Header("Gameplay Data")]
    [SerializeField]
    public List<AudioClip> VoicelinesWhat;
    [SerializeField]
    public List<AudioClip> VoicelinesYes;
    [SerializeField]
    public GameObject ArrowPrefab;
    [HideInInspector]
    public List<GameObject> Movements;

    bool isSelected = false;
    CharacterOperator characterComponent;
    FieldOperator fieldComponent;
    BoardOperator boardComponent;
    AudioSource audioComponent;

    public void sayWhat()
    {
        audioComponent.clip = VoicelinesWhat[Random.Range(0, VoicelinesWhat.Count)];
        audioComponent.Play();
    }
    public void sayYes()
    {
        audioComponent.clip = VoicelinesYes[Random.Range(0, VoicelinesYes.Count)];
        audioComponent.Play();
    }
    public void deselect()
    {
        isSelected = false;
        boardComponent.selectedCharacter = null;
    }
    public GameObject createArrow(GameObject field)
    {
        FieldOperator arrowFieldComponent = field.GetComponent<FieldOperator>();
        GameObject newArrow;
        newArrow = Instantiate(ArrowPrefab);
        newArrow.transform.SetPositionAndRotation(field.transform.position + new Vector3(0, arrowFieldComponent.FieldHeight, 0), new Quaternion());
        ArrowOperator arrowComponent = newArrow.GetComponent<ArrowOperator>();
        arrowComponent.player = gameObject;
        arrowComponent.field = field;
        return (newArrow);
    }
    public void deleteArrows()
    {
        for(int i=0;i<Movements.Count;i++)
        {
            Destroy(Movements[i]);
        }
    }
    public void makeDecision(GameObject decision)
    {
        ArrowOperator arrowComponent = decision.GetComponent<ArrowOperator>();
        FieldOperator arrowFieldComponent = arrowComponent.field.GetComponent<FieldOperator>();
        fieldComponent = characterComponent.field.GetComponent<FieldOperator>();

        sayYes();
        characterComponent.declareMovement(arrowFieldComponent.positionX - fieldComponent.positionX, arrowFieldComponent.positionY - fieldComponent.positionY);
        deleteArrows();
        deselect();
    }
    public void createPossibilities()
    {
        Movements = boardComponent.getPossibleMovements(characterComponent.field, characterComponent.moveRange, new List<GameObject>());
        for(int i = 0; i < Movements.Count; i++)
        {
            Movements[i] = createArrow(Movements[i]);
        }
    }
    private void OnMouseDown()
    {
        isSelected = true;
        sayWhat();
        boardComponent.characterSelected(gameObject);
        createPossibilities();
    }
    void Start()
    {
        audioComponent = GetComponent<AudioSource>();
        characterComponent = GetComponent<CharacterOperator>();
        fieldComponent = characterComponent.field.GetComponent<FieldOperator>();
        boardComponent = fieldComponent.board.GetComponent<BoardOperator>();
    }
    void Update()
    {

    }
}
