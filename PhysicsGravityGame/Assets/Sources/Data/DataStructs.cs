using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CelestialBodySettings {
    public PlanetSettings[] planetSettings;
    public float starMass;
    public float firstOrbitSemiMajorAxis;
    public float orbitSemiMajorAxisIncrement;
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

public struct CircleCastData {
    public GameEntity entity;
    public Vector2 point;
    public Vector2 normal;
    public Vector3 centroid;
}