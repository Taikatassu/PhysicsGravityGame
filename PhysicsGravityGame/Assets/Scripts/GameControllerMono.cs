using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameControllerMono : MonoBehaviour {

    public static readonly string starAssetName = "Star";
    public static readonly string planetAssetName = "Planet";
    public static readonly string projectileAssetName = "Projectile";
    public static readonly string blackHoleAssetName = "BlackHole";

    public CelestialConfig celestialConfig;

    [Range(0.1f, 100f)]
    public float timeScale;

    private Systems inputSystems;
    private Systems gameSystems;
    private Contexts contexts;

    private void Start() {
        contexts = Contexts.sharedInstance;
        var celestialBodySettings = celestialConfig.celestialBodySettings;
        contexts.meta.ReplaceCelestialBodySettings(celestialBodySettings);

        inputSystems = CreateInputSystems(contexts);
        inputSystems.Initialize();
        gameSystems = CreateGameSystems(contexts);
        gameSystems.Initialize();

        Time.timeScale = timeScale;
    }

    private void Update() {
        inputSystems.Execute();
        inputSystems.Cleanup();
    }

    private void FixedUpdate() {
        gameSystems.Execute();
        gameSystems.Cleanup();
    }

    public static Systems CreateGameSystems(Contexts contexts) {
        return new Feature("GameSystems")
            .Add(new CelestialBodyInitializationSystem(contexts))
            .Add(new ShootingSystem(contexts))
            .Add(new GravitySystem(contexts))
            .Add(new CollisionDetectionSystem(contexts))
            .Add(new ApplyVelocitySystem(contexts))
            //.Add(new HealthExpirationSystem(contexts))
            .Add(new GameEventSystems(contexts))
            .Add(new CleanupGameEntitiesSystem(contexts))
            ;
    }

    public static Systems CreateInputSystems(Contexts contexts) {
        return new Feature("InputSystems")
            .Add(new EmitInputSystem(contexts))
            .Add(new HandleInputSystem(contexts))
            ;
    }
}
