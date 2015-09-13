using UnityEngine;
using System.Collections;

namespace mygame
{

	public class Enemy : Entity
	{
		// Use this for initialization
		void Start ()
		{
			GameObject.Destroy(gameObject, 5.0f);
		}
		
		// Update is called once per frame
		void Update ()
		{
			if(isAlive_)
			{
				transform.Translate(0.0f, moveSpeed_ * Time.deltaTime, 0.0f);
			}
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			print ("Enemy: collision enter.");
		}
		
		void OnCollisionExit2D(Collision2D collision)
		{
			print ("Enemy: collision exit.");
		}
		
		void OnTriggerEnter2D(Collider2D other)
		{
			print("Enemy: trigger enter.");
		}
		
		void OnTriggerExit2D(Collider2D other)
		{
			print("Enemy: trigger exit.");
		}

		public override void onDead()
		{
			Object prefab = Resources.Load("prefabs/explose1");
			GameObject.Instantiate (prefab, transform.position, Quaternion.identity);
			
			GameObject.Destroy(gameObject);
		}
	}

}
