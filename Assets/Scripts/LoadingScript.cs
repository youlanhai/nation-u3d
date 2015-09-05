using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace mygame
{
	public class LoadingScript : MonoBehaviour
	{

		Slider 			slider_;
		AsyncOperation 	loadingStatus_;

		// Use this for initialization
		void Start ()
		{
			slider_ = transform.FindChild("progress").GetComponent<Slider>();
			slider_.value = 0.0f;

			StartCoroutine(LoadLevel());
		}

		IEnumerator LoadLevel()
		{
			string name = GameMgr.instance.getLoadingLevel();
			if(name != "")
			{
				print ("LoadLevel " + name);

				loadingStatus_ = Application.LoadLevelAsync(name);
				yield return loadingStatus_;

				GameMgr.instance.onLevelLoadFinish(name);
			}
		}
		
		// Update is called once per frame
		void Update ()
		{
			if(loadingStatus_ != null)
			{
				slider_.value = loadingStatus_.progress;
				//print ("Loading step " + loadingStatus_.progress);
			}
		}
	}

}