﻿using UnityEngine;
using System.Collections;

namespace mygame
{

	public class Bullet : Entity
	{
		// Use this for initialization
		void Start ()
		{
			GameObject.Destroy(gameObject, 3.0f);
		}
		
		// Update is called once per frame
		void Update ()
		{
			if(isAlive_)
			{
				transform.Translate(0.0f, moveSpeed_ * Time.deltaTime, 0.0f);
			}
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			//print("Bullet: trigger enter.");

			Entity target = other.GetComponent<Entity>();

			if(canIAttack(target))
			{
				target.setHP(target.hp_ - attack_);

				Object obj = Resources.Load("prefabs/explose3");
				GameObject.Instantiate (obj, transform.position, Quaternion.identity);

				Destroy(gameObject);
			}
		}
		
		void OnTriggerExit2D(Collider2D other)
		{
			//print("Bullet: trigger exit.");
		}
	}

}
