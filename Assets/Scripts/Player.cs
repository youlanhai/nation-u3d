using UnityEngine;
using System.Collections;


namespace mygame
{
	enum FlyDirection
	{
		None,
		Left,
		Right,
	};

	public class Player : MonoBehaviour
	{
		Animator 		animator_;
		FlyDirection	direction_ = FlyDirection.None;

		public float 	moveSpeed_ = 1.0f;

		void Start()
		{
			animator_ = GetComponent<Animator>();
		}

		void Update()
		{
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");

			Vector3 delta = new Vector3(horizontal, vertical, 0);
			transform.position += delta * moveSpeed_ * Time.deltaTime;

			if(horizontal > 0)
			{
				SetDirection(FlyDirection.Left);
			}
			else if(horizontal < 0)
			{
				SetDirection(FlyDirection.Right);
			}
		}

		void SetDirection(FlyDirection dir)
		{
			if(direction_ != dir)
			{
				direction_ = dir;
				if(direction_ == FlyDirection.Left)
				{
					animator_.Play("fly_left");
				}
				else if(direction_ == FlyDirection.Right)
				{
					animator_.Play("fly_right");
				}
			}
		}
	};
}
