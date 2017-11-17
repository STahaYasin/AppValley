using UnityEngine;
using System.Collections;

public class respawner : MonoBehaviour {

    public GameObject player_prefab;
    public GameObject player;
    public GameObject[] spawnPoint;
    public GameObject floor;

	void Start () {
        player = (GameObject) Instantiate(player_prefab, spawnPoint[0].transform.position, spawnPoint[0].transform.rotation);
	}
	
	
	void Update () {
	    if(player != null && floor != null)
        {
            if (player.transform.position.y < floor.transform.position.y)
            {
                player.transform.position = spawnPoint[0].transform.position;
            }
        }
	}
}