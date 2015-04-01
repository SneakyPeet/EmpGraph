namespace EmpGraph.Core
open FSharp.Data

module EmperorM2mApp =

    let notify message =
        let group = "gLDyhCvRN4uBPts7yQujMTp1NEBpsE"
        let app = "ai43NudSF8M61HfrANug4pNh6dkAiE"
        Http.RequestString
            ( "https://api.pushover.net/1/messages.json?", httpMethod = "POST",
              query = ["token", app; "user", group; "message", message],
              headers = [ "Accept", "application/json" ])
        |> ignore

    let processM2mFiles = 
        FileHelper.fetchFiles
        |> FileHelper.changeFileExtensions FileHelper.htmlExtension
        //|> Parse.processFiles
        |> FileHelper.archiveFiles
        |> ignore

    
        
