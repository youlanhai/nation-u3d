using UnityEngine;
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
			transform.Translate(0.0f, moveSpeed_ * Time.deltaTime, 0.0f);
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			print("Bullet: trigger enter.");

			Entity target = other.GetComponent<Entity>();

			if(canIDestroy(target))
			{
				Object obj = Resources.Load("prefabs/explose3");
				GameObject.Instantiate (obj, transform.position, Quaternion.identity);

				target.hp_ -= attack_;
				if(target.hp_ <= 0)
				{
					Object prefab = Resources.Load("prefabs/explose2");
					GameObject.Instantiate (prefab, target.transform.position, Quaternion.identity);

					GameObject.Destroy(target.gameObject);
				}

				Destroy(gameObject);
			}
		}
		
		void OnTriggerExit2D(Collider2D other)
		{
			print("Bullet: trigger exit.");
		}
	}

}
