// Learn more about F# at http://fsharp.org

open System
open System.IO
open FSharpOxfordDictionary.Client
open FSharp.Data
open Newtonsoft.Json

type Config = {AppId:string; ApiKey: string}
let config = "config.json" |> File.ReadAllLines |> String.concat "" |> JsonConvert.DeserializeObject<Config>

[<EntryPoint>]
let main argv =  
    let client = new OxfordDictionaryClient(new OxfordDictionarySettings(config.AppId.ToString(), config.ApiKey.ToString()))
    
    printfn "Enter your word:"
    let lemma = Console.ReadLine();
    
    let result = client.GetLemma("en", lemma)
    let serializedResult = client.GetLemma("en", lemma) |> JsonConvert.SerializeObject
    
    printfn "Lemma results: \n %s" serializedResult
    0 // return an integer exit code
