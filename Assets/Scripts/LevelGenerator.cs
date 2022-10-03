using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] PlatformPrefabs;
    public GameObject FirstPlatformPrefab;
    public int MinPlatforms;
    public int MaxPlatforms;
    public float DistanceBetweenPlatforms;
    public Transform FinishPlatform;
    public Transform LevelRoot;
    public Game Game;

    private void Awake()
    {
        int levelIndex = Game.LevelIndex;
        Random random = new Random(levelIndex);
        int platformsCount = RandomRange(random, MinPlatforms, MaxPlatforms + 1);
        for (int i = 0; i < platformsCount; i++)
        {
            int prefabIndex = RandomRange(random, 0, PlatformPrefabs.Length);
            GameObject platform = Instantiate(PlatformPrefabs[prefabIndex], transform);
            platform.transform.localPosition = CalculatePlatformPosition(i);
        }

        FinishPlatform.localPosition = CalculatePlatformPosition(platformsCount);
    }

    private int RandomRange(Random random, int min, int maxExclusive)
    {
        int number = random.Next();
        int length = maxExclusive - min;
        number %= length;
        return min + number;
    }

    private float RandomRange(Random random, float min, float max)
    {
        float t = (float)random.NextDouble();
        return Mathf.Lerp(min, max, t);
    }

    private Vector3 CalculatePlatformPosition(int platformIndex)
    {
        return new Vector3(-28.80779f, 21.39256f, -59.51464f + 50 + (DistanceBetweenPlatforms * platformIndex));
    }
}
