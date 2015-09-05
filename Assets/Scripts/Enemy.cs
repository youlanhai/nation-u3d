using UnityEngine;
using System.Collections;

namespace mygame
{

	public class Enemy : MonoBehaviour
	{
		float 	moveSpeed_ = -2.0f;

		// Use this for initialization
		void Start ()
		{
			GameObject.Destroy(gameObject, 5.0f);
		}
		
		// Update is called once per frame
		void Update ()
		{
			transform.Translate(0.0f, moveSpeed_ * Time.deltaTime, 0.0f);
		}
	}

}
