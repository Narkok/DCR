using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ControlType {
    Player,
    AI,

    /// Удалить после отладки и тестов
    /// Машина управляется игроком, но не считается машиной игрока
    FakePlayer
}


public static class ControlExtension {
    public static bool isPlayer(this ControlType control) {
        return control == ControlType.Player;
    }

    public static bool isAI(this ControlType control) {
        return control == ControlType.AI;
    }
}
