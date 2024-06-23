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

# VirusTotal Report

Since you should never run `.exe` files from people you don't trust, I urge you to verify the hash of the file you downloaded against the below.

Also, check the VirusTotal report. There were 2 detections out of 71 but both are false positives. If you don't believe me, look through the [source code](https://github.com/ethan-hann/CyberRadio-Assistant) and tell me where the virus is.

## Summary

- **File Name:** `CyberRadioAssistant.exe`
- **SHA-256:** 
`a781f290fd4363fd01bf700defbd9e1bc8d13deebe48e2d5527a5d90119721fd`
- **Detection Ratio:** 2/71
- **Date:** 2024-06-23 06:35:57 UTC

## Detailed Report

For the full VirusTotal report, please visit the following link:

[View VirusTotal Report](https://www.virustotal.com/gui/file/a781f290fd4363fd01bf700defbd9e1bc8d13deebe48e2d5527a5d90119721fd/detection)

## Key Findings

- **Antivirus Detections:** 
  - Symantec: **`Clean`**
  - McAfee: **`Clean`**
  - Kaspersky: **`Clean`**
  - Avast: **`Clean`**
  - BitDefender: **`Clean`**
  - Bkav Pro: `W64.AIDetectMalware` (**FALSE POSITIVE**)
  - MaxSecure: `Win.MxResIcn.Heur.Gen` (**FALSE POSITIVE**)

## Screenshots

Here are some screenshots from the VirusTotal report:

### Detection Overview
![vt_detection_overview](images/vt_detection_overview.png)

### Detailed Analysis
![vt_detailed_overview](images/vt_detailed_overview.png)
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