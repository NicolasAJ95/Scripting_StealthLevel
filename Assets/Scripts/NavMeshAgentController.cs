using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (Animator))]
public class NavMeshAgentController : MonoBehaviour 
{

	private NavMeshAgent agent;
	private Animator animator;

	public bool IsMoving
	{
		get
		{
			return agent.pathPending || agent.remainingDistance > agent.stoppingDistance;
		}
	}

	private void Awake ()
	{
		// Get references to components, shouldn't be null because of RequireComponent attribute
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
	}
	
	private void Update () 
	{
		SyncAnimation ();
	}

	public void SetDestination(Vector3 destination)
	{
		// Set the agents new destination
		agent.destination = destination;
		agent.Resume ();
	}

	public void Stop ()
	{
		agent.Stop ();
	}

	private void SyncAnimation ()
	{
		// Set speed parameter in animator
		float speed = IsMoving? 1f : 0f;
		animator.SetFloat ("Speed", speed);
	}
}
