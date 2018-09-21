using UnityEngine;
using Entitas;

public class ApplyVelocitySystem : IExecuteSystem {

    private IGroup<GameEntity> velocityEntities;

    public ApplyVelocitySystem(Contexts contexts) {
        velocityEntities = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.Velocity,
            GameMatcher.Position
            ));
    }

    public void Execute() {
        foreach(var e in velocityEntities) {
            e.ReplacePosition(e.position.value + e.velocity.value * Time.fixedDeltaTime);
        }
    }

}
