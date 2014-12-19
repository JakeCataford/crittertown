using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CactusGarden : SVBLM.Core.Singleton<CactusGarden> {
	public List<CactusSpot> spots = new List<CactusSpot>();

	public IEnumerator Start() {
		while(!DataService.IsReady()) {
			yield return null;
		}

		ReloadFromDB ();
		foreach (CactusSpot spot in spots) {
			spot.init();
		}
	}

	public static void ReloadFromDB() {
		Instance.spots.Clear ();
		IEnumerable<CactusSpot> bons = CactusSpot.All ();
		foreach (CactusSpot spot in bons) {
			Instance.spots.Add(spot);
		}
	}

	public static Cactus GetMyCactus(int critterId) {
		CactusSpot cs = Instance.spots.Find (x => x.Owner == critterId);
		if (cs != null) {
			return cs.CactusObject;
		}

		Cactus cactus = NextAvailableSpot ();
		cactus.spot.Owner = critterId;
		cactus.spot.Save ();
		return cactus;
	}

	public static Cactus NextAvailableSpot() {
		CactusSpot cactus = Instance.spots.Find (x => x.Owner == 0);
		if (cactus == null) {
			return null;
		}

		return cactus.CactusObject;
	}
	

}
