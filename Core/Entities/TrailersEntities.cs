using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TrailersEntities
    {
        public TrailersEntities(string trailerName,
                                string description,
                                string author,
                                string creator,
                                string image,
                                string link,
                                DateTimeOffset dateCreated)
        {
            TrailerName = trailerName;
            Description = description;
            Author = author;
            Creator = creator;
            Image = image;
            Link = link;
            DateCreated = dateCreated;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TrailerName { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Creator { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public DateTimeOffset DateCreated { get; set; } 
    }
}
