namespace ZaloraClone101.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item : MinimalItem
    {
        
        [StringLength(20)]
        public string sku { get; set; }

        public int? max_saving_percentage { get; set; }

        public float? special_price { get; set; }

        public float? max_price { get; set; }

        public string grouped_products { get; set; }

        [StringLength(50)]
        public string sizesystembrand { get; set; }

        public int? sizesystembrand_position { get; set; }

        public int? sub_cat_type_id { get; set; }

        public int? gender_id { get; set; }

        public int? attribute_set_id { get; set; }

        public string categories { get; set; }

        public float? max_special_price { get; set; }

        public string image { get; set; }

        public bool? is_new { get; set; }

    }
}
