namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.DataProcessor.ExportDto;
    using Boardgames.Utilities;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class Serializer
    {
        private static XmlHelper xmlHelper;
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            xmlHelper = new XmlHelper();
            ExportCreatorDto[] creators = context
                .Creators
                .Where(c => c.Boardgames.Any())
                .Select(c => new ExportCreatorDto()
                {
                    Name = c.FirstName + " " + c.LastName,
                    BoardgamesCount = c.Boardgames.Count,
                    Boardgames = c.Boardgames
                        .Select(b => new ExportBoardgameDto()
                        {                            
                            Name = b.Name,
                            YearPublished = b.YearPublished

                        })
                        .OrderBy(b => b.Name)
                        .ToArray()                    
                })
                .OrderByDescending(c => c.BoardgamesCount)
                .ThenBy(c => c.Name)
                .ToArray();

            return xmlHelper.Serialize(creators, "Creators");
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var sellers = context.Sellers
                
                .Where(s => s.BoardgamesSellers
                .Any(bs => bs.Boardgame.YearPublished >= year && 
                bs.Boardgame.Rating <= rating))                
                .Select(s => new
                {                   
                    s.Name,
                    s.Website,
                    Boardgames = s.BoardgamesSellers
                    .Where(bs => bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating)
                    .Select(bs => new
                    {
                        Name = bs.Boardgame.Name,
                        Rating = bs.Boardgame.Rating,
                        Mechanics = bs.Boardgame.Mechanics,
                        Category = bs.Boardgame.CategoryType.ToString()
                    })
                    .OrderByDescending(bs => bs.Rating)
                    .ThenBy(bs => bs.Name)
                    .ToArray()
                })
                .ToArray()
                .OrderByDescending(s => s.Boardgames.Length)
                .ThenBy(s => s.Name)
                .Take(5)
                .ToArray();
            return JsonConvert.SerializeObject(sellers, Formatting.Indented);
        }
    }
}