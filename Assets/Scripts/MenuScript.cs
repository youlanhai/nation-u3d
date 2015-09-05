using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		Button btnStart = transform.FindChild("btnStart").GetComponent<Button>();
		btnStart.onClick.AddListener(OnBtnStart);

		Button btnExit = transform.FindChild("btnExit").GetComponent<Button>();
		btnExit.onClick.AddListener(OnBtnExit);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnBtnStart()
	{

	}

	void OnBtnExit()
	{
		Application.Quit();
	}
}
