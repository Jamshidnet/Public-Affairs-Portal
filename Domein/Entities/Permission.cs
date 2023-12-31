﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("permissions")]
public partial class Permission
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("permission_name")]
    [StringLength(70)]
    public string PermissionName { get; set; }

    [InverseProperty("Owner")]
    public virtual ICollection<TranslatePermission> TranslatePermissions { get; set; } = new List<TranslatePermission>();
    [ForeignKey("PermissionId")]
    [InverseProperty("Permissions")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
