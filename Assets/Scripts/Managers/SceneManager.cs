﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManager: MonoBehaviour {

    [SerializeField] public ArenaType arenaType;

    private static SceneManager _shared;
    public static SceneManager Shared { get { return _shared; } }

    private Arena _arena;
    public Arena Arena  { get { return _arena; } }

    [HideInInspector] public Transform EquipmentContainer;
    [HideInInspector] public Transform VehicleContainer;
    [HideInInspector] public Transform BlumbContainer;
    [HideInInspector] public Transform AmmoContainer;

    private GameCamera _camera;

    [SerializeField] Vehicle.Data[] vehiclesInfo;
    private List<Vehicle> _vehicles = new List<Vehicle>();
    private List<Stuff> _stuff = new List<Stuff>();
    private List<Equipment> _equipment = new List<Equipment>();


    private void Awake() {
        _shared = this;
        _arena = GOManager.Create(arenaType.ArenaPath()).GetComponent<Arena>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>();

        EquipmentContainer = new GameObject("Equipments").transform;
        VehicleContainer = new GameObject("Vehicles").transform;
        BlumbContainer = new GameObject("Blumbs").transform;
        AmmoContainer = new GameObject("Ammo").transform;

        GenerateEquipment();
        GenerateVehicles();
        GenerateStuff();
    }

    
    /// Генерация машин на арене
    private void GenerateVehicles() {
        _vehicles.Clear();
        for (int i = 0; i < vehiclesInfo.Length; i++) {
            Vehicle.Data data = vehiclesInfo[i];
            Arena.Location location = _arena.RandomLocation;
            // Vehicle vehicle = GOManager.Create(data.vehicle.Path(), VehicleContainer).GetComponent<Vehicle>();
            Vehicle vehicle = GOManager.Create(data.vehicle.Path()).GetComponent<Vehicle>();
            vehicle.Setup(data, location);
            if (data.isPlayer) { _camera.Set(vehicle.transform); }
            _vehicles.Add(vehicle);
        }
    }


    /// Генерация оружий на арене
    private void GenerateEquipment() {
        _equipment.Clear();
        for (int i = 0; i < arenaType.EquipmentCount(); i++) {
            Arena.Location location = _arena.RandomLocation;
            WeaponType wt = WeaponTypeExtension.RandomWeapon();
            Equipment equipment = GOManager.Create(wt.EquipmentPath(), EquipmentContainer).GetComponent<Equipment>();
            equipment.Setup(location, wt);
            _equipment.Add(equipment);
        }
    }


    /// Генерация всякой фигни, которую можно подобрать на арене
    private void GenerateStuff() {
        _stuff.Clear();
    }
}
