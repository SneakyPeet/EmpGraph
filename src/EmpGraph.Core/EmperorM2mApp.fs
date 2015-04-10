namespace EmpGraph.Core
open FSharp.Data
open System.IO
open System.Text.RegularExpressions

module emperorM2mApp =

    type M2MDetails = { client:string; id:string; openValue:decimal; closeValue:decimal; deposit:decimal}

    type private m2mFile = HtmlProvider<"M2M_TypeProvider.html">
    let private bf = "Brought Forward"
    let private rbf = "BroughtForward"
    let private cf = "Carried Forward"
    let private rcf = "CarriedForward"
    let private ne = "Net equity"
    let private nowrap = "<td nowrap>"
    let private removeHtmlPattern = "<[^>]*>"
    let private noWhiteSpacePattern = @"\s+"
    let private depositMatch = "\|Deposit\|*\|"

    let clearFluf (file:string) = 
        let frombf = file.Substring(file.LastIndexOf(bf))
        let tone = frombf.Substring(0,frombf.IndexOf(ne))
        let nohtml = Regex.Replace(tone,removeHtmlPattern,"")
        let replacebf = nohtml.Replace(bf,rbf).Replace(cf,rcf)
        Regex.Replace(replacebf,noWhiteSpacePattern,"\n")

    let private parseToDetails (content:string) =
        let details = { client = "" ; id =""; openValue = 0m; closeValue = 0m; deposit = 0m }
        let clean = clearFluf content
        let deposit =  Regex.Match(clean, depositMatch)//.Result().Remove("|").Remove("Deposit")
        0

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
        
