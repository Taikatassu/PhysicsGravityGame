using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHelper : MonoBehaviour {

    public static readonly float gravitationalConstant = 0.0000000000667408f; //6.67408 * 10^-11
    //public static readonly float kEpsilon = 0.00001f;

    //Gravitational force:
    //F = G*(m1*m2) / r
    public static Vector2 GravitationalPotential(float mass1, float mass2, Vector2 position1, Vector2 position2) {
        var direction = position2 - position1;
        var distance = direction.magnitude;
        var normalizedDirection = direction / distance;
        return GravitationalPotential(mass1, mass2, distance) * normalizedDirection;
    }

    public static float GravitationalPotential(float mass1, float mass2, float distance) {
        return gravitationalConstant * ((mass1 * mass2) / distance);
    }

    //Energy from velocity:
    //E = 0.5 * m * v^2
    public static Vector2 KinematicEnergy(float mass, Vector2 velocity) {
        return new Vector2(KinematicEnergy(mass, velocity.x), KinematicEnergy(mass, velocity.y));
    }

    public static float KinematicEnergy(float mass, float velocity) {

        if(velocity < 0f)
            return 0.5f * mass * -Mathf.Pow(velocity, 2);

        return 0.5f * mass * Mathf.Pow(velocity, 2);
    }

    //Velocity from energy:
    //v = Sqrt(E / (0.5 * m))
    public static Vector2 EnergyToVelocity(float mass, Vector2 energy) {
        return new Vector2(EnergyToVelocity(mass, energy.x), EnergyToVelocity(mass, energy.y));
    }

    public static float EnergyToVelocity(float mass, float energy) {
        if(energy < 0f)
            return -Mathf.Sqrt(Mathf.Abs(energy / (0.5f * mass)));

        return Mathf.Sqrt(energy / (0.5f * mass));
    }

    //Orbital velocity:
    //v = Sqrt(G * m / r)
    public static float OrbitalVelocity(float mass, float orbitRadius) {
        return Mathf.Sqrt((gravitationalConstant * mass) / orbitRadius);
    }

    //private static float ClampAboveZero(float value) {
    //    return Mathf.Clamp(value, kEpsilon, float.MaxValue);
    //}

}
