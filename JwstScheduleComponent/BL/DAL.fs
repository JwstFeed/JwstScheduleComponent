namespace BL

open Model

module DalManager =
    let sources = ()

    let getsources: seq<string> = seq [for s in sources do if not <| s.isProcessed then yield s.url]

    let markUrlAsDone (source: string) = ()

    let insertNewSchedule (schedule: seq<Observation>) = ()

    let writeLog (log: string) = ()
