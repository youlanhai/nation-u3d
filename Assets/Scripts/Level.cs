using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace mygame
{
	[System.Serializable]
	public struct LevelNode
	{
		public float 		time;
		public GameObject 	entityPrefab;
		public Vector2 		position;
	}
	
	public class Level : MonoBehaviour
	{
		public float 		createInterval_ = 2.0f;
		public GameObject   randomEnemyPrefab_;

		public Rect			localGameView_;
		public LevelNode[] 	levelNodes_;

		float 				lastCreateTime_ = 0.0f;
		float 				totalTime_ = 0;
		int 				waveIndex_ = 0;

		// Use this for initialization
		void Awake ()
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
			totalTime_ += Time.deltaTime;
			if(waveIndex_ < levelNodes_.Length)
			{
				while(waveIndex_ < levelNodes_.Length &&
				      totalTime_ >= levelNodes_[waveIndex_].time)
				{
					createEntity(levelNodes_[waveIndex_++]);
				}
			}

			if(Time.time - lastCreateTime_ >= createInterval_)
			{
				lastCreateTime_ = Time.time;

				LevelNode node = new LevelNode();
				
				float x = Random.Range(localGameView_.xMin, localGameView_.xMax);
				node.time = totalTime_;
				node.position = new Vector3(x, localGameView_.yMax, 0);
				node.entityPrefab = randomEnemyPrefab_;

				createEntity(node);
			}
		}
		
		void createEntity(LevelNode node)
		{
			Vector3 position = new Vector3(node.position.x, node.position.y, 0.0f);
			position = transform.TransformPoint(position);

			Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);

			GameObject.Instantiate(node.entityPrefab, position, rotation);
		}
	}
}
