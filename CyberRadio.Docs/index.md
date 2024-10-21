---
_layout: landing
---

[![Static Badge](https://img.shields.io/badge/Cyber%20Radio%20Assistant-blue?logo=github&label=Github%20Repo&link=https%3A%2F%2Fgithub.com%2Fethan-hann%2FCyberRadio-Assistant)](https://github.com/ethan-hann/CyberRadio-Assistant)

![REPO-SIZE](https://img.shields.io/github/repo-size/ethan-hann/CyberRadio-Assistant)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/210b2b0ad9a748a6a35e3f7048acdf95)](https://app.codacy.com/gh/ethan-hann/CyberRadio-Assistant/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![GitHub Release](https://img.shields.io/github/v/release/ethan-hann/CyberRadio-Assistant?include_prereleases&display_name=release&style=flat)](https://github.com/ethan-hann/CyberRadio-Assistant/releases)
[![GPLv3](https://img.shields.io/static/v1.svg?label=📃%20License&message=GPL%20v3.0&color=informational)](https://choosealicense.com/licenses/gpl-3.0/)

[![Buy me a coffee](https://img.shields.io/static/v1.svg?label=Buy%20me%20a%20coffee&message=🥨&color=black&logo=buy%20me%20a%20coffee&logoColor=white&labelColor=6f4e37)](https://www.buymeacoffee.com/ethanhann) 

# Welcome to Cyber Radio Assistant! 📻

Cyber Radio Assistant is a standalone tool that helps you create and manage custom radio stations for the Cyberpunk 2077 mod, [radioExt](https://www.nexusmods.com/cyberpunk2077/mods/4591).

A brief list of features are below:

- **Create custom radio stations** using a GUI application instead of editing a `metadata.json` file.
- **Manage radio stations** downloaded from [NexusMods](https://www.nexusmods.com/cyberpunk2077/), with options to modify their metadata or playlist.
- **Add and delete songs** from your station, including songs from NexusMods.
- **Export stations to a staging folder** to avoid affecting your game directory directly.
- **Add a web stream** as a radio station instead of local audio files, and preview the stream from within the app.
- **Create and manage custom icons** for your stations, including integration with [Wolven Icon Generator (WIG)](https://github.com/ethan-hann/WolvenIconGenerator) for `.archive` file creation.
- **Localization** support for multiple languages, including English, Español, Français, Deutsch, Italiano, Português, Русский, and 中文.
- **Asynchronous task support** for background operations like icon generation, with progress tracking and cancellation.
- **User-friendly interface** with a clean and intuitive UI with drag-and-drop functionality for adding songs and icons. Detailed tooltips and user guidance simplify station setup and management.

This documentation has two parts:
1) A [**full guide**](docs/quick-start/introduction.md) that goes over how to use Cyber Radio Assistant 📃
2) An [**API reference**](api/RadioExt_Helper.forms.yml) that is useful if you want to see the classes and methods behind the scenes 😄

## Documentation Language Support
Currently, `docfx` does not provide a nice way to generate multi-language documentation, so these docs only support English.

<details>
  <summary><b>VirusTotal Report</b></summary>

Since you should never run `.exe` files from people you don't trust, I urge you to verify the hash of the file you downloaded against the below.

Also, check the VirusTotal report. There was 1 detection out of 70 but it is a false positive. If you don't believe me, look through the [source code](https://github.com/ethan-hann/CyberRadio-Assistant) and tell me where the virus is.

## Summary

- **File Name:** `CyberRadioAssistant.exe`
- **SHA-256:** 
`52558740d1a906c2d7e4618b4b9fe2e33f344eafcc2c5c972961ca9cbfdd156d`
- **Detection Ratio:** 1/70
- **Date:** 2024-10-20 17:43:52 UTC

## Detailed Report

For the full VirusTotal report, please visit the following link:

[View VirusTotal Report](https://www.virustotal.com/gui/file/52558740d1a906c2d7e4618b4b9fe2e33f344eafcc2c5c972961ca9cbfdd156d/detection)

## Key Findings

- **Antivirus Detections:** 
  - Symantec: **`Clean`**
  - McAfee: **`Clean`**
  - Kaspersky: **`Clean`**
  - Avast: **`Clean`**
  - BitDefender: **`Clean`**
  - Bkav Pro: `W64.AIDetectMalware` (**FALSE POSITIVE**)
  - MaxSecure: **`Clean`**

## Screenshots

Here are some screenshots from the VirusTotal report:

### Detection Overview
![vt_detection_overview](images/vt_detection_overview.png)

### Detailed Analysis
![vt_detailed_overview](images/vt_detailed_overview.png)

</details>

<details>
  <summary><b>TODO List</b></summary>

> [!TODO]  
> Implement editing of song's title within CRA.

> [!TODO]  
> Add station preview that allows previewing what the station would look and sound like in game.

> [!TODO]  
> Add a way to normalize the audio levels of station's songs.

</details>
