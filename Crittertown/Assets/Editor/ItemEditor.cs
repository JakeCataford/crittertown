using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class ItemEditor : EditorWindow {
	private Vector2 sidebarScrollPosition = Vector2.zero;
	private Vector2 mainAreaScroll = Vector2.zero;
	private int selectedItem;

	private List<ORM.Item> items = new List<ORM.Item>();

	[MenuItem ("Crittertown/Item Editor")]
	public static void  ShowWindow () {
		EditorWindow window = EditorWindow.GetWindow(typeof(ItemEditor));
		window.name = "Item Editor";
	}

	void OnFocus() {
		ReloadItems ();
	}

	void OnGUI () {
		if (selectedItem >= items.Count) selectedItem = 0;
		EditorGUILayout.BeginHorizontal(GUILayout.Width(position.width), GUILayout.Height(position.height));
		//SIDEBAR
		EditorGUILayout.BeginVertical (GUILayout.Width (position.width / 4f), GUILayout.Height (position.height));
		sidebarScrollPosition = EditorGUILayout.BeginScrollView (sidebarScrollPosition, GUILayout.Width (position.width / 4f), GUILayout.Height (position.height - 30f));
		List<string> names = new List<string> ();

		foreach(ORM.Item item in items) {
			names.Add(item.Title);
		}

		selectedItem = GUILayout.SelectionGrid (selectedItem, names.ToArray(), 1);
		EditorGUILayout.EndScrollView ();
		EditorGUILayout.BeginHorizontal (GUILayout.Width (position.width / 4f), GUILayout.Height(30f));
		if (GUILayout.Button ("+")) {
			NewItem();
		}
		if (GUILayout.Button ("-")) {
			DeleteItem();
		}
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.EndVertical ();

		//MAIN
		mainAreaScroll = EditorGUILayout.BeginScrollView (mainAreaScroll, GUILayout.Width (position.width * 0.75f), GUILayout.Height (position.height));
		if(items.Count > 0) {
			EditorGUILayout.BeginHorizontal ();
			items[selectedItem].Title = GUILayout.TextField (items[selectedItem].Title);
			items[selectedItem].Price = EditorGUILayout.IntField ("Price", items[selectedItem].Price, GUILayout.MaxWidth(200f));
			EditorGUILayout.EndHorizontal ();
			items[selectedItem].Description = GUILayout.TextArea (items[selectedItem].Description, GUILayout.Height (300f));
			EditorGUILayout.BeginVertical (GUILayout.MaxWidth (150f));
			items[selectedItem].editPrefab = (GameObject) EditorGUILayout.ObjectField ("Prefab", items[selectedItem].editPrefab, typeof(GameObject), false);
			if(items[selectedItem].editPrefab != null) items[selectedItem].Prefab = GetResourcePath(items[selectedItem].editPrefab);
			items[selectedItem].editTexture = (Texture2D) EditorGUILayout.ObjectField ("Preview Image", items[selectedItem].editTexture, typeof(Texture2D), false);
			if(items[selectedItem].editTexture != null) items[selectedItem].Preview = GetResourcePath(items[selectedItem].editTexture);
			items[selectedItem].Save ();
			EditorGUILayout.EndVertical ();
		} else {
			GUILayout.Label("No item selected. Select or create an item on the left to begin.");
		}

		EditorGUILayout.EndScrollView ();
		EditorGUILayout.EndHorizontal ();
	}

	private void ReloadItems() {
		ORM.Item.editMode = true;
		items.Clear ();
		foreach(ORM.Item item in ORM.Item.All()) {
			if(item.Prefab != null) item.editPrefab = Resources.Load<GameObject>(item.Prefab);
			if(item.Preview != null) item.editTexture = Resources.Load<Texture2D>(item.Preview);
			items.Add(item);
		}
	}

	private void NewItem() {
		ORM.Item item = new ORM.Item("New Item", "Description", "", "", 200);
		item.Save ();
		ReloadItems ();
	}

	private void DeleteItem() {
		items [selectedItem].Destroy ();
		ReloadItems ();
	}

	private string GetResourcePath(UnityEngine.Object prefab) {
		string path = AssetDatabase.GetAssetPath (prefab);
		Match resourceTrimmedPath = Regex.Match(path, @"Resources\/(.*)\.");
		return resourceTrimmedPath.Groups[1].Value;
	}
}
