using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManager: MonoBehaviour {

    [SerializeField] public ArenaType arenaType;

    private static SceneManager _shared;
    public static SceneManager Shared { get { return _shared; } }

    private Arena _arena;
    public Arena Arena  { get { return _arena; } }

    private CameraFollow _camera;

    [SerializeField] Vehicle.Data[] vehiclesInfo;
    private List<Vehicle> _vehicles = new List<Vehicle>();


    private void Awake() {
        _shared = this;
        _arena = GOManager.Create(arenaType.ArenaPath()).GetComponent<Arena>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        SetupScene();
    }


    private void SetupScene() {
        _vehicles.Clear();
        for (int i = 0; i < vehiclesInfo.Length; i++) {
            Vehicle.Data data = vehiclesInfo[i];
            Arena.Location location = _arena.RandomLocation;
            Vehicle vehicle = GOManager.Create(data.vehicle.Path()).GetComponent<Vehicle>();
            vehicle.Setup(data, location);
            if (data.isPlayer) { _camera.Set(vehicle.transform); }
            _vehicles.Add(vehicle);
        }
    }
}

