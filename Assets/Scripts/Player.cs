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

	public class Player : Entity
	{
		Animator 		animator_;
		AudioSource		fireAudio_;

		FlyDirection	direction_ = FlyDirection.None;

		public float 	fireCD_ = 0.5f;

		float 			lastFireTime_ = 0.0f;

		void Start()
		{
			animator_ = GetComponent<Animator>();

			fireAudio_ = GameObject.Find("MainCamera/fireAudio").gameObject.GetComponent<AudioSource>();
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

			if(Input.GetButton("Fire1"))
			{
				Fire();
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

		void Fire()
		{
			if(Time.time - lastFireTime_ >= fireCD_)
			{
				lastFireTime_ = Time.time;
				GameObject prefab = Resources.Load<GameObject>("prefabs/bullet2");

				GameObject bullet = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
				Entity ent = bullet.GetComponent<Entity>();
				ent.camp_ = camp_;

				fireAudio_.Play();
				print ("Player: fire");
			}
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			print ("Player: collision enter.");
		}

		void OnCollisionExit2D(Collision2D collision)
		{
			print ("Player: collision exit.");
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			print("Player: trigger enter.");
		}

		void OnTriggerExit2D(Collider2D other)
		{
			print("Player: trigger exit.");
		}
	};
}
