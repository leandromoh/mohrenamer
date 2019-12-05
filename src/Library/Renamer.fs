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

let project files funcs =
    let processName fileName = List.fold (|>) fileName funcs
    let getProjection (file: FileInfo) = { DirectoryName = file.DirectoryName 
                                           OldName = file.Name
                                           NewName = processName file.Name }
    files |> List.map getProjection

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
