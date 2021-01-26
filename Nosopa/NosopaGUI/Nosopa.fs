// Copyright © Haceau. All rights reserved.

namespace NosopaGUI

module Nosopa =
    open Avalonia.Controls
    open Avalonia.FuncUI
    open Avalonia.FuncUI.Types
    open Avalonia.FuncUI.DSL
    open Avalonia.Layout
    open Nosopa
    open System.Net.Http
    
    let client = new HttpClient()

    type State = { search : string; result : seq<string> }
    let init = { search = ""; result = [] }

    type Msg = Search | SetSearch of string
    
    let update (msg: Msg) (state: State) : State =
        match msg with
        | Search -> { state with result = (QiDian.search state.search client) }
        | SetSearch s -> { state with search = s }
    

    let searchBar (state: State) (dispatch) : IView =
        StackPanel.create [
            StackPanel.dock Dock.Top
            StackPanel.orientation Orientation.Horizontal
            StackPanel.height 30.0
            StackPanel.children [
                TextBox.create [
                    TextBox.margin (10.0, 10.0, 10.0, 0.0)
                    TextBox.height 30.0
                    TextBox.width 330.0
                    TextBox.onTextChanged (fun s -> s |> SetSearch |> dispatch)
                ]
                Button.create [
                    Button.margin (0.0, 10.0, 10.0, 0.0)
                    Button.height 30.0
                    Button.width 40.0
                    Button.content "搜索"
                    Button.onClick (fun _ -> dispatch Search)
                ]
            ]
        ] |> Helpers.generalize

    let searchResult (state: State) (dispatch) : IView =
        StackPanel.create [
            StackPanel.dock Dock.Bottom
            StackPanel.children [
                TextBlock.create [
                    TextBlock.margin (10.0, 10.0, 0.0, 0.0)
                    TextBlock.text "搜索结果:"
                ]
                for r in state.result do
                    TextBlock.create [
                        TextBlock.margin (30.0, 5.0, 0.0, 0.0)
                        TextBlock.text r
                    ]
            ]
        ] |> Helpers.generalize

    let view (state: State) (dispatch) =
        DockPanel.create [
            DockPanel.children [
                searchBar state dispatch
                searchResult state dispatch
            ]
        ]       