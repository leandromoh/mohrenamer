module AdvancedFuncs

open System
open System.IO

let removeByIndex startIndex count =
    fun (str: string) -> str.Remove(startIndex, count)

let removeOccurrence text =
    fun (str: string) -> str.Replace(text, String.Empty)

let insertAt startIndex text =
    fun (str: string) -> str.Insert(startIndex, text)

let insertBegin text =
    fun (str: string) -> str.Insert(0, text)

let insertEnd text =
    let f str = 
        let fileName = Path.GetFileNameWithoutExtension(str)
        let ext = Path.GetExtension(str)
        fileName + text + ext 
    f
    