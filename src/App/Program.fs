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
    let dirName, extension, recursive = ("C:\Users\leandro.vieira\Desktop\idempotnece", "png", bool.Parse "true")
    let files = getFiles dirName extension recursive
    let funcs = [ insertEnd "xxx" ; removeOccurrence "OS" ; removeByIndex 0 0 ]
    let projection = project files funcs
    projection |> List.iter (fun f -> printfn "%A\n%A\n" f.OldPath f.NewPath)
    printfn "want to renames? (y/n)"
    match Console.ReadLine(), lazy(hasNewNameConflicts projection) with
    | "y", x when not x.Value -> rename projection
                                 printfn "arquivos renomeados"
                                 0
    | "y", _ ->                  printfn "nao é possivel renomear pois haverá arquivos com nomes repetido"
                                 1
    | _ ->                       printfn "entao tchau"
                                 1
