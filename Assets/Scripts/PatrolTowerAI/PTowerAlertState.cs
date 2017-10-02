using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTowerAlertState : PTowerStateBase {

	private float searchTimer;


	public PTowerAlertState(PTowerAIController controlled): base(controlled){

	}

	public override void UpdateState()
	{
		AlertEnemies();
		Look();
	}

	private void Look(){
		Transform player = LookForPlayer ();
		if(player != null){
			AlertEnemies();
			//controlled.chaseTarget = player;
		}
		
		else
		ToPatrol();
	}

	private void AlertEnemies(){
		RaycastHit hit;
		Vector3 end = controlled.transform.position + (controlled.transform.forward * controlled.sightRange) ;
		if(Physics.SphereCast(controlled.transform.position, controlled.sightRange, controlled.transform.forward, out hit, 
								controlled .sightRange )&& hit.collider.CompareTag ("Enemy"))
		{
			hit.collider.gameObject.GetComponent <PirateAIController >().chaseTarget = controlled.chaseTarget ;
			hit.collider.gameObject.GetComponent <EBotAIController >().chaseTarget = controlled.chaseTarget;
		}
	
		Debug.Log("Pirates attack the intruder!");
	}

	private void ToPatrol(){
		controlled.MakeTransition (PTowerState.Patrol );
	}
}
