using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace mygame
{

	public class StatusScript : MonoBehaviour
	{

		Text	labBomb_;
		Text	labHP_;
		Text	labScore_;
		Text 	labNextNeeded_;
		Text	labLvl_;

		void Awake()
		{
			print ("status script awake.");
			labBomb_ = transform.FindChild("labBomb").GetComponent<Text>();
			labHP_ = transform.FindChild("labHP").GetComponent<Text>();
			labScore_ = transform.FindChild("labScore").GetComponent<Text>();
			labNextNeeded_ = transform.FindChild("labNextNeeded").GetComponent<Text>();
			labLvl_ = transform.FindChild("labLvl").GetComponent<Text>();
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}

		public void setScore(int score)
		{
			labScore_.text = score.ToString();
		}

		public void setHP(int hp)
		{
			labHP_.text = hp.ToString();
		}

		public void setBomb(int bomb)
		{
			labBomb_.text = bomb.ToString();
		}

		public void setScoreNextNeed(int score)
		{
			labNextNeeded_.text = score.ToString();
		}

		public void setLvl(int lvl)
		{
			labLvl_.text = lvl.ToString();
		}
	}

}
