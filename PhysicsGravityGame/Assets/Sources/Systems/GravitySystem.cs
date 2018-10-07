using UnityEngine;
using Entitas;

public class GravitySystem : IExecuteSystem {
    
    private IGroup<GameEntity> gravityEntities;

    public GravitySystem(Contexts contexts) {
        gravityEntities = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.Mass,
            GameMatcher.Velocity,
            GameMatcher.Position
            ));
    }

    public void Execute() {
        foreach (var e in gravityEntities) {
            Vector2 entityVelocity = e.velocity.value;
            Vector2 entityPosition = e.position.value;
            float entityMass = e.mass.value;
            foreach (var other in gravityEntities) {
                if (other == e) continue;
                if (entityMass > other.mass.value) continue;
                
                entityVelocity += PhysicsService.GravitationalPotentialVector(entityMass, other.mass.value, 
                    entityPosition, other.position.value) * Time.deltaTime;             
            }

            e.ReplaceVelocity(entityVelocity);
        }
    }
}
