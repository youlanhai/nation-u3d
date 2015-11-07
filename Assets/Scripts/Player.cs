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

	public class Player : Combat
	{

		FlyDirection	direction_ = FlyDirection.None;


		StatusScript	status_;
		int 			numBomb_ = 0;
		int 			nextNeededScore_ = 1000;

		public GameObject	lvlUpEffectPrefab_;

		Rigidbody2D 	rigidbody_;

		void Start()
		{
			print ("Player start.");
			animator_ = GetComponent<Animator>();
			fireAudio_ = transform.FindChild("fireAudio").GetComponent<AudioSource>();

			status_ = GameObject.Find("Canvas/status").GetComponent<StatusScript>();
//			status_.setLvl(lvl_);
//			status_.setScore(score_);
//			status_.setBomb(numBomb_);
//			status_.setHP(hp_);
//			status_.setScoreNextNeed(nextNeededScore_);

			rigidbody_ = GetComponent<Rigidbody2D> ();
		}

		void FixedUpdate()
		{
			if(!isAlive_)
			{
				return;
			}

			Vector2 delta = new Vector2 (0.0f, 0.0f);
			if(Application.platform == RuntimePlatform.Android ||
			   Application.platform == RuntimePlatform.IPhonePlayer)
			{
				if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					delta = Input.GetTouch(0).deltaPosition;
				}
			}
			else
			{
				float horizontal = Input.GetAxis("Horizontal");
				float vertical = Input.GetAxis("Vertical");
				
				delta = new Vector2(horizontal, vertical);
			}

			rigidbody_.velocity = delta * moveSpeed_;

			Rect rect = GameMgr.instance.gameView_;
			rigidbody_.position = new Vector2 (
				Mathf.Clamp(rigidbody_.position.x, rect.xMin, rect.xMax),
				Mathf.Clamp(rigidbody_.position.y, rect.yMin, rect.yMax)
				);

//			if(horizontal > 0)
//			{
//				SetDirection(FlyDirection.Left);
//			}
//			else if(horizontal < 0)
//			{
//				SetDirection(FlyDirection.Right);
//			}

			Fire();
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

		void OnTriggerEnter2D(Collider2D other)
		{
			print("Player: trigger enter.");
		}

		void OnTriggerExit2D(Collider2D other)
		{
			print("Player: trigger exit.");
		}

		public override void acquireScore(int score)
		{
			base.acquireScore(score);
			
			if(score_ >= nextNeededScore_)
			{
				onLvlUp();
			}
			else
			{
				status_.setScore(score_);
			}
		}

		void onLvlUp()
		{
			score_ -= nextNeededScore_;
			nextNeededScore_ *= 2;

			lvl_ += 1;
			attack_ += 10;
			defence_ += 5;
			numBomb_ += 1;
			
			status_.setLvl(lvl_);
			status_.setScoreNextNeed(nextNeededScore_);
			status_.setBomb(numBomb_);

			playLvlUpEffect();
		}

		void playLvlUpEffect()
		{
			Instantiate (lvlUpEffectPrefab_, transform.position, Quaternion.identity);
		}

		public override void setHP(int hp)
		{
			base.setHP(hp);
			status_.setHP(hp_);
		}
	};
}
