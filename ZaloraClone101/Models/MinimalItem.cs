using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZaloraClone101.Models
{
    public class MinimalItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_catalog_config { get; set; }

        public string name { get; set; }

        [StringLength(100)]
        public string brand { get; set; }

        [StringLength(20)]
        public string activated_at { get; set; }

        public string link { get; set; }

        public float? price { get; set; }

        [StringLength(255)]
        public string available_sizes { get; set; }

        public DateTime ActivatedAt()
        {
            return DateTime.Parse(activated_at);
        }
    }
}