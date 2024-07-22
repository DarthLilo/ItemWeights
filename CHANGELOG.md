# 1.2.0
General Additions
- Added visual feedback to terminal keybind
- Fixed custom prices not working on ship decorations
- Items can now be removed from the shop by setting their price to -1 (Only works on items such as the flashlight or shovel, not the cruiser or decoration items)
- Removed vehicle caching during terminal register call
- Cruiser now has its own config entry and actually works now
- Teleporter prices can now be configured
- Inverse teleporter prices can now be configured

Advanced
- There are now configuration options for TerminalKeywords and TerminalKeywordsConfirm, these are used to determine relations between terminal nodes to allow the modification of decoration items and hiding items from the shop. Do not remove any of the default values as it may prevent certain parts of the mod from working, however, if another mod doesn't work with price configuration you can try adding its terminal keywords and confirmation keywords to this and it may fix it.

# 1.1.0
- Scrap values can now be configured
- Terminal prices can now be configured
- Reduced logging

# 1.0.0
- Initial Release