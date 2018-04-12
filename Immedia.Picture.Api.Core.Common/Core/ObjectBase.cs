using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Immedia.Picture.Api.Core.Common.Core
{
    public abstract class ObjectBase :  IExtensibleDataObject
    {
        public static CompositionContainer Container { get; set; }

        #region IExtensibleDataObject Members

        public ExtensionDataObject ExtensionData { get; set; }

        #endregion

    }
}
