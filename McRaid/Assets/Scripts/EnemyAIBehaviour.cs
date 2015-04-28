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
	public List<GameObject> _playerSquad;

	public float _weaponRange;

	// Use this for initialization
	void Start () 
	{
		_active = true;
		_agent 	= GetComponent<NavMeshAgent>();
		//_currentPosition = GetComponent<transform.position>();
		//_currentPosition = Vector3(0,0,0);
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
			//check for target
			if(_hasTarget)
			{
				//compare targets postion against weapon range and sight range
				TrackTargetPosition();
				//check for range
				if(_targetInRange)
				{
					Shoot ();
				}
				//if not in range, move to range
				else
				{
					MoveToRange ();
				}
			}
			else
			{
				FindTarget ();
			}
		}
	}

	bool CheckForLoS(GameObject target)
	{
		//get direction/distance from soldier to target
		Vector3 _direction = (target.transform.position - _currentPosition).normalized;
		float _distanceFromTarget = Vector3.Distance (target.transform.position, _currentPosition);

		if (_distanceFromTarget <= _sightRange) 
		{
			//check if has direct line to target, needs to cast for walls, not low cover(or anything else that can be seen past)
			if (Physics.Raycast (_currentPosition, _direction, _distanceFromTarget)) 
			{
				Debug.DrawRay(_currentPosition, _direction, Color.red);
				_targetInSight = false;
				return false;
			} 
			else 
			{
				_targetInSight = true;
				return true;
			}
		} 
		else
		{
			_targetInSight = false;
			return false;
		}
	}

	void FindTarget()
	{
		float _lowestDistance = Mathf.Infinity;
		//loop through player soldiers, check for LOS;
		//if LOS found, find nearest, set nearest as target;
		for (int i = 0; i < _playerSquad.Count; i++) 
		{
			CheckForLoS (_playerSquad [i]);
			if (_targetInSight) 
			{
				float _distanceFromTarget = Vector3.Distance(_playerSquad [i].transform.position, _currentPosition);
				if (_distanceFromTarget < _lowestDistance) 
				{
					_lowestDistance = _distanceFromTarget;
					_target = _playerSquad [i];
					_hasTarget = true;
				}
			}
		}
	}

	void Shoot()
	{

	}

	void MoveToRange()
	{
		//Vector3 _distanceFromTarget = _target.transform.position - _currentPosition;
		//Vector3 _positionInRange = _distanceFromTarget - _weaponRange;


		//FindPath(_positionInRange);
	}

	void Seek()
	{
		//FindPath(_targetLastPosition);
	}

	void IdlePatrol()
	{

	}

	void TrackTargetPosition()
	{
		//update targets info with position
		_targetLastPosition = _target.transform.position;
		float _distanceToTarget = Vector3.Distance (_targetLastPosition, _currentPosition);

		//if target gets out of LOS, target is lost
		if(_distanceToTarget > _sightRange)
		{
			_hasTarget = false;
		}
		else if(!CheckForLoS(_target))
		{
			_hasTarget = false;
		}

		if(_distanceToTarget <= _weaponRange)
		{
			_targetInRange = true;
		}
		else
			_targetInRange = false;


	}
}
