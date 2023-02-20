open BL
open BL.DAL

let getUrls = 
    DalManager.Getsources

let writeLog log = 
    DalManager.WriteLog log

let processSingleSource (source: string) = 
    source
        |> UrlProcessor.GetObservations
        |> insertNewSchedule
    markUrlAsDone source 
        |> ignore

[<EntryPoint>]
let main argv =
    try
        getUrls 
        |> Seq.iter processSingleSource
    with
    | ex -> writeLog $"{ex.Message} | {ex.InnerException} | {ex.StackTrace}"
    0
