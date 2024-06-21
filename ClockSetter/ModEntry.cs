using StardewModdingAPI;
using StardewValley;

namespace ClockSetter;

internal sealed class ModEntry : Mod
{
    public override void Entry(IModHelper helper)
    {
        var hour = Game1.timeOfDay / 100;
        var minute = Game1.timeOfDay % 100;

        helper.Events.GameLoop.GameLaunched += (sender, e) =>
        {
            var configMenu = helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null) return;

            configMenu.Register(
                mod: ModManifest,
                reset: () =>
                {
                    hour = Game1.timeOfDay / 100;
                    minute = Game1.timeOfDay % 100;
                },
                save: () => Game1.timeOfDay = (hour * 100) + minute
            );

            configMenu.AddNumberOption(
                mod: ModManifest,
                name: () => "Hour",
                getValue: () => Game1.timeOfDay / 100,
                setValue: value => hour = value,
                min: 5,
                max: 25
            );
            
            configMenu.AddNumberOption(
                mod: ModManifest,
                name: () => "Minute",
                getValue: () => Game1.timeOfDay % 100,
                setValue: value => minute = value,
                min: 0,
                max: 50,
                interval: 10
            );
        };
    }
}