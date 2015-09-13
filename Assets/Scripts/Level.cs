using UnityEngine;
using System.Collections;

namespace mygame
{
	
	public class Level : MonoBehaviour
	{
		public float 		createInterval_ = 2.0f;
		public Rect			gameView_;

		float 				lastCreateTime_ = 0.0f;
		
		// Use this for initialization
		void Start ()
		{
			GameMgr.instance.setGameView(gameView_);
		}
		
		// Update is called once per frame
		void Update ()
		{
			if(Time.time - lastCreateTime_ >= createInterval_)
			{
				lastCreateTime_ = Time.time;

				float x = Random.Range(gameView_.xMin, gameView_.xMax);
				Vector3 position = new Vector3(x, gameView_.yMax, 0);

				position = transform.TransformPoint(position);
				position.z = 0.0f;

				GameObject prefab = Resources.Load("prefabs/enemy7") as GameObject;
				GameObject.Instantiate(prefab, position, Quaternion.identity);
			}
		}
	}
	
}
