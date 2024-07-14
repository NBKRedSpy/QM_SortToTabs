[h1]Quasimorph Sort To Tabs[/h1]


A mod to automatically move items to specific tabs, with rules defined by the user defined in the configuration file.

For example, weapons and ammo to the first tab, armor on the second, bartering items to the seventh tab, etc.

Press F5 to apply the move rules for the items on that tab.  Press S to invoke the game's normal sort.

The rules and hotkeys can be changed in the configuration file.

There are default rules, but if there are any suggestions for better defaults, post a discussion or @ me on the [url=https://discord.gg/y8bRVNzzm6]official Discord server[/url].

[h2]Default Rules[/h2]

The default rules are:
|Item|Destination Tab|
|--|--|
|Weapons|1|
|Ammo|2|
|Armor|3|
|Medical and Food|4|
|Repair Items|5|
|All other items|6|

The rules are relatively easy to change in the config file.

[h1]Save Backup[/h1]

There have not been any reported issues, but it would be best to backup the saves just in case.

The saves are located at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph[/i] and begin with [i]slot_[/i]

This is out of abundance of caution as there was another mod which moved items and had corruption issues.

[h1]Configuration[/h1]

[h2]Files[/h2]

The configuration file will be created on the first game run and can be found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_SortToTabs.json[/i].

[h3]Data[/h3]

The rules are based on matches to items' identifiers.  The game exports the data into [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_SortToTabs\DataExport.csv[/i], which is written on the game start.

If the data needs to be refreshed due to new items or categories being added to the game, delete the file and run the game.

This file is for the user's reference and not used by the mod.

[h2]Shortcut Keys[/h2]

The refresh and sort keys can be found in the config file.  Valid keys can be found at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html

[h2]Rules[/h2]

The list of rules is searched for a match from top to bottom.  First match wins.
The rules have three optional parts:  Id (ex: army_knife), Type (ex: ArmorRecord), and the SubType (ex: QuestItem).  Any blank parts will not be used to match an item.

Any item that does not have a matching rule will not be moved.

Most rules will only need a single identifier set.

Example rules:
[table]
[tr]
[td]TabNumber
[/td]
[td]Id
[/td]
[td]Type
[/td]
[td]SubType
[/td]
[td]Result
[/td]
[/tr]
[tr]
[td]1
[/td]
[td]army_knife
[/td]
[td]
[/td]
[td]
[/td]
[td]Moves all army knives to tab 1
[/td]
[/tr]
[tr]
[td]2
[/td]
[td]
[/td]
[td]WeaponRecord
[/td]
[td]
[/td]
[td]Moves all weapons to tab 2
[/td]
[/tr]
[tr]
[td]2
[/td]
[td]
[/td]
[td]AmmoRecord
[/td]
[td]
[/td]
[td]Moves all ammo to tab 2
[/td]
[/tr]
[tr]
[td]7
[/td]
[td]
[/td]
[td]
[/td]
[td]QuestItem
[/td]
[td]Moves all Quest items to tab 7
[/td]
[/tr]
[tr]
[td]6
[/td]
[td]
[/td]
[td]
[/td]
[td]
[/td]
[td](All Blank) moves all unmatched items to tab 6
[/td]
[/tr]
[/table]

Example Entry in the config file:
[code]
{
      "TabNumber": 2,
      "ItemMatch": {
        "Id": "",
        "RecordType": "AmmoRecord",
        "SubType": ""
      }
    },
[/code]

[h2]DataExport.csv[/h2]

The DataExport.csv file will contain the identifiers used by the rules.

Example data:
[code]
Id,Type,SubType
quest_fresco_data,TrashRecord,QuestItem
knuckles,WeaponRecord,
human_ear,TrashRecord,Resource
merkUSB,DatadiskRecord,
[/code]

[h1]Bad Config File[/h1]

If the configuration has a loading issue (such as it being incorrectly formatted), the game will use the defaults.
There will be an error in the log player.log indicating this, but will not be obvious to users otherwise.

To recover, delete the config file and restart the game.  A new, config file with the defaults will be created.

[h1]Debugging[/h1]

To assist with debugging the rules while running the game, the mod will reload the configuration any time the file is saved.

The config option[i]DebugLogMatches[/i] can be enabled to log which rule matched the item.

The game's log can be found here [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\Player.log[/i]

[h1]Source Code[/h1]

Source code is available on GitHub https://github.com/NBKRedSpy/QM_SortToTabs
