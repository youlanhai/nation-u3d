using UnityEngine;
using System.Collections;

namespace mygame
{

	public class Bullet : Entity
	{
		Combat					owner_;
		bool 					isActive_ = false;

		public GameObject		explosePrefab_;

		public void init(Combat owner, gamedata.BulletData data)
		{
			owner_ = owner;

			camp_ = owner_.camp_;
			lvl_ = owner_.lvl_;
			attack_ = owner_.attack_;
			
			Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
			rigidbody.velocity = transform.up * data.velocity;
			rigidbody.AddForce(transform.up * data.accelerate);
		}

		// Use this for initialization
		void Start ()
		{
		}
		
		// Update is called once per frame
		void Update ()
		{
			if(isAlive_)
			{
				if(!isActive_)
				{
					if(GameMgr.instance.gameView_.Contains(transform.position))
					{
						isActive_ = true;
					}
				}
				else
				{
					if(!GameMgr.instance.gameView_.Contains(transform.position))
					{
						isAlive_ = false;
						isActive_ = false;
						Destroy(gameObject, 0.1f);
					}
				}
			}
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			Combat target = other.GetComponent<Combat>();

			if(target != null && canIAttack(target))
			{
				target.impact(owner_, -attack_);

				if(explosePrefab_ != null)
				{
					GameObject.Instantiate (explosePrefab_, transform.position, Quaternion.identity);
				}
				Destroy(gameObject);
			}
		}
	}

}
