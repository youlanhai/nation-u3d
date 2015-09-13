using UnityEngine;
using System.Collections;

namespace mygame
{
	
	public class Combat : Entity
	{
		public int 		score_ = 0;

		public int  getScore() { return score_; }
		public void setScore(int score){ score_ = score; }
		public virtual void acquireScore(int score)
		{
			score_ += score;
		}

		public void impact(Combat src, int hpDelta)
		{
			setHP(hp_ + hpDelta);
			if(!isAlive_)
			{
				src.acquireScore(score_);
			}
		}
	}
	
}
