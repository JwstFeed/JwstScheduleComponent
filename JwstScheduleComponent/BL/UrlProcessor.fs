namespace BL

open System
open Model
open Utils.WebUtils
open Utils.StringUtils

module UrlProcessor =
    let private unwantedRowsNumber = 4
    let private validColumnNumber = 9
    let private keys = dict [ 
        "VisitID", 0; 
        "PcsMode", 1; 
        "VisitType", 2; 
        "ScheduledStartTime", 3;
        "Duration", 4;
        "ScienceInstumentAndMode", 5;
        "TargetName", 6;
        "Category", 7;
        "KeyWords", 8;
    ]   
    
    let private cutUnwantedRows (obsTable: string): string[] =
        obsTable.RemoveRows unwantedRowsNumber

    let private isRowNotEmpty (obs: Observation): bool =
        isNotEmpty obs.TargetName
    
    let private toScheduleTableRow (observationsFileContent: string): string[] =
        let delimiter = "#unique#"
        observationsFileContent
            |> replace "  " delimiter
            |> split delimiter
            |> Seq.filter isNotEmpty
            |> Seq.map trim
            |> Seq.toArray

    let private getKey (key: string): int =
        match keys.TryGetValue key with
        | true, value -> value
        | _           -> raise (Exception $"No Key Found: {key}")
    
    let private getScheduleStartTime (obsRow: string[]): DateTime =
        DateTime.Parse obsRow.[getKey "ScheduledStartTime"]

    let private getVisitId (obsRow: string[]): string =
        obsRow.[getKey "VisitID"]
    
    let private getClusterIndex (obsRow: string[]): string =
        let unixTime = (getScheduleStartTime obsRow).ToUnixTime()
        let visitId = getVisitId obsRow
        $"{unixTime}_{visitId}"
    
    let private toObservation (fileObservationRow: string): Observation =
        let observationRow = toScheduleTableRow fileObservationRow
        match observationRow.Length with
        | x when x = validColumnNumber -> Observation (ScienceInstumentAndMode = observationRow.[getKey "ScienceInstumentAndMode"],
                                                       ScheduledStartTime = getScheduleStartTime observationRow,
                                                       TargetName = observationRow.[getKey "TargetName"],
                                                       VisitType = observationRow.[getKey "VisitType"],
                                                       ClusterIndex = getClusterIndex observationRow,
                                                       Category = observationRow.[getKey "Category"],
                                                       KeyWords = observationRow.[getKey "KeyWords"],
                                                       PcsMode = observationRow.[getKey "PcsMode"],
                                                       VisitID = getVisitId observationRow)
        | _                            -> Observation ()

    let public GetObservations (url: string): seq<Observation> =
        url 
            |> getUrlContent
            |> cutUnwantedRows
            |> Seq.map toObservation
            |> Seq.filter isRowNotEmpty
