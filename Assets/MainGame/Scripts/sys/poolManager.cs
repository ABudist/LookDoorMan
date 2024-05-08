using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class poolItem
{
	public GameObject go;
	public int num;
}



public class poolManager : MonoBehaviour {

	public static poolManager instance;
	public List<poolItem> PoolItem;
	private List<GameObject> poolObjs;
	public bool isAuto = false;

	void Awake()
	{
		instance = this;
		poolObjs = new List<GameObject> ();
		for(int i=0;i<PoolItem.Count;i++)
		{
			for(int j=0;j<PoolItem[i].num;j++)
			{
				GameObject obj = Instantiate (PoolItem[i].go);
				obj.SetActive (false);
				obj.transform.parent = transform;
				poolObjs.Add (obj);
			}
		}		
	}

	// Use this for initialization
	void Start () {
		
	
	}


	public GameObject GetGameObject(string go, Vector3 pos)
	{
		string goName = go+"(Clone)";
		for(int i = 0; i<poolObjs.Count;i++)
		{
			if(!poolObjs[i].activeSelf  && poolObjs[i].name==goName)
			{
				poolObjs [i].SetActive (true);
				poolObjs [i].transform.position = pos;
				return poolObjs [i];
			}
		}

		if(isAuto)
		{
			
			GameObject obj = Instantiate (getPoolGo(goName));
			obj.name = obj.name.Replace("(Clone)(Clone)","(Clone)");
			obj.transform.parent = transform;
			poolObjs.Add (obj);
			return obj;
		}
		return null;
	}


	public GameObject GetGameObject(string go, Vector3 pos,Quaternion rotation)
	{
		string goName = go+"(Clone)";
		for(int i = 0; i<poolObjs.Count;i++)
		{
			if(!poolObjs[i].activeSelf  && poolObjs[i].name==goName)
			{
				poolObjs [i].SetActive (true);
				poolObjs [i].transform.position = pos;
				poolObjs [i].transform.rotation = rotation;
				return poolObjs [i];
			}
		}

		if(isAuto)
		{

			GameObject obj = Instantiate (getPoolGo(goName));
			obj.name = obj.name.Replace("(Clone)(Clone)","(Clone)");
			obj.transform.parent = transform;
			poolObjs.Add (obj);
			return obj;
		}
		return null;
	}

	public GameObject getPoolGo(string goName)
	{
		foreach(GameObject go in poolObjs)
		{
			if(go.name == goName)
			{
				
				go.SetActive (false);
				return go;
			}
		}


		return null;
	}


}
