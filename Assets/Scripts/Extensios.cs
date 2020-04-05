using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GOManager {
    public static GameObject Load(string path) {
        return (GameObject)Resources.Load(path);
    }

    public static GameObject Create(string path, Transform parrentTransform = null) {
        return Object.Instantiate((GameObject)Resources.Load(path), parrentTransform);
    }
}
