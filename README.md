# Better Chest Organizer

Better Chest Organizer is a mod for [Stardew Valley](https://www.stardewvalley.net/) that aims to provide better automatic chest organization.

## Examples

### Example 1

<p align="center">
  <img src="https://github.com/bernardosulzbach/BetterChestOrganizer/raw/master/images/example-1-1.png"
       alt="A chest with artisan products after sorting by the in-game button">
  <figcaption>A chest with artisan products after sorting by the in-game button. The jellies (red jars) are mixed with the other product types.</figcaption>
</p>

<p align="center">
  <img src="https://github.com/bernardosulzbach/BetterChestOrganizer/raw/master/images/example-1-2.png"
       alt="A chest with artisan products after sorting by the mod">
   <figcaption>A chest with artisan products after sorting by the mod. Different product types are clustered together.</figcaption>
</p>

### Example 2

<p align="center">
  <img src="https://github.com/bernardosulzbach/BetterChestOrganizer/raw/master/images/example-2-1.png"
       alt="A chest with mushrooms of varying quality after sorting by the in-game button">
  <figcaption>A chest with mushrooms of varying quality after sorting by the in-game button. The mushrooms are sorted by their variety, but their quality has not been taken into account.</figcaption>
</p>

<p align="center">
  <img src="https://github.com/bernardosulzbach/BetterChestOrganizer/raw/master/images/example-2-2.png"
       alt="A chest with mushrooms of varying quality after sorting by the mod">
  <figcaption>A chest with mushrooms of varying quality after sorting by the mod. The mushrooms are sorted by their variety and then by their quality.</figcaption>
</p>

## Compatibility

This mod was last tested using [SMAPI](https://smapi.io/) 3.5.0 and Stardew Valley 1.4.5 from Steam on openSUSE Tumbleweed Linux (Kernel 5.6.8-1-default) in May 2020.

## Performance 

This mod performs performance logging.
The time it takes to sort a chest (with up to 36 stacks) seems to always be in the 0.5–5.0 ms range.
Also, the first time a chest is sorted in a play session is usually slower than it would normally be.
This is likely due to the dynamic loading of some of the code associated with the mod.
Cases that, as of version 1.0.0, require items to be compared based on some of the parts of their names are particularly slow.

## License

This mod is licensed under the BSD 3-Clause License. See the provided LICENSE file for more details.
