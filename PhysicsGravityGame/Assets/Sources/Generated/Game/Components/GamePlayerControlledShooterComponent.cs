//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly PlayerControlledShooterComponent playerControlledShooterComponent = new PlayerControlledShooterComponent();

    public bool isPlayerControlledShooter {
        get { return HasComponent(GameComponentsLookup.PlayerControlledShooter); }
        set {
            if (value != isPlayerControlledShooter) {
                var index = GameComponentsLookup.PlayerControlledShooter;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : playerControlledShooterComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherPlayerControlledShooter;

    public static Entitas.IMatcher<GameEntity> PlayerControlledShooter {
        get {
            if (_matcherPlayerControlledShooter == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PlayerControlledShooter);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlayerControlledShooter = matcher;
            }

            return _matcherPlayerControlledShooter;
        }
    }
}