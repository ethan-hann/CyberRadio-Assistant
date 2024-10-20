# Localization

Cyber Radio Assistant supports three languages currently:

- :gb: - English
- :fr: - French
- :es: - Spanish

If you wish to provide translations for the application, that's awesome! 🥳 There is one major prerequiste you MUST meet if you want to translate the application:

**An ability to read and understand English (the primary language the application was built for) as well as being fluent in the language you are translating to.**

If you can do that, great! Let's get started.

---

Providing translations is easy!

1. Download and open the template file [translations.csv](https://drive.tortal.tech/wl/?id=597V5ZbK6aiQFlaMZmD7Vqa9kowUXNv8&fmode=download) in an editor of your choice. Excel would probably work best here but you can also just use VSCode or Notepad.

2. The very first line in the file, `Name,English` is where you should start. After `English,`, enter the language you are translating to. Also, include the [culture .NET tag](https://www.venea.net/web/culture_code) for the language in parentheses. For example, if I was providing translations for Italian that first line would look like this:

    `Name,English,Italian (it)`

    > [!NOTE]
    > The `Culture .NET` language tag is specific to Microsoft's .NET framework and has the format `LanguageCode2[-Country/RegionCode2]`. A list of these codes can be found [here](https://www.venea.net/web/culture_code).
    > 
    > If you don't include the Culture .NET tag, I won't know what the actual translation is for 😉

3. Once you've got the first line done, simply go through each line in the `.csv` file until you've translated everything.

    > [!IMPORTANT]
    > You must keep the same formatting when possible as the English (default culture) version. This means punctuation and placeholders. Of course, I understand that some languages handle punctuation differently; use your best judgement in these cases.
    >
    > For placeholders, this means lines like `Error pausing stream: {0}`. The `{0}` is a placeholder and should be present in the final translated string in the same logical place.

4. After you've got the whole file translated, save it and create a new issue on GitHub using the template: [Translation Request](https://github.com/ethan-hann/CyberRadio-Assistant/issues/new?assignees=ethan-hann&labels=translation&projects=&template=translation-request.md&title=%5BTRANSLATION%5D+-+%5BLanguage%5D). You will upload your translated `.csv` file there for review.

**After your translations are reviewed, they will be added to the next release of Cyber Radio Assistant!**