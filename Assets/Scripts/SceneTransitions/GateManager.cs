using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateManager : MonoBehaviour
{
	[SerializeField] private string nextScene;

	private GameObject[] gates;

	// Graps all gates that exists at the beginning of the level
	void Start()
    {
        gates = (GameObject.FindGameObjectsWithTag("gate"));
    }

    // Update is called once per frame
    void Update()
    {
    	bool hasOracle = false; 
		bool hasMonster = false;

		foreach (var gate in gates)
		{
			var gateComponent = gate.GetComponent<Gate>();
			if (gateComponent.HasOracle == true)
			{
				hasOracle = true;
			}
			if (gateComponent.HasMonster == true)
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
