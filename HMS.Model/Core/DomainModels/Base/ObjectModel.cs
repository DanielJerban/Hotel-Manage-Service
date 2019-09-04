using System;

namespace HMS.Model.Core.DomainModels.Base
{
    public class ObjectModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public ObjectModel()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now; 
        }
    }
}
