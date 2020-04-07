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


public static class InputManager {

    public static string Throttle = "Vertical";   // W & S
    public static string Break    = "Break";      // SPACE
    public static string Turn     = "Horizontal"; // A & D
    public static string Jump     = "Jump";       // J
    public static string Drift    = "Drift";      // H
	public static string Boost    = "Boost";      // LEFT SHIFT
    public static string LClick   = "Left Click"; // LEFT MOUSE CLICK

    public static float Input(string input) {
        return UnityEngine.Input.GetAxis(input);
    }

    public static bool isActive(string input) {
        return UnityEngine.Input.GetAxis(input) > 0;
    }
}