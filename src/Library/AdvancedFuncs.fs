module AdvancedFuncs

open System

let removeByIndex startIndex count =
    fun (str: string) -> str.Remove(startIndex, count)

let removeOccurrence text =
    fun (str: string) -> str.Replace(text, String.Empty)

let insertAt startIndex text =
    fun (str: string) -> str.Insert(startIndex, text)

let insertBegin text =
    fun (str: string) -> str.Insert(0, text)

let insertEnd text =
    fun (str: string) -> str + text
    