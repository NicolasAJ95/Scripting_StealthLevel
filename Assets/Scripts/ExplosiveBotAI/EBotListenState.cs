using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBotListenState : EBotStateBase  {

	public EBotListenState (EBotAIController controlled): base (controlled){

	}

	public override void UpdateState(){
		Listen();
	}

	private void Listen(){
		Transform player = LookForPlayer ();
		if(player != null)
		ToChase(player);
	}

	private void ToChase(Transform player){
		controlled.chaseTarget = player;
		controlled.MakeTransition(EBotState.Chase);
	}
}
