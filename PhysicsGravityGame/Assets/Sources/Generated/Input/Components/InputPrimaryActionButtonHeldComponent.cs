//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity primaryActionButtonHeldEntity { get { return GetGroup(InputMatcher.PrimaryActionButtonHeld).GetSingleEntity(); } }

    public bool inputPrimaryActionButtonHeld {
        get { return primaryActionButtonHeldEntity != null; }
        set {
            var entity = primaryActionButtonHeldEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().inputPrimaryActionButtonHeld = true;
                } else {
                    entity.Destroy();
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    static readonly PrimaryActionButtonHeldComponent primaryActionButtonHeldComponent = new PrimaryActionButtonHeldComponent();

    public bool inputPrimaryActionButtonHeld {
        get { return HasComponent(InputComponentsLookup.PrimaryActionButtonHeld); }
        set {
            if (value != inputPrimaryActionButtonHeld) {
                var index = InputComponentsLookup.PrimaryActionButtonHeld;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : primaryActionButtonHeldComponent;

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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherPrimaryActionButtonHeld;

    public static Entitas.IMatcher<InputEntity> PrimaryActionButtonHeld {
        get {
            if (_matcherPrimaryActionButtonHeld == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.PrimaryActionButtonHeld);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherPrimaryActionButtonHeld = matcher;
            }

            return _matcherPrimaryActionButtonHeld;
        }
    }
}
