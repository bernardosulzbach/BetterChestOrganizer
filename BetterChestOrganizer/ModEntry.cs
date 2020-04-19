using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace BetterChestOrganizer
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.World.ChestInventoryChanged += this.OnChestInventoryChange;
        }

        private void OnChestInventoryChange(object sender, ChestInventoryChangedEventArgs eventArgs)
        {
            this.Monitor.Log($"Changed chest has:", LogLevel.Debug);
            foreach (var item in eventArgs.Chest.items)
            {
                this.Monitor.Log($"  {item.Stack} x {item.DisplayName}", LogLevel.Debug);
            }
        }
    }
}