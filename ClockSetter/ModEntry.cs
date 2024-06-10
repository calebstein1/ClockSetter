using System.Threading.Tasks.Sources;
using StardewModdingAPI;
using StardewValley;

namespace ClockSetter;

internal sealed class ModEntry : Mod
{
    public override void Entry(IModHelper helper)
    {
        var timeOfDay = Game1.timeOfDay;

        helper.Events.GameLoop.GameLaunched += (sender, e) =>
        {
            var configMenu = helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null) return;

            configMenu.Register(
                mod: ModManifest,
                reset: () => timeOfDay = Game1.timeOfDay,
                save: () => Game1.timeOfDay = timeOfDay
            );

            configMenu.AddNumberOption(
                mod: ModManifest,
                name: () => "Hour",
                getValue: () => Game1.timeOfDay,
                setValue: value => timeOfDay = value,
                min: 600,
                max: 2600,
                interval: 10
            );
        };
    }
}