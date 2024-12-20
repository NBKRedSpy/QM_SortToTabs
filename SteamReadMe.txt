[h1]Quasimorph Sort To Tabs[/h1]


A mod to automatically move items to specific tabs using rules defined by the user.

For example, weapons to the first tab, ammo on the second, armor on the third, etc.

Press F5 to apply the move rules for the items on that tab.  Press S to invoke the game's normal sort.

The rules and hotkeys can be changed in the configuration file.

The mod starts with default search rules.  If there are any suggestions for better defaults, post a discussion or @ me on the [url=https://discord.gg/y8bRVNzzm6]official Discord server[/url].

[h2]Default Rules[/h2]

The default rules are:
[table]
[tr]
[td]Item
[/td]
[td]Destination Tab
[/td]
[/tr]
[tr]
[td]Weapons
[/td]
[td]1
[/td]
[/tr]
[tr]
[td]Ammo
[/td]
[td]2
[/td]
[/tr]
[tr]
[td]Armor
[/td]
[td]3
[/td]
[/tr]
[tr]
[td]Medical and Food
[/td]
[td]4
[/td]
[/tr]
[tr]
[td]Repair Items
[/td]
[td]5
[/td]
[/tr]
[tr]
[td]All other items
[/td]
[td]6
[/td]
[/tr]
[/table]

[h1]Configuration[/h1]

[h2]Files[/h2]

The configuration file will be created on the first game run and can be found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_SortToTabs\QM_SortToTabs.json[/i].

See the rules section below.

[h3]Data[/h3]

The rules are based on matches to items' identifiers.  The game exports those identifiers to [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_SortToTabs\DataExport.csv[/i], which is written on the game start.

If the data needs to be refreshed due to new items or categories being added to the game, delete the file and run the game.

[h2]Shortcut Keys[/h2]

The refresh and sort keys can be found in the config file.  Valid keys can be found at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html

[h2]Rules[/h2]

The rules search for a match from top to bottom.  First match wins.
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
  "TabNumber": 1,
  "AltTabNumber": 0,
  "ItemMatch": {
    "Id": "",
    "RecordType": "WeaponRecord",
    "SubType": ""
  }
},

[/code]

[h2]DataExport.csv (Data Identifier Lookup)[/h2]

The DataExport.csv file will contain the identifiers used by the rules.

Example data:
[code]
Id,Type,SubType
quest_fresco_data,TrashRecord,QuestItem
knuckles,WeaponRecord,
human_ear,TrashRecord,Resource
+merkUSB,DatadiskRecord,
[/code]

[h2]Special Recycling Rules[/h2]

If the ship has the recycler and the recycler is not in use, the rules will treat the recycler as an 8th tab.

If a rule targets the 8th tab but the recycler is not available or in use, the rule will not move the item.
However, if the property [i]AltTabNumber[/i] is set, that tab will be used as the target instead.

This allows a user to create a staging tab until the recycler is available.  When the recycler is ready, running the sort on that tab will move the staged items to the recycler.

Example:
[code]
    {
      "TabNumber": 8,
      "AltTabNumber": 7,
      "ItemMatch": {
        "Id": "pmc_pistol",
        "RecordType": "",
        "SubType": ""
      }
    },
[/code]

[h1]Mod Upgrade Note[/h1]

If the configuration file does not have the [i]AltTabNumber[/i] option, either add it manually where needed, or delete the configuration file.  The game will generate a new config on start.

[h1]Bad Config File[/h1]

If the configuration has a loading issue (such as it being incorrectly formatted), the game will use the defaults.
There will be an error in the log player.log indicating this, but will not be obvious to users otherwise.

To recover, delete the config file and restart the game.  A new, config file with the defaults will be created.

[h1]Debugging[/h1]

To assist with debugging the rules while running the game, the mod will reload the configuration any time the file is saved.

The config option[i]DebugLogMatches[/i] can be enabled to log which rule matched the item.

The game's log can be found here [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\Player.log[/i]

[h1]Support[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Source Code[/h1]

Source code is available on GitHub https://github.com/NBKRedSpy/QM_SortToTabs

[h1]Change Log[/h1]

[h2]1.3.0[/h2]
[list]
[*]Moved config file directory.
[/list]

[h2]1.2.0[/h2]
[list]
[*]Version .8 support.
[/list]

[h2]1.1.0[/h2]
[list]
[*]Added Recycling upgrade support.
[*]Added Fast Trade and After Raid screens
[*]Included new [i]AltTabNumber[/i] member for busy recycler rules.
[/list]
