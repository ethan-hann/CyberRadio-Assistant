---
name: Translation Request
about: Submit a request for new localizations or translations for the app.
title: "[TRANSLATION] - [Language]"
labels: translation
assignees: ethan-hann
body:
  - type: input
    id: language_name
    attributes:
      label: Language for Translation
      description: "Specify the language you are providing translations for (e.g., Italian, German)."
      placeholder: "e.g., Italian"
    validations:
      required: true
  - type: input
    id: culture_tag
    attributes:
      label: Culture Tag (.NET)
      description: "Provide the appropriate culture tag for the language (e.g., it-IT for Italian)."
      placeholder: "e.g., it-IT"
      value: ""
    validations:
      required: true
  - type: markdown
    attributes:
      value: "**Upload your `translations.xlsx` file** to this issue for review and verification."
---

## Translation Request

For translation instructions, please refer to the [Localization Guide](https://ethan-hann.github.io/CyberRadio-Assistant/docs/advanced-topics/localization.html).

### 1. Language Information
- **Language for Translation**: Specify the language you are providing translations for.
- **Culture Tag (.NET)**: Please provide the appropriate culture tag.  
  [List of Culture Tags](https://www.venea.net/web/culture_code)

### 2. Translation File
- **Upload your `translations.xlsx` file** to this issue for review and verification.


