using JanitorApp.Services;

var statusService = new StatusService();
var menu = new MenuService();

await statusService.InitializeAsync();
await menu.MainMenu();




