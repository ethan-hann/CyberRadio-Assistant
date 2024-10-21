# Localization

Cyber Radio Assistant supports a few languages currently:

- **English** 🇺🇸
- **Español (Spanish)** 🇪🇸
- **Français (French)** 🇫🇷
- **Deutsch (German)** 🇩🇪
- **Italiano (Italian)** 🇮🇹
- **Português (Portuguese)** 🇵🇹
- **Русский (Russian)** 🇷🇺
- **中文 (Chinese)** 🇨🇳

If you wish to provide translations for the application, that's awesome! 🥳 There is one major prerequiste you MUST meet if you want to translate the application:

**An ability to read and understand English (the primary language the application was built for) as well as being fluent in the language you are translating to.**

If you can do that, great! Let's get started.

1. Download and open the template file [translations.xlsx](https://drive.tortal.tech/wl/?id=Z2SZSDQjDwsKy4A0b7Wnw1Xub5yLR5US&fmode=download) in an editor of your choice. Excel would work best here but you can also use [OpenOffice](https://www.openoffice.org/) or [Microsoft Office Online](https://www.office.com/) if you don't have the Microsoft Office Suite.

2. The template spreadsheet has two columns: `Key` and `English (en)`. Please do not delete these columns. Instead, to add a new translation, add a new column with your language as the column header. Also, include the [culture .NET tag](https://www.venea.net/web/culture_code) for the language in parentheses.

    For example, if I was providing translations for Italian the first few lines would look like this:

    |     | A                 | B                 | C              |
    | --- | ----------------- | ----------------- | -------------- |
    | 1   | Key               | English (en)      | Italian (it)   |
    | 2   | lblApiHelp.Text   | Your API key...   | ...            |

    > [!NOTE]
    > The `Culture .NET` language tag is specific to Microsoft's .NET framework and has the format `LanguageCode2[-Country/RegionCode2]`. A list of these codes can be found [here](https://www.venea.net/web/culture_code).
    >
    > If you don't include the Culture .NET tag, I won't know what the actual translation is for 😉

3. Once you've got the first line done, simply go through each line in the `.xlsx` file until you've translated everything.

    > [!IMPORTANT]
    > You must keep the same formatting when possible as the English (default culture) version. This means punctuation and placeholders. Of course, I understand that some languages handle punctuation differently; use your best judgement in these cases.
    >
    > For placeholders, this means lines like `Error pausing stream: {0}`. The `{0}` is a placeholder and should be present in the final translated string in the same logical place.

4. After you've got the whole file translated, save it and create a new issue on GitHub using the template: [Translation Request](https://github.com/ethan-hann/CyberRadio-Assistant/issues/new?assignees=ethan-hann&labels=translation&projects=&template=translation-request.md&title=%5BTRANSLATION%5D+-+%5BLanguage%5D). You will upload your translated `.xlsx` file there for review.

**After your translations are reviewed, they will be added to the next release of Cyber Radio Assistant!**

## Translation Fixes

For full disclosure, I am only English speaking. Therefore, in order to provide a multi-lingual application I utilitzed Google Translate. Of course, as everyone knows, Google Translate doesn't always translate things appropiately or with the correct context.

If you come across any issues with translations in the application, please feel free to submit a translation request following the steps above with one small change:

- Only translate the lines that have incorrect translations.

## Future Translations

Updates to CRA may include new strings that need to be translated for your added language. If this is the case, it would be awesome if you decided to include updated translations when there is an update to the application. The `translations.xlsx` file will always contain the most recent strings to be translated.

However, if you opt not to update your translations, I will use Google Translate to translate the added strings in your language. If you decide to translate later, you can submit an updated translation following this guide.
