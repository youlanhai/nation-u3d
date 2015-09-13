using UnityEngine;
using System.Collections;

namespace mygame
{

	public class Bullet : Combat
	{
		Combat		owner_;

		public void setOwner(Combat owner)
		{
			owner_ = owner;

			camp_ = owner_.camp_;
			lvl_ = owner_.lvl_;
			attack_ = owner_.attack_;
		}

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

			Combat target = other.GetComponent<Combat>();

			if(canIAttack(target))
			{
				target.impact(owner_, -attack_);

				Object obj = Resources.Load("prefabs/explose/explose3");
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
