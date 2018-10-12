//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public RadiusListenerComponent radiusListener { get { return (RadiusListenerComponent)GetComponent(GameComponentsLookup.RadiusListener); } }
    public bool hasRadiusListener { get { return HasComponent(GameComponentsLookup.RadiusListener); } }

    public void AddRadiusListener(System.Collections.Generic.List<IRadiusListener> newValue) {
        var index = GameComponentsLookup.RadiusListener;
        var component = CreateComponent<RadiusListenerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceRadiusListener(System.Collections.Generic.List<IRadiusListener> newValue) {
        var index = GameComponentsLookup.RadiusListener;
        var component = CreateComponent<RadiusListenerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveRadiusListener() {
        RemoveComponent(GameComponentsLookup.RadiusListener);
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

    static Entitas.IMatcher<GameEntity> _matcherRadiusListener;

    public static Entitas.IMatcher<GameEntity> RadiusListener {
        get {
            if (_matcherRadiusListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.RadiusListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRadiusListener = matcher;
            }

            return _matcherRadiusListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddRadiusListener(IRadiusListener value) {
        var listeners = hasRadiusListener
            ? radiusListener.value
            : new System.Collections.Generic.List<IRadiusListener>();
        listeners.Add(value);
        ReplaceRadiusListener(listeners);
    }

    public void RemoveRadiusListener(IRadiusListener value, bool removeComponentWhenEmpty = true) {
        var listeners = radiusListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveRadiusListener();
        } else {
            ReplaceRadiusListener(listeners);
        }
    }
}
