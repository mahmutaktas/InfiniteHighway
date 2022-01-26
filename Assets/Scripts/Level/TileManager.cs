using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] groundPrefabs;
    public float zSpawn = 0f;
    public float tileLength = 30;
    public int numberOfTiles = 5;
    public Transform player;

    List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        for (int i=0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(1, groundPrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (player.position.z - 35 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(1, groundPrefabs.Length));
            DeleteTile();
        }
        
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(groundPrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
