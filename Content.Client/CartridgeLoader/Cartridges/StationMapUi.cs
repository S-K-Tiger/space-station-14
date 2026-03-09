using Robust.Shared.GameObjects;
using Robust.Shared.Player;
using Robust.Shared.Map;

namespace Content.Client.CartridgeLoader.Cartridges;

/// Gutted version of <see cref="Content.Client.Pinpointer.UI.StationMapWindow"/>
public sealed partial class StationMapUi : Content.Client.UserInterface.Fragments.UIFragment
{
    protected StationMapUiFragment? _fragment;
    protected IEntityManager? _entMan;
    protected ICommonSession? _localSession;

    public override Robust.Client.UserInterface.Control GetUIFragmentRoot()
    {
        return _fragment!;
    }

    public override void Setup(BoundUserInterface userInterface, EntityUid? fragmentOwner)
    {
        _entMan ??= IoCManager.Resolve<IEntityManager>();
        _localSession ??= IoCManager.Resolve<ISharedPlayerManager>().LocalSession;
        _fragment = new StationMapUiFragment();

        EntityUid? gridUid = null;

        if(fragmentOwner.HasValue) {
            if (
                _entMan.TryGetComponent<Content.Shared.Pinpointer.StationMapComponent>(fragmentOwner.Value, out var comp) &&
                comp.TargetGrid != null
            )
            {
                gridUid = comp.TargetGrid;
            }
            else if (_entMan.TryGetComponent<TransformComponent>(fragmentOwner.Value, out var xform))
            {
                gridUid = xform?.GridUid;
            }
        }

        // For some reason in testing fragmentOwner appeared to be a R&D computer. Using the local session to get the PDA users character.
        _fragment.Set(gridUid, _localSession?.AttachedEntity);
    }

    public override void UpdateState(BoundUserInterfaceState state)
    {
    }
}
