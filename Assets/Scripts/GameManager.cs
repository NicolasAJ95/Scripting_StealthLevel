using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Queue<int> indexTryOrder;
    public int[] indexCorrectOrder;
    private int switchesOn;
    [SerializeField ]
    private Switch[] switches;

	// Use this for initialization
	void Start () {
        switches = FindObjectsOfType<Switch>(); 
        indexTryOrder = new Queue<int>();
        switchesOn = 0;
	}
	
    public void AddSwitch(int switchIndex)
    {
        indexTryOrder.Enqueue(switchIndex);
        Debug.Log("Switch activated, index: " + switchIndex);
        switchesOn++;
        if(switchesOn >= 3)
        {
            StartCoroutine("VerifyIndexOrder");
        }
    }

    IEnumerator VerifyIndexOrder()
    {
        yield return new WaitForSeconds(1);
        TurnOffSwitches();
        Debug.Log("Verifying... ");
        int[] tryOrder = indexTryOrder.ToArray();
        Debug.Log(tryOrder[0] + ", " + tryOrder[1] + ", " + tryOrder[2]);
        for(int i = 0; i < tryOrder.Length; i++)
        {
            if(tryOrder[i] != indexCorrectOrder[i])
            {
                Debug.Log("Wrong");
                indexTryOrder.Clear();
            }
            else
            {
                Debug.Log("Correct");
                indexTryOrder.Clear();
            }
        }


    }

    private void TurnOffSwitches()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            switches[i].switchOn = false;
            switchesOn = 0;
        }
    }

	// Update is called once per frame
	void Update () {
        Debug.Log(indexTryOrder);
	}
}
