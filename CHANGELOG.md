# ℹ️ Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.446] - TBD

### What's Changed
- **Forbidden Path Support**: Added a way to detect paths that should be forbidden to be set as the staging folder. This is customizable via the configuration (except for obvious paths like system32 and Windows directories). By default, this is all paths relating to a game launcher or mod manager. This change was done to prevent user's from losing their stations in case of another program's shennanigans.
- Fixed bug relating to access permissions on log file directory. [#75](https://github.com/ethan-hann/CyberRadio-Assistant/issues/75)

## [2.0.445] - 2024-10-20

### What's Changed
- **Support for generating custom icons**: Added functionality to create custom `.archive` icons and extract existing `.archive` icons for a station. [#11](https://github.com/ethan-hann/CyberRadio-Assistant/commit/333223b2df8490c28d7352ead2da92b13d3a9404)
- **Enable/Disable Stations**: Added functionality to enable and disable stations easily from the UI. [#17](https://github.com/ethan-hann/CyberRadio-Assistant/pull/17)
- **Copy Songs from `songs.sgls` File**: Improved song export process by copying songs based on the `songs.sgls` file, saving disk space and preventing unnecessary operations. [#14](https://github.com/ethan-hann/CyberRadio-Assistant/pull/18)
- **Custom Settings Support**: Added custom settings including disabling automatic update checks, flagging auto-export to game, setting staging path, game path, selected language, and window size. [#15](https://github.com/ethan-hann/CyberRadio-Assistant/pull/19)
- **Log File Viewer**: Introduced a log viewer to track real-time log entries related to icon import/export operations. [#16](https://github.com/ethan-hann/CyberRadio-Assistant/pull/20)
- **Revert Changes**: Added a feature to revert changes made to stations. [#24](https://github.com/ethan-hann/CyberRadio-Assistant/pull/24)
- **Remove All Songs Button**: New button to remove all songs from a station with one click. [#27](https://github.com/ethan-hann/CyberRadio-Assistant/pull/27)
- **Settings Migration**: Migrated user settings to ensure seamless transitions between versions. [#28](https://github.com/ethan-hann/CyberRadio-Assistant/pull/28)
- **Enhanced Export Process and Error Logging**: Improved the export process, along with better error logging and handling. [#31](https://github.com/ethan-hann/CyberRadio-Assistant/pull/31)
- **New Resources, UI Elements, and Localization**: Added new resources, improved UI elements, and expanded localization options. [#39](https://github.com/ethan-hann/CyberRadio-Assistant/pull/39)
- **Game Station Changes Detection and Staging Backup**: Added detection for game radio station changes and automatic backup of the staging folder. [#41](https://github.com/ethan-hann/CyberRadio-Assistant/pull/41)
- **Station Filter Textbox**: Added a station filter textbox to allow quick filtering of stations. [#43](https://github.com/ethan-hann/CyberRadio-Assistant/pull/43)
- **Restore Preview and UI Enhancements**: Added a restore preview functionality along with various UI enhancements. [#44](https://github.com/ethan-hann/CyberRadio-Assistant/pull/44)
- **Translation Fixes and General Cleanup**: Fixed translation issues and performed general codebase cleanup. [#45](https://github.com/ethan-hann/CyberRadio-Assistant/pull/45)

---

## [1.1.0] - 2024-06-28

### Added

- Delete stations from staging folder. [#4](https://github.com/ethan-hann/CyberRadio-Assistant/pull/5)
- Automate the process of extracting a radio.garden web stream. [#6](https://github.com/ethan-hann/CyberRadio-Assistant/pull/13)
- Update checking capabilities. [#7](https://github.com/ethan-hann/CyberRadio-Assistant/pull/12)
- Ability to enable/disable stations. [#8](https://github.com/ethan-hann/CyberRadio-Assistant/pull/17)
- New QOL menu options under `File`
  - `Open Staging Path`
  - `Open Game Path`
- Various UI additions to allow for feature implementation.
  - New buttons to enable/disable stations.
  - New button to parse radio.garden stream.
  - New label to show the number of enabled/disabled stations beneath the station's list.

### Changed

- Export list view to show which stations are enabled/disabled before exporting.

### Fixed

- Inconsistencies with some buttons not being styled like others (yellow background with hover color).
- Station names not synchronizing correctly to list when changing name in properties. [#10](https://github.com/ethan-hann/CyberRadio-Assistant/pull/17)
- Various bug fixes and improvements.

## [1.0.0] - 2024-06-23

### Added

- Initial release of application.

[2.0.445]: https://github.com/ethan-hann/CyberRadio-Assistant/releases/tag/v2.0.445
[1.1.0]: https://github.com/ethan-hann/CyberRadio-Assistant/releases/tag/v1.1.0
[1.0.0]: https://github.com/ethan-hann/CyberRadio-Assistant/releases/tag/v1.0.0