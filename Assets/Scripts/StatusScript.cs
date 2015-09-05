using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusScript : MonoBehaviour {

	Text	labBomb_;
	Text	labHP_;
	Text	labScore_;
	Text 	labNextNeeded_;
	Text	labLvl_;

	// Use this for initialization
	void Start ()
	{
		labBomb_ = transform.FindChild("labBomb").GetComponent<Text>();
		labHP_ = transform.FindChild("labHP").GetComponent<Text>();
		labScore_ = transform.FindChild("labScore").GetComponent<Text>();
		labNextNeeded_ = transform.FindChild("labNextNeeded").GetComponent<Text>();
		labLvl_ = transform.FindChild("labLvl").GetComponent<Text>();

		labBomb_.text = "0";
		labHP_.text = "100";
		labLvl_.text = "1";
		labScore_.text = "0";
		labNextNeeded_.text = "1000";
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
