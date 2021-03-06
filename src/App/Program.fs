﻿// Learn more about F# at http://fsharp.org
// Based on https://docs.microsoft.com/pt-br/dotnet/fsharp/get-started/get-started-command-line

open System
open CommandLine
open Renamer
open AdvancedFuncs

type ColorEnum = Red=0 | Yellow=1 | Blue=2      // enum 

type Options = {
  [<Option('d', "directory", Required = true, HelpText = "Source directory of files.")>] directory : string;
  [<Option('r', "recursive", Required = true, HelpText = "Should or not include all subdirectories in a search operation.")>] recursive : bool;
  [<Option('p', "project", HelpText = "Should or not include all subdirectories in a search operation.")>] projection : bool;
}

let renames projection = 
    renameForward projection
    printfn "arquivos renomeados"
    printfn "want to revert? (y/n)"
    match Console.ReadLine() with
    | "n" -> 1
    | _ ->   renameBackward projection
             0

let menu =
    let dirName, extension, recursive = ("C:\Users\leandro\Desktop\\batata", "txt", bool.Parse "true")
    let files = getFiles dirName extension recursive
    let funcs = [ insertEnd "xxx" ; removeOccurrence "OS" ; removeByIndex 0 0 ]
    let finalFunc = List.reduce (>>) funcs |> processFileName
    let projection = project files finalFunc
    projection |> List.iter (fun f -> printfn "%A\n%A\n" f.OldPath f.NewPath)
    printfn "want to renames? (y/n)"
    match Console.ReadLine(), lazy(hasNewNameConflicts projection) with
    | "y", x when not x.Value -> renames projection
    | "y", _ ->                  printfn "nao é possivel renomear pois haverá arquivos com nomes repetido"
                                 2
    | _ ->                       printfn "entao tchau"
                                 3

[<EntryPoint>]
let main argv = menu
