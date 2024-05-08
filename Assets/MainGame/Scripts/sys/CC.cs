using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
using static EventTriggerListener;
using UnityEngine.U2D;
using UnityEngine.UI;

class CC:HotSingleton<CC>
{    
    public T AddComp<T>(GameObject go) where T : Component
    {
        T comp = go.GetComponent<T>();
        if (comp == null)
        {
            comp = go.AddComponent<T>();
        }
        return comp;
    }       

    public T GetUIGo<T>(GameObject Parent, string Path="") where T : class
    {
        if(Path=="")
        {
            return Parent.GetComponent<T>();
        }
        else
        {
            if (typeof(T) == typeof(GameObject))
            {
                return Parent.transform.Find(Path).gameObject as T;
            }
            if (typeof(T) == typeof(Transform))
            {
                return Parent.transform.Find(Path) as T;
            }
            return Parent.transform.Find(Path).GetComponent<T>();
        }
    }
 
    public void RegBtn(GameObject Btn, VoidDelegate act)
    {
        EventTriggerListener.Get(Btn).onClick = act;
    }



}