namespace EmpGraph.Core
open FSharp.Data

module emperorM2mApp =

//    let parse

    let parseZipFileStream stream =
        let streams = 
            stream
            |>zipHelper.getM2Mfiles
            |> Seq.toList
        errorHandling.Success streams.Length
        
