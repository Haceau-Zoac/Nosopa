open System
open System.Net.Http
open System.Text.RegularExpressions

[<EntryPoint>]
let main args =
    
    use client = new HttpClient()
    if args.Length < 1 then
        printfn "Using: Nosopa <Name>"
        1
    else
        $"https://www.qidian.com/search?kw={args.[0]}"
        |> client.GetStringAsync
        |> fun t -> t.Result
        |> fun s -> Regex.Matches(s, "<h4><a href=\"//book.qidian.com/info/\\d+\" target=\"_blank\"([\\s\\S]*?)</h4>")
        |> Seq.map (fun m -> m.Value)
        |> Seq.map (fun s -> Regex.Replace(s, "<[\\s\\S]*?>|</[\\s\\S]*?>", ""))
        |> Seq.iter Console.WriteLine
        0