using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace mygame
{
	public class Level : MonoBehaviour
	{
		public float 		createInterval_ = 2.0f;
		public GameObject   randomEnemyPrefab_;

		public Rect			localGameView_;

		float 				totalTime_ = 0;
		int 				waveIndex_ = -1;
		int 				aliveEnemy_ = 0;

		gamedata.LevelDataMgr levelData_;

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
			GameMgr.instance.Level = this;
		}

		void Start()
		{
			levelData_ = GameMgr.instance.LevelData;
		}
		
		// Update is called once per frame
		void Update ()
		{
			totalTime_ += Time.deltaTime;

			if(aliveEnemy_ <= 0)
			{
				++waveIndex_;

				if(waveIndex_ >= levelData_.Count)
				{
					// game succed
				}
				else
				{
					gamedata.LevelWaveData waveData = levelData_.getWaveData(waveIndex_);
					aliveEnemy_ += waveData.datas.Length;

					foreach(gamedata.LevelData data in waveData.datas)
					{
						createEntity(data);
					}
				}
			}
		}
		
		void createEntity(gamedata.LevelData data)
		{
			Rect rect = GameMgr.instance.gameView_;
			float startX = rect.x + rect.width * 0.5f;
			float startY = rect.yMax;

			GameObject prefab = Resources.Load<GameObject>("prefabs/plane/" + data.prefab);
			Vector3 position = new Vector3(startX + data.position.x, startY + data.position.y, 0.0f);
			Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f + data.rotation);
			
			print ("enity position: " + position + " rotation:" + rotation);
			
			GameObject obj = Instantiate(prefab, position, rotation) as GameObject;
			Enemy ent = obj.GetComponent<Enemy>();
			ent.WaveIndex = waveIndex_;
			ent.bulletID_ = data.bullet;

			Rigidbody2D rigidbody = ent.GetComponent<Rigidbody2D> ();
			rigidbody.velocity = ent.transform.up * data.velocity;
			rigidbody.AddForce(ent.transform.up * data.accelerate);
		}

		public void onEnemyDead(Enemy ent)
		{
			if(ent.WaveIndex == waveIndex_)
			{
				--aliveEnemy_;
			}
		}
	}
}
