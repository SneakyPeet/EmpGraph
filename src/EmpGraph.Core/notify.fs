namespace EmpGraph.Web.Controllers
open FSharp.Data

module notify =

    let sendPushNotification user message =
        let app = "ai43NudSF8M61HfrANug4pNh6dkAiE"
        Http.RequestString
            ( "https://api.pushover.net/1/messages.json?", httpMethod = "POST",
              query = ["token", app; "user", user; "message", message; "title", "Emperor M2M"],
              headers = [ "Accept", "application/json" ])
        |> ignore