module QuantumPuzzleGenerator.Tracking

open FSharpx.Collections

open Xamarin.Forms
open Xamarin.Essentials

open Microsoft.AppCenter
open Microsoft.AppCenter.Analytics
open Microsoft.AppCenter.Crashes

// safe initialization

let initialize (): unit =
    let areAnalyticsEnabled =
        Preferences.areAnalyticsEnabledKey
        |> Preferences.tryGetBool
        |> Option.defaultValue false

    if areAnalyticsEnabled then
        let keysStr =
            sprintf
                "ios=%s;android=%s"
                Secrets.secrets.["AppCenterIOSSecret"]
                Secrets.secrets.["AppCenterAndroidSecret"]

        try
            AppCenter.Start(keysStr, typeof<Analytics>, typeof<Crashes>)
        with _ -> ()

        try
            Analytics.SetEnabledAsync(true)
            |> Async.AwaitTask
            |> Async.StartImmediate
        with _ -> ()
    else
        ()

// turning off

let stop (): unit =
    try
        Analytics.SetEnabledAsync(false)
        |> Async.AwaitTask
        |> Async.StartImmediate
    with _ -> ()

// dimensions

let properties (): Map<string, string> =
    [ // Constants
      ("appVersion", Constants.version)

      // DeviceInfo
      ("deviceInfo.Manufacturer", DeviceInfo.Manufacturer)
      ("deviceModel", DeviceInfo.Model)
      ("operatingSystemVersionNumber", DeviceInfo.VersionString)

      // Device.info
      ("internalScreenWidthInPixels", string Device.info.PixelScreenSize.Width)
      ("internalScreenHeightInPixels", string Device.info.PixelScreenSize.Height)
      ("currentOrientation",
       match Device.info.CurrentOrientation with
       | Internals.DeviceOrientation.Landscape -> "landscape"
       | Internals.DeviceOrientation.Portrait -> "portrait"
       | Internals.DeviceOrientation.LandscapeLeft -> "landscapeLeft"
       | Internals.DeviceOrientation.LandscapeRight -> "landscapeRight"
       | Internals.DeviceOrientation.PortraitDown -> "portraitDown"
       | Internals.DeviceOrientation.PortraitUp -> "portraitUp"
       | _ -> "other") ]
    |> Map.ofList

// tracking

let track (event: string): unit =
    try
        Analytics.TrackEvent(event, properties ())
    with _ -> ()
    
// page events

let pageViewed (pageName: string): unit =
    pageName
    |> sprintf "viewed_page_%s"
    |> track

// credits events

let codeOnGitHubClicked (): unit =
    track "code_on_github_clicked"

let developerOnLinkedInClicked (): unit =
    track "developer_on_linkedin_clicked"

let appOnGooglePlayVisited (): unit =
    track "app_on_google_play_visited"
    
let appOnGooglePlayShared (): unit =
    track "app_on_google_play_shared"
    
let appOnAppleStoreVisited (): unit =
    track "app_on_apple_store_visited"
    
let appOnAppleStoreShared (): unit =
    track "app_on_apple_store_shared"
    
let privacyPolicyClicked (): unit =
    track "privacy_policy_clicked"
