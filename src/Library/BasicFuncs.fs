module BasicFuncs

open System.Linq
open System.Globalization

let upper =
    fun (str: string) -> str.ToUpper()

let lower =
    fun (str: string) -> str.ToLower()

let firstUpper =
    fun (str: string) -> str.First().ToString().ToUpper() + str.Substring(1).ToLower()

let titleCase =
    CultureInfo.CurrentCulture.TextInfo.ToTitleCase

let replace xs =
    let folder (acc:string) (from:string, too:string) = acc.Replace(from, too)
    fun (str: string) -> List.fold folder str xs
    