using UnityEngine;
using System.Collections;

namespace mygame
{
	public class Entity : MonoBehaviour
	{
		public enum Camp
		{
			Attack,
			Defence,
			Peace
		}

		public enum Relation
		{
			Peace,
			Good,
			Bad,
		}

		public float 	moveSpeed_ = 2.0f;
		public bool 	destroyable_ = false;
		public Camp 	camp_ = Camp.Peace;
		public int 		hp_ = 1000;
		public int 		attack_ = 500;
		public int 		defence_ = 100;
		public int 		eno_ = 0;
		public int 		lvl_ = 0;

		public bool		isAlive_ = true;

		public Relation whatRelation(Entity target)
		{
			if(target.camp_ == Camp.Peace ||
			   camp_ == Camp.Peace)
			{
				return Relation.Peace;
			}

			if(target.camp_ == camp_)
			{
				return Relation.Good;
			}
			else
			{
				return Relation.Bad;
			}
		}

		public virtual bool canIAttack(Entity target)
		{
			return target.isAlive_ && (whatRelation(target) == Relation.Bad);
		}

		public virtual void setHP(int hp)
		{
			if(isAlive_)
			{
				hp_ = Mathf.Max(hp, 0);
				if(hp_ == 0)
				{
					setAlive(false);
				}
			}
		}
		
		public void setAlive(bool alive)
		{
			isAlive_ = alive;
			if(isAlive_)
			{
				onAlive();
			}
			else
			{
				onDead();
			}
		}
		
		protected virtual void onDead()
		{
			print ("onDead: " + gameObject.name);
		}

		protected virtual void onAlive()
		{
		}
	}
	
}
