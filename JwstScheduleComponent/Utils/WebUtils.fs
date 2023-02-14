namespace Utils

open System
open System.IO
open System.Net

module WebUtils =
    let getUrlContent (url: string) = 
           (new StreamReader(WebRequest.Create(Uri url)
                                       .GetResponse()
                                       .GetResponseStream()))
                                       .ReadToEnd()      