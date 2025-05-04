using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private Collider2D graphArea;
    
    #region Singleton and Awake

    public static LevelManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    
    
    
    public void RecalculateGraphsNextSecond()
    {
        StartCoroutine(RecalculateGraphsCoroutine());
    }

    private IEnumerator RecalculateGraphsCoroutine()
    {
        yield return new WaitForSeconds(1);
        var guo = new GraphUpdateObject(graphArea.bounds);
        // guo.updatePhysics = true;
        AstarPath.active.UpdateGraphs(guo);
    }
    
}
