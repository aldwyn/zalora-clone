namespace ZaloraClone101.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cart")]
    public partial class Cart
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int item_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public string user_id { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime cart_date { get; set; }

        [ForeignKey("item_id")]
        public Item item { get; set; }
    }
}
