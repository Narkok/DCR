using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager: MonoBehaviour {

    public class Line {
        public Vector3 start;
        public Vector3 end;

        public Line(Vector3 start, Vector3 end) {
            this.start = start;
            this.end = end;
        }
    }

    public class Direction {
        public Vector3 start;
        public Vector3 dir;

        public Direction(Vector3 start, Vector3 dir) {
            this.start = start;
            this.dir = dir;
        }
    }

    public static List<Vector3> points = new List<Vector3>();
    public static List<Line> lines= new List<Line>();
    public static List<Direction> directions = new List<Direction>();

    [SerializeField] public float pointRadius = 0.1f;
    [SerializeField] public Color pointColor = Color.white;

    [SerializeField] public Color lineColor = Color.red;
    [SerializeField] public Color linePointsColor = Color.magenta;

    [SerializeField] public Color directionColor = Color.blue;
    [SerializeField] public Color directionStartColor = Color.cyan;


    public static void AddDirection(Vector3 start, Vector3 dir) {
        directions.Add(new Direction(start, dir));
    }


    public static void ShowDirection(Vector3 start, Vector3 dir) {
        directions.Clear();
        directions.Add(new Direction(start, dir));
    }


    public static void AddLine(Vector3 start, Vector3 end) {
        lines.Add(new Line(start, end));
    }


    public static void ShowLine(Vector3 start, Vector3 end) {
        lines.Clear();
        lines.Add(new Line(start, end));
    }


    public static void AddPoint(Vector3 point) {
        points.Add(point);
    }


    public static void ShowPoint(Vector3 point) {
        points.Clear();
        points.Add(point);
    }


    private void OnDrawGizmos() {
        Gizmos.color = pointColor;
        foreach (Vector3 point in DebugManager.points) {
            Gizmos.DrawSphere(point, pointRadius);
        }

        foreach (Line line in DebugManager.lines) {
            Gizmos.color = lineColor;
            Gizmos.DrawLine(line.start, line.end);
            Gizmos.color = linePointsColor;
            Gizmos.DrawSphere(line.start, pointRadius);
            Gizmos.DrawSphere(line.end, pointRadius);
        }

        foreach (Direction direction in DebugManager.directions) {
            Gizmos.color = directionColor;
            Gizmos.DrawLine(direction.start, direction.start + direction.dir);
            Gizmos.color = directionStartColor;
            Gizmos.DrawSphere(direction.start, pointRadius);
        }

    }
}
