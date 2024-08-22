# 1. Changes to improve Performance
 - Add Pooling Tool in Script/Tool to Cache the Item and Cell for Reuse.
 - Init List with Capacity Help to Improve Performance.[https://www.code4it.dev/csharptips/initialize-lists-size/]
 - Move Explode Item of the Bonus to Board for easy to control.
# 2. Use Script Table Object
- Add Script Table Object Into the Game Setting.
- Folder(Asset/GameData) contains the sprite data.
# 3. The Reset button in the Panel Pause
- Board Data and Cell Data are created to save the data when players start the game.
- Board Data contains list of CellsData and when restart we can take the data in here to instantiate;
- ISaveLoad can be implemented to save, load the data by any way.
# 4. FillGapsWithNewItems Function
- A new Dictionary (m_amountEachNormalItem) added into Board class to count amount of each normalType in the current board.
- This Dictionary update amount when Item get Collapse in the Game.
- When get a new Item, Utils class will look at the m_amountEachNormal to decide what will be instantiated.
# 5. Suggestion
- Bonus Item contains 3 functions on each power up. This is not clean because when we need to change one Bonus Item, we will have to go into 1 class and change. Other side, when we have to add, remove some power up will be difficult to do.
- So with this, we need to implement IBonusItem interface to implement BonusItem.