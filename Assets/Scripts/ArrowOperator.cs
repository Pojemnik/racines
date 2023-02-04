using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOperator : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject field;

    private void OnMouseDown()
    {
        print("here i am");
        PlayerOperator playerControler = player.GetComponent<PlayerOperator>();

        playerControler.makeDecision(gameObject);
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
