using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameControllerMono : MonoBehaviour {

    public static readonly string starAssetName = "Star";
    public static readonly string planetAssetName = "Planet";

    public CelestialConfig celestialConfig;

    [Range(0.1f, 100f)]
    public float timeScale;

    private Systems gameSystems;
    private Contexts contexts;

    private void Start() {
        contexts = Contexts.sharedInstance;
        var celestialBodySettings = celestialConfig.celestialBodySettings;
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
