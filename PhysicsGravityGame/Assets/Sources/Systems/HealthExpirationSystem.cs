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
        foreach(var e in entities) {
            if(e.health.value <= 0f) e.isDestroyed = true;
        }
    }
}
