using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusScript : MonoBehaviour {

	Text	labBomb_;
	Text	labHP_;
	Text	labScore_;
	Text 	labNextNeeded_;
	Text	labLvl;

	// Use this for initialization
	void Start ()
	{
		labBomb_ = transform.FindChild("labBomb").GetComponent<Text>();
		labHP_ = transform.FindChild("labHP").GetComponent<Text>();
		labScore_ = transform.FindChild("labScore").GetComponent<Text>();
		labNextNeeded_ = transform.FindChild("labNextNeeded").GetComponent<Text>();
		labLvl = transform.FindChild("labLvl").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
