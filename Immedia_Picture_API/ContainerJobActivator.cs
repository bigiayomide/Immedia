using System.ComponentModel.Composition.Hosting;

namespace Immedia.Picture.Api
{
    internal class ContainerJobActivator
    {
        private CompositionContainer container;

        public ContainerJobActivator(CompositionContainer container)
        {
            this.container = container;
        }
    }
}