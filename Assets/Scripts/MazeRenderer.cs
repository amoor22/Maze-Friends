using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    
    #region Variables
    [SerializeField]
    [Range(1, 50)]
    private int width = 10;
    
    [SerializeField]
    [Range(1, 50)]
    private int height = 10; 

    [SerializeField]
    private Transform lightPowerup;
    [SerializeField]
    private Transform wallPrefab = null;
    [SerializeField]
    private Transform cube;

    [SerializeField]
    private float size = 1f;
    [SerializeField]
    private float startX = 0f;
    [SerializeField]
    private float startY = 0f;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private int leftOrRight;
    private bool isRightMaze;

    public int lightPowerupNum = 4;
    public int lightPowerupSpawned = 0;
    #endregion

    #region Unity methods

    void Start()
    {
        wallPrefab.tag = "Wall";
        if (gameObject.name == "MazeRenderer")
        {
            waitTime = 0.5f;
            isRightMaze = false;
        }
        else 
        {
            waitTime = 1f;
            isRightMaze = true;
        }
        // var maze = MazeGenerator.Generate(width, height, waitTime);
        // Draw(maze);
        StartCoroutine(GenerateCoroutine());
    }
    IEnumerator GenerateCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        var maze = MazeGenerator.Generate(width, height, isRightMaze);
        Draw(maze);
    }
    private void Draw(WallState[,] maze)
    {
        System.Random random = new System.Random();
        var list = new int[lightPowerupNum,2];
        for (int m=0; m <lightPowerupNum; m++)
        {
            for (int a=0; a < 2; a++)
            {
                if (a == 0)
                {
                    list[m,a] = random.Next(2, width);
                } else {
                    list[m,a] = random.Next(2, height);
                }
            }
        }
        for (int i=0; i<width; ++i) {
            for (int j=0; j<height; ++j) {
                var cell = maze[i, j];
                var position = new Vector3((-width / 2 + i) + (wallPrefab.localScale.x * width * 0.5f * leftOrRight), (-height / 2 + j) + startY);
                // spawnLightPowerup(position, i, j);
                for (int m = 0; m < list.GetLength(0); m++)
                {
                    if (list[m, 0] == i && list[m, 1] == j)
                    {
                        var light = Instantiate(lightPowerup, transform) as Transform;
                        light.transform.position = position;
                        lightPowerupSpawned++;

                    }
                }
                if (gameObject.CompareTag("Maze1"))
                {
                    if (i == 0 && j == 0)
                    {
                        var cubeInstance = Instantiate(cube, transform) as Transform;
                        cubeInstance.transform.position = position;
                    }
                } 
                else if (gameObject.CompareTag("Maze2"))
                {
                    if (i == width - 1 && j == 0)
                    {
                        var cubeInstance = Instantiate(cube, transform) as Transform;
                        cubeInstance.transform.position = position;
                    }
                }
                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, size / 2, 0);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }
                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0);
                    leftWall.eulerAngles = new Vector3(0, 0, 90);
                }
                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(size / 2, 0);
                        rightWall.eulerAngles = new Vector3(0, 0, 90);
                    }
                }
                if (j == 0)
                { 
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, -size / 2);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                    }
                }
            }
        }
    }
    private void spawnLightPowerup(Vector3 position, int i, int j)
    {
        if (lightPowerupSpawned < lightPowerupNum){
            if (new System.Random().Next(1, 8) == 6)
            {
            Debug.Log("Dead");
                var light = Instantiate(lightPowerup, transform) as Transform;
                light.transform.position = position;
                lightPowerupSpawned++;
            }
        }
    }
    void Update()
    {
        
    }
  
    #endregion
}
