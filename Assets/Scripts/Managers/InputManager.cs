using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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