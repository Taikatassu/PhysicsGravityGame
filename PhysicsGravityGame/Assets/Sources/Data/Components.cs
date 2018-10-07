using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Event(EventTarget.Self)]
public class PositionComponent : IComponent {
    public Vector2 value;
}

[Game]
public class VelocityComponent : IComponent {
    public Vector2 value;
}

[Game]
public class MassComponent : IComponent {
    public float value;
}

[Game]
public class ForceComponent : IComponent {
    public Vector2 force;
}

[Meta, Unique]
public class CelestialBodySettingsComponent : IComponent {
    public CelestialBodySettings value;
}

[System.Serializable]
public struct CelestialBodySettings {
    public PlanetSettings[] planetSettings;
    public float starMass;
    public float orbitRadiusIncrement;
    public float firstOrbitRadius;
    public float orbitEccentricity;
    public bool placePlanetOnDifferentSectors;
    public bool overrideOrbitEccentricity;
}

[System.Serializable]
public struct PlanetSettings {
    public string name;
    public Color color;
    public float mass;
    public float scale;
    public float orbitEccentricity;
}