[h1]Quasimorph Sort To Tabs[/h1]


A mod to automatically move items to specific tabs, as defined by the user.

For example, weapons and ammo to the first tab, armor on the second, bartering items to the seventh tab, etc.

Also adds a shortcut to run the tab sort.  Defaults to S, but can be configured.

[h1]Warning[/h1]

This mod is in [i]beta[/i].

Another mod which also moved items caused game locks on mission end.  I believe this mod will not have that issue, but it would be best to backup the save game.

If you wish to test the mod, make sure to backup your saves which are located at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph[/i]

[h2]Defaults Request For Help[/h2]

I'm looking for useful default values for the sort.  Please put any suggestions in the discussions tab or @ me on the official Discord server.

[h1]Usage[/h1]

When on a screen with cargo tabs, press the F5 key to invoke the distribution.  This will only affect items that are on the current tab.

The items will be distributed based on the rules defined in the configuration file.

[h1]Configuration[/h1]

[h2]Files[/h2]

The configuration file will be created on the first game run and can be found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_SortToTabs.json[/i].

[h3]Data[/h3]

The item keys required for the rules will be found in the [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_SortToTabs\DataExport.csv[/i] file.  It will be created on the game's first run.
If the data needs to be refreshed, delete the file.  The next time the game is run, the data will be extracted again.

This file is only for informational purposes and is not used by the mod.

[h2]Refresh Key[/h2]

The refresh key can be found in the config file.  Valid keys can be found at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html

[h2]Rules[/h2]

The rules are searched for a match from top to bottom.  First match wins.
The rules have three optional parts:  Id (ex: army_knife), Type (ex: ArmorRecord), and the SubType (ex: QuestItem).
Any item that does not have a matching rule will not be moved.
Most rules will only need a single item set.

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

The DataExport.csv file will contain the information required for the rules.

Example data:
[code]
Id,Type,SubType
quest_fresco_data,TrashRecord,QuestItem
knuckles,WeaponRecord,
human_ear,TrashRecord,Resource
merkUSB,DatadiskRecord,
[/code]

[h1]Debugging[/h1]

The mod will automatically load changes to the config file while the game is running.

There is also an option called [i]DebugLogMatches[/i].  If set to true, the item match information will be logged to the game's log.

The game's log can be found here [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\Player.log[/i]

[h1]Source Code[/h1]

Source code is available on GitHub https://github.com/NBKRedSpy/QM_SortToTabs
