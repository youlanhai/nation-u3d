using UnityEngine;
using System.Collections;

namespace mygame
{
	public class GameMgr
	{
		public static GameMgr instance = new GameMgr();

		public Rect		gameView_;

		string 			loadingLevelName_ = "main_scene";
		
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

		public void setGameView(Rect rc)
		{
			gameView_ = rc;



			//Camera camera = GameObject.Find("root/MainCamera").GetComponent<Camera>();
			//camera.orthographicSize = gameView_.height * 0.5f;
			//camera.aspect = gameView_.width / gameView_.height;

		}
	};
}
