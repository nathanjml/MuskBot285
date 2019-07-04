namespace FSharpOxfordDictionary.Responses
    module Lemma =
        type GrammaticalFeature = {
            Id: string
            Text: string
            Type: string
        }

        type Inflection = {
            Id: string
            Text: string
        }

        type LexicalCategory = {
            Id: string
            Text: string
        }

        type LexicalEntry = {
            GrammaticalFeatures: GrammaticalFeature[]
            InflectionOf: Inflection[]
            Language: string
            LexicalCategory: LexicalCategory
            Text: string
        }

        type LemmaResult = {
            Id: string
            Language: string
            LexicalEntries: GrammaticalFeature[]
        }
        
        type LemmaResponse = {
            Results: LemmaResult[]
        }
