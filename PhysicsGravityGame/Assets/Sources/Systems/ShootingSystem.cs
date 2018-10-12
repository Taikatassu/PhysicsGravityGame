using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class ShootingSystem : ReactiveSystem<GameEntity>, ICleanupSystem {
    private Contexts contexts;
    private IGroup<GameEntity> shooters;

    public ShootingSystem(Contexts contexts) : base(contexts.game) {
        this.contexts = contexts;
        shooters = contexts.game.GetGroup(GameMatcher.AllOf(
               GameMatcher.ShootDirection,
               GameMatcher.Position,
               GameMatcher.Radius
            ));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.ShootDirection);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasShootDirection;
    }

    protected override void Execute(List<GameEntity> entities) {
        var spawnDistanceMultiplier = 1.1f;
        var shootVelocity = 30f;
        var projectileMass = 0.001f;
        var projectileRadius = 0.25f;
        foreach (var e in entities) {
            var spawnPosition = e.position.value + e.shootDirection.value * e.radius.value * spawnDistanceMultiplier;
            var initialVelocity = e.shootDirection.value * shootVelocity;
            var projectileEntity = contexts.game.CreateEntity();
            ViewService.LoadAsset(contexts, projectileEntity, GameControllerMono.projectileAssetName, spawnPosition);
            projectileEntity.ReplacePosition(spawnPosition);
            projectileEntity.ReplaceVelocity(initialVelocity);
            projectileEntity.ReplaceMass(projectileMass);
            projectileEntity.ReplaceRadius(projectileRadius);
        
        }
    }

    public void Cleanup() {
        foreach (var e in shooters.GetEntities()) {
            e.RemoveShootDirection();
        }
    }
}
