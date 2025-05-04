using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PitCollision : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("KILLL " + other.tag);
		if (other.tag == "Monster" || other.tag == "Oracle")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

}
