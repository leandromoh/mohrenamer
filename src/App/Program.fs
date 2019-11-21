// Learn more about F# at http://fsharp.org
// Based on https://docs.microsoft.com/pt-br/dotnet/fsharp/get-started/get-started-command-line

open System
open CommandLine

type ColorEnum = Red=0 | Yellow=1 | Blue=2      // enum 

type options = {
  [<Option('d', "directory", Required = true, HelpText = "Source directory of files.")>] directory : string;
  [<Option('r', "recursive", Required = true, HelpText = "Should or not include all subdirectories in a search operation.")>] recursive : bool;
  [<Option('p', "project", HelpText = "Should or not include all subdirectories in a search operation.")>] projection : bool;
}

[<EntryPoint>]
let main argv =
    
    0 // return an integer exit code

