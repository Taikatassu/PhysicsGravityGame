using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CollisionDetectionSystem : IExecuteSystem {

    private IGroup<GameEntity> collideables;
    private Contexts contexts;

    public CollisionDetectionSystem(Contexts contexts) {
        this.contexts = contexts;

        collideables = contexts.game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Collideable,
                GameMatcher.Position,
                GameMatcher.Velocity,
                GameMatcher.Mass,
                GameMatcher.Radius
            ));
    }

    public void Execute() {
        foreach(var e in collideables) {

            var castOrigin = e.position.value;
            var castDirection = e.velocity.value * Time.fixedDeltaTime;
            var castRadius = e.radius.value;

            var circleCastDatas = PhysicsService.CircleCastAll(castOrigin, castRadius, castDirection, castDirection.magnitude, e);
            if(circleCastDatas.Length > 0) {
                foreach(var data in circleCastDatas) {
                    if(data.entity.mass.value > e.mass.value) continue;

                    data.entity.isDestroyed = true;
                    ViewService.LoadAsset(contexts, null, "CollisionEffectTest", data.point);

                    //DamageService.DealDamageOnCollision(e, data.entity);
                }
            }
        }
    }
}
