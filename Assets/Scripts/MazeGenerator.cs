using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum WallState 
{
    LEFT = 1,
    RIGHT = 2,
    UP = 4,
    DOWN = 8,

    VISITED = 128,
}

public struct Position 
{
    public int X;
    public int Y;
}

public struct WallBreak 
{
    public Position Position;
    public WallState SharedWall;
}
public struct Neighbour 
{
    public Position position;
    public WallState SharedWall;
}

public static class MazeGenerator
{
    private static WallState GetOppositeWall(WallState wall)
    {
        switch(wall)
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.RIGHT;
        }
    }
    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, int width, int height, bool isRightMaze)
    {
        // making changes
        var rng = new System.Random(/* seed */);
        Stack<Position> positionStack = new Stack<Position>();
        var position = new Position { X = rng.Next(0, width), Y = rng.Next(0, height) };
        maze[position.X, position.Y] |= WallState.VISITED; // 1000 1111
        positionStack.Push(position);
        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbors = GetUnvisitedNeighbours(current, maze, width, height);
            if (neighbors.Count > 0) // still have not reached the end
            {
                positionStack.Push(current);

                var randIndex = rng.Next(0, neighbors.Count);
                var randomNeighbour = neighbors[randIndex];

                var nPosition = randomNeighbour.position;
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);

                maze[nPosition.X, nPosition.Y] |=  WallState.VISITED;

                positionStack.Push(nPosition);
            }
        }
        if (isRightMaze)
        {
            maze[0, (int) height / 2] &= ~WallState.LEFT;
            // maze[width - 1, height - 1] &= ~WallState.UP;
            
        }
        else
        {
            maze[width - 1, (int) height / 2] &= ~WallState.RIGHT;
            // maze[0, height - 1] &= ~WallState.UP;
        }
        return maze;
    }

    public static List<Neighbour> GetUnvisitedNeighbours(Position p, WallState[,] maze, int width, int height)
    {
        List<Neighbour> neighbours = new List<Neighbour>();
        if (p.X > 0) // left
        {
            if (!maze[p.X - 1, p.Y].HasFlag(WallState.VISITED))
            {
                neighbours.Add(new Neighbour
                {
                    position = new Position 
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.LEFT
                });
            }
        }
        if (p.X < width - 1) // right
        {
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.VISITED))
            {
                neighbours.Add(new Neighbour
                {
                    position = new Position 
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.RIGHT
                });
            }
        }
        if (p.Y < height - 1) // up
        {
            if (!maze[p.X, p.Y + 1].HasFlag(WallState.VISITED))
            {
                neighbours.Add(new Neighbour
                {
                    position = new Position 
                    {
                        X = p.X,
                        Y = p.Y + 1
                    },
                    SharedWall = WallState.UP
                });
            }
        }
        if (p.Y > 0) // down
        {
            if (!maze[p.X, p.Y - 1].HasFlag(WallState.VISITED))
            {
                neighbours.Add(new Neighbour
                {
                    position = new Position 
                    {
                        X = p.X,
                        Y = p.Y - 1
                    },
                    SharedWall = WallState.DOWN
                });
            }
        }
        return neighbours;
    }

    public static WallState[,] Generate(int width, int height, bool isRightMaze)
    {
        WallState[,] maze = new WallState[width, height];
        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;
        for (int i=0; i < width; ++i) {
            for (int j=0; j < height; ++j) {
                maze[i, j] = initial;
            }
        }        
        return ApplyRecursiveBacktracker(maze, width, height, isRightMaze); 
    }
}
