using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
public class VelocityComponent : IComponent {
    public Vector2 value;
}

[Game]
public class MassComponent : IComponent {
    public float value;
}

[Game]
public class PlayerControlledShooterComponent : IComponent {
}

[Game]
public class ShootDirectionComponent : IComponent {
    public Vector2 value;
}

[Game, Event(EventTarget.Self)]
public class PositionComponent : IComponent {
    public Vector2 value;
}

[Game, Event(EventTarget.Self)]
public class RadiusComponent : IComponent {
    public float value;
}

[Game, Event(EventTarget.Self)]
public class OrbitalPeriodComponent : IComponent {
    public float value;
}

[Game, Event(EventTarget.Self)]
public class ColorComponent : IComponent {
    public Color value;
}

[Game, Event(EventTarget.Self)]
public class DestroyedComponent : IComponent {
}

[Game, Unique]
public class BlackHoleComponent : IComponent {
}

[Meta, Unique]
public class CelestialBodySettingsComponent : IComponent {
    public CelestialBodySettings value;
}


#region Input
[Input, Unique, FlagPrefix("Input")]
public class PrimaryActionButtonPressedComponent : IComponent {
}

[Input, Unique, FlagPrefix("Input")]
public class PrimaryActionButtonHeldComponent : IComponent {
}

[Input, Unique, FlagPrefix("Input")]
public class PrimaryActionButtonReleasedComponent : IComponent {
}

[Input, Unique, FlagPrefix("Input")]
public class SecondaryActionButtonPressedComponent : IComponent {
}

[Input, Unique, FlagPrefix("Input")]
public class SecondaryActionButtonHeldComponent : IComponent {
}

[Input, Unique, FlagPrefix("Input")]
public class SecondaryActionButtonReleasedComponent : IComponent {
}

[Input, Unique, FlagPrefix("Input")]
public class MousePositionComponent : IComponent {
    public Vector2 value;
}
#endregion