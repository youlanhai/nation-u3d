using UnityEngine;
using System.Collections;

namespace mygame
{
	
	public class Level : MonoBehaviour
	{
		public float 		createInterval_ = 2.0f;

		float 				lastCreateTime_ = 0.0f;

		public Vector3 		monsterCenter_ = new Vector3(0.0f, 5.0f, 0.0f);
		public float 		monsterRange_ = 2.0f;
		
		// Use this for initialization
		void Start ()
		{

		}
		
		// Update is called once per frame
		void Update ()
		{
			if(Time.time - lastCreateTime_ >= createInterval_)
			{
				lastCreateTime_ = Time.time;

				float x = Random.Range(-monsterRange_, monsterRange_);
				Vector3 position = new Vector3(x, monsterCenter_.y, monsterCenter_.z);

				GameObject prefab = Resources.Load("prefabs/enemy7") as GameObject;
				GameObject.Instantiate(prefab, position, Quaternion.identity);
			}
		}
	}
	
}
