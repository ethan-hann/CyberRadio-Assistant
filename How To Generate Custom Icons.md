# How to generate InkAtlas files for Cyberpunk 2077 Modding
Two tools are required:
- The Wolvenkit CLI tool (dotnet tool)
  - `dotnet tool install -g wolvenkit.cli`
- The inkatlas generator tool (python tool)
  - [Github Link](https://github.com/DoctorPresto/Cyberpunk-Helper-Scripts/blob/main/generate_inkatlas.py)
  - Right now, there is no arguments that can be passed in. It is supplied it's arguments during execution of the script.
  - I have converted it (and it's dependencies) into an `.exe` file that can be run from within CRA.

The inkatlas tool generates three files from the PNG's within a folder:
- `filename.inkatlas.json`
- `filename.png`
- `filename_1080.png`

The output file path **MUST** be within this structure for the tool to work:

`../source/raw/`

This is because the tool expects to be run against a WolvenKit project. We can bypass this by "mimicking" what a project structure would look like.

Once we have the raw files, we can generate the `.archive` file using the WolvenKit CLI tool:

- Generate `.inkatlas`:
  - `cp77tools convert deserialize "X:\Files\Downloads\generate inkatlas script\dist\output\source\raw\test.inkatlas.json"`
- Generate `.xbm` texture file:
  - `cp77tools import -p "X:\Files\Downloads\generate inkatlas script\dist\output\source\raw"`

Create a directory structure for the faux mod: `<radio name>\base\icon\`

- Generate final `.archive` file:
  - `cp77tools pack -p "X:\Files\Downloads\generate inkatlas script\dist\output\source\raw\<radio name>"`
  - For this to work, we need to make sure our directory structure within the root folder is as follows:
    - `<radio name>\base\icon\<..files generated above..>`
    - The `radio name` will become the name of the final `.archive` file. For instance, if the folder structure is `awesome station\base\icon\..`, the output file would be `awesome station.archive`.

## Sequence of commands example
-------------------------------
```batch
X:\Files\Downloads\generate inkatlas script\generate_test>mkdir source

X:\Files\Downloads\generate inkatlas script\generate_test>generate_inkatlas.exe

INKATLAS GENERATOR INPUT:
    Enter the path to the folder containing your individual icon PNG images: .
    Enter the path to output the raw inkatlas files for you to import in Wolvenkit: .\source
    Enter the name for your new inkatlas file (without extension): awesome icon
Data has been saved to .\source\raw\awesome icon.inkatlas.json
Combined image has been saved to .\source\raw\awesome icon.png
Combined image has been saved to .\source\raw\awesome icon_1080.png

X:\Files\Downloads\generate inkatlas script\generate_test>cp77tools convert deserialize ".\source\raw\awesome icon.inkatlas.json"
[ 0: Information ] - Found 1 files to process.
[ 0: Success     ] - Imported awesome icon.inkatlas.json to X:\Files\Downloads\generate inkatlas script\generate_test\source\raw\awesome icon.inkatlas.
[ 0: Success     ] - Converted X:\Files\Downloads\generate inkatlas script\generate_test\source\raw\awesome icon.inkatlas.json to CR2W
[ 0: Information ] - Elapsed time: 4200ms.

X:\Files\Downloads\generate inkatlas script\generate_test>cp77tools import -p ".\source\raw"
[ 0: Warning     ] - Image dimension (width and/or height) is an odd number. Texture might not work as expected.
[ 0: Information ] - Imported 2/2 file(s)

X:\Files\Downloads\generate inkatlas script\generate_test>mkdir ..\base\icon

X:\Files\Downloads\generate inkatlas script\generate_test>mkdir base\icon

X:\Files\Downloads\generate inkatlas script\generate_test>mkdir "awesome mod\base\icon"

X:\Files\Downloads\generate inkatlas script\generate_test>cp ".\source\raw\awesome icon.xbm" "base\icon\awesome icon.xbm
"
'cp' is not recognized as an internal or external command,
operable program or batch file.

X:\Files\Downloads\generate inkatlas script\generate_test>copy ".\source\raw\awesome icon.xbm" "base\icon\awesome icon.x
bm"
The system cannot find the path specified.
        0 file(s) copied.

X:\Files\Downloads\generate inkatlas script\generate_test>copy ".\source\raw\awesome icon.xbm" "awesome mod\base\icon\aw
esome icon.xbm"
        1 file(s) copied.

X:\Files\Downloads\generate inkatlas script\generate_test>copy ".\source\raw\awesome icon.inkatlas" "awesome mod\base\ic
on\awesome icon.inkatlas"
        1 file(s) copied.

X:\Files\Downloads\generate inkatlas script\generate_test>cp77tools pack -p "awesome mod"
[ 0: Success     ] - Finished packing X:\Files\Downloads\generate inkatlas script\generate_test\awesome mod.archive.
```

## To unpack a .archive and get back a png file
-----------------------------------------------
First, unbundle the `.archive` file:

`cp77tools unbundle -p "X:\Files\Downloads\generate inkatlas script\dist\vwave icon.archive" -o "X:\Files\Downloads\generate inkatlas script\dist\output"`

Then, we can convert the `.xbm` file to a `.png`:

`cp77tools export --uext png -p "X:\Files\Downloads\generate inkatlas script\dist\output\base\icon\vwave.xbm" -o "X:\Files\Downloads\generate inkatlas script\dist\output\base\icon"`