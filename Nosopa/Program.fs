// Copyright © Haceau. All rights reserved.

open Nosopa
open System
open System.Net.Http

[<EntryPoint>]
let main args =
    
    use client = new HttpClient()
    if args.Length < 1 then
        printfn "Using: Nosopa <Name>"

        1
    else
        QiDian.search args.[0] client
        |> Seq.iter Console.WriteLine

        0