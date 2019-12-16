module Renamer

open System.IO
open System.Linq
open MoreLinq

type FileConfiguration =
  { DirectoryName: string
    OldName: string
    NewName: string
    OldPath: string
    NewPath: string }

let createRecord dirName oldName newName  =
    { DirectoryName = dirName
      OldName = oldName
      NewName = newName
      OldPath = Path.Combine(dirName, oldName)
      NewPath = Path.Combine(dirName, newName)
    }

let processFileName fileName funcs = 
    let name = Path.GetFileNameWithoutExtension(fileName)
    let ext = Path.GetExtension(fileName)
    let newName = List.fold (|>) name funcs
    newName + ext 

let project (files: FileInfo list) funcs =
    files |> List.map (fun f -> createRecord f.DirectoryName f.Name <| processFileName f.Name funcs)

let private rename getNames =
    List.iter (getNames >> File.Move)

let renameForward =
    rename (fun f -> f.OldPath, f.NewPath)

let renameBackward =
    rename (fun f -> f.NewPath, f.OldPath)

let hasNewNameConflicts files = 
    List.length files <> MoreEnumerable.DistinctBy(files, fun f -> f.NewName).Count()
    
let getFiles dirName ext recursive =
    let searchType = if recursive then SearchOption.AllDirectories else SearchOption.TopDirectoryOnly
    Directory.GetFiles(dirName, "*." + ext, searchType)
    |> Array.map FileInfo
    |> Array.toList
