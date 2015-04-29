using UnityEngine;
using System.Collections;

public class BearSoldier : MonoBehaviour 
{
	//base stats;
    public float _maxHealth;
	public float _currentHealth;
	public float _speed;
	public float _evasion;
	public float _armour;
	public float _accuracy;
	public float _crits;
	public float _ammoRemaining;
	public float _reloadTimer;

	//range stats
	public float _sightRange;

	//gun stats
	public float _weaponRange;
	public float _weaponAmmo;
	public float _weaponDamage;
	public float _weaponReloadTime;

    NavMeshAgent _navAgent;
    BearSoldier _target;
    Vector3 _destination;

	// Use this for initialization
	protected void Start() 
	{
		//_ammoRemaining = gun._ammoCapacity

        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.speed = _speed;
	}
	
	// Update is called once per frame
	protected void Update() 
	{
        Debug.DrawLine(transform.position, _destination);
	}

	protected void DamageEnemy(/*enemy*/)
	{
		//open to change by design
		//enemy._health -= weapon._damage - enemy._armour;
	}

	public void Move(Vector3 a_position)
	{
        _navAgent.SetDestination(a_position);
        _destination = a_position;
	}

	public void ShootEnemy(/*enemy*/)
	{
		//if enemy in range && reloading != true

		//when fireRate allows

			//fire weapon at enemy --->fire weapon animation, bullet effects, etc
			//reduce _ammoRemaining count
			
		//open to change by design
			
			//roll against accuracy - enemy._evasion
				//if succeed DamageEnemy()
			//else miss

			//if _ammoRemaining == 0
			//reload

		//else noFire
	}

	protected void Reload(/*gun*/)
	{
		//reloading == 1

		//reloadTimer == gun._reloadTime
		//reloadTimer -= dt

		//when reloadTimer == 0
		//_ammoRemaining == gun._ammoCapacity
		//reloading == 0
	}
}
