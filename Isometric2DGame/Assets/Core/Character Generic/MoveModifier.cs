using UnityEngine;
using UnityEngine.UI;

public class MoveModifier : MonoBehaviour
{
    public GroundType groundType = GroundType.none;
}

public enum GroundType
{
    none,
    mud,
    ice
}

