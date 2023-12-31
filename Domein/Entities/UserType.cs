﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("user_type")]
public partial class UserType
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("user_type")]
    [StringLength(50)]
    public string TypeName { get; set; }

    [InverseProperty("Owner")]
    public virtual ICollection<TranslateUserType> TranslateUserTypes { get; set; } = new List<TranslateUserType>();

    [InverseProperty("UserType")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
