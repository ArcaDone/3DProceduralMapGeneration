using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutGenerator : MonoBehaviour
{
    public LevelChunkData[] levelChunkData;
    public LevelChunkData firstChunk;

    private LevelChunkData previousChunk;

    public Vector3 spawnOrigin;

    private Vector3 spawnPosition;

    public int chunksToSpawn = 10;

    public bool animate;

    public int desertChunckSizeIndex = 2;
    public int forestChunckSizeIndex = 5;
    public int iceChunckSizeIndex = 8;



    void OnEnable()
    {
        TriggerExit.OnChunkExited += PickAndSpawnChunk;
    }

    private void OnDisable()
    {
        TriggerExit.OnChunkExited -= PickAndSpawnChunk;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            PickAndSpawnChunk();
        }
    }

    void Start()
    {

        previousChunk = firstChunk;

        for (int i = 0; i < chunksToSpawn; i++)
        {
            PickAndSpawnChunk();
        }
    }
    
    LevelChunkData PickNextChunk()
    {
        List<LevelChunkData> allowedChunkList = new List<LevelChunkData>();
        LevelChunkData nextChunk = null;

        LevelChunkData.Direction nextRequiredDirection = LevelChunkData.Direction.North;

        switch (previousChunk.exitDirection)
        {
            case LevelChunkData.Direction.North:
                nextRequiredDirection = LevelChunkData.Direction.South;
                spawnPosition += new Vector3(0f, 0, previousChunk.chunkSize.y);
                break;
            case LevelChunkData.Direction.East:
                nextRequiredDirection = LevelChunkData.Direction.West;
                spawnPosition += new Vector3(previousChunk.chunkSize.x, 0, 0);
                break;
            case LevelChunkData.Direction.South:
                nextRequiredDirection = LevelChunkData.Direction.North;
                spawnPosition += new Vector3(0, 0, -previousChunk.chunkSize.y);
                break;
            case LevelChunkData.Direction.West:
                nextRequiredDirection = LevelChunkData.Direction.East;
                spawnPosition += new Vector3(-previousChunk.chunkSize.x, 0, 0);

                break;
            default:
                break;
        }


        for (int i = 0; i < levelChunkData.Length; i++)
        {
            if (levelChunkData[i].entryDirection == nextRequiredDirection)
            {
                allowedChunkList.Add(levelChunkData[i]);
            }
        }



        nextChunk = allowedChunkList[Random.Range(0, allowedChunkList.Count)];

        HillDownGeneration(nextChunk);

        return nextChunk;

    }

    void PickAndSpawnChunk()
    {
        LevelChunkData chunkToSpawn = PickNextChunk();

        GameObject objectFromChunk = null;

        objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, desertChunckSizeIndex)];
        
        previousChunk = chunkToSpawn;
        var element = Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);

        if (animate)
        {
            element.AddComponent<DropTween>();
            DropTween.IncreaseDropTime();
        }

    }

    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin += originDelta;
    }

    void HillDownGeneration(LevelChunkData nextChunk)
    {
        switch (previousChunk.upAndDown)
        {
            case LevelChunkData.GetUpAndDown.Up:

                if (previousChunk.heightDifference == LevelChunkData.DifferenceInHeight.One)
                {
                    MoveUpOrDown(nextChunk, 0f, 2.8f, -2.8f, 2.8f);
                }

                else if (previousChunk.heightDifference == LevelChunkData.DifferenceInHeight.Two)
                {
                    MoveUpOrDown(nextChunk, 3.35f, 5.8f, 0f, 5.8f);
                }
                break;

            case LevelChunkData.GetUpAndDown.Down:

                if (previousChunk.heightDifference == LevelChunkData.DifferenceInHeight.One)
                {
                    MoveUpOrDown(nextChunk, -2.8f, 0f, -5.8f, 0f);
                }

                else if (previousChunk.heightDifference == LevelChunkData.DifferenceInHeight.Two)
                {
                    MoveUpOrDown(nextChunk, -2.8f, 0f, -5.8f, 0f);
                }
                break;

            case LevelChunkData.GetUpAndDown.Nothing:
                if (nextChunk.heightDifference == LevelChunkData.DifferenceInHeight.One && nextChunk.upAndDown == LevelChunkData.GetUpAndDown.Down)
                {
                    spawnPosition += new Vector3(0f, -2.8f, 0f);
                } else if (nextChunk.heightDifference == LevelChunkData.DifferenceInHeight.Two && nextChunk.upAndDown == LevelChunkData.GetUpAndDown.Down)
                {
                    spawnPosition += new Vector3(0f, -5.8f, 0f);
                } else if (nextChunk.heightDifference == LevelChunkData.DifferenceInHeight.Zero && nextChunk.upAndDown == LevelChunkData.GetUpAndDown.Down)
                {
                    spawnPosition += new Vector3(0f, -2.8f, 0f);
                }
                break;

            default:
                break;
        }
    }
    
    void MoveUpOrDown(LevelChunkData nextChunk, float oneDown, float oneUp, float twoDown, float straightUp)
    {
        if (nextChunk.heightDifference == LevelChunkData.DifferenceInHeight.One && nextChunk.upAndDown == LevelChunkData.GetUpAndDown.Down)
        {
            spawnPosition += new Vector3(0f, oneDown, 0f);
        }
        else if (nextChunk.heightDifference == LevelChunkData.DifferenceInHeight.One && nextChunk.upAndDown == LevelChunkData.GetUpAndDown.Up)
        {
            spawnPosition += new Vector3(0f, oneUp, 0f);
        }else if (nextChunk.heightDifference == LevelChunkData.DifferenceInHeight.Two && nextChunk.upAndDown == LevelChunkData.GetUpAndDown.Down)
        {
            spawnPosition += new Vector3(0f, twoDown, 0f);
        }
        else
        {
            spawnPosition += new Vector3(0f, straightUp, 0f);
        }
    }

}
