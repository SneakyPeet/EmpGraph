namespace EmpGraph.Web.Controllers
open FSharp.Data

module notify =

    let sendPushNotification message =
        let group = "gLDyhCvRN4uBPts7yQujMTp1NEBpsE"
        let app = "ai43NudSF8M61HfrANug4pNh6dkAiE"
        Http.RequestString
            ( "https://api.pushover.net/1/messages.json?", httpMethod = "POST",
              query = ["token", app; "user", group; "message", message],
              headers = [ "Accept", "application/json" ])
        |> ignore