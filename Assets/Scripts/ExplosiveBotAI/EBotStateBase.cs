using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBotState{
	Chase, Listen
}
public abstract class EBotStateBase : MonoBehaviour {
protected EBotAIController controlled;
public EBotStateBase (EBotAIController controlled){
	this.controlled = controlled;
}

public virtual void StartState(){

	}

	public virtual void OnTriggerEnter(Collider other){

	}

	public abstract void UpdateState();

	protected Transform LookForPlayer(){
		RaycastHit hit;
		Vector3 end = controlled.transform.position + (controlled.transform.forward * controlled.sightRange) ;
	
		if(Physics.SphereCast(controlled.transform.position, controlled.sightRange, controlled.transform.forward, out hit, 
								controlled .sightRange )&& hit.collider.CompareTag ("Player"))
		{
			Debug.Log("Player found");
			return hit.transform ;
		}else
		return null;
	}

}
