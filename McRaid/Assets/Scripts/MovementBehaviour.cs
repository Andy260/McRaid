using UnityEngine;
using System.Collections;

public class MovementBehaviour : MonoBehaviour 
{
	protected NavMeshAgent _agent;
	protected Vector3 _goal;
	
	void Start () 
	{
		_agent 	= GetComponent<NavMeshAgent>();
	}

	void Update () 
	{

	}

	protected void FindPath(Vector3 a_goal)
	{
		_agent.destination 	=	a_goal;
		_goal 				= a_goal;
	}
}