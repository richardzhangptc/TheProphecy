using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private Collider2D graphArea;
    [SerializeField] private Player1Controller monster;
    
    
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

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Lvl1_Section1")
        {
            monster.frozen = true;
        }
    }

    public void Unfreeze()
    {
        monster.frozen = false;
    }


    public void RecalculateGraphsNextSecond()
    {
        StartCoroutine(RecalculateGraphsCoroutine());
    }

    private IEnumerator RecalculateGraphsCoroutine()
    {
        if (graphArea == null)
        {
            yield break;
        }
        yield return new WaitForSeconds(1);
        var guo = new GraphUpdateObject(graphArea.bounds);
        // guo.updatePhysics = true;
        AstarPath.active.UpdateGraphs(guo);
    }
    
}
