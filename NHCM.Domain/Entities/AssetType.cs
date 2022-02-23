using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class AssetType
    {
        public AssetType()
        {
            InverseParent = new HashSet<AssetType>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public short? ParentId { get; set; }

        public virtual AssetType Parent { get; set; }

        public virtual ICollection<AssetType> InverseParent { get; set; }


        // Manually Added
        public virtual ICollection<PersonAsset> PersonAssets { get; set; }
    }
}
