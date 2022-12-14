using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SL2021.Domain.General;

namespace SL2021.Domain
{
    public class Content : EntityMutable<int>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public decimal Price { get; set; }
        //public decimal Price_2 { get; set; }
        //public decimal Price_3 { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
      
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}