﻿using System.ComponentModel.DataAnnotations;

namespace CPW___219_eCommerceSite.Models
{
    /// <summary>
    /// Represents a single item for sale
    /// </summary>
    public class item
    {
        /// <summary>
        /// The unique identifier for the item
        /// </summary>
        [Key]
        public int ItemId { get; set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The sale price of the item
        /// </summary>
        [Range(0, 1000)]
        public double Price { get; set; }

        // Todo: add country of origin
    }

    /// <summary>
    /// A single Item that has been added to the users
    /// shopping cart cookie
    /// </summary>
    public class CartItemViewModel
    {
        public int ItemId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }
    }
}
