namespace EmpGraph.Web.Controllers
open System.Web.Mvc
open EmpGraph.Core

type FileController() =
    inherit Controller()

    let returnWithNotification result = 
        match result with
        | errorHandling.Failure f -> notify.sendPushNotification f
        | errorHandling.Success s -> notify.sendPushNotification ("Success: Files Processed " + s.ToString())

    let returnEmpty result =
        ()

    [<HttpPost>]
    member this.ParseM2M()  =
        let files = this.Request.Files
        match files.Count with
        | 0 -> notify.sendPushNotification "No File Posted"
        | _ -> 
            let file = this.Request.Files.Get(0)
            let result = emperorM2mApp.parseZipFileStream file.InputStream
            returnEmpty result
        ()
