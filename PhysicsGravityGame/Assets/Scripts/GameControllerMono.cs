using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameControllerMono : MonoBehaviour {

    public static readonly string starAssetName = "Star";
    public static readonly string planetAssetName = "Planet";


    public PlanetSettings[] planetSettings;
    public float starMass;
    public float orbitRadiusIncrement;
    public float firstOrbitRadius;
    public float orbitEccentricity;
    public bool placePlanetOnDifferentSectors;
    public bool overrideOrbitEccentricity;

    [Range(0.1f, 100f)]
    public float timeScale;

    private Systems gameSystems;
    private Contexts contexts;

    private void Start() {
        contexts = Contexts.sharedInstance;
        var celestialBodySettings = new CelestialBodySettings {
            planetSettings = planetSettings,
            starMass = starMass,
            orbitRadiusIncrement = orbitRadiusIncrement,
            firstOrbitRadius = firstOrbitRadius,
            orbitEccentricity = orbitEccentricity,
            placePlanetOnDifferentSectors = placePlanetOnDifferentSectors,
            overrideOrbitEccentricity = overrideOrbitEccentricity
        };
        contexts.meta.ReplaceCelestialBodySettings(celestialBodySettings);

        gameSystems = CreateGameSystems(contexts);
        gameSystems.Initialize();

        Time.timeScale = timeScale;
    }

    private void FixedUpdate() {
        gameSystems.Execute();
        gameSystems.Cleanup();
    }

    public static Systems CreateGameSystems(Contexts contexts) {
        return new Feature("GameSystems")
            .Add(new CelestialBodyInitializationSystem(contexts))
            .Add(new GravitySystem(contexts))
            .Add(new ApplyVelocitySystem(contexts))
            .Add(new GameEventSystems(contexts))
            ;
    }
}
