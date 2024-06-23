---
_layout: landing
---

![REPO-SIZE](https://img.shields.io/github/repo-size/ethan-hann/CyberRadio-Assistant)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/210b2b0ad9a748a6a35e3f7048acdf95)](https://app.codacy.com/gh/ethan-hann/CyberRadio-Assistant/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![GitHub Release](https://img.shields.io/github/v/release/ethan-hann/CyberRadio-Assistant?include_prereleases&display_name=release&style=flat)](https://github.com/ethan-hann/CyberRadio-Assistant/releases)
[![GPLv3](https://img.shields.io/static/v1.svg?label=📃%20License&message=GPL%20v3.0&color=informational)](https://choosealicense.com/licenses/gpl-3.0/)

[![Buy me a coffee](https://img.shields.io/static/v1.svg?label=Buy%20me%20a%20coffee&message=🥨&color=black&logo=buy%20me%20a%20coffee&logoColor=white&labelColor=6f4e37)](https://www.buymeacoffee.com/ethanhann) 

# Welcome to Cyber Radio Assistant! 📻

Cyber Radio Assistant is a standalone tool that helps you create and manage custom radio stations for the Cyberpunk 2077 mod, [radioExt](https://www.nexusmods.com/cyberpunk2077/mods/4591).

A brief list of features are below:

- Create custom radio stations in a GUI application instead of by editing a `metadata.json` file.
- Manipulate radio stations downloaded from [NexusMods](https://www.nexusmods.com/cyberpunk2077/).
- Add/delete songs from the radio station (even ones downloaded from Nexus).
- Export to a staging folder first so the game directory doesn't get messed up.
- Add a web stream for the audio instead of songs and preview the audio directly from the application.
- Localization for English, Spanish, and French. More localizations can be added by cloning the GitHub and submitting a pull request.

This documentation has two parts:
1) a [full guide](docs/introduction.md) that goes over how to use Cyber Radio Assistant 📃
2) an [API reference](api/RadioExt_Helper.forms.yml) that is useful if you want to see the classes and methods behind the scenes 😄

# [Github Repo](https://github.com/ethan-hann/CyberRadio-Assistant)

---
# To-Do List

> [!TODO]
> Ensure deleted stations are also deleted from the staging folder!

> [!TODO]
> Fix the bug where a new station may not be selected automatically in the list box.

> [!TODO]
> Add a check for the FM number in the display name. Add it automatically if not present!

> [!TODO]
> Synchronize display name and station name in listbox upon editing the display name in the station properties.