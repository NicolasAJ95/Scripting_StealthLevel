using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBotAIController : MonoBehaviour {

	public float sightRange = 10f;
	public float searchingTurnSpeed = 120f;

	public Transform chaseTarget;
	public NavMeshAgentController navMeshAgent;

	private EBotStateBase currentState;
	private Dictionary<EBotState, EBotStateBase> states;

	private void Awake ()
	{
		states = new Dictionary<EBotState, EBotStateBase> ();
		states.Add (EBotState.Listen, new EBotListenState (this));
		states.Add (EBotState.Chase, new EBotChaseState (this));
		currentState = states [EBotState.Listen];
	}
	
	private void Update () 
	{
		currentState.UpdateState ();
	}

	public void MakeTransition (EBotState state)
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
