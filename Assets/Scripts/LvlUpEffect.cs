using UnityEngine;
using System.Collections;

namespace mygame
{
	public class LvlUpEffect : MonoBehaviour
	{
		// Use this for initialization
		void Start ()
		{
		}
		
		// Update is called once per frame
		void Update ()
		{
		}
		
		void onAnimationEnd()
		{
			Destroy(gameObject);
		}

		void onPlaySound()
		{
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
		}
	}
}
