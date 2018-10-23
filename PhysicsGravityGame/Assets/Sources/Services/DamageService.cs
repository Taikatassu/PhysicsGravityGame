using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageService {

    public static void DealDamageOnCollision(GameEntity collidingEntity1, GameEntity collidingEntity2) {

        if(!collidingEntity1.hasMass || !collidingEntity2.hasMass) {
            Debug.LogWarning("Unable to deal damage: at least one of the colliding bodies is missing 'Mass' component!");
            return;
        }

        var velocity1 = (collidingEntity1.hasVelocity)
            ? collidingEntity1.velocity.value
            : Vector2.zero;
        var velocity2 = (collidingEntity2.hasVelocity)
            ? collidingEntity2.velocity.value
            : Vector2.zero;
        var relativeVelocity = PhysicsService.RelativeVelocityMagnitude(velocity1, velocity2);

        var relativeMass1 = PhysicsService.RelativeMass(collidingEntity1.mass.value, collidingEntity2.mass.value);
        var relativeMass2 = PhysicsService.RelativeMass(collidingEntity2.mass.value, collidingEntity1.mass.value);

        Debug.Log("Damage collision: relativeVelocity:" + relativeVelocity + ", relativeMass1: " + relativeMass1 + ", relativeMass2: " + relativeMass2);

        //TODO: Reduce health by calculated damage value (decide suitable damage alogrithm)

    }

}
