﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LxGreg.Models
{
    public class Asset
    {
        public int Id { get; set; }
        [Display(Name = "物料编码")]
        public string ItemNumber { get; set; }
        [Display(Name = "物料短编码")]
        public string ItemShortNumber { get; set; }
        [Display(Name = "物料名称")]
        public string ItemName { get; set; }
        [Display(Name = "规格型号")]
        public string Model { get; set; }
        [Display(Name = "备注")]
        public string Mark { get; set; }
        public IEnumerable<Order> orders { get; set; }
        public IEnumerable<Stock> stocks { get; set; }

    }

    public class Unit
    {
        public int Id { get; set; }
        [Display(Name = "单位")]
        public string UnitName { get; set; }
        public IEnumerable<Order> orders { get; set; }
        public IEnumerable<Stock> stocks { get; set; }
    }

    public class Store
    {
        [Display(Name = "仓库编码")]
        public int Id { get; set; }
        [Display(Name = "仓库名称")]
        public string StoreName { get; set; }
        public IEnumerable<Order> orders { get; set; }
        public IEnumerable<Stock> stocks { get; set; }
    }
    public class Stock
    {
        public int id { get; set; }
        public Store store { get; set; }
        public int storeId { get; set; }
        public Unit unit { get; set; }
        public int unitId { get; set; }
        public Asset asset { get; set; }
        public int assetId { get; set; }
        [Display(Name = "当前库存")]
        public int CurrentQuntity { get; set; }
    }
    public class Order
    {
        public string Id { get; set; }
        [Display(Name = "下单时间")]
        public DateTime OrderTime { get; set; }
        public Store store { get; set; }
        public int storeId { get; set; }
        public Unit unit { get; set; }
        public int unitId { get; set; }
        public Asset asset { get; set; }
        public int assetId { get; set; }
        [Display(Name = "数量")]
        public int Quntity { get; set; }
        public Manager Taker { get; set; }
        public string TakerId { get; set; }
        public Manager Operater { get; set; }
        [Display(Name = "操作人ID")]
        public string OperaterId { get; set; }
        [Display(Name = "备注")]
        public string Mark { get; set; }
        public bool take { get; set; }

    }
}
