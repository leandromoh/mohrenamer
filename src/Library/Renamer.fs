module Renamer

open System.IO
open System.Linq
open MoreLinq

type FileConfiguration =
  { DirectoryName: string
    OldName: string
    NewName: string }
    member this.OldPath = Path.Combine(this.DirectoryName, this.OldName)
    member this.NewPath = Path.Combine(this.DirectoryName, this.NewName)

let processFileName fileName funcs = 
    let name = Path.GetFileNameWithoutExtension(fileName)
    let ext = Path.GetExtension(fileName)
    let newName = List.fold (|>) name funcs
    newName + ext 

let project (files: FileInfo list) funcs =
    files |> List.map (fun f -> { DirectoryName = f.DirectoryName 
                                  OldName = f.Name
                                  NewName = processFileName f.Name funcs })

let rename files =
    List.iter 
        (fun (file: FileConfiguration) -> File.Move(file.OldPath, file.NewPath))
        files

let hasNewNameConflicts files = 
    List.length files <> MoreEnumerable.DistinctBy(files, fun f -> f.NewName).Count()
    
let getFiles dirName ext recursive =
    let searchType = if recursive then SearchOption.AllDirectories else SearchOption.TopDirectoryOnly
    Directory.GetFiles(dirName, "*." + ext, searchType)
    |> Array.map FileInfo
    |> Array.toList
