# Quasimorph Sort To Tabs

![alt text](SortExample.png)

A mod to automatically move items to specific tabs using rules defined by the user.

For example, weapons to the first tab, ammo on the second, armor on the third, etc.

Press F5 to apply the move rules for the items on that tab.  Press S to invoke the game's normal sort.

The rules and hotkeys can be changed in the configuration file.  See the [Configuration](#configuration) section below.

# Previous Users
Users that have used the mod before Quasimorph 0.8.5 will be prompted to reset the item rules.
It is recommended to do the reset.  Regardless of the choice, the previous config file will be backed up to `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_SortToTabs\QM_SortToTabs.json.upgrade-backup`.  

The mod can be reset to the new defaults at any time by deleting the config file located at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_SortToTabs\QM_SortToTabs.json`

## Default Rules
The default rules are:

|Item|Destination Tab|
|--|--|
|Emergency case|1|
|Weapons|1|
|Placeable items (turrets, mines, etc.)|1|
|Grenades|1|
|Ammo|2|
|Armor|3|
|Medical and Food|4|
|Repair Items|5|
|Containers, robots, resources, barter items, quest items|6|
|All other items|1|

# Support
If you enjoy my mods and want to buy me a coffee, check out my [Ko-Fi](https://ko-fi.com/nbkredspy71915) page.
Thanks!

# Configuration

## Files

The configuration file will be created on the first game run and can be found at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_SortToTabs\QM_SortToTabs.json`.  

See the [rules](#rules) section below.

## Shortcut Keys
The refresh and sort keys can be found in the config file.  Valid keys can be found at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html

## Rules
The rules search for a match from top to bottom.  First match wins.
The rules have the following optional parts:  Id (ex: army_knife), Type (ex: ArmorRecord), SubType (ex: QuestItem), and Category (Tezctlan).  Any blank values will not be used to match an item.  

Any item that does not have a matching rule will not be moved.

Most rules will only need a single value such as Type or Id.

A rule with all blank values will match every item.  This should be put at the end of the rules.

The data for every item is exported to DataExport.csv.  See the [Data](#data) section below for more info.

Example rules:

|TabNumber|Id|Type|SubType|ItemClass|Category|Result|
|--|--|--|--|--|--|--|
|4||||Alcohol||Moves all alcohol items to tab 4|
|5|||||Tezctlan|Moves all Tezctlan items to tab 5|
|1|army_knife|||||Moves all army knives to tab 1|
|2||WeaponRecord||||Moves all weapons to tab 2|
|2||AmmoRecord||||Moves all ammo to tab 2|
|7|||QuestItem|||Moves all Quest items to tab 7|
|6||||||(All Blank) moves all unmatched items to tab 6|

Example Entry in the config file:
```json
{
  "TabNumber": 1,
  "AltTabNumber": 0,
  "ItemMatch": {
    "Id": "",
    "RecordType": "WeaponRecord",
    "SubType": "",
    "Category": "",
    "ItemClass": ""
  }
},
```

### Category Note
While items can have more than one category, the rule's Category property only supports *one* item. 

Example from the DataExport.csv:
|Id|Type|SubType|ItemClass|Categories|
|--|--|--|--|--|
|venus_knife_1|WeaponRecord||Weapon|Tezctlan XiomaraMasks|

To match all Tezctlan and XiomaraMasks, there would need to be one rule for each.  Using "Tezctlan XiomaraMasks" will not work.

### Data
The mod will export all of the items' info to `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_SortToTabs\DataExport.csv`.


Example data from DataExport.csv:
```
ItemName,Id,Type,SubType,ItemClass,Categories
claw,common_knife_1,WeaponRecord,,Weapon,Military Police Common Medical Science CResistance UnchainedBelt SheduThousand
Elite Knife,military_knife_1,WeaponRecord,,Weapon,Managment Military Police CResistance UnchainedBelt SheduThousand
shiv,miner_knife_1,WeaponRecord,,Weapon,Miner Common Worker CResistance UnchainedBelt
```

The ItemName column is the item's name as displayed in the game, and is not use by the rules.
The Categories column contains one or more value and is separated by spaces.

## Special Recycling Rules
If the ship a recycler and the recycler is not in use, the rules will treat the recycler as an eighth tab.

If a rule targets the eighth tab but the recycler is not available or in use, the rule will not move the item.
However, if the property `AltTabNumber` is set, that tab will be used as the target instead.

This allows a user to create a staging tab until the recycler is available.  When the recycler is ready, running the sort on that tab will move the staged items to the recycler.

Example:

```json
{
  "TabNumber": 8,
  "AltTabNumber": 7,
  "ItemMatch": {
    "Id": "pmc_pistol",
    "RecordType": "",
    "SubType": "",
    "Category": "",
    "ItemClass": ""
  }
},
```

# Bad Config File
If the configuration has a loading issue (such as it being incorrectly formatted), the game will use the defaults.
There will be an error in the log player.log indicating this, but will not be obvious to users otherwise.

To recover, delete the config file and restart the game.  A new config file with the defaults will be created.

# Debugging
To assist with debugging the rules while running the game, the mod will reload the configuration any time the file is saved.

The config option`DebugLogMatches` can be enabled to log which rule matched the item.  This will write every item and the rule that matched that item to the game's Player.log.

The game's log can be found here `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\Player.log`

# Credits
* Thanks to GitHub user WiliamRogers1886 for providing an interim 0.8.5 patch.

# Source Code
Source code is available on GitHub https://github.com/NBKRedSpy/QM_SortToTabs

# Change Log

## 1.4.1.2
* 0.8.5 Support
* Add conversion from mod's config file to support 0.8.5's item changes.
* Support for Categories.
* Always writes the data export file if the data has changed.

## 1.3.0
* Moved config file directory.

## 1.2.0
* Version .8 support.

## 1.1.0
* Added Recycling upgrade support.
* Added Fast Trade and After Raid screens
* Included new `AltTabNumber` member for busy recycler rules.

