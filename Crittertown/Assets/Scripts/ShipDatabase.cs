using UnityEngine;
using System.Collections;
using SQLite4Unity3d;

namespace ORM {
	public class ShipDatabase {
		private const string DATABASE_NAME = "crittertown_db";
		private ISQLiteConnection _connection;

		public ShipDatabase() {
			var factory = new Factory();
			var databasePath = Application.streamingAssetsPath + "/" + DATABASE_NAME;
			_connection = factory.Create (databasePath);
			VerifyTables ();
		}

		public void VerifyTables() {
			_connection.CreateTable<Item> ();
			_connection.CreateTable<WorldItem> ();
			_connection.CreateTable<Critter> ();
			_connection.CreateTable<Inventory> ();
			_connection.CreateTable<CactusSpot>();
		}

		public ISQLiteConnection GetConnection() {
			return _connection;
		}
	}
}
