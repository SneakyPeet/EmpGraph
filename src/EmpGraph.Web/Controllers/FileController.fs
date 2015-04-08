namespace EmpGraph.Web.Controllers
open System.Web.Mvc
open EmpGraph.Core

type FileController() =
    inherit Controller()

    [<HttpPost>]
    member this.ParseM2M()  =
        let files = this.Request.Files
        match files.Count with
        | 0 -> emperorM2mApp.notify "No File Posted"
        | _ -> 
            let file = this.Request.Files.Get(0)
            let result = emperorM2mApp.parseZipFileStream file.InputStream
            match result with
            | errorHandling.Failure f -> emperorM2mApp.notify f
            | errorHandling.Success s -> emperorM2mApp.notify "Success"
        ()
