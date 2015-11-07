using UnityEngine;
using System.Collections;

namespace mygame
{

	public class GameCanvas : MonoBehaviour
	{
		public static GameCanvas 	instance;

		public RebornPanel		rebornPanel;
		public StatusScript  	statusPanel;

		// Use this for initialization
		void Awake ()
		{
			print ("GameCanvas awake.");
			instance = this;
		}
	}
}
