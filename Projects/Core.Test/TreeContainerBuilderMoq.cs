using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using FluentAssertions;
using LOGI.Framework.Toolkit.Core.Common.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Toolkit.Core.Test
{
    public class UserSearchItemTestMethodory : TreeContainerBuilder
    {
        public UserSearchItemTestMethodory(string key)
            : base(key)
        {

        }
    }

    #region Obj

    [Serializable]
    public class BaseFilterItem
    {
        public string ItemKey { get; set; }
        public string ItemValue { get; set; }
    }

    [Serializable]
    public class BaseSightSeeingFilterData
    {
        public SightseeingOrderByEnum OrderType { get; set; }

        public List<FilterItem> Categories { get; set; }
        public List<FilterItem> Types { get; set; }
        public List<FilterItem> Languages { get; set; }
    }

    [Serializable]
    public class FilterItem : BaseFilterItem
    {

        public bool IsSelected { get; set; }
    }

    public enum SightseeingOrderByEnum
    {
        Name,
        Price,
        Recommended
    }

    [Serializable]
    public class SightSeeingFilterData : BaseSightSeeingFilterData
    {

        public SightSeeingFilterData()
        {
            Categories = new List<FilterItem>();
            Types = new List<FilterItem>();
            Languages = new List<FilterItem>();
            OrderType = SightseeingOrderByEnum.Name;
            PageItemCount = 10;
        }

        public int PageItemCount { get; set; }



        public string CurrencySymbol { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public string FilteredName { get; set; }
        public decimal FilteredMinPrice { get; set; }
        public decimal FilteredMaxPrice { get; set; }

        public bool OrderByIsDesc { get; set; }

    }
    #endregion

    [TestClass]
    public class TreeContainerBuilderMoq
    {
        #region GetItemTest
        [TestMethod]
        public void TreeContainerBuilder_GetItemTest()
        {
            #region Initialize Obj
            #region SightSeeing
            var maxPrice = 100;
            var minPrice = 1;

            var oldFilters = new SightSeeingFilterData
            {
                FilteredName = "1",
                OrderByIsDesc = false,
                OrderType = SightseeingOrderByEnum.Price,
                PageItemCount = 10,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                FilteredMinPrice = minPrice,
                FilteredMaxPrice = maxPrice,
                Languages = new List<FilterItem>() { new FilterItem() { ItemKey = "1", ItemValue = "1" }, new FilterItem() { IsSelected = true, ItemKey = "2", ItemValue = "2" } },
                CurrencySymbol = ""
            };

            var newFilters = new SightSeeingFilterData
            {
                FilteredName = "3",
                OrderByIsDesc = false,
                OrderType = SightseeingOrderByEnum.Price,
                PageItemCount = 10,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                FilteredMinPrice = minPrice,
                FilteredMaxPrice = maxPrice,
                CurrencySymbol = ""
            };
            #endregion

            #endregion

            var userSearchItemTestMethodory = new UserSearchItemTestMethodory("Test");

            var count = 1;
            for (int i = 1; i <= count; i++)
            {
                userSearchItemTestMethodory.CurrentContainer.Add("Item"+i).Insert(new TreeItemValue()
                {
                    Change = true,
                    Key = string.Format("Item{0}Key",i),
                    Value = oldFilters
                });
            }

            var oldFindItem = userSearchItemTestMethodory.CurrentContainer.GetItem<SightSeeingFilterData>("Item1Key");

            oldFindItem.Should().NotBeNull("Key: 'Item1Key' Get ile değer döndürmeli");
            oldFindItem.FilteredName.Should().NotBeNull("Key: 'Item1Key' değer taşımalı ").And.NotBeEmpty("Key: 'Item1Key' değer taşımalı ").And.Be("1");


            oldFindItem.FilteredName = "2";

            var findItem = userSearchItemTestMethodory.CurrentContainer.GetItem<SightSeeingFilterData>("Item1Key");

            findItem.Should().NotBeNull("Key: 'Item1Key' Get ile değer döndürmeli");
            findItem.FilteredName.Should().NotBeNull("Key: 'Item1Key' değer taşımalı ").And.NotBeEmpty("Key: 'Item1Key' değer taşımalı ").And.Be("1");

            var newFilterChangeKey = "Item999Key";
            userSearchItemTestMethodory.CurrentContainer.Add("Item999").Insert(new TreeItemValue()
            {
                Change = true,
                Key = newFilterChangeKey,
                Value = newFilters
            });

            findItem = userSearchItemTestMethodory.CurrentContainer.GetItem<SightSeeingFilterData>(newFilterChangeKey);

            findItem.Should().NotBeNull("Key: 'Item1Key' Get ile değer döndürmeli");
            findItem.FilteredName.Should().NotBeNull("Key: 'Item1Key' değer taşımalı ").And.NotBeEmpty("Key: 'Item1Key' değer taşımalı ").And.Be("3");

            try
            {
                var serializedData =
                new LOGI.Framework.Toolkit.Core.Serialization.CoreXmlSerializer().Serialize(
                    userSearchItemTestMethodory.CurrentContainer);

                var deSerializedData =
                new LOGI.Framework.Toolkit.Core.Serialization.CoreXmlSerializer().DeSerialize<TreeContainer>(serializedData);
            }
            catch (Exception)
            {
                
            }

            try
            {
                var serializedData =
                new LOGI.Framework.Toolkit.Core.Serialization.JsonSerializer().Serialize(
                    userSearchItemTestMethodory.CurrentContainer);
            }
            catch (Exception)
            {

            }

        }

        [TestMethod]
        public void TreeContainerBuilder_GetItemTestSerialize()
        {
            #region Initialize Obj
            
            var fItem = new BaseFilterItem();
            fItem.ItemKey = "Key";
            fItem.ItemValue = "Val";
            
            #endregion

            var userSearchItemTestMethodory = new UserSearchItemTestMethodory("Test");

            var count = 1;
            for (int i = 1; i <= count; i++)
            {
                userSearchItemTestMethodory.CurrentContainer.Add("Item" + i).Insert(new TreeItemValue()
                {
                    Change = true,
                    Key = string.Format("Item{0}Key", i),
                    Value = fItem
                });
            }

            try
            {
                var serializedData =
                new LOGI.Framework.Toolkit.Core.Serialization.CoreXmlSerializer().Serialize(
                    userSearchItemTestMethodory.CurrentContainer);

                var deSerializedData =
                new LOGI.Framework.Toolkit.Core.Serialization.CoreXmlSerializer().DeSerialize<TreeContainer>(serializedData);
            }
            catch (Exception)
            {

            }

            try
            {
                var serializedData =
                new LOGI.Framework.Toolkit.Core.Serialization.JsonSerializer().Serialize(
                    userSearchItemTestMethodory.CurrentContainer);
            }
            catch (Exception)
            {

            }

        }
        #endregion

    }
}
