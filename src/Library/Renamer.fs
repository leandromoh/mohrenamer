module Renamer

open System.IO

let project files funcs =
    let processName fileName = List.fold (fun name f -> f name) fileName funcs
    let getProjection (file:FileInfo) = (file.DirectoryName, file.Name, processName file.Name) 
    files |> List.map getProjection

let rename files =
    let move (dirName, oldName, newName) = 
        let oldPath = Path.Combine(dirName, oldName)
        let newPath = Path.Combine(dirName, newName)
        File.Move(oldPath, newPath)
    List.iter move files

let getFiles dirName ext recursive =
    let searchType = if recursive then SearchOption.AllDirectories else SearchOption.TopDirectoryOnly
    Directory.GetFiles(dirName, "*." + ext, searchType)
    |> Array.map FileInfo
    |> Array.toList
