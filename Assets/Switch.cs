using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    [SerializeField]
    private int index;
    private bool playerIsClose;
    public bool switchOn;
    private GameManager gM;
    // Use this for initialization
    void Start () {
        playerIsClose = false;
        switchOn = false;
        gM = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose == true && switchOn == false)
        {
            switchOn = true;
            gM.AddSwitch (index);           
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag ("Player"))
        {
            Debug.Log("Hi");
            playerIsClose = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Bye");
            playerIsClose = false;
        }
    }
}
