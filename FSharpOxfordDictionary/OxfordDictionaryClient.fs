namespace FSharpOxfordDictionary
open HttpFs.Client
open System
open Hopac

module Client =
    type OxfordDictionarySettings(appId:string, apiKey:string, apiVersion:string, baseUrl: string) =
        let appId = appId
        let apiKey = apiKey
        let apiVersion = apiVersion
        let baseUrl = baseUrl
        new (appId:string, apiKey:string, apiVersion:string) = OxfordDictionarySettings(apiKey, apiVersion, "https://od-api.oxforddictionaries.com/api/")
        new (appId:string, apiKey: string) = OxfordDictionarySettings(appId, apiKey, "v2")

        
        member this.AppId = appId
        member this.ApiKey = apiKey
        member this.ApiVersion = apiVersion
        member this.BaseUrl = baseUrl
        
    type OxfordDictionaryClient(settings:OxfordDictionarySettings) =
        let appId = settings.AppId
        let apiKey = settings.ApiKey
        let url = settings.BaseUrl + settings.ApiVersion + "/"
        
        type ApiHttpResponse =
            | Ok of body:string
            | Error of statusCode:int
            | Exception of e:exn
        
        let getResponse(url:string) : Async<ApiHttpResponse> =
            let response = Request.createUrl Get url
                          |> Request.setHeader(ContentType (ContentType.create("application", "json")))
                          |> Request.setHeader("app_id", appId)
                          |> Request.setHeader("app_key", apiKey)
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
                        
        