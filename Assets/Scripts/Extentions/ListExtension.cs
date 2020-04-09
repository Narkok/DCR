using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public static class ListExtention {
    public static Vector3 Closest(this List<Vector3> list, Vector3 point) {
        if (!list.Any()) { return new Vector3(); }
        Vector3 closestPoint = list[0];
        float distance = Vector3.Distance(point, closestPoint);
        for (int i = 1; i < list.Count; i++) {
            float newDistance = Vector3.Distance(point, list[i]);
            if (newDistance < distance) {
                distance = newDistance;
                closestPoint = list[i];
            }
        }
        return closestPoint;
    }
}
