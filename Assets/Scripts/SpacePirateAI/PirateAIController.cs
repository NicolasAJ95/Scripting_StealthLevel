using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PirateAIController : MonoBehaviour 
{

	public Transform[] wayPoints;
    public Transform eyes;
	public float sightRange = 20f;
	public float searchingTurnSpeed = 120f;
    public float searchingDuration = 4f;
	public Vector3 offset = new Vector3 (0,.5f,0);
	public NavMeshAgentController navMeshAgent;
	public Transform chaseTarget;

	private PirateStateBase currentState;
	private Dictionary<PirateState, PirateStateBase> states;

	private void Awake ()
	{
		states = new Dictionary<PirateState, PirateStateBase> ();
		states.Add (PirateState.Patrol, new PatrolState (this));
		states.Add (PirateState.Alert, new AlertState (this));
		states.Add (PirateState.Chase, new ChaseState (this));
		currentState = states [PirateState.Patrol];
	}
	
	private void Update () 
	{
		currentState.UpdateState ();
	}

	public void MakeTransition (PirateState state)
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
