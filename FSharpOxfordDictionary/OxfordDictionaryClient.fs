namespace FSharpOxfordDictionary

open System.IO
open HttpFs.Client
open Hopac
open Responses.Lemma
open Newtonsoft.Json
open YoLo

module Client =
    type OxfordDictionarySettings(appId:string, apiKey:string, apiVersion:string, baseUrl: string) =
        let appId = appId
        let apiKey = apiKey
        let apiVersion = apiVersion
        let baseUrl = baseUrl
        new (appId:string, apiKey:string, apiVersion:string) = OxfordDictionarySettings(appId, apiKey, apiVersion, "https://od-api.oxforddictionaries.com/api/")
        new (appId:string, apiKey: string) = OxfordDictionarySettings(appId, apiKey, "v2")

        
        member this.AppId = appId
        member this.ApiKey = apiKey
        member this.ApiVersion = apiVersion
        member this.BaseUrl = baseUrl
        
    type ApiHttpResponse =
        | Ok of body:string
        | Error of statusCode:int
        | Exception of e:exn
        
    type OxfordDictionaryClient(settings:OxfordDictionarySettings) =
        let appId = settings.AppId
        let apiKey = settings.ApiKey
        let url = (settings.BaseUrl, settings.ApiVersion) |> Path.Combine
                         
        let getApiResponse(url, appId, apiKey) : Async<ApiHttpResponse> =
            let response = Request.createUrl Get url
                          |> Request.setHeader(Accept "application/json")
                          |> Request.setHeader(Custom ("app_id", appId))
                          |> Request.setHeader(Custom ("app_key", apiKey))
                          |> getResponse
                          
            
                          
            response |> Alt.afterJob (fun resp ->
                     match resp.statusCode with
                     | x when x < 300 ->                     
                         resp
                         |> Response.readBodyAsString
                         |> Job.map Ok
                     | x ->
                        Error resp.statusCode
                        |> Job.result
                     )|> Alt.toAsync
            
        let getLemma(lang, word) : LemmaResult =
            let uri = (url, "lemmas", lang, word) |> Path.Combine
            printfn "uri %s" uri
            let lemmaResult = getApiResponse(uri, appId, apiKey)
                              |> Async.RunSynchronously
                                 
                              
            
            lemmaResult |> fun response ->
                        match response with
                        | Ok body -> body |> JsonConvert.DeserializeObject<LemmaResult>
                        | _ -> Unchecked.defaultof<LemmaResult> 
            
            
        member this.GetLemma = getLemma
            