open BL

let getUrls = 
    DalManager.Getsources

let writeLog log = 
    DalManager.WriteLog log

let processSingleSource (source: string) = 
    source
        |> UrlProcessor.GetObservations
        |> DalManager.InsertNewSchedule
    DalManager.MarkUrlAsDone source 
        |> ignore

[<EntryPoint>]
let main argv =
    getUrls |> Seq.iter(processSingleSource)
    0
