using UnityEngine;
using System.Collections;

namespace mygame
{
	
	public class Level : MonoBehaviour
	{
		public float 		createInterval_ = 2.0f;
		public Rect			localGameView_;

		float 				lastCreateTime_ = 0.0f;
		
		// Use this for initialization
		void Start ()
		{
			Rect gameView = new Rect();

			Vector3 pos = transform.TransformPoint(new Vector3(localGameView_.x, localGameView_.y, 0.0f));
			gameView.x = pos.x;
			gameView.y = pos.y;
			gameView.width = localGameView_.width * transform.lossyScale.x;
			gameView.height = localGameView_.height * transform.lossyScale.y;

			GameMgr.instance.setGameView(gameView);
		}
		
		// Update is called once per frame
		void Update ()
		{
			if(Time.time - lastCreateTime_ >= createInterval_)
			{
				lastCreateTime_ = Time.time;

				Rect gameView = GameMgr.instance.gameView_;

				float x = Random.Range(gameView.xMin, gameView.xMax);
				Vector3 position = new Vector3(x, gameView.yMax, 0);

				GameObject prefab = Resources.Load("prefabs/enemy7") as GameObject;
				GameObject.Instantiate(prefab, position, Quaternion.identity);
			}
		}
	}
	
}
