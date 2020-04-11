using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManager: MonoBehaviour {

    [SerializeField] public ArenaType arenaType;

    private static SceneManager _shared;
    public static SceneManager Shared { get { return _shared; } }

    private Arena _arena;
    public Arena Arena  { get { return _arena; } }


    private void Awake() {
        _shared = this;
        _arena = GOManager.Create(arenaType.ArenaPath()).GetComponent<Arena>();
    }
}

