﻿namespace EmpGraph.Core
open System.IO
open System

module empFiles =
    let excelSearchPattern = "*.xls"

    let htmlExtension = ".html"

    let changeFileExtenion file extension =
        let newFile = Path.ChangeExtension(file, extension)
        File.Move(file, newFile)
        newFile
        
    let changeExtensions extension files= 
        files 
        |> Array.toList
        |> List.map(fun(file) -> changeFileExtenion file extension)

    let changeExtensionsToHtml = changeExtensions htmlExtension

    let changeEmperorFileExtensions filePath = 
        Directory.GetFiles(filePath, excelSearchPattern)
        |> changeExtensionsToHtml

    //parse
    let parseEmperorFiles files =
        files

    //archive
    let archiveFile file = 
        let fileDirectory = Path.GetDirectoryName(file)
        let fileName = Path.GetFileName(file)
        let archiveDirectory = fileDirectory + "\Archive"
        let _ = Directory.CreateDirectory(archiveDirectory)
        let finalPath = archiveDirectory + @"\" + fileName
        File.Move(file,finalPath)
        fileName

    let archiveEmperorFiles files =
        files 
        |> List.map(fun(file) -> archiveFile file)
        

    //process
    let processEmperorM2MFiles directory =
        changeEmperorFileExtensions "C:\EmpFile"
        |> parseEmperorFiles
        |> archiveEmperorFiles
        |> ignore