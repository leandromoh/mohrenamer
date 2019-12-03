// Learn more about F# at http://fsharp.org
// Based on https://docs.microsoft.com/pt-br/dotnet/fsharp/get-started/get-started-command-line

open System
open CommandLine
open Renamer
open AdvancedFuncs

type ColorEnum = Red=0 | Yellow=1 | Blue=2      // enum 

type options = {
  [<Option('d', "directory", Required = true, HelpText = "Source directory of files.")>] directory : string;
  [<Option('r', "recursive", Required = true, HelpText = "Should or not include all subdirectories in a search operation.")>] recursive : bool;
  [<Option('p', "project", HelpText = "Should or not include all subdirectories in a search operation.")>] projection : bool;
}

[<EntryPoint>]
let main argv =
    let files = getFiles "C:\Users\leandro.vieira\Desktop\idempotnece" "png" <| bool.Parse "true"
    let funcs = [ insertEnd "xxx" ; removeOccurrence "OS" ; removeByIndex 0 1 ]
    project files funcs
    |> List.iter (printfn "%A")
    0 // return an integer exit code

