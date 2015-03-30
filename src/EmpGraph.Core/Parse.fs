namespace EmpGraph.Core

open System.IO
open FSharp.Data 

module internal Parse =

    let processFiles files =
        files

//type m2mTypeProvider = HtmlProvider<"M2M_TypeProvider.html">
//
//let fileToHtmlTypeProvider file = 
//    use filestream = File.OpenRead(file)
//    m2mTypeProvider.Load(filestream)
//
//let parseRow row =
//    row
//
//let readEmperorHtmlFile file =
//    let doc = fileToHtmlTypeProvider file
//    doc.Tables.Table1.Rows
//    |> parseRow
////    let document = fileToHtmlDocument file
////    document
//
//let parseEmperorHtmlFile file =
//    file
//    |> readEmperorHtmlFile
//
//let parseEmperorHtmlFiles files = 
//    files
//    |> List.map(fun(file) -> parseEmperorHtmlFile file)
//    |> ignore
//    files