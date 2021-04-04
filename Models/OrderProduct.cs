using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class OrderProduct
    {
        public long Id { get; set; }
        
        [ForeignKey(nameof(ProductTemplate))]
        public long ProductTemplateId { get; set; }

        public virtual OrderProductTemplate ProductTemplate { get; set; }

        [ForeignKey(nameof(Order))]
        public long OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}