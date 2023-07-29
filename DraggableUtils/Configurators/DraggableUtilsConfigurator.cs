using Bindito.Core;
using DraggableUtils.Factorys;
using DraggableUtils.Tools;
using DraggableUtils.Tools.Factories;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;
using TimberApi.ToolSystem;

namespace DraggableUtils.Configurators
{
    [Configurator(SceneEntrypoint.InGame)]
    public class DraggableUtilsConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<DraggableToolFactory>().AsSingleton();
            containerDefinition.Bind<PauseTool>().AsSingleton();
            containerDefinition.Bind<HaulPrioritizeTool>().AsSingleton();
            containerDefinition.Bind<EmptyStorageTool>().AsSingleton();
            
            containerDefinition.MultiBind<IToolFactory>().To<EmptyStorageToolFactory>().AsSingleton();
            containerDefinition.MultiBind<IToolFactory>().To<HaulPrioritizeToolFactory>().AsSingleton();
            containerDefinition.MultiBind<IToolFactory>().To<PauseToolFactory>().AsSingleton();
        }
    }
}