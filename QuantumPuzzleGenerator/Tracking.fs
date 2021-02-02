module QuantumPuzzleGenerator.Tracking

open System.Globalization
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
    [ // VersionTracking
      ("appBuild", VersionTracking.CurrentBuild)
      ("appVersion", VersionTracking.CurrentVersion)

      // DeviceInfo
      ("deviceManufacturer", DeviceInfo.Manufacturer)
      ("deviceModel", DeviceInfo.Model)
      ("devicePlatform", DeviceInfo.Platform.ToString())
      ("deviceIdiom", DeviceInfo.Idiom.ToString())
      ("deviceType", DeviceInfo.DeviceType.ToString())
      ("deviceOSVersion", DeviceInfo.VersionString)

      // Device.info
      ("internalScreenWidth", string Device.info.PixelScreenSize.Width)
      ("internalScreenHeight", string Device.info.PixelScreenSize.Height)
      ("internalOrientation",
       match Device.info.CurrentOrientation with
       | Internals.DeviceOrientation.Landscape -> "Landscape"
       | Internals.DeviceOrientation.Portrait -> "Portrait"
       | Internals.DeviceOrientation.LandscapeLeft -> "LandscapeLeft"
       | Internals.DeviceOrientation.LandscapeRight -> "LandscapeRight"
       | Internals.DeviceOrientation.PortraitDown -> "PortraitDown"
       | Internals.DeviceOrientation.PortraitUp -> "PortraitUp"
       | _ -> "Other")

      ("region", RegionInfo.CurrentRegion.TwoLetterISORegionName)
      ("language", CultureInfo.CurrentCulture.TwoLetterISOLanguageName) ]
    |> Map.ofList

// tracking

let track (event: string): unit =
    try
        Analytics.TrackEvent(event, properties ())
    with _ -> ()

// page events

let pageViewed (pageName: string): unit =
    pageName |> sprintf "viewed_page_%s" |> track

// credits events

let codeOnGitHubClicked (): unit = track "code_on_github_clicked"

let developerOnLinkedInClicked (): unit = track "developer_on_linkedin_clicked"

let appOnGooglePlayVisited (): unit = track "app_on_google_play_visited"

let appOnGooglePlayShared (): unit = track "app_on_google_play_shared"

let appOnAppleStoreVisited (): unit = track "app_on_apple_store_visited"

let appOnAppleStoreShared (): unit = track "app_on_apple_store_shared"

let privacyPolicyClicked (): unit = track "privacy_policy_clicked"
