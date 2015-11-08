using UnityEngine;
using System.Collections;

namespace mygame
{
	
	public class Combat : Entity
	{
		public int 			score_ = 0;
		public float 		fireCD_ = 0.5f;

		public int			bulletID_ = 0;
		public GameObject 	explosePrefab_;
		
		protected float 			lastFireTime_ = 0.0f;
		protected Animator 			animator_;
		protected AudioSource		fireAudio_;

		public int  getScore() { return score_; }
		public void setScore(int score){ score_ = score; }
		public virtual void acquireScore(int score)
		{
			score_ += score;
		}

		public override bool isAttackable()
		{
			return true;
		}

		public void impact(Combat src, int hpDelta)
		{
			setHP(hp_ + hpDelta);
			if(!isAlive_)
			{
				src.acquireScore(score_);
			}
		}

		
		public void Fire()
		{
			if(bulletID_ > 0 && Time.time - lastFireTime_ >= fireCD_)
			{
				lastFireTime_ = Time.time;

				gamedata.BulletEmitter emiter = GameMgr.instance.BulletData.bullets[bulletID_];

				foreach(gamedata.BulletData data in emiter.items)
				{
					GameObject prefab = Resources.Load<GameObject>("prefabs/bullet/" + data.prefab);
					Vector3 position = transform.position + new Vector3(data.position.x, data.position.y, 0.0f);
					Quaternion rotation = transform.rotation * Quaternion.Euler(0.0f, 0.0f, data.rotation);

					GameObject bullet = Instantiate(prefab, position, rotation) as GameObject;
					Bullet ent = bullet.GetComponent<Bullet>();
					ent.init(this, data);
				}

				if(fireAudio_ != null)
				{
					fireAudio_.Play();
				}
			}
		}

		
		protected override void onDead()
		{
			if(explosePrefab_ != null)
			{
				GameObject.Instantiate(explosePrefab_, transform.position, Quaternion.identity);
			}
			
			gameObject.SetActive(false);
		}
	}
}
