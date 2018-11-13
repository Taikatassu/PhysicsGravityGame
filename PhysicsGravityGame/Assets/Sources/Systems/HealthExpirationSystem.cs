using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class HealthExpirationSystem : ReactiveSystem<GameEntity> {
    private Contexts contexts;

    public HealthExpirationSystem(Contexts contexts) : base(contexts.game) {
        this.contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Health);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasHealth && !entity.isDestroyed;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (var e in entities) {
            if (e.health.value <= 0f) {

                e.isDestroyed = true;

                if (!e.hasPosition || !e.hasColor || !e.hasRadius || !e.hasVelocity || !e.hasMass) continue;

                var debrisCount = GameControllerMono.debrisCount;

                var debrisGeneration = (e.hasDebrisGeneration)
                    ? e.debrisGeneration.value + 1
                    : 1;
                debrisCount = Mathf.Max(1, debrisCount / debrisGeneration);

                if (debrisGeneration > GameControllerMono.debrisMaxGeneration
                    && !GameControllerMono.debrisFinalGenerationPersistant) {
                    continue;
                }

                var sectorAngle = 360f / debrisCount;
                var parentPosition = e.position.value;
                var debrisColor = e.color.value;
                var debrisScale = e.radius.value * GameControllerMono.debrisSizeMultiplier;
                var debrisMass = e.mass.value * GameControllerMono.debrisSizeMultiplier;
                var parentVelocity = e.velocity.value;
                var parentVelocityMagnitude = parentVelocity.magnitude;

                for (int i = 0; i < debrisCount; i++) {
                    var debrisSpawnPosition = parentPosition + (Vector2)(Quaternion.Euler(0f, 0f, sectorAngle * i)
                        * (Vector2.up * GameControllerMono.debrisSpawnDistance));
                    var awayFromParentVector = (debrisSpawnPosition - parentPosition).normalized;
                    var debrisVelocity = awayFromParentVector * parentVelocityMagnitude
                        * GameControllerMono.debrisVelocityMultiplier;

                    var debrisEntity = contexts.game.CreateEntity();
                    ViewService.LoadAsset(contexts, debrisEntity, GameControllerMono.planetAssetName, debrisSpawnPosition);
                    debrisEntity.ReplaceColor(debrisColor);
                    debrisEntity.ReplaceRadius(debrisScale);
                    debrisEntity.ReplaceMass(debrisMass);
                    debrisEntity.ReplacePosition(debrisSpawnPosition);
                    debrisEntity.ReplaceVelocity(debrisVelocity);
                    debrisEntity.isCollideable = true;
                    debrisEntity.AddDebrisGeneration(debrisGeneration);

                    if (!GameControllerMono.debrisFinalGenerationPersistant) {
                        debrisEntity.AddHealth(1f);

                    } else if (debrisGeneration < GameControllerMono.debrisMaxGeneration) {
                        debrisEntity.AddHealth(1f);
                    }

                }
            }
        }
    }
}
