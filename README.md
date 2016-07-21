# Cake.Modules
Modules for cakebuild.

## Cake.LoadRemote.Module
Extend #load with nuget support
this module will download and install the nuget package and reference the scripts where the #load directive is defined.

### Installation
Unzip the release file for "Cake.LoadRemote.Module" in youre modules directory.

### Usage
Put .cake files in a nuget package and reference the nuget like this #load "nuget:?package=Some.Nuget.Package"

### Configs
The config.json file handles configurations for youre nuget package create this file at the root of the scripts.

#### Example of a config.json file
{
  "FileOrder": [
    "pub.cake",
    "lib.cake"
  ]
}

#### Config Definitions
FileOrder: Defines a script load order, scriptA will load before scriptB.
