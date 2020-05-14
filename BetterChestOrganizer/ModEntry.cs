using System;
using System.Diagnostics;
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
        private const bool Debugging = false;

        private double _maximumElapsedMilliseconds = 0.0;
        private double _totalElapsedMilliseconds = 0.0;

        private int _sortings = 0;


        public override void Entry(IModHelper helper)
        {
            helper.Events.World.ChestInventoryChanged += this.OnChestInventoryChange;
        }

        private void LogChestContents(StardewValley.Objects.Chest chest)
        {
            foreach (var item in chest.items)
            {
                this.Monitor.Log($" - {item.Stack,3} x {item.DisplayName}", LogLevel.Debug);
            }
        }

        private void UpdateElapsedTimeInformation(double elapsedMilliseconds)
        {
            _maximumElapsedMilliseconds = Math.Max(_maximumElapsedMilliseconds, elapsedMilliseconds);
            _totalElapsedMilliseconds += elapsedMilliseconds;
            _sortings++;
        }

        private void OnChestInventoryChange(object sender, ChestInventoryChangedEventArgs eventArgs)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            if (Context.IsMultiplayer)
            {
                this.Monitor.Log(
                    $"Disabled because the current context is multiplayer. The mod has not been tested in MP yet.",
                    LogLevel.Warn);
                return;
            }

            var chest = eventArgs.Chest;
            if (Debugging)
            {
                this.Monitor.Log($"After inventory change, the chest has", LogLevel.Debug);
                LogChestContents(chest);
            }

            var copiedList = chest.items.ToList();
            copiedList = copiedList.OrderBy(item => item, new ItemComparer()).ToList();
            chest.items.Clear();
            chest.items.AddRange(copiedList);
            if (Debugging)
            {
                this.Monitor.Log($"After ordering, the chest has", LogLevel.Debug);
                LogChestContents(chest);
            }

            stopWatch.Stop();
            UpdateElapsedTimeInformation(stopWatch.Elapsed.TotalMilliseconds);
            var maximum = $"Maximum: {_maximumElapsedMilliseconds:F3} ms";
            var mean = $"Mean: {_totalElapsedMilliseconds / _sortings:F3} ms";
            var message = $"Took {stopWatch.Elapsed.TotalMilliseconds:F3} ms to sort a chest. {maximum}. {mean}.";
            this.Monitor.Log(message, LogLevel.Info);
        }
    }
}