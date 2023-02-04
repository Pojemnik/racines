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

    public void deselect()
    {
        isSelected = false;
    }
    public GameObject createArrow(GameObject field)
    {
        FieldOperator arrowFieldComponent = field.GetComponent<FieldOperator>();
        GameObject newArrow;
        newArrow = Instantiate(ArrowPrefab);
        newArrow.transform.SetPositionAndRotation(field.transform.position + new Vector3(0, arrowFieldComponent.FieldHeight, 0), new Quaternion());
        return (newArrow);
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
        boardComponent.characterSelected(gameObject);
        audioComponent.clip = VoicelinesWhat[Random.Range(0, VoicelinesWhat.Count)];
        audioComponent.Play();
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
