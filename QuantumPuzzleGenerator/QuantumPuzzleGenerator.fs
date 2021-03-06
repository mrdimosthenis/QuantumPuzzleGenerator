﻿// Copyright Fabulous contributors. See LICENSE.md for license.
namespace QuantumPuzzleGenerator

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

module App =

    let view (model: Model.Model) (dispatch: Model.Msg -> unit) =
        let page =
            match model.SelectedPage with
            | Model.Page.IntroPage -> Pages.Intro.stackLayout model dispatch
            | Model.Page.HomePage -> Pages.Home.stackLayout model dispatch
            | Model.Page.LessonCategoriesPage -> Pages.LessonCategories.stackLayout model dispatch
            | Model.Page.LearnPage -> Pages.Learn.stackLayout model dispatch
            | Model.Page.PlayPage -> Pages.Play.stackLayout model dispatch
            | Model.Page.CreditsPage -> Pages.Credits.stackLayout model dispatch

        View.ContentPage(content = View.ScrollView(backgroundColor = Constants.backgroundColor, content = page))

    let program =
        let initCmd () =
            async {
                do! Async.Sleep Constants.introWaitMillis
                return Model.SelectPage Model.HomePage
            }
            |> Cmd.ofAsyncMsg

        let initModelAndCmd () = Model.initModel (), initCmd ()

        XamarinFormsProgram.mkProgram initModelAndCmd Model.update view
#if DEBUG
        |> Program.withConsoleTrace
#endif

type App() as app =
    inherit Application()

    let runner =
        App.program |> XamarinFormsProgram.run app

    override this.OnStart() = Tracking.initialize ()

#if DEBUG
    // Uncomment this line to enable live update in debug mode.
// See https://fsprojects.github.io/Fabulous/Fabulous.XamarinForms/tools.html#live-update for further  instructions.
//
//do runner.EnableLiveUpdate()
#endif

    // Uncomment this code to save the application state to app.Properties using Newtonsoft.Json
// See https://fsprojects.github.io/Fabulous/Fabulous.XamarinForms/models.html#saving-application-state for further  instructions.
#if APPSAVE
    let modelId = "model"

    override __.OnSleep() =

        let json =
            Newtonsoft.Json.JsonConvert.SerializeObject(runner.CurrentModel)

        Console.WriteLine("OnSleep: saving model into app.Properties, json = {0}", json)
        app.Properties.[modelId] <- json

    override __.OnResume() =
        Console.WriteLine "OnResume: checking for model in app.Properties"

        try
            match app.Properties.TryGetValue modelId with
            | true, (:? string as json) ->

                Console.WriteLine("OnResume: restoring model from app.Properties, json = {0}", json)

                let model =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<App.Model>(json)

                Console.WriteLine("OnResume: restoring model from app.Properties, model = {0}", (sprintf "%0A" model))
                runner.SetCurrentModel(model, Cmd.none)

            | _ -> ()
        with ex -> App.program.onError ("Error while restoring model found in app.Properties", ex)

    override this.OnStart() =
        Console.WriteLine "OnStart: using same logic as OnResume()"
        this.OnResume()
#endif

    member __.Program = runner
