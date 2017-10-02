using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PTowerState{
	Patrol, Alert
}

public abstract class PTowerStateBase {
protected PTowerAIController controlled;

	public PTowerStateBase(PTowerAIController controlled){
		this.controlled = controlled;
	}

	public virtual void StartState(){

	}

	public virtual void OnTriggerEnter(Collider other){

	}

	public abstract void UpdateState();

	protected Transform LookForPlayer(){
		RaycastHit hit;
		Vector3 end = controlled.eye.transform.position + (controlled.eye.transform.forward * controlled.sightRange  + new Vector3(0, -6, 0)) ;
		Debug.DrawLine(controlled.eye.transform.position, end);
		if(Physics.SphereCast(controlled.eye.transform.position, 2f, controlled.eye.transform.forward, out hit, 
								controlled .sightRange )&& hit.collider.CompareTag ("Player"))
		{
			return hit.transform ;
		}else
		return null;
	}
}
