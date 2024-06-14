using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    public GameObject tileToSpawn;
    public GameObject referenceObject;
    [SerializeField]
    public GameObject player;
    public GameObject groundToSpawn;
    public GameObject coinPrefab;
    public GameObject PowerUpScorePrefab;
    private float powerUpScoreSpawnRate = 0.05f;
    
    public GameObject SlowDownPrefab;
    private float SlowDownSpawnRate = 0.05f;

    public float maxDistanceFromPlayer = 0.5f;
    public float distanceBetweenTiles = 5.0f;
    public float randomValue = 0.7f;

    private List<GameObject> tiles = new List<GameObject>();
    private Vector3 previousTilePosition;
    private Vector3 direction, mainDirection = new Vector3(0, 0, 1), otherDirection = new Vector3(1, 0, 0);
    private float coinSpawnRate = 0.5f;

    public void changePlayer(GameObject p){
        player = p;
    }

    void Start()
    {
        previousTilePosition = referenceObject.transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, previousTilePosition) <= maxDistanceFromPlayer * 2)
        {
            float random = Random.Range(0.0f, 1.0f);
            if (random >= randomValue)
            {
                ChangeDirection();
            }

            SpawnTileAndManageCoinsAndPowerUps();
            CleanUpTiles();
        }
    }

    private void ChangeDirection()
    {
        Vector3 temp = mainDirection;
        mainDirection = otherDirection;
        otherDirection = temp;
        direction = mainDirection;
    }

    private void SpawnTileAndManageCoinsAndPowerUps()
    {
            Vector3 spawnPos = previousTilePosition + distanceBetweenTiles * direction;
            GameObject temp = Instantiate(tileToSpawn, spawnPos, Quaternion.identity);
            tiles.Add(temp);
            SpawnCoins(temp.GetComponent<Collider>());
            SpawnScorePowerUp(temp.GetComponent<Collider>());
            SpawnSlowDown(temp.GetComponent<Collider>());
            previousTilePosition = spawnPos;
    }

    private void SpawnSlowDown(Collider collider)
    {
        float choice = Random.Range(0.0f, 1.0f);
        if (choice <= SlowDownSpawnRate)
        {
            GameObject temp = Instantiate(SlowDownPrefab, collider.transform);
            temp.transform.position = GetRandomPointInCollider(collider);
        }
    }
    private void SpawnScorePowerUp(Collider collider)
    {
        float choice = Random.Range(0.0f, 1.0f);
        if (choice <= powerUpScoreSpawnRate)
        {
            GameObject temp = Instantiate(PowerUpScorePrefab, collider.transform);
            temp.transform.position = GetRandomPointInCollider(collider);
        }
    }

    private void SpawnCoins(Collider collider)
    {
        float choice = Random.Range(0.0f, 1.0f);
        if (choice >= coinSpawnRate)
        {
            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                GameObject temp = Instantiate(coinPrefab, collider.transform);
                temp.transform.position = GetRandomPointInCollider(collider);
            }
        }
    }

    private Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            5,
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        );
        return point;
    }

    private void CleanUpTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            
            if (Vector3.Distance(player.transform.position, tiles[i].transform.position) > maxDistanceFromPlayer)
            {
                Destroy(tiles[i]);
                tiles.RemoveAt(i);
                i--;
            }
            else break;
        }
    }
}
