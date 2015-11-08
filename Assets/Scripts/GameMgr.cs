using UnityEngine;
using System.Collections;

namespace mygame
{
	public class GameMgr
	{
		public static GameMgr instance = new GameMgr();

		public Rect		gameView_;

		public Player 	player_;

		public Level    Level{ get; set; }

		string 			loadingLevelName_ = "main_scene";

		gamedata.BulletDataMgr bulletData_;
		public gamedata.BulletDataMgr BulletData{ get{ return bulletData_; }}

		public GameMgr()
		{
			loadBulletData("levels/bullets");
		}

		bool loadBulletData(string file)
		{
			TextAsset textAsset = Resources.Load<TextAsset>(file);
			if(textAsset == null)
			{
				Debug.LogError("Failed to load file " + file);
				return false;
			}

			LitJson.JsonData data;
			try
			{
				data = LitJson.JsonMapper.ToObject(textAsset.text);
			}
			catch(System.Exception e)
			{
				Debug.LogError("Failed to parse json file " + file + ", exception: " + e.ToString());
				return false;
			}
			
			bulletData_ = new gamedata.BulletDataMgr(data);
			return true;
		}
		
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



//			Camera camera = GameObject.Find("root/MainCamera").GetComponent<Camera>();
//			float aspect = gameView_.width / gameView_.height;
//
//			if(camera.aspect > aspect)
//			{
//				camera.orthographicSize = gameView_.height * 0.5f;
//			}
//			else if(camera.aspect < aspect)
//			{
//				// 屏幕宽度太窄，有部分画面会超出屏幕。
//
//				float height = gameView_.width / camera.aspect;
//				camera.orthographicSize = height * 0.5f;
//			}
		}
	};
}
