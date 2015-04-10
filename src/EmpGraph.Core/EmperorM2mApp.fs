namespace EmpGraph.Core
open FSharp.Data
open System.IO
open System.Text.RegularExpressions

module emperorM2mApp =

    type M2MDetails = { client:string; id:string; openValue:string; closeValue:string; deposit:string}

    let private bf = "Brought Forward"
    let private rbf = "BroughtForward"
    let private cf = "Carried Forward"
    let private rcf = "CarriedForward"
    let private dp = "Deposit"
    let private ne = "Net equity"
    let private nowrap = "<td nowrap>"
    let private removeHtmlPattern = "<[^>]*>"
    let private noWhiteSpacePattern = @"\s+"

    let clearFluf (file:string) = 
        let frombf = file.Substring(file.LastIndexOf(bf))
        let tone = frombf.Substring(0,frombf.IndexOf(ne))
        let nohtml = Regex.Replace(tone,removeHtmlPattern,"")
        let replacebf = nohtml.Replace(bf,rbf).Replace(cf,rcf)
        Regex.Replace(replacebf,noWhiteSpacePattern,"|")

    let rec getDetail (details:M2MDetails) lines =
        match lines with
        | [] -> details
        | head::tail -> 
            match head with
            | "BroughtForward" -> 
                let n = { details with openValue = tail.Head }
                getDetail n tail
            | "Deposit" -> 
                let n = { details with deposit = tail.Head }
                getDetail n tail
            | "CarriedForward" ->
                let n = { details with closeValue = tail.Head }
                getDetail n tail
            | _ -> getDetail details tail

    let private parseToDetails (content:string) =
        let details = { client = "" ; id =""; openValue = ""; closeValue = ""; deposit = "" }
        (clearFluf content).Split('|')
        |> Array.toList
        |> getDetail details
        
        

    let private parse (file:Stream) =
        use reader = new StreamReader(file)
        reader.ReadToEnd() |> parseToDetails 

    let parseZipFileStream stream =
        let streams = 
            stream
            |>zipHelper.getXlsFiles
            |> Seq.toList //todo take out
            |> List.map parse
        errorHandling.Success streams.Length
        
