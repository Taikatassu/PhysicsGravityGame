using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CelestialConfig")]
public class CelestialConfig : ScriptableObject {
    public string configName;
    public CelestialBodySettings celestialBodySettings;
}
