namespace BL

open Model

module DalManager =
    let sources = DAL.GetSources

    let Getsources: seq<string> = seq [for s in sources do if not <| s.isProcessed then yield s.url]

    let MarkUrlAsDone (source: string) = ()

    let InsertNewSchedule (schedule: seq<Observation>) = ()

    let WriteLog (log: string) = ()