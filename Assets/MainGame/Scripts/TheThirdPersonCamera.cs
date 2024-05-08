using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheThirdPersonCamera : MonoBehaviour
{
    public Transform playerTransform; 
    public Vector3 deviation;
    public bool IsInit = false;

    void Start()
    {
        
    }

    void Update()
    {
        
        if(MangerGame.inst!=null &&MangerGame.inst.IsGameStart)
        {
            if(!IsInit)
            {
                IsInit = true;
                deviation = transform.position - MangerGame.inst.HeroGO.transform.position;
            }
            transform.position = MangerGame.inst.HeroGO.transform.position + deviation;
        }      
    }

}
