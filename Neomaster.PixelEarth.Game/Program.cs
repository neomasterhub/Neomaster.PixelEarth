using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Presentation;

GameEngineBuilder
  .Create()
  .AddDefaultServices()
  .AddGameStage(new GameStage { Condition = () => true })
  .Build()
  .Run();
