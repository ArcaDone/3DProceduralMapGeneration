using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelChunkData")]
public class LevelChunkData : ScriptableObject
{
    public enum Direction
    {
        North, East, South, West
    }

    public enum GetUpAndDown
    {
        Up, Down, Nothing
    }

    public enum DifferenceInHeight
    {
        Zero, One, Two
    }

    public Vector2 chunkSize = new Vector2(10f, 10f);

    public GameObject[] levelChunks;
    public Direction entryDirection;
    public Direction exitDirection;
    public GetUpAndDown upAndDown;
    public DifferenceInHeight heightDifference;

}
