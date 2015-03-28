namespace EmpGraph.Core

module EmperorM2mApp =

    let processM2mFiles = 
        FileHelper.fetchFiles
        |> FileHelper.changeFileExtensions
        |> Parse.processFiles
        |> FileHelper.archiveFiles
        |> ignore