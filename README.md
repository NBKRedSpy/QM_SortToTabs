# Quasimorph Sort To Tabs

![alt text](SortExample.png)

A mod to automatically move items to specific tabs, as defined by the user.  

For example, weapons and ammo to the first tab, armor on the second, bartering items to the seventh tab, etc.

Also adds a shortcut to run the tab sort.  Defaults to S, but can be configured.

# Warning
This mod is in *beta*.

Another mod which also moved items caused game locks on mission end.  I believe this mod will not have that issue, but it would be best to backup the save game.

If you wish to test the mod, make sure to backup your saves which are located at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph`

## Defaults Request For Help
I'm looking for useful default values for the sort.  Please put any suggestions in the discussions tab or @ me on the official Discord server.

# Usage

When on a screen with cargo tabs, press the F5 key to invoke the distribution.  This will only affect items that are on the current tab.  

The items will be distributed based on the rules defined in the configuration file.

# Configuration

## Files

The configuration file will be created on the first game run and can be found at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_SortToTabs.json`.  


### Data
The item keys required for the rules will be found in the `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_SortToTabs\DataExport.csv` file.  It will be created on the game's first run.
If the data needs to be refreshed, delete the file.  The next time the game is run, the data will be extracted again.

This file is only for informational purposes and is not used by the mod.



## Refresh Key
The refresh key can be found in the config file.  Valid keys can be found at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html


## Rules
The rules are searched for a match from top to bottom.  First match wins.
The rules have three optional parts:  Id (ex: army_knife), Type (ex: ArmorRecord), and the SubType (ex: QuestItem).
Any item that does not have a matching rule will not be moved.
Most rules will only need a single item set.

Example rules:

|TabNumber|Id|Type|SubType|Result|
|--|--|--|--|--|
|1|army_knife|||Moves all army knives to tab 1|
|2||WeaponRecord||Moves all weapons to tab 2|
|2||AmmoRecord||Moves all ammo to tab 2|
|7|||QuestItem|Moves all Quest items to tab 7|
|6||||(All Blank) moves all unmatched items to tab 6|

Example Entry in the config file:
```json
{
      "TabNumber": 2,
      "ItemMatch": {
        "Id": "",
        "RecordType": "AmmoRecord",
        "SubType": ""
      }
    },
```

## DataExport.csv
The DataExport.csv file will contain the information required for the rules.

Example data:
```
Id,Type,SubType
quest_fresco_data,TrashRecord,QuestItem
knuckles,WeaponRecord,
human_ear,TrashRecord,Resource
merkUSB,DatadiskRecord,
```

# Debugging
The mod will automatically load changes to the config file while the game is running.

There is also an option called `DebugLogMatches`.  If set to true, the item match information will be logged to the game's log.  

The game's log can be found here `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\Player.log`


# Source Code
Source code is available on GitHub https://github.com/NBKRedSpy/QM_SortToTabs



