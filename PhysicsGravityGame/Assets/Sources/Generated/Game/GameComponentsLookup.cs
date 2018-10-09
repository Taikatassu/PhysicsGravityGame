//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class GameComponentsLookup {

    public const int BlackHole = 0;
    public const int Destroyed = 1;
    public const int DestroyedListener = 2;
    public const int Mass = 3;
    public const int PlayerControlledShooter = 4;
    public const int Position = 5;
    public const int PositionListener = 6;
    public const int Radius = 7;
    public const int ShootDirection = 8;
    public const int Velocity = 9;

    public const int TotalComponents = 10;

    public static readonly string[] componentNames = {
        "BlackHole",
        "Destroyed",
        "DestroyedListener",
        "Mass",
        "PlayerControlledShooter",
        "Position",
        "PositionListener",
        "Radius",
        "ShootDirection",
        "Velocity"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(BlackHoleComponent),
        typeof(DestroyedComponent),
        typeof(DestroyedListenerComponent),
        typeof(MassComponent),
        typeof(PlayerControlledShooterComponent),
        typeof(PositionComponent),
        typeof(PositionListenerComponent),
        typeof(RadiusComponent),
        typeof(ShootDirectionComponent),
        typeof(VelocityComponent)
    };
}
