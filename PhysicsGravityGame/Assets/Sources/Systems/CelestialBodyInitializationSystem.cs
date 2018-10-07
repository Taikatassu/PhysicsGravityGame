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
        //var starMass = 198900f;
        var starEntity = contexts.game.CreateEntity();
        starEntity.ReplaceMass(settings.starMass);
        starEntity.ReplacePosition(Vector2.one * 50f);
        starEntity.ReplaceVelocity(Vector2.zero);
        ViewService.LoadAsset(contexts, starEntity, GameControllerMono.starAssetName);

        //Planet(s)
        var planetSettings = settings.planetSettings;
        if (planetSettings == null || planetSettings.Length <= 0) return;
        //var planetMass = 0.5974f;
        var sectorAngle = 360f / planetSettings.Length;
        for (int i = 0; i < planetSettings.Length; i++) {
            var currentPlanetSettings = planetSettings[i];
            var planetOrbitRadius = settings.firstOrbitRadius + (i * settings.orbitRadiusIncrement);
            var planetInitialPosition = (settings.placePlanetOnDifferentSectors)
                ? starEntity.position.value + (Vector2)(Quaternion.Euler(0f, 0f, sectorAngle * i)
                    * (Vector2.up * planetOrbitRadius))
                : starEntity.position.value + (Vector2.up * planetOrbitRadius);

            var orbitEccentricity = (settings.overrideOrbitEccentricity)
                ? settings.orbitEccentricity
                : currentPlanetSettings.orbitEccentricity;
            var orbitalVelocity = PhysicsService.OrbitalVelocity(settings.starMass + currentPlanetSettings.mass,
                planetOrbitRadius, orbitEccentricity);
            var directionFromOrbitCenter = planetInitialPosition - starEntity.position.value;
            var planetInitialVelocity = Quaternion.Euler(0f, 0f, 90f) * (directionFromOrbitCenter.normalized * orbitalVelocity);

            var planetEntity = contexts.game.CreateEntity();
            planetEntity.ReplaceMass(currentPlanetSettings.mass);
            planetEntity.ReplacePosition(planetInitialPosition);
            planetEntity.ReplaceVelocity(planetInitialVelocity);
            var planetObject = ViewService.LoadAsset(contexts, planetEntity, GameControllerMono.planetAssetName, planetInitialPosition);

            planetObject.GetComponentInChildren<SpriteRenderer>().color = currentPlanetSettings.color;
            planetObject.transform.localScale = Vector3.one * currentPlanetSettings.scale;
            var planetTrail = planetObject.GetComponent<TrailRenderer>();
            planetTrail.startWidth = currentPlanetSettings.scale * 1.5f;
            planetTrail.material.color = currentPlanetSettings.color;

            //TODO: This does not currently work for non-circular orbits. "planetOrbitRadius" is not the semi-major axis!!
            //          Find out how to determine semi-major axis form existing information!
            var orbitalPeriod = PhysicsService.OrbitalPeriod(planetOrbitRadius, settings.starMass);
            planetTrail.time = orbitalPeriod;
        }
    }
}
