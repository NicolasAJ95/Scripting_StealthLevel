using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTowerAIController : MonoBehaviour {

   public Transform eye;
	public float sightRange = 20f;
	public float searchingTurnSpeed = 120f;

	public Transform chaseTarget;

	private PTowerStateBase currentState;
	private Dictionary<PTowerState, PTowerStateBase> states;

	private void Awake ()
	{
		states = new Dictionary<PTowerState, PTowerStateBase> ();
		states.Add (PTowerState.Patrol, new PTowerPatrolState (this));
		states.Add (PTowerState.Alert, new PTowerAlertState (this));
		currentState = states [PTowerState.Patrol];
	}
	
	private void Update () 
	{
		currentState.UpdateState ();
	}

	public void MakeTransition (PTowerState state)
	{
		Debug.Log (state);
		currentState = states[state];
		currentState.StartState ();
	}

	private void OnTriggerEnter(Collider other)
	{
		currentState.OnTriggerEnter (other);
	}
}
