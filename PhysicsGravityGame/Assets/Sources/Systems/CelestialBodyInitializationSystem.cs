using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using UnityEngine.UI;

public class CelestialBodyInitializationSystem : IInitializeSystem {

    private Contexts contexts;
    public CelestialBodyInitializationSystem(Contexts contexts) {
        this.contexts = contexts;
    }

    public void Initialize() {
        var settings = contexts.meta.celestialBodySettings.value;

        //Star
        var starEntity = contexts.game.CreateEntity();
        starEntity.ReplaceMass(settings.starMass);
        starEntity.ReplacePosition(Vector2.zero);
        starEntity.ReplaceVelocity(Vector2.zero);
        starEntity.ReplaceRadius(10f);
        ViewService.LoadAsset(contexts, starEntity, GameControllerMono.starAssetName);

        //Planet(s)
        var planetSettings = settings.planetSettings;
        if (planetSettings == null || planetSettings.Length <= 0) return;
        var sectorAngle = 360f / planetSettings.Length;
        for (int i = 0; i < planetSettings.Length; i++) {
            var currentPlanetSettings = planetSettings[i];
            var startOnPeriapsis = true;


            var orbitEccentricity = (settings.overrideOrbitEccentricity)
                ? settings.orbitEccentricity
                : currentPlanetSettings.orbitEccentricity;
            var orbitSemiMajorAxis = settings.firstOrbitSemiMajorAxis + (i * settings.orbitSemiMajorAxisIncrement);
            var orbitFocusDistanceFromCenter = PhysicsService.OrbitFocusPosition(orbitSemiMajorAxis, orbitEccentricity);
            var planetInitialDistanceFromFocus = (startOnPeriapsis)
                ? orbitSemiMajorAxis - orbitFocusDistanceFromCenter
                : orbitSemiMajorAxis + orbitFocusDistanceFromCenter;

            var planetInitialPosition = (settings.placePlanetOnDifferentSectors)
                ? starEntity.position.value + (Vector2)(Quaternion.Euler(0f, 0f, sectorAngle * i)
                    * (Vector2.up * planetInitialDistanceFromFocus))
                : starEntity.position.value + (Vector2.up * planetInitialDistanceFromFocus);

            var orbitalVelocity = PhysicsService.OrbitalVelocity(settings.starMass + currentPlanetSettings.mass,
                planetInitialDistanceFromFocus, orbitEccentricity, startOnPeriapsis);
            var directionFromOrbitCenter = planetInitialPosition - starEntity.position.value;
            var planetInitialVelocity = Quaternion.Euler(0f, 0f, 90f) * (directionFromOrbitCenter.normalized * orbitalVelocity);

            var planetEntity = contexts.game.CreateEntity();
            planetEntity.ReplaceMass(currentPlanetSettings.mass);
            planetEntity.ReplacePosition(planetInitialPosition);
            planetEntity.ReplaceVelocity(planetInitialVelocity);
            planetEntity.ReplaceRadius(currentPlanetSettings.scale);

            if (i == 2) {
                planetEntity.isPlayerControlledShooter = true;
            }

            //TODO: Initialize unity gameObject component values elsewhere
            //          Create components and listeners for specific values, like trail color / time, etc.
            var planetAsset = ViewService.LoadAsset(contexts, planetEntity, GameControllerMono.planetAssetName, planetInitialPosition);

            planetAsset.GetComponentInChildren<SpriteRenderer>().color = currentPlanetSettings.color;
            planetAsset.transform.localScale = Vector3.one * currentPlanetSettings.scale;
            var planetTrail = planetAsset.GetComponent<TrailRenderer>();
            planetTrail.startWidth = currentPlanetSettings.scale;
            planetTrail.material.color = currentPlanetSettings.color;

            var orbitalPeriod = PhysicsService.OrbitalPeriod(orbitSemiMajorAxis, settings.starMass);
            planetTrail.time = orbitalPeriod;
        }
    }
}
