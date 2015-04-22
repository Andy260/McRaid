using UnityEngine;
using System.Collections;

public class BearSoldier : MonoBehaviour 
{
	//base stats;
	public float _health;
	public float _speed;
	public float _evasion;
	public float _armour;
	public float _accuracy;
	public float _crits;
	public float _ammoRemaining;
	public float _reloadTimer;

	//range stats
	public float _sightRange;




	// Use this for initialization
	void Start () 
	{
		//_ammoRemaining = gun._ammoCapacity
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton(0))
		{
			RaycastHit hit;
			Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
			
			if (Physics.Raycast(ray, out hit))
			{
				Debug.Log("Hit: " + hit.transform.name);
				if (hit.transform.name == "Ground")
				{
					Debug.DrawLine(Camera.main.transform.position, hit.point);
				}
			}                   
		}
	}

	void DamageEnemy(/*enemy*/)
	{
		//open to change by design
		//enemy._health -= weapon._damage - enemy._armour;
	}

	void Move(Vector3 movePosition)
	{

	}

	void ShootEnemy(/*enemy*/)
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

	void Reload(/*gun*/)
	{
		//reloading == 1

		//reloadTimer == gun._reloadTime
		//reloadTimer -= dt

		//when reloadTimer == 0
		//_ammoRemaining == gun._ammoCapacity
		//reloading == 0
	}

}
