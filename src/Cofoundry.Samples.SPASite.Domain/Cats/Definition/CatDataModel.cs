using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Domain;
using System.ComponentModel.DataAnnotations;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class CatDataModel : ICustomEntityVersionDataModel
    {
        [Display(Description = "A short description or tag-line to describe the cat")]
        public string Description { get; set; }

        [Display(Name = "Breed", Description = "Identity the breed of cat if possible")]
        [CustomEntity(BreedCustomEntityDefinition.DefinitionCode)]
        public int? BreedId { get; set; }

        [Display(Name = "Features", Description = "Extra features or proiperties that help categorize this cat")]
        [CustomEntityCollection(FeatureCustomEntityDefinition.DefinitionCode)]
        public int[] FeatureIds { get; set; }

        [Display(Name = "Images", Description = "The top image will be the main image that displays in the grid")]
        [ImageCollection]
        public int[] ImageAssetIds { get; set; }
    }
}
