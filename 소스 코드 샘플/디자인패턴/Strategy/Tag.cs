using UnityEngine;
using System;
using System.Collections;

public class Tag : MonoBehaviour, IOnClick
{
    ITagBehavior tagBehavior = null;

    public void Initialize(String typeName)
    {
        switch (typeName)
        {
            case "TypeA":
                tagBehavior = new TypeA();
                break;
            case "TypeB":
                tagBehavior = new TypeB();
                break;
			case "TypeC":
                tagBehavior = new TypeC();
                break;
            case "BasicTagBehavior":
                tagBehavior = new BasicTagBehavior();
                break;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void PointToCoordinate(Vector3 v3)
    {
        tagBehavior.PointToCoordinate(v3);
    }




}
