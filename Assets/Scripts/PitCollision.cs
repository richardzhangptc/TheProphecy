using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PitCollision : MonoBehaviour
{
	private GameObject[] platforms;

	void Start()
	{
		platforms = GameObject.FindGameObjectsWithTag("Platform");
		Debug.Log(platforms.Length);
	}


	void OnTriggerStay2D(Collider2D other)
	{
		Debug.Log("KILLL " + other.tag);
		// monster can't get on platform so dies
		if (other.gameObject.tag == "Monster" || other.gameObject.tag == "MonsterHitBox")
		{
			killPlayer();
		}
		// Oracle is safe on platform, but otherwise dies
		else if (other.gameObject.tag == "OracleHitBox" || other.gameObject.tag == "Oracle")
		{
			bool onPlatform = false;
			foreach(var platform in platforms)
			{
				MovingPlatformController currMPC = platform.GetComponent<MovingPlatformController>();
				if (currMPC != null && currMPC.playerOnPlatform())
				{
					onPlatform = true;
					Debug.Log("CHICKEN JOCKEY");
				}
			}
			
			if (!onPlatform)
			{
				killPlayer();
			}
		}
	}

	private void killPlayer()
	{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
