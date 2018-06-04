using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BreadthFirstSearch {

    public enum WorldType
    {
        cube,
        octahedron,
        sphere
    };

    public delegate bool IsInWorld(Vector3 startPosition, Vector3 nextPosition, float radius);
    public delegate bool IsOnWorldSurface(Vector3 startPosition, Vector3 nextPosition, float radius);

    public static Vector3[] directions = new Vector3[] {
        Vector3.right,
        Vector3.left,
        Vector3.up,
        Vector3.down,
        Vector3.forward,
        Vector3.back
    };

	public List<Vector3> rightHemisphere;
    public List<Vector3> leftHemisphere;
    public List<Vector3> upHemisphere;
    public List<Vector3> downHemisphere;
    public List<Vector3> forwardHemisphere;
    public List<Vector3> backHemisphere;

    public List<Vector3> allNodes;

    public IsInWorld isInWorld;
    IsOnWorldSurface isOnWorldSurface;

    public BreadthFirstSearch(int radius, WorldType worldType) {
        if (worldType == WorldType.sphere) {
            isInWorld = IsInSphere;
            isOnWorldSurface = IsOnSphereSurface;
        } else if (worldType == WorldType.octahedron) {
            isInWorld = IsInOctahedron;
            isOnWorldSurface = IsOnOctahedronSurface;
        } else {
            isInWorld = IsInCube;
            isOnWorldSurface = IsOnCubeSurface;
        }

		Vector3 startPosition = new Vector3(0,0,0);

		rightHemisphere = new List<Vector3>();
		leftHemisphere = new List<Vector3>();
        upHemisphere = new List<Vector3>();
        downHemisphere = new List<Vector3>();
        forwardHemisphere = new List<Vector3>();
		backHemisphere = new List<Vector3>();

		allNodes = WalkBreadthFirst(startPosition, radius);

		DebugHemisphere(backHemisphere);
	}

	void DebugHemisphere(List<Vector3> hemisphere) {
		StringBuilder sb = new StringBuilder();
		foreach (Vector3 position in hemisphere) {
			sb.Append("hemisphere position= " + position + "\n");
		}
		//Debug.Log(sb.ToString());
	}



	List<Vector3> WalkBreadthFirst(Vector3 startPosition, int radius)
	{
		StringBuilder sb = new StringBuilder();

		int nodeTestVal = 0;

		List<Vector3> visitedNodes = new List<Vector3>();
		Queue<Vector3> nodeQueue = new Queue<Vector3>();

		visitedNodes.Add(startPosition);
		nodeQueue.Enqueue(startPosition);
		sb.Append("startNode added id= " + nodeTestVal + "   position= " + startPosition + "\n");
		nodeTestVal++;

		// wrap this in a for loop counting down from levels

		while (nodeQueue.Count > 0) {
			Vector3 current = nodeQueue.Dequeue();
			Vector3 nextNode;

			foreach (Vector3 direction in directions) {
				nextNode = current + direction;

				if (!visitedNodes.Contains(nextNode) && isInWorld(startPosition, nextNode, radius)) {
					nodeQueue.Enqueue(nextNode);
					visitedNodes.Add(nextNode);

					if (isOnWorldSurface(startPosition, nextNode, radius)) {
						AddToHemispheres(startPosition, nextNode, radius);
					}

					sb.Append("node added id= " + nodeTestVal + "   position= " + nextNode + "\n");
					nodeTestVal++;
				}
			}
		}
        //Debug.Log(sb.ToString());

        return visitedNodes;
	}

	void AddToHemispheres(Vector3 startPosition, Vector3 nextPosition, int radius) {
		// for each of the directions
		// if it is in the hemisphere or on the plane between the hemispheres
		// then add nextPosition to the leading surface for that direction

		Vector3 change = nextPosition - startPosition;
		if (change.x <= 0 && !isInWorld(startPosition, nextPosition + Vector3.left, radius)) {
			leftHemisphere.Add(nextPosition);
		}
		if (change.x >= 0 && !isInWorld(startPosition, nextPosition + Vector3.right, radius)) {
            rightHemisphere.Add(nextPosition);
		}
        if (change.y <= 0 && !isInWorld(startPosition, nextPosition + Vector3.down, radius)) {
            downHemisphere.Add(nextPosition);
        }
        if (change.y >= 0 && !isInWorld(startPosition, nextPosition + Vector3.up, radius)) {
            upHemisphere.Add(nextPosition);
        }
        if (change.z <= 0 && !isInWorld(startPosition, nextPosition + Vector3.back, radius)) {
            backHemisphere.Add(nextPosition);
		}
		if (change.z >= 0 && !isInWorld(startPosition, nextPosition + Vector3.forward, radius)) {
            forwardHemisphere.Add(nextPosition);
		}
	}

	bool IsInSphere(Vector3 startPosition, Vector3 nextPosition, float radius) {
		float distance = (startPosition - nextPosition).magnitude;
		return distance <= radius;
	}

	bool IsOnSphereSurface(Vector3 startPosition, Vector3 nextPosition, float radius) {
		float distance = (startPosition - nextPosition).magnitude;
		//return (distance <= radius && distance > radius - 1f);
        return Mathf.Ceil(distance) == radius;
	}

	bool IsInOctahedron(Vector3 startPosition, Vector3 nextPosition, float radius) {
		Vector3 change = startPosition - nextPosition;
		float x = Mathf.Abs(change.x);
		float y = Mathf.Abs(change.y);
		float z = Mathf.Abs(change.z);
		return (x + y + z) <= radius;
	}

	bool IsOnOctahedronSurface(Vector3 startPosition, Vector3 nextPosition, float radius) {
		Vector3 change = startPosition - nextPosition;
		float x = Mathf.Abs(change.x);
		float y = Mathf.Abs(change.y);
		float z = Mathf.Abs(change.z);
		return (x + y + z) == radius;
	}

    bool IsInCube(Vector3 startPosition, Vector3 nextPosition, float radius) {
        Vector3 change = startPosition - nextPosition;
        float x = Mathf.Abs(change.x);
        float y = Mathf.Abs(change.y);
        float z = Mathf.Abs(change.z);
        return x <= radius && y <= radius && z <= radius;
    }

    bool IsOnCubeSurface(Vector3 startPosition, Vector3 nextPosition, float radius) {
        Vector3 change = startPosition - nextPosition;
        float x = Mathf.Abs(change.x);
        float y = Mathf.Abs(change.y);
        float z = Mathf.Abs(change.z);
        return x == radius && y == radius && z == radius;
    }
}
