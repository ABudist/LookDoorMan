using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save : MonoBehaviour {


	public static save instance;
	// Use this for initialization

	void Awake()
	{
		instance = this;
	}
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void SaveAdd(int data,string Savetag)
	{

		int t = PlayerPrefs.GetInt (Savetag);
		t += data;

		PlayerPrefs.SetInt (Savetag, t);
	}

	public void Save(int data,string Savetag)
	{			
		PlayerPrefs.SetInt (Savetag, data);
	}

	public void Save(float data,string Savetag)
	{			
		PlayerPrefs.SetFloat (Savetag, data);
	}



	public void SaveString(string data,string Savetag)
	{			
		PlayerPrefs.SetString (Savetag, data);
	}



	public int loadInt(string loadTag)
	{
		int temp = PlayerPrefs.GetInt (loadTag,0);
		if(temp ==null || temp ==0)
		{
			return 0;
		}
		else
		{
			return temp;
		}
	}

	public float loadfloat(string loadTag)
	{
		float temp = PlayerPrefs.GetFloat (loadTag,0f);
		if(temp ==null || temp ==0)
		{
			return 0f;
		}
		else
		{
			return temp;
		}
	}


	
}
