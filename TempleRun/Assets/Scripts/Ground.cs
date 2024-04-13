using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    public GameObject tileToSpawn;
    public GameObject referenceObject;
    public GameObject player;

    public GameObject groundToSpawn;

    
    public float maxDistanceFromPlayer = 100f;
    public float distanceBetweenTiles = 5.0f;
    public float randomValue = 0.7f;
    
    private List<GameObject> tiles = new List<GameObject>();
    private Vector3 previousTilePosition;
    private Vector3 direction, mainDirection = new Vector3(0, 0, 1), otherDirection = new Vector3(1, 0, 0);

    void Start()
    {
        previousTilePosition = referenceObject.transform.position;
       
    }

    void Update()
    {
        if (Random.value < randomValue)
            direction = mainDirection;
        else
        {
            Vector3 temp = direction;
            direction = otherDirection;
            mainDirection = direction;
            otherDirection = temp;
        }
        if(Random.value<randomValue){
            Vector3 spawnPos = previousTilePosition + distanceBetweenTiles * direction;
            if(Vector3.Distance(player.transform.position, spawnPos) <= maxDistanceFromPlayer * 5)
            {   
                
                GameObject temp = Instantiate(tileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
                tiles.Add(temp);
                SpawnCoins(temp.GetComponent<Collider>());
                previousTilePosition = spawnPos;
                
            }

        }
        else{
            Vector3 spawnPos = previousTilePosition + 4f * direction;
            if(Vector3.Distance(player.transform.position, spawnPos) <= maxDistanceFromPlayer * 5)
            {   
                
                GameObject temp = Instantiate(groundToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
                tiles.Add(temp);
                SpawnCoins(temp.GetComponent<Collider>());
                previousTilePosition = spawnPos;
                
            }
            Vector3 spawnPos2 = previousTilePosition + 4f * direction;
            if(Vector3.Distance(player.transform.position, spawnPos2) <= maxDistanceFromPlayer * 5)
            {   
                
                GameObject temp = Instantiate(groundToSpawn, spawnPos2, Quaternion.Euler(0, 0, 0));
                tiles.Add(temp);
                SpawnCoins(temp.GetComponent<Collider>());
                previousTilePosition = spawnPos2;
                
            }
            Vector3 spawnPos3 = previousTilePosition + 4f * direction;
            if(Vector3.Distance(player.transform.position, spawnPos3) <= maxDistanceFromPlayer * 5)
            {   
                
              GameObject temp = Instantiate(groundToSpawn, spawnPos3, Quaternion.Euler(0, 0, 0));
                tiles.Add(temp);
                SpawnCoins(temp.GetComponent<Collider>());
                previousTilePosition = spawnPos3;
                
            }
             Vector3 spawnPos4 = previousTilePosition + 4f * direction;
            if(Vector3.Distance(player.transform.position, spawnPos4) <= maxDistanceFromPlayer * 5)
            {   
                
              GameObject temp = Instantiate(groundToSpawn, spawnPos4, Quaternion.Euler(0, 0, 0));
                tiles.Add(temp);
                SpawnCoins(temp.GetComponent<Collider>());
                previousTilePosition = spawnPos4;
                
            }
        }
        
        for(int i = 0; i < tiles.Count; i++)
        {
            if(Vector3.Distance(player.transform.position, tiles[i].transform.position) > maxDistanceFromPlayer * 5)
            {   
                Destroy(tiles[i]);
                tiles.RemoveAt(i);
            }
        }
    }

    public GameObject coinPrefab;
    void SpawnCoins(Collider collider){
        for(int i =0;i<Random.Range(1,3);i++){
            GameObject temp =Instantiate(coinPrefab);
            temp.transform.position = GetRandomPointInCollider(collider);
        }
    }
    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x,collider.bounds.max.x),
            Random.Range(collider.bounds.min.y,collider.bounds.max.y),
            Random.Range(collider.bounds.min.z,collider.bounds.max.z)
        );
        if(point != collider.ClosestPoint(point)){
            point= GetRandomPointInCollider(collider);
        }
        point.y =5;
        return point;
    }
}
