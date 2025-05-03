using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateManager : MonoBehaviour
{
	[SerializeField] private string nextScene;

	private List<Gate> gates;

	// Graps all gates that exists at the beginning of the level
	void Start()
    {
        GameObject[] gateObjects = (GameObject.FindGameObjectsWithTag("gate"));
		foreach (var gateObject in gateObjects)
		{
			gates.Add(gateObject.GetComponent<Gate>());
		}
    }

    // Update is called once per frame
    void Update()
    {
    	bool hasOracle = false; 
		bool hasMonster = false;

		foreach (Gate gate in gates)
		{
			if (gate.HasOracle == true)
			{
				hasOracle = true;
			}
			if (gate.HasMonster == true)
			{
				hasMonster = true;
			}
		}
		
		if (hasOracle && hasMonster)
		{
			SceneTransition();
		}
    }

	private void SceneTransition()
	{
		SceneManager.LoadScene(nextScene);
	}
}
