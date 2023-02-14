namespace Utils

open System

module StringUtils =
    let newLineArr = Environment.NewLine.ToCharArray()
    
    let replace (oldValue: string) (newValue: string) (originalStr: string): string =
        originalStr.Replace(oldValue, newValue)
    
    let split (delimiter: string) (originalStr: string): string[] =
        originalStr.Split(delimiter)
    
    let isNotEmpty (originalStr: string): bool =
        not <| String.IsNullOrEmpty originalStr
    
    let trim (originalStr: string): string =
        originalStr.Trim()

    type System.DateTime with
        member x.ToUnixTime(): string = ((DateTimeOffset)x).ToUnixTimeSeconds().ToString()

    type System.String with
        member x.RemoveRows(rowsNum): string[] = x.Split(newLineArr).[rowsNum..]