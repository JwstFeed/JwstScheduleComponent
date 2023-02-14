namespace Model

open System

type Observation() =
    member val public ClusterIndex = "" with get, set
    member val public VisitID = "" with get, set
    member val public PcsMode = "" with get, set
    member val public VisitType = "" with get, set
    member val public ScheduledStartTime = DateTime.UtcNow with get, set
    member val public Duration = "" with get, set
    member val public ScienceInstumentAndMode = "" with get, set
    member val public TargetName = "" with get, set
    member val public Category = "" with get, set
    member val public KeyWords = "" with get, set