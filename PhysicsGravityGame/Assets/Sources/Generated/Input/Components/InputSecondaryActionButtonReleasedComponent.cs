//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity secondaryActionButtonReleasedEntity { get { return GetGroup(InputMatcher.SecondaryActionButtonReleased).GetSingleEntity(); } }

    public bool inputSecondaryActionButtonReleased {
        get { return secondaryActionButtonReleasedEntity != null; }
        set {
            var entity = secondaryActionButtonReleasedEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().inputSecondaryActionButtonReleased = true;
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

    static readonly SecondaryActionButtonReleasedComponent secondaryActionButtonReleasedComponent = new SecondaryActionButtonReleasedComponent();

    public bool inputSecondaryActionButtonReleased {
        get { return HasComponent(InputComponentsLookup.SecondaryActionButtonReleased); }
        set {
            if (value != inputSecondaryActionButtonReleased) {
                var index = InputComponentsLookup.SecondaryActionButtonReleased;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : secondaryActionButtonReleasedComponent;

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

    static Entitas.IMatcher<InputEntity> _matcherSecondaryActionButtonReleased;

    public static Entitas.IMatcher<InputEntity> SecondaryActionButtonReleased {
        get {
            if (_matcherSecondaryActionButtonReleased == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.SecondaryActionButtonReleased);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherSecondaryActionButtonReleased = matcher;
            }

            return _matcherSecondaryActionButtonReleased;
        }
    }
}