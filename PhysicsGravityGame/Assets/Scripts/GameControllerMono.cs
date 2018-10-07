using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameControllerMono : MonoBehaviour {

    public static readonly string starAssetName = "Star";
    public static readonly string planetAssetName = "Planet";


    private Systems gameSystems;
    private Contexts contexts;

    private void Start() {
        contexts = Contexts.sharedInstance;
        gameSystems = CreateGameSystems(contexts);
        gameSystems.Initialize();

        Time.timeScale = 1f;
    }

    private void FixedUpdate() {
        gameSystems.Execute();
        gameSystems.Cleanup();
    }

    public static Systems CreateGameSystems(Contexts contexts) {
        return new Feature("GameSystems")
            .Add(new GravitySystem(contexts))
            .Add(new ApplyVelocitySystem(contexts))
            .Add(new GameEventSystems(contexts))
            ;
    }
}
