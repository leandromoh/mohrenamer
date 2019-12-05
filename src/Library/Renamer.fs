module Renamer

open System.IO

type FileConfiguration =
  { DirectoryName: string
    OldName: string
    NewName: string }

let project files funcs =
    let processName fileName = List.fold (fun name f -> f name) fileName funcs
    let getProjection (file:FileInfo) = { DirectoryName = file.DirectoryName 
                                          OldName = file.Name
                                          NewName = processName file.Name }
    files |> List.map getProjection

let rename files =
    let move file = 
        let oldPath = Path.Combine(file.DirectoryName, file.OldName)
        let newPath = Path.Combine(file.DirectoryName, file.NewName)
        File.Move(oldPath, newPath)
    List.iter move files

let getFiles dirName ext recursive =
    let searchType = if recursive then SearchOption.AllDirectories else SearchOption.TopDirectoryOnly
    Directory.GetFiles(dirName, "*." + ext, searchType)
    |> Array.map FileInfo
    |> Array.toList
