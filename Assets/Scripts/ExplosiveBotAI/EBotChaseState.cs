using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBotChaseState : EBotStateBase {

	public EBotChaseState (EBotAIController controlled): base(controlled){

	}

	public override void UpdateState(){
		Chase();
		Look();
	}

	private void Look(){
		Transform player = LookForPlayer();
		if(player != null)
		controlled.chaseTarget = player;
		else
		ToListen();
	}

	private void Chase(){
		controlled.navMeshAgent .SetDestination (controlled.chaseTarget.position);
			if (IsCloseEnough())
				GameObject.Destroy(controlled.gameObject );
	}

	private bool IsCloseEnough(){
		return (controlled.chaseTarget.position - controlled.transform.position).magnitude < 1;
	}

	private void ToListen(){
		controlled.chaseTarget = null;
		controlled.MakeTransition(EBotState.Listen);
	}

}
