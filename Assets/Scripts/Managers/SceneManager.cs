using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManager: MonoBehaviour {

    [SerializeField] public ArenaType arenaType;

    private static SceneManager _shared;
    public static SceneManager Shared { get { return _shared; } }

    private Transform _equipmentContainer;

    private Arena _arena;
    public Arena Arena  { get { return _arena; } }

    private CameraFollow _camera;

    [SerializeField] Vehicle.Data[] vehiclesInfo;
    private List<Vehicle> _vehicles = new List<Vehicle>();
    private List<Stuff> _stuff = new List<Stuff>();
    private List<Equipment> _equipment = new List<Equipment>();


    private void Awake() {
        _shared = this;
        _arena = GOManager.Create(arenaType.ArenaPath()).GetComponent<Arena>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        GenerateVehicles();
        GenerateStuff();
        GenerateEquipment();
    }

    
    /// Генерация машин на арене
    private void GenerateVehicles() {
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


    /// Генерация оружий на арене
    private void GenerateEquipment() {
        GameObject equipmentContainer = new GameObject("Equipments");
        _equipmentContainer = equipmentContainer.transform;

        _equipment.Clear();
        for (int i = 0; i < arenaType.EquipmentCount(); i++) {
            Arena.Location location = _arena.RandomLocation;
            WeaponType wt = WeaponTypeExtension.RandomWeapon();
            Equipment equipment = GOManager.Create(wt.EquipmentPath(), _equipmentContainer).GetComponent<Equipment>();
            equipment.Setup(location, wt);
            _equipment.Add(equipment);
        }
    }


    /// Генерация всякой фигни, которую можно подобрать на арене
    private void GenerateStuff() {
        _stuff.Clear();
    }
}
