using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : PirateStateBase {

	private int nextWayPoint;

	public PatrolState (PirateAIController controlled): base (controlled){

	}

	public override void UpdateState() {
		Patrol();
		Search();
	}

	public override void StartState(){
		SetCurrentPath();
	}

	public override void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player"))
		controlled.MakeTransition (PirateState.Alert);
	}

	private void Patrol(){
		if(!controlled.navMeshAgent .IsMoving ){
			nextWayPoint = (nextWayPoint + 1) % controlled.wayPoints .Length;
			SetCurrentPath(); 
		}
	}

	private void SetCurrentPath(){
		controlled.navMeshAgent.SetDestination(controlled.wayPoints[nextWayPoint].position);
	}

	private void Search(){
		Transform player = LookForPlayer ();
		if(player != null)
		ToChase(player);
	}

	private void ToChase (Transform player){
		controlled.chaseTarget = player;
		controlled.MakeTransition(PirateState.Chase);
	}
}
