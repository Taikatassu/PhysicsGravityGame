using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameControllerMono : MonoBehaviour {

    public static readonly string starAssetName = "Star";
    public static readonly string planetAssetName = "Planet";
    public static readonly string projectileAssetName = "Projectile";
    public static readonly string blackHoleAssetName = "BlackHole";
    public static readonly int debrisCount = 5;
    public static readonly int debrisMaxGeneration = 3;
    public static readonly float debrisSpawnDistance = 1f;
    public static readonly float debrisSizeMultiplier = 0.75f;
    public static readonly float debrisVelocityMultiplier = 1f;
    public static readonly bool debrisFinalGenerationPersistant = true;

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
            //TODO: Implement shooting direction visualizer
            .Add(new ShootingSystem(contexts))
            .Add(new GravitySystem(contexts))
            .Add(new CollisionDetectionSystem(contexts))
            .Add(new ApplyVelocitySystem(contexts))
            .Add(new HealthExpirationSystem(contexts))
            //TODO: Implement health visualization system (requires "max health" component)
            //TODO: Implement score system (how many planets left / destroyed)
            //TODO: Implement game state system (end / restart game once the player planet or all the other planets have been destroyed)
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
