using Godot;
using GodotUtils;
using System;
using System.Collections.Generic;

namespace Template;

public partial class RoomGeneration : Node
{
    [Export] TileMapLayer tileMap;

    public override void _Ready()
    {
        GenerateRoom();
    }

    private void GenerateRoom()
    {
        List<Vector2I> floorTiles = GetFloorTiles();
        AddFrogs(floorTiles);
    }

    private List<Vector2I> GetFloorTiles()
    {
        List<Vector2I> floorTiles = new();

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                string tileName = tileMap.GetCustomData<string>(new Vector2I(x, y), "name");

                if (tileName == "floor")
                {
                    floorTiles.Add(new Vector2I(x, y));
                }
            }
        }

        return floorTiles;
    }

    private void AddFrogs(List<Vector2I> floorTiles)
    {
        Random random = new();

        for (int i = 0; i < 2; i++)
        {
            Vector2I randomFloorTile = floorTiles[random.Next(floorTiles.Count)];
            Vector2 randomFloorPosition = tileMap.MapToLocal(randomFloorTile) * tileMap.Scale;

            Frog frog = Game.LoadPrefab<Frog>(Prefab.Frog);
            frog.Position = randomFloorPosition;

            AddChild(frog);
        }
    }
}

