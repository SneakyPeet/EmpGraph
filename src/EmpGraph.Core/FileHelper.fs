namespace EmpGraph.Core

open System.IO
open System

module internal FileHelper =

    let htmlExtension = ".html"
    let xlsExtension = ".xls"
    
    let fetchFiles =
        let fileSearchPattern = "*" + xlsExtension
        let filePath = "C:\EmpFile"
        Directory.GetFiles(filePath, fileSearchPattern)
        |> Array.toList

    let changeFileExtenion extension file=
        let newFile = Path.ChangeExtension(file, extension)
        File.Move(file, newFile)
        newFile

    let changeFileExtensions extension files =
        files
        |> List.map(fun(file) -> changeFileExtenion extension file) 

    let archiveFile file = 
        let fileDirectory = Path.GetDirectoryName(file)
        let fileName = Path.GetFileNameWithoutExtension(file) + Guid.NewGuid().ToString().Substring(0, 5) + xlsExtension
        let archiveDirectory = fileDirectory + "\Archive"
        let _ = Directory.CreateDirectory(archiveDirectory)
        let finalPath = archiveDirectory + @"\" + fileName
        File.Move(file,finalPath)
        fileName

    let archiveFiles files = 
        files 
        |> List.map(fun(file) -> archiveFile file)

