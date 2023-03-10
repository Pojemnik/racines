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
    public List<AudioClip> VoicelinesPick;
    [SerializeField]
    public List<AudioClip> VoicelinesDrop;
    [SerializeField]
    public List<AudioClip> VoicelinesDie;
    [SerializeField]
    public GameObject ArrowPrefab;
    [SerializeField]
    public bool Killable;
    [HideInInspector]
    public List<List<GameObject>> Movements;
    [HideInInspector]
    public List<GameObject> Arrows;
    [HideInInspector]
    public GameObject carriedSpice = null;
    [HideInInspector]
    public bool isSelected = false;
    [HideInInspector]
    public CharacterOperator characterComponent;
    [HideInInspector]
    public FieldOperator fieldComponent;
    [HideInInspector]
    public BoardOperator boardComponent;
    [HideInInspector]
    public AudioSource audioComponent;

    public void sayDie()
    {
        audioComponent.clip = VoicelinesDie[Random.Range(0, VoicelinesDie.Count)];
        audioComponent.Play();
    }
    public void sayPick()
    {
        audioComponent.clip = VoicelinesPick[Random.Range(0, VoicelinesPick.Count)];
        audioComponent.Play();
    }
    public void sayDrop()
    {
        audioComponent.clip = VoicelinesDrop[Random.Range(0, VoicelinesDrop.Count)];
        audioComponent.Play();
    }
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
    public void kill()
    {
        sayDie();
        if(Killable)
        {
            characterComponent.kill();
        }
    }
    public void deselect()
    {
        deleteArrows();
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
            Destroy(Arrows[i]);
        }
    }
    public void makeDecision(GameObject decision)
    {
        ArrowOperator arrowComponent = decision.GetComponent<ArrowOperator>();
        FieldOperator arrowFieldComponent = arrowComponent.field.GetComponent<FieldOperator>();
        fieldComponent = characterComponent.field.GetComponent<FieldOperator>();

        sayYes();
        for(int i=0;i<Movements.Count;i++)
        {
            if(Arrows[i] == decision)
            {
                characterComponent.declareMovement(Movements[i]);
            }
        }
        deselect();
    }
    public void createPossibilities()
    {
        Movements = boardComponent.getPossibleMovements(characterComponent.field, new List<GameObject>(), characterComponent.moveRange, characterComponent.moveRange, new List<List<GameObject>>(), false);
        Arrows = new List<GameObject>();
        if(Movements.Count != 0)
        {
            for (int i = 0; i < Movements.Count; i++)
            {
                Arrows.Add(createArrow(Movements[i][Movements[i].Count - 1]));
            }
        }
        else
        {
            deselect();
            characterComponent.moved = true;
        }
    }
    public void spicePickup(GameObject spice)
    {
        if (carriedSpice == null)
        {
            SpiceOperator spiceController = spice.GetComponent<SpiceOperator>();

            spiceController.caryingCharacter = gameObject;
            carriedSpice = spice;
            sayPick();
        }
    }
    private void OnMouseDown()
    {
        if(characterComponent.field == characterComponent.destinationField[characterComponent.destinationField.Count - 1] && !characterComponent.moved)
        {
            if (isSelected)
            {
                deselect();
            }
            else
            {
                isSelected = true;
                sayWhat();
                boardComponent.characterSelected(gameObject);
                createPossibilities();
            }
        }
    }
    private void Awake()
    {
        boardComponent = FindObjectOfType<BoardOperator>();
    }
    void Start()
    {
        audioComponent = GetComponent<AudioSource>();
        characterComponent = GetComponent<CharacterOperator>();
        isSelected = false;
    }
    void Update()
    {

    }
}
