namespace EmpGraph.Web.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http
namespace EmpGraph.Web.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Web
open System.Web.Mvc
open System.Web.Mvc.Ajax
open EmpGraph.Core

type FileController() =
    inherit Controller()

    [<HttpPost>]
    member this.ParseM2M(formCollection:FormCollection)  =
        let file = this.Request.Files.Get("file")
        match file with
        | null -> EmperorM2mApp.notify "No File Posted"
        | _ -> 
            let result = EmperorM2mApp.parseZipFileStream file.InputStream
            match result with
            | errorHandling.Failure f -> EmperorM2mApp.notify f
            | errorHandling.Success s -> EmperorM2mApp.notify "Success"
        ()
