using System.Collections.Generic;
using UnityEngine;

public class TextureUtils
{
    // col,row for each TextureType
    private static Vector2[] GRASS_TOP = CreateUVs(2, 9);
    private static Vector2[] GRASS_SIDE = CreateUVs(3, 0);

    private static Vector2[] DIRT = CreateUVs(2, 0);
    private static Vector2[] STONE = CreateUVs(0, 0);
    private static Vector2[] TNT = CreateUVs(8, 0);
    private static Vector2[] DIAMOND = CreateUVs(2,3);

    // map to UVs for non-uniform blocks, only grass blocks for now
    private static Dictionary<Vector3, Vector2[]> grassBlockUVs = new Dictionary<Vector3, Vector2[]>
    {
        { Vector3.up, GRASS_TOP },
        { Vector3.down, DIRT },
        { Vector3.left, GRASS_SIDE },
        { Vector3.right, GRASS_SIDE },
        { Vector3.forward, GRASS_SIDE },
        { Vector3.back, GRASS_SIDE },
    };

    // map to UVs for uniform blocks, which are blocks with the same texture on all 6 sides
    private static Dictionary<Block.BlockType, Vector2[]> uniformBlockUVs = new Dictionary<Block.BlockType, Vector2[]>
    {
        { Block.BlockType.DIRT, DIRT },
        { Block.BlockType.STONE, STONE },
        { Block.BlockType.TNT, TNT },
        { Block.BlockType.DIAMOND, DIAMOND }
    };

    // the only public function, generic getter for uvs for any blockType and any direction quad
    public static Vector2[] GetUVs(Block.BlockType blockType, Vector3 direction) {
        if (blockType == Block.BlockType.GRASS) {
            return GetGrassUVsByDirection(direction);
        } else {
            return GetUniformBlockUVs(blockType);
        }
    }

    private static Vector2[] GetGrassUVsByDirection(Vector3 direction) {
        Vector2[] ret;
        grassBlockUVs.TryGetValue(direction, out ret);
        return ret;
    }

    private static Vector2[] GetUniformBlockUVs(Block.BlockType blockType) {
        Vector2[] ret;
        uniformBlockUVs.TryGetValue(blockType, out ret);
        return ret;
    }

    // args are col and row, each in the range from 0 to 15 inclusive
    private static Vector2[] CreateUVs(int col, int row) {
        // store in counter clockwise order starting at top right
        Vector2 topRight = CreateUVPoint(col + 1, row);
        Vector2 topLeft = CreateUVPoint(col, row);
        Vector2 bottomLeft = CreateUVPoint(col, row + 1);
        Vector2 bottomRight = CreateUVPoint(col + 1, row + 1);
        return new Vector2[] { topRight, topLeft, bottomLeft, bottomRight };
    }

    // get 2 floats from 0.0 to 1.0
    private static Vector2 CreateUVPoint(int x, int y) {
        return new Vector2(x / 16f, (16 - y) / 16f);
    }
}
