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
        ViewService.LoadAsset(contexts, starEntity, GameControllerMono.starAssetName);
        starEntity.ReplaceMass(settings.starMass);
        starEntity.ReplacePosition(Vector2.zero);
        starEntity.ReplaceVelocity(Vector2.zero);
        starEntity.ReplaceRadius(5f);
        starEntity.isCollideable = true;

        //Planet(s)
        var planetSettings = settings.planetSettings;
        if(planetSettings == null || planetSettings.Length <= 0) return;
        var sectorAngle = 360f / planetSettings.Length;
        for(int i = 0; i < planetSettings.Length; i++) {
            var currentPlanetSettings = planetSettings[i];
            var startOnPeriapsis = true;

            //Calculate orbit ellipse
            var orbitEccentricity = (settings.overrideOrbitEccentricity)
                ? settings.orbitEccentricity
                : currentPlanetSettings.orbitEccentricity;
            var orbitSemiMajorAxis = settings.firstOrbitSemiMajorAxis + (i * settings.orbitSemiMajorAxisIncrement);
            var orbitFocusDistanceFromCenter = PhysicsService.OrbitFocusPosition(orbitSemiMajorAxis, orbitEccentricity);
            var planetInitialDistanceFromFocus = (startOnPeriapsis)
                ? orbitSemiMajorAxis - orbitFocusDistanceFromCenter
                : orbitSemiMajorAxis + orbitFocusDistanceFromCenter;

            //Calculate initial values
            var planetInitialPosition = (settings.placePlanetOnDifferentSectors)
                ? starEntity.position.value + (Vector2)(Quaternion.Euler(0f, 0f, sectorAngle * i)
                    * (Vector2.up * planetInitialDistanceFromFocus))
                : starEntity.position.value + (Vector2.up * planetInitialDistanceFromFocus);
            var directionFromOrbitCenter = planetInitialPosition - starEntity.position.value;
            var orbitalVelocity = PhysicsService.OrbitalVelocity(settings.starMass + currentPlanetSettings.mass,
                planetInitialDistanceFromFocus, orbitEccentricity, startOnPeriapsis);
            var planetInitialVelocity = Quaternion.Euler(0f, 0f, 90f) * (directionFromOrbitCenter.normalized * orbitalVelocity);
            var orbitalPeriod = PhysicsService.OrbitalPeriod(orbitSemiMajorAxis, settings.starMass);

            //Create planet
            var planetEntity = contexts.game.CreateEntity();
            ViewService.LoadAsset(contexts, planetEntity, GameControllerMono.planetAssetName, planetInitialPosition);
            planetEntity.ReplaceColor(currentPlanetSettings.color);
            planetEntity.ReplaceRadius(currentPlanetSettings.scale);
            planetEntity.ReplaceMass(currentPlanetSettings.mass);
            planetEntity.ReplacePosition(planetInitialPosition);
            planetEntity.ReplaceVelocity(planetInitialVelocity);
            planetEntity.ReplaceOrbitalPeriod(orbitalPeriod);
            planetEntity.isCollideable = true;

            if(i == 2) {
                planetEntity.isPlayerControlledShooter = true;
            }
        }
    }
}
