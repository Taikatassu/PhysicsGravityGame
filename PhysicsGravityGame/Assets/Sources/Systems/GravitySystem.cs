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

        var planetCount = 9;
        var orbitRadiusIncrement = 3f;
        var firstOrbitRadius = 15f;
        var placePlanetOnDifferentSectors = false;

        //Star
        var starMass = 198900f * 100f;
        var starEntity = contexts.game.CreateEntity();
        starEntity.ReplaceMass(starMass);
        starEntity.ReplacePosition(Vector2.one * 50f);
        starEntity.ReplaceVelocity(Vector2.zero);
        ViewService.LoadAsset(contexts, starEntity, GameControllerMono.starAssetName);

        //Planet(s)
        var planetMass = 0.5974f;
        var sectorAngle = 360f / planetCount;
        for (int i = 0; i < planetCount; i++) {
            var planetOrbitRadius = firstOrbitRadius + (i * orbitRadiusIncrement);
            var planetInitialPosition = (placePlanetOnDifferentSectors)
                ? starEntity.position.value + (Vector2)(Quaternion.Euler(0f, 0f, sectorAngle * i)
                    * (Vector2.up * planetOrbitRadius))
                : starEntity.position.value + (Vector2.up * planetOrbitRadius);

            var orbitalVelocity = OrbitalVelocity(starMass + planetMass, planetOrbitRadius);
            Debug.Log("Start velocity: " + orbitalVelocity);
            var directionFromOrbitCenter = planetInitialPosition - starEntity.position.value;
            var planetInitialVelocity = Quaternion.Euler(0f, 0f, 90f) * (directionFromOrbitCenter.normalized * orbitalVelocity);
            Debug.DrawRay(planetInitialPosition, planetInitialVelocity, Color.red, 5f);

            var planetEntity = contexts.game.CreateEntity();
            planetEntity.ReplaceMass(planetMass);
            planetEntity.ReplacePosition(planetInitialPosition);
            planetEntity.ReplaceVelocity(planetInitialVelocity);
            ViewService.LoadAsset(contexts, planetEntity, GameControllerMono.planetAssetName, planetInitialPosition);
        }
    }

    public void Execute() {
        foreach (var e in gravityEntities) {

            Vector2 entityVelocity = e.velocity.value;
            Vector2 entityPosition = e.position.value;
            float entityMass = e.mass.value;
            foreach (var other in gravityEntities) {
                if (other == e) continue;
                if (entityMass > other.mass.value) continue;

                //Vector2 otherPosition = other.position.value;

                //Causes circular orbits:
                //float dx = otherPosition.x - entityPosition.x;
                //float dy = otherPosition.y - entityPosition.y;
                //float distanceBetweenBodies = Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));
                //float angle_radians = Mathf.Atan2(dy, dx);
                //float accelerationMagnitude = (gravitationalConstant
                //    * (entityMass + other.mass.value)) / Mathf.Pow(distanceBetweenBodies, 2);
                //Vector2 accelerationVector = new Vector2(accelerationMagnitude
                //    * Mathf.Cos(angle_radians), accelerationMagnitude * Mathf.Sin(angle_radians));



                //var direction = otherPosition - entityPosition;
                //var distance = direction.magnitude;
                //var normalizedDirection = direction / distance;
                //float accelerationMagnitude = (gravitationalConstant
                //    * (entityMass + other.mass.value)) / Mathf.Pow(distance, 2);
                //Vector2 accelerationVector = accelerationMagnitude * normalizedDirection;

                entityVelocity += GravitationalPotentialVector(entityMass, other.mass.value, entityPosition, other.position.value) * Time.deltaTime;

                //Causes elliptical orbits:
                //var gravitationalForce = GravitationalForce(entityMass, other.mass.value, entityPosition, otherPosition);
                //entityVelocity += gravitationalForce * Time.deltaTime;

            }
            e.ReplaceVelocity(entityVelocity);


            //foreach (var other in gravityEntities) {

            //}

            //var kinematicEnergy = KinematicEnergy(e.mass.value, e.velocity.value);
            //var externalGravitationalForces = Vector2.zero;
            //foreach (var other in gravityEntities) {
            //    if (other == e) continue;

            //    externalGravitationalForces += GravitationalPotential(e.mass.value, other.mass.value, e.position.value, other.position.value);
            //}

            //var totalEnergy = kinematicEnergy + externalGravitationalForces;
            //var newVelocity = EnergyToVelocity(e.mass.value, totalEnergy);
            //e.ReplaceVelocity(newVelocity);
        }
    }

    private static float gravitationalConstant = 0.000667f; //0.0000000000667408f; //6.67408 * 10^-11
    public static readonly float kEpsilon = 0.00001f;

    private static float ClampAboveZero(float value) {
        return Mathf.Clamp(value, kEpsilon, float.MaxValue);
    }

    //Orbital velocity:
    //v = Sqrt(G * m / r)
    public static float OrbitalVelocity(float mass, float orbitRadius) {
        orbitRadius = ClampAboveZero(orbitRadius);
        var eccentricity = 0.15f;
        return Mathf.Sqrt((gravitationalConstant * mass) / orbitRadius * (1 + eccentricity));
    }

    //Gravitational potential:
    //https://physics.stackexchange.com/questions/52566/how-to-calculate-linar-velocity-of-planet-orbit
    //        _____________________
    //       / G(M + m)
    // v =  /___________  * (1 + e)     , where a is then angle between velocity vector and the radius and e is eccentricity
    //    \/   r sin(a)
    public static float GravitationalPotentialMagnitude(float mass1, float mass2, float distance) {

        return (gravitationalConstant * (mass1 + mass2)) / Mathf.Pow(distance, 2);
    }

    public static Vector2 GravitationalPotentialVector(float mass1, float mass2, Vector2 position1, Vector2 position2) {
        var direction = position2 - position1;
        var distance = direction.magnitude;
        var normalizedDirection = direction / distance;

        return GravitationalPotentialMagnitude(mass1, mass2, distance) * normalizedDirection;
    }


    #region Old stuff that doesn't seem to work
    //Reduced mass (Obsolete):
    //public static float ReducedMass(float mass1, float mass2) {
    //    return ((mass1 * mass2) / (mass1 - mass2));
    //}

    //Gravitational force:
    //Energy: U = - ((G * M * m) / r), where r is the distance between the center of the masses
    //Force: F = -((G * M * m) / r^2) * R, where R a vector of lenght 1 pointing from M to m
    //public static Vector2 GravitationalForce(float mass1, float mass2, Vector2 position1, Vector2 position2) {
    //    var direction = position2 - position1;
    //    var distance = direction.magnitude;
    //    var normalizedDirection = direction / distance;
    //    return GravitationalForce(mass1, mass2, distance) * normalizedDirection;
    //}

    //public static float GravitationalForce(float mass1, float mass2, float distance) {
    //    distance = ClampAboveZero(distance);
    //    //return ((gravitationalConstant * mass1 * mass2) / Mathf.Pow(distance, 2)); //Elliptical. Works, though without the negative sign in front?
    //    return Mathf.Sqrt((gravitationalConstant * (mass1 + mass2)) / distance); //Circular (?) Causes moving elliptical orbits

    //    //Sqrt((G * M * m) / r^2)
    //    //(G * (M + m)) / r
    //    //-> (G * (M + m)) / r^2 ??????????
    //}

    //Kinetic energy:
    //E = 0.5f * m * v^2
    //public static Vector2 KineticEnergy(float mass, Vector2 velocity) {
    //}


    //----------------------------------------------------------------------------------------------


    ////Energy from velocity:
    ////E = 0.5 * m * v^2
    //public static Vector2 KineticEnergy(float mass, Vector2 velocity) {
    //    return new Vector2(KineticEnergy(mass, velocity.x), KineticEnergy(mass, velocity.y));
    //}

    //public static float KineticEnergy(float mass, float velocity) {

    //    if (velocity < 0f)
    //        return 0.5f * mass * -Mathf.Pow(velocity, 2);

    //    return 0.5f * mass * Mathf.Pow(velocity, 2);
    //}

    ////Gravitational force:
    ////F = G*(m1*m2) / r
    //public static Vector2 GravitationalPotential(float mass1, float mass2, Vector2 position1, Vector2 position2) {
    //    var direction = position2 - position1;
    //    var distance = ClampAboveZero(direction.magnitude);
    //    var normalizedDirection = direction / distance;
    //    return GravitationalPotential(mass1, mass2, distance) * normalizedDirection;
    //}

    //public static float GravitationalPotential(float mass1, float mass2, float distance) {
    //    distance = ClampAboveZero(distance);
    //    return gravitationalConstant * ((mass1 * mass2) / distance);
    //}

    ////Velocity from energy:
    ////v = Sqrt(E / (0.5 * m))
    //public static Vector2 EnergyToVelocity(float mass, Vector2 energy) {
    //    return new Vector2(EnergyToVelocity(mass, energy.x), EnergyToVelocity(mass, energy.y));
    //}

    //public static float EnergyToVelocity(float mass, float energy) {
    //    mass = ClampAboveZero(mass);
    //    if (energy < 0f)
    //        return -Mathf.Sqrt(Mathf.Abs(energy / (0.5f * mass)));

    //    return Mathf.Sqrt(energy / (0.5f * mass));
    //}
    #endregion
}
