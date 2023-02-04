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

    bool isSelected = false;
    CharacterController characterComponent;
    FieldOperator fieldComponent;
    AudioSource audioComponent;

    void checkInput()
    {
    }
    private void OnMouseDown()
    {
        print("SOUND");
        isSelected = true;
        audioComponent.clip = VoicelinesWhat[Random.Range(0, VoicelinesWhat.Count)];
        audioComponent.Play();
    }
    void Start()
    {
        audioComponent = GetComponent<AudioSource>();
        characterComponent = GetComponent<CharacterController>();
    }
    void Update()
    {
        if(isSelected)
        {
            checkInput();
        }
    }
}
