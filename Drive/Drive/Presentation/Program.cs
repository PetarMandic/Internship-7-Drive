using Drive.Presenation.Factories;
using Drive.Presenation.Extensions;

var mainMenuActions = MainMenuFactory.CreateActions();
ActionExtensions.PrintActionsAndOpen(mainMenuActions, "");

