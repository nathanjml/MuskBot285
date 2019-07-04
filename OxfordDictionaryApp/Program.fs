// Learn more about F# at http://fsharp.org

open System
open FSharpOxfordDictionary.Client

[<EntryPoint>]
let main argv =
    printfn "Enter your api key:"
    let apiKey = Console.ReadLine();
    let client = new OxfordDictionaryClient(new OxfordDictionarySettings(apiKey))
    
    printfn "Enter your word:"
    let lemma = Console.ReadLine();
    let definitionResult = client.GetLemma(lemma)
    0 // return an integer exit code
