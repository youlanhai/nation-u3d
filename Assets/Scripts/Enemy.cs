using UnityEngine;
using System.Collections;

namespace mygame
{

	public class Enemy : Combat
	{
		// Use this for initialization
		void Start ()
		{
			animator_ = GetComponent<Animator>();

			Transform t = transform.FindChild("fireAudio");
			if(t != null)
			{
				fireAudio_ = t.GetComponent<AudioSource>();
			}

			GameObject.Destroy(gameObject, 5.0f);
		}
		
		// Update is called once per frame
		void Update ()
		{
			if(isAlive_)
			{
				transform.Translate(0.0f, moveSpeed_ * Time.deltaTime, 0.0f);
				Fire();
			}
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			print("Enemy: trigger enter.");
		}
		
		void OnTriggerExit2D(Collider2D other)
		{
			print("Enemy: trigger exit.");
		}

		protected override void onDead()
		{
			base.onDead();
			GameObject.Destroy(gameObject, 1.0f);
		}
	}

}
