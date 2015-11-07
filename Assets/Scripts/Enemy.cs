using UnityEngine;
using System.Collections;

namespace mygame
{

	public class Enemy : Combat
	{
		bool 	isActive_ = false;

		// Use this for initialization
		void Start ()
		{
			animator_ = GetComponent<Animator>();

			Transform t = transform.FindChild("fireAudio");
			if(t != null)
			{
				fireAudio_ = t.GetComponent<AudioSource>();
			}

			GetComponent<Rigidbody2D> ().velocity = transform.up * moveSpeed_;
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
						isActive_ = false;
						isAlive_ = false;
						Destroy(gameObject, 0.1f);
					}
				}

				if(isActive_)
				{
					Fire();
				}
			}
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			Player player = other.gameObject.GetComponent<Player>();
			if(player != null && canIAttack(player))
			{
				player.impact(this, -attack_);
			}
		}

		protected override void onDead()
		{
			base.onDead();
			GameObject.Destroy(gameObject, 1.0f);
		}
	}

}
