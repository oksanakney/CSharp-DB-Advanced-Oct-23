using Boardgames.Common;
using Boardgames.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Boardgame")]
    public class ImportBoardgameDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(ValidationConstants.BoardgameNameMinLength)]
        [MaxLength(ValidationConstants.BoardgameNameMaxLength)]
        public string Name { get; set; } = null!;

        [XmlElement("Rating")]
        [Range(ValidationConstants.BoardgameMinRating, ValidationConstants.BoardgameMaxRating)]
        public double Rating { get; set; }

        [XmlElement("YearPublished")]
        [Range(ValidationConstants.BoardGameMinYearPublished, ValidationConstants.BoardGameMaxYearPublished)]
        public int YearPublished { get; set; }

        [XmlElement("CategoryType")]
        public int CategoryType { get; set; }
        [XmlElement("Mechanics")]
        [Required]
        public string Mechanics { get; set; } = null!;
    }
}