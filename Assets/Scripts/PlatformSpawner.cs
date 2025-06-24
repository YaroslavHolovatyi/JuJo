using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public float minYDistance = 1.5f;
    public float maxYDistance = 2.5f;
    public float platformDespawnOffset = 10f;
    public int initSpawn = 5;
    public int maxPlatformsOnScreen = 15;

    [Range(0.5f, 2f)]
    public float verticalDistanceModifier = 1f;

    [Range(0.1f, 1.5f)]
    public float xDistanceModifier = 1.5f;

    [Range(0f, 0.5f)]
    public float horizontalMarginPercentMin = 0.05f;  // лівий край (5% за замовчуванням)

    [Range(0f, 0.5f)]
    public float horizontalMarginPercentMax = 0.05f;  // правий край (5% за замовчуванням)

    private float nextY = 0f;
    private float screenMinX, screenMaxX;
    private List<GameObject> spawnedPlatforms = new List<GameObject>();
    private bool initialized = false;

    void Start()
    {
        StartCoroutine(DelayedInit());
    }

    IEnumerator DelayedInit()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        float maxPlatformWidth = 0f;
        foreach (var prefab in platformPrefabs)
        {
            float width = prefab.GetComponent<SpriteRenderer>().bounds.size.x;
            if (width > maxPlatformWidth)
                maxPlatformWidth = width;
        }

        SetHorizontalBounds(maxPlatformWidth);
        nextY = Camera.main.transform.position.y - 2f;

        for (int i = 0; i < initSpawn; i++)
        {
            SpawnPlatform();
        }

        initialized = true;
    }

    void Update()
    {
        if (!initialized) return;

        float cameraY = Camera.main.transform.position.y;

        while (nextY < cameraY + 15f && spawnedPlatforms.Count < maxPlatformsOnScreen)
        {
            SpawnPlatform();
        }

        for (int i = spawnedPlatforms.Count - 1; i >= 0; i--)
        {
            if (spawnedPlatforms[i].transform.position.y < cameraY - platformDespawnOffset)
            {
                Destroy(spawnedPlatforms[i]);
                spawnedPlatforms.RemoveAt(i);
            }
        }
    }

    void SetHorizontalBounds(float platformWidth)
    {
        Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float totalWidth = right.x - left.x;
        float usableWidth = totalWidth * xDistanceModifier;

        float centerX = (right.x + left.x) / 2f;
        screenMinX = centerX - usableWidth / 2f + platformWidth / 2f;
        screenMaxX = centerX + usableWidth / 2f - platformWidth / 2f;
    }

    void SpawnPlatform()
    {
        GameObject prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
        float platformWidth = prefab.GetComponent<SpriteRenderer>().bounds.size.x;

        float minX = screenMinX + platformWidth / 2f;
        float maxX = screenMaxX - platformWidth / 2f;

        // Центрована базова позиція
        float baseX = Random.Range(minX, maxX);

        // Додаткове рандомне відхилення в межах ±0.5 одиниць
        float xNoise = Random.Range(-0.5f, 0.5f);
        float finalX = Mathf.Clamp(baseX + xNoise, minX, maxX);

        GameObject newPlatform = Instantiate(prefab, new Vector3(finalX, nextY, 0), Quaternion.identity);
        spawnedPlatforms.Add(newPlatform);

        float yDistance = Random.Range(minYDistance * verticalDistanceModifier, maxYDistance * verticalDistanceModifier);
        nextY += yDistance;
    }

}
