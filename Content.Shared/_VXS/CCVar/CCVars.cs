using Robust.Shared;
using Robust.Shared.Configuration;
namespace Content.Shared._VXS.CCVar;

[CVarDefs]
public sealed class CCVars
{
        /*
     * Ghost Respawn
     */

    public static readonly CVarDef<float> GhostRespawnTime =
        CVarDef.Create("ghost.respawn_time", 60f, CVar.SERVER | CVar.REPLICATED);

    public static readonly CVarDef<int> GhostRespawnMaxPlayers =
        CVarDef.Create("ghost.respawn_max_players", 200, CVar.SERVERONLY);
}
