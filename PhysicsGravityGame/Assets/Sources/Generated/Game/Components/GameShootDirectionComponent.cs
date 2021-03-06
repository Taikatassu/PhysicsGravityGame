//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ShootDirectionComponent shootDirection { get { return (ShootDirectionComponent)GetComponent(GameComponentsLookup.ShootDirection); } }
    public bool hasShootDirection { get { return HasComponent(GameComponentsLookup.ShootDirection); } }

    public void AddShootDirection(UnityEngine.Vector2 newValue) {
        var index = GameComponentsLookup.ShootDirection;
        var component = CreateComponent<ShootDirectionComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceShootDirection(UnityEngine.Vector2 newValue) {
        var index = GameComponentsLookup.ShootDirection;
        var component = CreateComponent<ShootDirectionComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveShootDirection() {
        RemoveComponent(GameComponentsLookup.ShootDirection);
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

    static Entitas.IMatcher<GameEntity> _matcherShootDirection;

    public static Entitas.IMatcher<GameEntity> ShootDirection {
        get {
            if (_matcherShootDirection == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ShootDirection);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherShootDirection = matcher;
            }

            return _matcherShootDirection;
        }
    }
}
