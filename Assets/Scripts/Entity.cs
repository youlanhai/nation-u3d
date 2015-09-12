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

		public bool canIDestroy(Entity target)
		{
			return whatRelation(target) == Relation.Bad;
		}
	}
	
}
