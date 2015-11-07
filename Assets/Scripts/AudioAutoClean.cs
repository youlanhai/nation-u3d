using UnityEngine;
using System.Collections;

namespace mygame
{

	public class AudioAutoClean : MonoBehaviour
	{
		// Use this for initialization
		void Start ()
		{
			AudioSource audio = GetComponent<AudioSource>();
			Destroy(gameObject, audio.clip.length + 0.1f);
		}
	}

}
