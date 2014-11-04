using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;

namespace ORM {
	public class Model<T> where T : ORM.Model<T>, new() {
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public static bool editMode = false;

		public Model() { }

		private static ISQLiteConnection GetDatabaseConnection() {
			if (editMode) {
				ShipDatabase db = new ShipDatabase();
				return db.GetConnection();
			} else {
				return DataService.GetConnection();
			}
		}

		public static void Reset() {
			GetDatabaseConnection().DropTable<T> ();
			GetDatabaseConnection().CreateTable<T> ();
		}

		public int Save() {
			if (Id == 0) {
				return GetDatabaseConnection().Insert(this);
			} else {
				return GetDatabaseConnection().Update(this);
			}
		}

		public int Destroy() {
			return GetDatabaseConnection ().Delete<T> (this.Id);
		}

		public T FindById(int id) {
			return GetDatabaseConnection().Find<T>(id);
		}

		public static IEnumerable<T> All() {
			return GetDatabaseConnection ().Table<T> ();
		}
	}
}
