using UnityEngine;
using System.Collections;

namespace mygame
{
	public class GameMgr
	{
		public static GameMgr instance = new GameMgr();


		string 		loadingLevelName_ = "main_scene";
		
		public string getLoadingLevel()
		{
			return loadingLevelName_;
		}

		public void loadLevel(string name)
		{
			loadingLevelName_ = name;
			Application.LoadLevel("loading");
		}

		public void onLevelLoadFinish(string name)
		{
			if(name == loadingLevelName_)
			{
				Debug.Log("Loading finish " + name);
				loadingLevelName_ = "";
			}
		}
	};
}
