using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAIBehaviour : BearSoldier {
	 
	public NavMeshAgent _agent;
	public Vector3 _currentPosition;
	public GameObject _target;

	public Vector3 _targetLastPosition;

	public bool _targetInSight;
	public bool _targetInRange;
	public bool _active;
	public bool _hasTarget;
	public List<BearSoldier> _playerSquad;

	// Use this for initialization
	void Start () 
	{
		_currentPosition = GetComponent<Transform>;
		_target = null;

		_targetInSight = false;
		_targetInRange = false;
		_active = false;
	}

	// Update is called once per frame
	void Update () 
	{
		//behaviour checks
		if (_active) 
		{
			FindTarget ();
			if(_hasTarget)
			{

			}
			else
			{
				//stay active, wait (unlikely event)
				IdlePatrol();
			}
		}
	}

	void CheckForLoS(GameObject target)
	{
		//get direction/distance from soldier to target
		Vector3 _direction = (target.transform.position - _currentPosition).normalized;
		Vector3 _distanceFromTarget = target.transform.position - _currentPosition;

		if (_distanceFromTarget <= _sightRange) 
		{
			//check if has direct line to target, needs to cast for walls
			if (Physics.Raycast (_currentPosition, _direction, _distanceFromTarget)) 
			{
				_targetInSight = false;
			} else 
				_targetInSight = true;
		} 
		else
			_targetInSight = false;
	}

	void FindTarget()
	{
		//loop through player soldiers, check for LOS;
		//if LOS found, find nearest, set nearest as target;
	}

	void Shoot()
	{

	}

	void Seek()
	{

	}

	void IdlePatrol()
	{

	}

	void TrackTargetPosition()
	{
		if(_targetInSight)
			_targetLastPosition = _target.transform.position;
	}
}
