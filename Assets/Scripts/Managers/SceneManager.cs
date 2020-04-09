using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManager: MonoBehaviour {

    [SerializeField] public ArenaType arenaType;

    private static SceneManager shared;
    public static SceneManager Shared { get { return shared; } }

    private Arena arena;
    public Arena Arena  { get { return arena; } }


    private void Awake() {
        shared = this;
        InitializeArena();
    }


    private void InitializeArena() {
        GameObject arenaGO = GOManager.Create(arenaType.ArenaPath());
        arena = arenaGO.GetComponent<Arena>();
    }
}

