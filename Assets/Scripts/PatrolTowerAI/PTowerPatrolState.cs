using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTowerPatrolState : PTowerStateBase {

public PTowerPatrolState(PTowerAIController controlled): base (controlled){

}

public override void UpdateState(){
	Patrol();
	Search();
}

private void Patrol(){
	controlled.eye.transform .Rotate  (0,controlled .searchingTurnSpeed * Time.deltaTime, 0); 
}

private void Search(){
	Transform player = LookForPlayer ();
	if(player != null)
	ToAlert(player);
}

private void ToAlert (Transform player){
	controlled.MakeTransition(PTowerState .Alert );
}
}
