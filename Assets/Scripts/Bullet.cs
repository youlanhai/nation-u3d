using UnityEngine;
using System.Collections;

namespace mygame
{

	public class Bullet : MonoBehaviour
	{

		public float moveSpeed_ = 1.0f;

		// Use this for initialization
		void Start ()
		{
			GameObject.Destroy(gameObject, 3.0f);
			moveSpeed_ = 2.0f;
		}
		
		// Update is called once per frame
		void Update ()
		{
			transform.Translate(0.0f, moveSpeed_ * Time.deltaTime, 0.0f);
		}

		void OnTriggerEnter (Collider other)
		{
			print ("collid");
		}
	}

}
