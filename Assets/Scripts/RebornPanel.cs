using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace mygame
{

	public class RebornPanel : MonoBehaviour
	{

		// Use this for initialization
		void Start ()
		{
			Button btnReborn = transform.FindChild("btnReborn").gameObject.GetComponent<Button>();
			btnReborn.onClick.AddListener(this.onBtnReborn);
		
			Button btnExit = transform.FindChild("btnExit").gameObject.GetComponent<Button>();
			btnExit.onClick.AddListener(this.onBtnExit);
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}

		void onBtnReborn()
		{
			gameObject.SetActive(false);

			GameMgr.instance.Player.reborn();
		}

		void onBtnExit()
		{
			gameObject.SetActive(false);

			Application.LoadLevel("entry");
		}
	}

}
