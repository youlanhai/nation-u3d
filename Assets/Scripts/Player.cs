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

		int 			numBomb_ = 0;
		int 			nextNeededScore_ = 1000;

		public GameObject	lvlUpEffectPrefab_;
		public GameObject	bornEffectPrefab_;

		Rigidbody2D 	rigidbody_;
		bool 			hasBorn_ = false;

		float 			invincibleTime_ = 0.0f;

		void Awake()
		{
			GameMgr.instance.Player = this;
			
			animator_ = GetComponent<Animator>();
			fireAudio_ = GetComponent<AudioSource>();
			rigidbody_ = GetComponent<Rigidbody2D> ();
		}

		void Start()
		{
			print ("Player start.");

			StatusScript statusPanel = GameCanvas.instance.statusPanel;
			statusPanel.setLvl(lvl_);
			statusPanel.setScore(score_);
			statusPanel.setBomb(numBomb_);
			statusPanel.setHP(hp_);
			statusPanel.setScoreNextNeed(nextNeededScore_);

			playBornEffect();
		}

		void FixedUpdate()
		{
			if(!isAlive_)
			{
				return;
			}

			if(!hasBorn_)
			{
				tickBorn();
				return;
			}

			tickMotion();
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

		public override void acquireScore(int score)
		{
			base.acquireScore(score);
			
			if(score_ >= nextNeededScore_)
			{
				onLvlUp();
			}
			else
			{
				GameCanvas.instance.statusPanel.setScore(score_);
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

			GameCanvas.instance.statusPanel.setLvl(lvl_);
			GameCanvas.instance.statusPanel.setScoreNextNeed(nextNeededScore_);
			GameCanvas.instance.statusPanel.setBomb(numBomb_);

			playLvlUpEffect();
		}

		void playLvlUpEffect()
		{
			Instantiate (lvlUpEffectPrefab_, transform.position, Quaternion.identity);
		}

		void playBornEffect()
		{
			Rect rect = GameMgr.instance.gameView_;
			rigidbody_.position = new Vector2(rect.center.x, rect.yMin);
			rigidbody_.velocity = new Vector2(0.0f, 3.0f);

			Instantiate(bornEffectPrefab_, transform.position, transform.rotation);
		}

		void tickBorn()
		{
			Rect rect = GameMgr.instance.gameView_;
			if(rigidbody_.position.y >= rect.yMin + 2.0f)
			{
				hasBorn_ = true;
				invincibleTime_ += 3.0f;
			}
		}

		void tickMotion()
		{
			if(invincibleTime_ > 0.0f)
			{
				invincibleTime_ = Mathf.Max(invincibleTime_ - Time.fixedDeltaTime, 0.0f);
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
			
			Fire();
		}

	 	public override bool isAttackable()
		{
			return hasBorn_ && !(invincibleTime_ > 0.0f);
		}

		public override void setHP(int hp)
		{
			base.setHP(hp);
			GameCanvas.instance.statusPanel.setHP(hp_);
		}

		protected override void onDead()
		{
			base.onDead();

			hasBorn_ = false;
			GameCanvas.instance.rebornPanel.gameObject.SetActive(true);
		}

		public void reborn()
		{
			if(isAlive_)
			{
				return;
			}

			gameObject.SetActive(true);
			setHP(hpMax_);
			setAlive(true);

			playBornEffect();
		}
	};
}
