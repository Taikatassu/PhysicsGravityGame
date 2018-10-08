﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsService {

    private static readonly float gravitationalConstant = 0.000667f;
    private static readonly float kEpsilon = 0.00001f;

    private static float ClampAboveZero(float value) {
        return Mathf.Clamp(value, kEpsilon, float.MaxValue);
    }

    //Orbital velocity:
    //v = Sqrt(G * m / r)
    public static float OrbitalVelocity(float mass, float distanceFromOrbitCenterOfMass, float orbitEccentricity, bool startOnPeriapsis) {
        distanceFromOrbitCenterOfMass = ClampAboveZero(distanceFromOrbitCenterOfMass);
        var eccentricityMultiplier = (startOnPeriapsis)
            ? 1 + orbitEccentricity
            : 1 - orbitEccentricity;
        return Mathf.Sqrt((gravitationalConstant * mass) / distanceFromOrbitCenterOfMass * eccentricityMultiplier);
    }

    //Gravitational potential:
    //https://physics.stackexchange.com/questions/52566/how-to-calculate-linar-velocity-of-planet-orbit
    //        _____________________
    //       / G(M + m)
    // v =  /___________  * (1 + e)     , where a is then angle between velocity vector and the radius and e is eccentricity
    //    \/   r sin(a)
    public static float GravitationalPotentialMagnitude(float mass1, float mass2, float distance) {

        return (gravitationalConstant * (mass1 + mass2)) / Mathf.Pow(distance, 2);
    }

    public static Vector2 GravitationalPotentialVector(float mass1, float mass2, Vector2 position1, Vector2 position2) {
        var direction = position2 - position1;
        var distance = direction.magnitude;
        var normalizedDirection = direction / distance;

        return GravitationalPotentialMagnitude(mass1, mass2, distance) * normalizedDirection;
    }

    public static float OrbitalPeriod(float semiMajorAxis, float mass) {
        return Mathf.Sqrt(Mathf.Pow(semiMajorAxis, 3) / (gravitationalConstant * mass)) * 2 * Mathf.PI;
    }

    public static float OrbitFocusPosition(float semiMajorAxis, float eccentricity) {
        var semiMinorAxis = Mathf.Sqrt(Mathf.Pow(semiMajorAxis, 2) - (Mathf.Pow(eccentricity, 2) * Mathf.Pow(semiMajorAxis, 2)));
        return Mathf.Sqrt(Mathf.Pow(semiMajorAxis, 2) - Mathf.Pow(semiMinorAxis, 2));
    }
}
