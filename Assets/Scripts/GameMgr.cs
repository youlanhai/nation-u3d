using UnityEngine;
using System.Collections;

namespace mygame
{
	public class GameMgr
	{
		public static GameMgr instance = new GameMgr();

		public Rect		gameView_;
		public Player 	Player{ get; set; }
		public Level    Level{ get; set; }

		string 			loadingLevelName_ = "main_scene";

		gamedata.BulletDataMgr bulletData_;
		public gamedata.BulletDataMgr BulletData{ get{ return bulletData_; }}

		gamedata.LevelDataMgr levelData_;
		public gamedata.LevelDataMgr LevelData{ get{ return levelData_; } }


		public GameMgr()
		{
			loadBulletData("levels/bullets");
			loadLevelData("levels/level1");
		}

		bool loadBulletData(string file)
		{
			LitJson.JsonData data = JsonHelper.LoadJsonFile(file);
			if(data == null)
			{
				return false;
			}
			
			bulletData_ = new gamedata.BulletDataMgr(data);
			return true;
		}

		bool loadLevelData(string file)
		{
			LitJson.JsonData data = JsonHelper.LoadJsonFile(file);
			if(data == null)
			{
				return false;
			}

			levelData_ = new gamedata.LevelDataMgr(data);
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
