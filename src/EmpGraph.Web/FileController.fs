namespace EmpGraph.Web.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http
open System.Threading.Tasks;
open EmpGraph.Core

[<RoutePrefix("api2/file")>]
type fileController() =
    inherit ApiController()

    [<Route("")>]
    member x.Post() : IHttpActionResult =
        match x.Request.Content.IsMimeMultipartContent() with
        | true -> EmperorM2mApp.notify "Trigger Works"
                  x.Ok("Success") :> _
        | false -> EmperorM2mApp.notify "Trigger Did Not Attach File" 
                   x.BadRequest("Does Not Contain Mime Content") :> _
