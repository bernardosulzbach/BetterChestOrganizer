using System;
using System.Linq;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;

namespace BetterChestOrganizer
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.World.ChestInventoryChanged += this.OnChestInventoryChange;
        }

        private void PrintChestContents(StardewValley.Objects.Chest chest)
        {
            foreach (var item in chest.items)
            {
                this.Monitor.Log($" - {item.Stack,3} x {item.DisplayName}", LogLevel.Debug);
            }
        }

        private void OnChestInventoryChange(object sender, ChestInventoryChangedEventArgs eventArgs)
        {
            if (Context.IsMultiplayer)
            {
                this.Monitor.Log($"Disabled because the current context is multiplayer. The mod has not been tested in MP yet.", LogLevel.Warn);
                return;
            }
            var chest = eventArgs.Chest;
            this.Monitor.Log($"After inventory change, the chest has", LogLevel.Debug);
            PrintChestContents(chest);
            var copiedList = chest.items.ToList();
            copiedList.Sort(ItemComparator.CompareItems);
            chest.items.Clear();
            chest.items.AddRange(copiedList);
            this.Monitor.Log($"After ordering, the chest has", LogLevel.Debug);
            PrintChestContents(chest);
        }
    }
}