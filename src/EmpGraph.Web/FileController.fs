namespace EmpGraph.Web.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http
open System.Threading.Tasks
open System.IO
open EmpGraph.Core

[<RoutePrefix("api2/file")>]
type fileController() =
    inherit ApiController()

    let extractZipfile (streamProvider:MultipartMemoryStreamProvider) =
        let file = streamProvider.Contents.First();
        let fileName = file.Headers.ContentDisposition.FileName
        match fileName.Contains ".zip" with
        | true -> errorHandling.Success "Works"
        | _ -> errorHandling.Failure "Not a .zip file"

    let extractFileContent (request:HttpRequestMessage)=
        let streamProvider = new MultipartMemoryStreamProvider();
        let task = request.Content.ReadAsMultipartAsync(streamProvider)
        task.Wait()
        let count = streamProvider.Contents.Count
        match count with
        | 1 -> errorHandling.Success streamProvider
        | _ -> errorHandling.Failure "Can only handle 1 file per request"
        
    let parse request =
        request
        |> extractFileContent
        |> errorHandling.bind extractZipfile
        //|> EmperorM2mApp.parseZipFileStream
        //|> EmperorM2mApp.notify
        

    [<Route("")>]
    member x.Post() : IHttpActionResult =
        match x.Request.Content.IsMimeMultipartContent() with
        | true -> 
            let result = parse x.Request
            match result with
            | errorHandling.Success s -> x.Ok(s) :> _
            | errorHandling.Failure f-> x.BadRequest(f) :> _
        | false -> x.BadRequest("Does Not Contain Mime Content") :> _
//        | true -> EmperorM2mApp.notify "Trigger Works"
//                  x.Ok("Success") :> _
//        | false -> EmperorM2mApp.notify "Trigger Did Not Attach File" 
//                   x.BadRequest("Does Not Contain Mime Content") :> _
