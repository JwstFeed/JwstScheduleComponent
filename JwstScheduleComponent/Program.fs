open BL

let getUrls = 
    DalManager.Getsources

let updateSourceStatus (source: string) = 
    DalManager.UpdateSourceStatus source

let writeLog log = 
    DalManager.WriteLog log
    printfn "%s" log

let processSingleSource (source: string) = 
    source
        |> UrlProcessor.GetObservations
        |> DalManager.InsertNewSchedule
    DalManager.UpdateSourceStatus source 
        |> ignore

[<EntryPoint>]
let main argv =
    getUrls |> Seq.iter(processSingleSource)
    0