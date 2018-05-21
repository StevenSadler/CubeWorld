using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadUtils {
    private static int[] triangles = new int[] { 3, 1, 0, 3, 2, 1 };

    // set all vertices for a cube of size 1
    private static Vector3 p0 = new Vector3(-0.5f, -0.5f, 0.5f);
    private static Vector3 p1 = new Vector3(0.5f, -0.5f, 0.5f);
    private static Vector3 p2 = new Vector3(0.5f, -0.5f, -0.5f);
    private static Vector3 p3 = new Vector3(-0.5f, -0.5f, -0.5f);
    private static Vector3 p4 = new Vector3(-0.5f, 0.5f, 0.5f);
    private static Vector3 p5 = new Vector3(0.5f, 0.5f, 0.5f);
    private static Vector3 p6 = new Vector3(0.5f, 0.5f, -0.5f);
    private static Vector3 p7 = new Vector3(-0.5f, 0.5f, -0.5f);

    // set vertices in each quad
    private static Vector3[] vBottom = new Vector3[] { p0, p1, p2, p3 };
    private static Vector3[] vTop = new Vector3[] { p7, p6, p5, p4 };
    private static Vector3[] vLeft = new Vector3[] { p7, p4, p0, p3 };
    private static Vector3[] vRight = new Vector3[] { p5, p6, p2, p1 };
    private static Vector3[] vFront = new Vector3[] { p4, p5, p1, p0 };
    private static Vector3[] vBack= new Vector3[] { p6, p7, p3, p2 };

    private static Dictionary<Vector3, Vector3[]> vertices = new Dictionary<Vector3, Vector3[]>
    {
        { Vector3.down, vBottom },
        { Vector3.up, vTop },
        { Vector3.left, vLeft },
        { Vector3.right, vRight },
        { Vector3.forward, vFront },
        { Vector3.back, vBack }
    };
    private static Dictionary<Vector3, Vector3[]> normals = new Dictionary<Vector3, Vector3[]>
    {
        { Vector3.down, new Vector3[] { Vector3.down, Vector3.down, Vector3.down, Vector3.down } },
        { Vector3.up, new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up } },
        { Vector3.left, new Vector3[] { Vector3.left, Vector3.left, Vector3.left, Vector3.left } },
        { Vector3.right, new Vector3[] { Vector3.right, Vector3.right, Vector3.right, Vector3.right } },
        { Vector3.forward, new Vector3[] { Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward } },
        { Vector3.back, new Vector3[] { Vector3.back, Vector3.back, Vector3.back, Vector3.back } }
    };

    public static int[] GetTriangles() { return triangles; }

    public static Vector3[] GetVertices(Vector3 direction) {
        Vector3[] verts;
        vertices.TryGetValue(direction, out verts);
        return verts;
    }

    public static Vector3[] GetNormals(Vector3 direction) {
        Vector3[] norms;
        normals.TryGetValue(direction, out norms);
        return norms;
    }
}
