module Renamer

open System.IO

let project files funcs =
    let processName fileName = List.fold (fun name f -> f name) fileName funcs
    let getProjection (file:FileInfo) = (file.DirectoryName, file.Name, processName file.Name) 
    files |> List.map getProjection

let getFiles dirName ext recursive =
    let searchType = if recursive then SearchOption.AllDirectories else SearchOption.TopDirectoryOnly
    Directory.GetFiles(dirName, "*." + ext, searchType)
    |> Array.map FileInfo
    |> Array.toList
