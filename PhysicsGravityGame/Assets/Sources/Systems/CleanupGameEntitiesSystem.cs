using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CleanupGameEntitiesSystem : ICleanupSystem {

    private IGroup<GameEntity> destroyedGameEntities;

    public CleanupGameEntitiesSystem(Contexts contexts) {
        destroyedGameEntities = contexts.game.GetGroup(GameMatcher.Destroyed);
    }

    public void Cleanup() {
        foreach (var e in destroyedGameEntities.GetEntities()) {
            e.Destroy();
        }
    }
}
