using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManageObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToEnable = new List<GameObject>();

    public void ANIM_EnableSelectedObjects()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }
    
    public void ANIM_Destroy()
    {
        Destroy(gameObject);
    }
    
    public void ANIM_DestroyParent()
    {
        Destroy(transform.parent.gameObject);
    }

    public void ANIM_Disable()
    {
        this.enabled = false;
    }
    
    public void ANIM_DisableAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }
    
    
}