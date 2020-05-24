using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ControlType {
    /// Прямое управление игроком
    Player,
    /// Управление ИИ контроллером
    AI,
    /// Внешнее управление через сеть
    External
}


public static class ControlExtension {
    public static bool isPlayer(this ControlType controlType) {
        return controlType == ControlType.Player;
    }

    public static bool isAI(this ControlType controlType) {
        return controlType == ControlType.AI;
    }

    public static bool isExternal(this ControlType controlType) {
        return controlType == ControlType.External;
    }
}
