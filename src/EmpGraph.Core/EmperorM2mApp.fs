namespace EmpGraph.Core
open FSharp.Data
open System.IO

module emperorM2mApp =

    type private m2mFile = HtmlProvider<"M2M_TypeProvider.html">

    type M2MDetails = { client:string; id:string; openValue:decimal; closeValue:decimal; deposit:decimal}

    let rec parseRowsToDetails rows details =
        match rows with
        | [] -> details
        | head :: tail -> parseRowsToDetails tail details

    let private parse (file:Stream) =
        let m2m = m2mFile.Load(file)
        m2m.Tables.Table1.Rows |> Array.toList |> parseRowsToDetails

    let parseZipFileStream stream =
        let streams = 
            stream
            |>zipHelper.getXlsFiles
            |> Seq.toList //todo take out
            |> List.map parse
        errorHandling.Success streams.Length
        
