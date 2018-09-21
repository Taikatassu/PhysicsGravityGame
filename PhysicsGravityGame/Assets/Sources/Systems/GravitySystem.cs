using UnityEngine;
using Entitas;

public class GravitySystem : IInitializeSystem, IExecuteSystem {

    private Contexts contexts;
    private IGroup<GameEntity> gravityEntities;

    public GravitySystem(Contexts contexts) {
        this.contexts = contexts;
        gravityEntities = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.Mass,
            GameMatcher.Velocity,
            GameMatcher.Position
            ));
    }

    public void Initialize() {

        //Set mass, initial velocity and position (create separate system?)

        var planetCount = 24;
        var massScale = Mathf.Pow(10, -25);

        Debug.Log("Mercury velocity: " + PhysicsHelper.OrbitalVelocity(1.988435f * Mathf.Pow(10f, 30f), 46001272f * Mathf.Pow(10f, 3f)));

        //Star
        var starMass = 1.9891f * Mathf.Pow(10, 30) * massScale;
        var starEntity = contexts.game.CreateEntity();
        starEntity.AddMass(starMass);
        starEntity.AddPosition(Vector2.zero);
        starEntity.AddVelocity(Vector2.zero);
        ViewService.LoadAsset(contexts, starEntity, GameControllerMono.starAssetName);

        //Planet(s)
        var sectorAngle = 360f / planetCount;
        for(int i = 0; i < planetCount; i++) {
            var planetMass = 5.972f * Mathf.Pow(10, 24f) * massScale;
            var planetOrbitRadius = 2f;
            var planetInitialPosition = (Vector2)(Quaternion.Euler(0f, 0f, sectorAngle * i) * (Vector3.up * planetOrbitRadius));

            var orbitalVelocity = PhysicsHelper.OrbitalVelocity(starMass, planetOrbitRadius);
            Debug.Log("Start velocity: " + orbitalVelocity);
            var planetInitialVelocity = Quaternion.Euler(0f, 0f, 90f) * (planetInitialPosition.normalized * orbitalVelocity);

            var planetEntity = contexts.game.CreateEntity();
            planetEntity.AddMass(planetMass);
            planetEntity.AddPosition(planetInitialPosition);
            planetEntity.AddVelocity(planetInitialVelocity); //(planetStartVelocity);
            ViewService.LoadAsset(contexts, planetEntity, GameControllerMono.planetAssetName, planetInitialPosition);
        }
    }

    public void Execute() {
        //Calculate kinematic energy
        //Calculate gravitational forces from other gravity entities
        //Calculate total energy
        //Convert energy to velocity

        foreach(var e in gravityEntities) {

            var kinematicEnergy = PhysicsHelper.KinematicEnergy(e.mass.value, e.velocity.value);
            var externalGravitationalForces = Vector2.zero;
            foreach(var other in gravityEntities) {
                if(other == e) continue;

                externalGravitationalForces += PhysicsHelper.GravitationalPotential(e.mass.value, other.mass.value, e.position.value, other.position.value);
            }

            var totalEnergy = kinematicEnergy + externalGravitationalForces;
            var newVelocity = PhysicsHelper.EnergyToVelocity(e.mass.value, totalEnergy);
            e.ReplaceVelocity(newVelocity);
        }
    }
}
