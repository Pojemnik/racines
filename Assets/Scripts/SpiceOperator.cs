using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiceOperator : MonoBehaviour
{
    [Header("Gameplay Data")]
    [SerializeField]
    public float BackDistance;
    [SerializeField]
    public float BackHeight;
    [HideInInspector]
    public int positionX;
    [HideInInspector]
    public int positionY;
    [HideInInspector]
    public GameObject caryingCharacter = null;
    [HideInInspector]
    public GameObject board;

    public void drop()
    {
        BoardOperator boardController = FindObjectOfType<BoardOperator>();
        PlayerOperator playerComponent = caryingCharacter.GetComponent<PlayerOperator>();
        CharacterOperator characterComponent = caryingCharacter.GetComponent<CharacterOperator>();
        FieldOperator fieldComponent;

        if (playerComponent != null)
        {
            playerComponent.carriedSpice = null;
            for (int i = 0; i < boardController.rootSpice.Count; i++)
            {
                if (boardController.rootSpice[i] == gameObject)
                {
                    boardController.rootSpice.RemoveAt(i);
                    i++;
                }
            }
            playerComponent.sayDrop();
            fieldComponent = characterComponent.field.GetComponent<FieldOperator>();
            positionX = fieldComponent.positionX;
            positionY = fieldComponent.positionY;
            transform.SetPositionAndRotation(characterComponent.field.transform.position, Quaternion.Euler(0, 0, 0));
        }
    }
    public void score()
    {
        BoardOperator boardController = FindObjectOfType<BoardOperator>();
        PlayerOperator playerComponent = caryingCharacter.GetComponent<PlayerOperator>();

        if (playerComponent != null)
        {
            playerComponent.carriedSpice = null;
            for(int i=0;i<boardController.rootSpice.Count;i++)
            {
                if(boardController.rootSpice[i] == gameObject)
                {
                    boardController.rootSpice.RemoveAt(i);
                    i++;
                }
            }
            playerComponent.sayDrop();
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        BoardOperator boardController = FindObjectOfType<BoardOperator>();
        board = boardController.gameObject;
        positionX = (int)Mathf.Round(transform.position.x);
        positionY = (int)Mathf.Round(transform.position.z);
        boardController.setSpice(gameObject);
    }
    void Start()
    {
        
    }
    void Update()
    {
        CharacterOperator characterController;
        Vector3 position;
        Quaternion rotation;

        if (caryingCharacter!=null)
        {
            characterController = caryingCharacter.GetComponent<CharacterOperator>();
            rotation = caryingCharacter.transform.rotation;
            position = caryingCharacter.transform.position + (rotation * new Vector3(0,0,-BackDistance)) + new Vector3(0, BackHeight, 0);
            transform.SetPositionAndRotation(position, rotation);
        }
    }
}
