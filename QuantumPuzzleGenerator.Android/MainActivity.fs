// Copyright 2018 Fabulous contributors. See LICENSE.md for license.
namespace QuantumPuzzleGenerator.Android

open System

open Android.App
open Android.Content.PM
open Android.Runtime
open Android.Views
open Android.OS
open Xamarin.Forms.Platform.Android
open QuantumPuzzleGenerator

[<Activity(Label = "Quantum Puzzle Generator",
           Icon = "@drawable/icon",
           Theme = "@style/MainTheme",
           MainLauncher = true,
           ConfigurationChanges =
               (ConfigChanges.ScreenSize
                ||| ConfigChanges.Orientation))>]
type MainActivity() =
    inherit FormsAppCompatActivity()

    let mutable _app: App option = None

    override this.OnCreate(bundle: Bundle) =
        FormsAppCompatActivity.TabLayoutResource <- Resources.Layout.Tabbar
        FormsAppCompatActivity.ToolbarResource <- Resources.Layout.Toolbar

        base.OnCreate(bundle)
        Xamarin.Essentials.Platform.Init(this, bundle)
        Xamarin.Forms.Forms.Init(this, bundle)

        let appCore = App()
        this.LoadApplication(appCore)

        _app <- Some appCore

    override this.OnRequestPermissionsResult(requestCode: int,
                                             permissions: string [],
                                             [<GeneratedEnum>] grantResults: Android.Content.PM.Permission []) =
        Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults)
        base.OnRequestPermissionsResult(requestCode, permissions, grantResults)

    override this.OnKeyDown(keycode: Keycode, _: KeyEvent) =
        match _app with
        | Some appCore ->
            match keycode with
            | Keycode.Back -> appCore.Program.Dispatch Model.Msg.BackClick
            | _ -> ()

            true
        | None -> true
