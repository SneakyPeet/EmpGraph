namespace EmpGraph.Web.Controllers
open System.Web.Mvc
open System
open EmpGraph.Core

type FileController() =
    inherit Controller()

    let getSuccessMessage (result:List<emperorM2mApp.M2MDetails>) =
        match result.Length with
        | 0 -> "No Results Parsed"
        | 1 -> 
            let r = result.Head
            let m = String.Format("Movement: {0}% | Open: R{1} | Close: R{2}", r.change.ToString("0.00"), r.openValue.ToString("#,##0.00"), r.closeValue.ToString("#,##0.00"))
            match r.deposit with
            | 0m -> m
            | _ -> String.Format("{0} | Deposit: R{1}", m, r.deposit.ToString("#,##0.00"))
        | _ -> "Multiple Files Parsed"

    let returnWithNotification result = 
        match result with
        | errorHandling.Failure f -> notify.sendPushNotification f
        | errorHandling.Success s -> notify.sendPushNotification (getSuccessMessage s)

    let returnEmpty result =
        let c = 1 
        let a = c + 1
        match result with
        | errorHandling.Failure f -> ()
        | errorHandling.Success s -> s |> getSuccessMessage |> ignore

    [<HttpPost>]
    member this.ParseM2M()  =
        let files = this.Request.Files
        match files.Count with
        | 0 -> notify.sendPushNotification "No File Posted"
        | _ -> 
            let file = this.Request.Files.Get(0)
            let result = emperorM2mApp.parseZipFileStream file.InputStream
            returnWithNotification result
        ()
