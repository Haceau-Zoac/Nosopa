// Copyright © Haceau. All rights reserved.

namespace Nosopa

open System.Net.Http
open System.Text.RegularExpressions

module QiDian =
    let search keyword (client: HttpClient) =
        $"https://www.qidian.com/search?kw={keyword}"
        |> client.GetStringAsync
        |> fun t -> t.Result
        |> fun r -> if r.IndexOf("<h3>没有输入有效关键词</h3>") = -1 then r else ""
        |> fun s -> Regex.Matches(s, "<h4><a href=\"//book.qidian.com/info/\\d+\" target=\"_blank\"([\\s\\S]*?)</h4>")
        |> fun m -> [ for i = 0 to m.Count - 1 do m.[i] ]
        |> Seq.map (fun m -> m.Value)
        |> Seq.map (fun s -> Regex.Replace(s, "<[\\s\\S]*?>|</[\\s\\S]*?>", ""))