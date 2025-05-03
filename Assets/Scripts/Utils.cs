using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utils.Utils 
{
    public class Utils 
    {
        public static GameObject FindClosestRootWithSprite(GameObject obj) //used for finding roots
        {
            if(obj.GetComponent<SpriteRenderer>() != null)
            {
                return obj;
            }
            while(obj.transform.parent != null)
            {
                if (obj.transform.parent.gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    return obj.transform.parent.gameObject;
                }
                obj = obj.transform.parent.gameObject;
            }
            return obj;
        }

        public static bool CloseEnough(Vector3 v1, Vector3 v2, float epsilon)
        {
            if (Vector3.Distance(v1, v2) <= epsilon)
            {
                return true;
            }

            return false;
        }
        public static bool CloseEnough(float f1, float f2, float epsilon)
        {
            if (Mathf.Abs(f1 - f2) <= epsilon)
            {
                return true;
            }

            return false;
        }
        
        
        public static Vector2 DegreeToVector2(float degree)
        {
            float radian = degree * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }
        

    }
}
