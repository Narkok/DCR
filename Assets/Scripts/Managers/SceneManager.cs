using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SceneManager: MonoBehaviour {

    [SerializeField] private GameCanvasManager canvas;

    [SerializeField] public ArenaType arenaType;

    private static SceneManager _shared;
    public static SceneManager Shared { get { return _shared; } }

    private Arena _arena;
    public Arena Arena  { get { return _arena; } }

    [HideInInspector] public Transform ExplosionContainer;
    [HideInInspector] public Transform EquipmentContainer;
    [HideInInspector] public Transform VehicleContainer;
    [HideInInspector] public Transform BlumbContainer;
    [HideInInspector] public Transform StuffContainer;
    [HideInInspector] public Transform AmmoContainer;

    private GameCamera _camera;

    [SerializeField] List<Vehicle.Data> vehiclesInfo;

    private List<Stuff> _stuff = new List<Stuff>();
    public List<Stuff> Stuff { get { return _stuff; } }

    private List<Vehicle> _vehicles = new List<Vehicle>();
    public List<Vehicle> Vehicles { get { return _vehicles; } }

    private List<Equipment> _equipment = new List<Equipment>();
    public List<Equipment> Equipment { get { return _equipment; } }

    private int _followingVehicleNum;


    private void Awake() {
        _shared = this;
        _arena = GOManager.Create(arenaType.ArenaPath()).GetComponent<Arena>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>();

        ExplosionContainer = new GameObject("Explosions").transform;
        EquipmentContainer = new GameObject("Equipments").transform;
        VehicleContainer = new GameObject("Vehicles").transform;
        BlumbContainer = new GameObject("Blumbs").transform;
        StuffContainer = new GameObject("Stuff").transform;
        AmmoContainer = new GameObject("Ammo").transform;

        GenerateEquipment();
        GenerateVehicles();
        GenerateStuff();

        canvas.Setup();
    }

    
    /// Генерация машин на арене
    private void GenerateVehicles() {
        _vehicles.Clear();
        for (int i = 0; i < vehiclesInfo.Count; i++) {
            Vehicle.Data vehicleData = vehiclesInfo[i];
            Arena.Location location = _arena.RandomLocation;
            Vehicle vehicle = GOManager.Create(vehicleData.vehicleType.Path(), VehicleContainer).GetComponent<Vehicle>();
            vehicle.Setup(vehicleData, location, i);
            _vehicles.Add(vehicle);
            
            if (vehicleData.controlType.isPlayer()) { 
                _followingVehicleNum = i;
                _camera.Target = vehicle.GetComponent<CameraTarget>();
                _camera.transform.rotation = vehicle.transform.rotation;
                _camera.transform.position = 20 * (vehicle.transform.up - vehicle.transform.forward) +  vehicle.transform.position;
            }
        }
    }


    /// Генерация оружий на арене
    private void GenerateEquipment() {
        _equipment.Clear();
        for (int i = 0; i < arenaType.EquipmentCount(); i++) {
            Arena.Location location = _arena.RandomLocation;
            WeaponType wt = WeaponExtension.RandomWeapon();
            Equipment equipment = GOManager.Create(wt.EquipmentPath(), EquipmentContainer).GetComponent<Equipment>();
            equipment.Setup(location, wt);
            _equipment.Add(equipment);
        }
    }


    /// Генерация всякой фигни, которую можно подобрать на арене
    private void GenerateStuff() {
        _stuff.Clear();
        for (int i = 0; i < arenaType.StuffCount(); i++) {
            Arena.Location location = _arena.RandomLocation;
            StuffType st = StuffExtention.RandomStuff();
            Stuff stuff = GOManager.Create(st.Path(), StuffContainer).GetComponent<Stuff>();
            stuff.Setup(location, st);
            _stuff.Add(stuff);
        }
    }


    /// Удаление подобранного оружия
    public void Destroy(Equipment equipment) {
        _equipment.Remove(equipment);
        Destroy(equipment.gameObject);
    }


    /// Удаление подобранного бонуса
    public void Destroy(Stuff stuff) {
        _stuff.Remove(stuff);
        Destroy(stuff.gameObject);
    }


    /// Удаление уничтоженной машины со сцены
    public void Destroy(Vehicle vehicle) {
        _vehicles.Remove(vehicle);
        Destroy(vehicle.gameObject);
    }


    private void Update() {
        //if (Input.GetKeyDown(KeyCode.L)) {
        //    _followingVehicleNum = (_followingVehicleNum + 1) % Vehicles.Count;
        //    _camera.Target = Vehicles[_followingVehicleNum].GetComponent<CameraTarget>();
        //}
    }
}
