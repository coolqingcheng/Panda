﻿using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class PostTags : PandaBaseTable
{
    [Required, StringLength(20)] 
    public string TagName { get; set; }

    /// <summary>
    /// 关联总数
    /// </summary>
    public int Count { get; set; }

    public int PostCount { get; set; }
}