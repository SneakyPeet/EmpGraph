namespace EmpGraph.Web.Controllers
open System.Web.Mvc
open System
open EmpGraph.Core

type FileController() =
    inherit Controller()

    let getSuccessMessage (result:List<M2MDetails>) =
        match result.Length with
        | 0 -> "No Results Parsed"
        | 1 -> 
            let r = result.Head
            let m = String.Format("Portfolio {4} on {3} | Movement: {0}% | Open: R{1} | Close: R{2}", r.change.ToString("0.00"), r.openValue.ToString("#,##0.00"), r.closeValue.ToString("#,##0.00"), r.date.ToString("dd MMM yy"), r.account)
            match r.deposit with
            | 0m -> m
            | _ -> String.Format("{0} | Deposit: R{1}", m, r.deposit.ToString("#,##0.00"))
        | _ -> "Multiple Files Parsed"

    let returnWithNotification user result = 
        match result with
        | errorHandling.Failure f -> notify.sendPushNotification user f
        | errorHandling.Success s -> notify.sendPushNotification user (getSuccessMessage s)

    let returnEmpty user result =
        let c = 1 
        let a = c + 1
        match result with
        | errorHandling.Failure f -> ()
        | errorHandling.Success s -> s |> getSuccessMessage |> ignore

    [<HttpPost>]
    member this.ParseM2M(parseuser)  =
        let files = this.Request.Files
        match files.Count with
        | 0 -> notify.sendPushNotification parseuser "No File Posted"
        | _ -> 
            let file = this.Request.Files.Get(0)
            let result = emperorM2mApp.parseZipFileStream file.InputStream
            returnWithNotification parseuser result
        ()
