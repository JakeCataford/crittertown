using UnityEngine;
using System.Collections;
using System.IO;
using SQLite4Unity3d;
using System.Collections.Generic;
using System;

public class DataService : SVBLM.Core.Singleton<DataService>  {
	private const string DATABASE_NAME = "crittertown_db";
	private ISQLiteConnection _connection;
	private bool ready = false;

	public static void Init() {
		if(!Instance.ready) Instance.StartCoroutine (Instance.InitializeDatabase ());
	}

	private IEnumerator InitializeDatabase() {
		var factory = new Factory();
		var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DATABASE_NAME);
		if (!File.Exists (filepath)) {
#if UNITY_EDITOR
			var loadDb = Application.streamingAssetsPath + "/" + DATABASE_NAME;
			File.Copy(loadDb, filepath);
#elif UNITY_ANDROID
			var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DATABASE_NAME);
			yield return loadDb;
			File.WriteAllBytes(filepath, loadDb.bytes);
#else
			var loadDb = Application.streamingAssetsPath + "/" + DATABASE_NAME;
			File.Copy(loadDb, filepath);
#endif
			CreateDB();
			UI.Toast("DB Created");
		}
		
		var dbPath = filepath;
		_connection = factory.Create(dbPath);
		ready = true;


		yield break;
	}
	
	public IEnumerator CreateDB(){
		yield return _connection.CreateTable<ORM.Item> ();
		yield return _connection.CreateTable<Critter> ();
		yield return _connection.CreateTable<ORM.WorldItem> ();
		yield return _connection.CreateTable<Inventory> ();
	}
	
	public static bool IsReady() {
		return Instance.ready;
	}

	public static ISQLiteConnection GetConnection() {
		return Instance._connection;
	}
}