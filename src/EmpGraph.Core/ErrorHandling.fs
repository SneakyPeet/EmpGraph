namespace EmpGraph.Core
module errorHandling =

    type Result<'TSuccess> =
        | Success of 'TSuccess
        | Failure of string

    let bind oneTrackFunction twoTrackInput =
        match twoTrackInput with
        | Success s -> oneTrackFunction s
        | Failure f -> Failure f

    let tryCatch f x =
        try
            f x |> Success
        with
        | ex -> Failure ex.Message