using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiceOperator : MonoBehaviour
{
    [Header("Gameplay Data")]
    [SerializeField]
    public float BackDistance;
    [HideInInspector]
    public int positionX;
    [HideInInspector]
    public int positionY;
    [HideInInspector]
    public GameObject caryingCharacter = null;
    [HideInInspector]
    public GameObject board;

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
            position = caryingCharacter.transform.position + (rotation * new Vector3(0,0,-BackDistance));
            transform.SetPositionAndRotation(position, rotation);
        }
    }
}
