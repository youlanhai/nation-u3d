using UnityEngine;
using System.Collections;

namespace mygame
{
	
	public class Combat : Entity
	{
		public int 			score_ = 0;
		public float 		fireCD_ = 0.5f;

		public GameObject	bulletPrefab_;
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
			if(Time.time - lastFireTime_ >= fireCD_)
			{
				lastFireTime_ = Time.time;
				
				GameObject bullet = Instantiate(bulletPrefab_, transform.position, transform.rotation) as GameObject;
				Bullet ent = bullet.GetComponent<Bullet>();
				ent.setOwner(this);

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
