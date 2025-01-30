[h1]Quasimorph Sort To Tabs[/h1]


A mod to automatically move items to specific tabs using rules defined by the user.

For example, weapons to the first tab, ammo on the second, armor on the third, etc.

Press F5 to apply the move rules for the items on that tab.  Press S to invoke the game's normal sort.

The rules and hotkeys can be changed in the configuration file.  See the Configuration section below.

[h1]Previous Users[/h1]

Users that have used the mod before Quasimorph 0.8.5 will be prompted to reset the item rules.
It is recommended to do the reset.  Regardless of the choice, the previous config file will be backed up to [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_SortToTabs\QM_SortToTabs.json.upgrade-backup[/i].

The mod can be reset to the new defaults at any time by deleting the config file located at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_SortToTabs\QM_SortToTabs.json[/i]

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
[td]Emergency case
[/td]
[td]1
[/td]
[/tr]
[tr]
[td]Weapons
[/td]
[td]1
[/td]
[/tr]
[tr]
[td]Placeable items (turrets, mines, etc.)
[/td]
[td]1
[/td]
[/tr]
[tr]
[td]Grenades
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
[td]Containers, robots, resources, barter items, quest items
[/td]
[td]6
[/td]
[/tr]
[tr]
[td]All other items
[/td]
[td]1
[/td]
[/tr]
[/table]

[h1]Support[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Configuration[/h1]

[h2]Files[/h2]

The configuration file will be created on the first game run and can be found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_SortToTabs\QM_SortToTabs.json[/i].

See the rules section below.

[h2]Shortcut Keys[/h2]

The refresh and sort keys can be found in the config file.  Valid keys can be found at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html

[h2]Rules[/h2]

The rules search for a match from top to bottom.  First match wins.
The rules have the following optional parts:  Id (ex: army_knife), Type (ex: ArmorRecord), SubType (ex: QuestItem), and Category (Tezctlan).  Any blank values will not be used to match an item.

Any item that does not have a matching rule will not be moved.

Most rules will only need a single value such as Type or Id.

A rule with all blank values will match every item.  This should be put at the end of the rules.

The data for every item is exported to DataExport.csv.  See the Data section below for more info.

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
[td]ItemClass
[/td]
[td]Category
[/td]
[td]Result
[/td]
[/tr]
[tr]
[td]4
[/td]
[td]
[/td]
[td]
[/td]
[td]
[/td]
[td]Alcohol
[/td]
[td]
[/td]
[td]Moves all alcohol items to tab 4
[/td]
[/tr]
[tr]
[td]5
[/td]
[td]
[/td]
[td]
[/td]
[td]
[/td]
[td]
[/td]
[td]Tezctlan
[/td]
[td]Moves all Tezctlan items to tab 5
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
[td]
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
[td]
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
[td]
[/td]
[td]
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
    "SubType": "",
    "Category": "",
    "ItemClass": ""
  }
},
[/code]

[h3]Category Note[/h3]

While items can have more than one category, the rule's Category property only supports [i]one[/i] item.

Example from the DataExport.csv:
|Id|Type|SubType|ItemClass|Categories|
|--|--|--|--|--|
|venus_knife_1|WeaponRecord||Weapon|Tezctlan XiomaraMasks|

To match all Tezctlan and XiomaraMasks, there would need to be one rule for each.  Using "Tezctlan XiomaraMasks" will not work.

[h3]Data[/h3]

The mod will export all of the items' info to [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_SortToTabs\DataExport.csv[/i].

Example data from DataExport.csv:
[code]
ItemName,Id,Type,SubType,ItemClass,Categories
claw,common_knife_1,WeaponRecord,,Weapon,Military Police Common Medical Science CResistance UnchainedBelt SheduThousand
Elite Knife,military_knife_1,WeaponRecord,,Weapon,Managment Military Police CResistance UnchainedBelt SheduThousand
shiv,miner_knife_1,WeaponRecord,,Weapon,Miner Common Worker CResistance UnchainedBelt
[/code]

The ItemName column is the item's name as displayed in the game, and is not use by the rules.
The Categories column contains one or more value and is separated by spaces.

[h2]Special Recycling Rules[/h2]

If the ship a recycler and the recycler is not in use, the rules will treat the recycler as an eighth tab.

If a rule targets the eighth tab but the recycler is not available or in use, the rule will not move the item.
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
    "SubType": "",
    "Category": "",
    "ItemClass": ""
  }
},
[/code]

[h1]Bad Config File[/h1]

If the configuration has a loading issue (such as it being incorrectly formatted), the game will use the defaults.
There will be an error in the log player.log indicating this, but will not be obvious to users otherwise.

To recover, delete the config file and restart the game.  A new config file with the defaults will be created.

[h1]Debugging[/h1]

To assist with debugging the rules while running the game, the mod will reload the configuration any time the file is saved.

The config option[i]DebugLogMatches[/i] can be enabled to log which rule matched the item.  This will write every item and the rule that matched that item to the game's Player.log.

The game's log can be found here [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\Player.log[/i]

[h1]Credits[/h1]
[list]
[*]Thanks to GitHub user WiliamRogers1886 for providing an interim 0.8.5 patch.
[/list]

[h1]Source Code[/h1]

Source code is available on GitHub https://github.com/NBKRedSpy/QM_SortToTabs

[h1]Change Log[/h1]

[h2]1.4.1.2[/h2]
[list]
[*]0.8.5 Support
[*]Add conversion from mod's config file to support 0.8.5's item changes.
[*]Support for Categories.
[*]Always writes the data export file if the data has changed.
[/list]

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
