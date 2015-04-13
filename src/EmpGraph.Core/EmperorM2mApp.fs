namespace EmpGraph.Core
open FSharp.Data
open System
open System.Globalization
open System.IO
open System.Text.RegularExpressions

module emperorM2mApp =

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

    let toDecimal (currency:string) =
        let cleanCurrency = currency.Replace(",","")
        Decimal.Parse(cleanCurrency)

    let toDate (date:string) = 
        DateTime.ParseExact(date, "dd-MMM-yy", CultureInfo.InvariantCulture)

    let calcChange (d:M2MDetails) =
        let c = ((d.closeValue-d.deposit)/d.openValue) - 1m
        {d with change = c*100m}

    let rec getDetail (details:M2MDetails) lines =
        match lines with
        | [] -> calcChange details
        | head::tail -> 
            match head with
            | "BroughtForward" -> 
                let n = { details with openValue = (toDecimal tail.Head); date = toDate tail.Tail.Head }
                getDetail n tail
            | "Deposit" -> 
                let n = { details with deposit = (toDecimal tail.Head) }
                getDetail n tail
            | "CarriedForward" ->
                let n = { details with closeValue = (toDecimal tail.Head) }
                getDetail n tail
            | _ -> getDetail details tail

    let private parseToDetails (content:string) =
        let details = { account =""; date = DateTime.Now; openValue = 0m; closeValue = 0m; deposit = 0m; change = 0m }
        (clearFluf content).Split('|')
        |> Array.toList
        |> getDetail details
        
    let parseAccountName fileName = 
        (Path.GetFileNameWithoutExtension fileName).Substring(9)
        

    let private parse fileInput =
        use reader = new StreamReader(fileInput.file)
        let details = reader.ReadToEnd() |> parseToDetails
        let account = fileInput.name |> parseAccountName
        { details with account = account }

    let parseZipFileStream stream =
        let results = 
            stream
            |>zipHelper.getXlsFiles
            |> Seq.toList //todo take out
            |> List.map parse
        errorHandling.Success results
        
