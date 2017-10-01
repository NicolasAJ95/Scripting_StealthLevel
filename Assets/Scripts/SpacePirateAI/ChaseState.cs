using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : PirateStateBase 
{

	public ChaseState(PirateAIController controlled): base(controlled){
	}

	public override void UpdateState(){
		Chase();
		Look();
	}

	private void Look(){
		Transform player = LookForPlayer ();
		if(player != null)
		controlled.chaseTarget = player;
		else
		ToAlert();

	}

	private void Chase(){
		controlled.navMeshAgent.SetDestination(controlled.chaseTarget.position);
			if (IsCloseEnough ())
			//Interaction when caught
				GameObject.Destroy (controlled.chaseTarget.gameObject);
	}

	private bool IsCloseEnough(){
		return (controlled.chaseTarget.position - controlled.transform.position).magnitude < 1;
	}

	private void ToAlert(){
		controlled.chaseTarget = null;
		controlled.MakeTransition (PirateState.Alert);
	}

}
