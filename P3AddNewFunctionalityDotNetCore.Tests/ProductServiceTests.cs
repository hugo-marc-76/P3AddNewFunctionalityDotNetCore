﻿using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        private ProductViewModel product;

        public ProductServiceTests()
        {
            product = new ProductViewModel();
        }

        [Fact]
        public void TestMissingPrice()
        {
            product.Name = "Product1";
            product.Stock = "2";
            product.Price = null;
            Assert.False(ValidateModel(product));
            Assert.Equal("MissingPrice", GetFirstErrorMessage(product));
        }

        [Fact]
        public void TestMissingName()
        {
            product.Name = string.Empty;
            product.Price = "25.0";
            product.Stock = "10";
            Assert.False(ValidateModel(product));
            Assert.Equal("MissingName", GetFirstErrorMessage(product));
        }

        [Fact]
        public void TestPriceNotANumber()
        {
            product.Name = "Product2";
            product.Price = "K";
            product.Stock = "10";
            Assert.False(ValidateModel(product));
            Assert.Equal("PriceNotANumber", GetFirstErrorMessage(product));
        }

        [Fact]
        public void TestPriceNotGreaterThanZero()
        {
            product.Name = "Product3";
            product.Price = "-5.0";
            product.Stock = "10";
            Assert.False(ValidateModel(product));
            Assert.Equal("PriceNotGreaterThanZero", GetFirstErrorMessage(product));
        }

        [Fact]
        public void TestQuantityNotGreaterThanZero()
        {
            product.Name = "Product4";
            product.Price = " 10.0";
            product.Stock = "0";
            Assert.False(ValidateModel(product));
            Assert.Equal("StockNotGreaterThanZero", GetFirstErrorMessage(product));
        }

        [Fact]
        public void TestStockNotANumber()
        {
            product.Name = "Product5";
            product.Price = "10.0";
            product.Stock = "abc";
            Assert.False(ValidateModel(product));
            Assert.Equal("StockNotAnInteger", GetFirstErrorMessage(product));
        }

        [Fact]
        public void TestPriceWithDecimals()
        {
            product.Name = "Product6";
            product.Price = "5.8";
            product.Stock = "10";
            Assert.True(ValidateModel(product));
        }

        [Fact]
        public void TestNegativePrice()
        {
            product.Name = "Product7";
            product.Price = "-5";
            product.Stock = "10";
            Assert.False(ValidateModel(product));
            Assert.Equal("PriceNotGreaterThanZero", GetFirstErrorMessage(product));
        }

        [Fact]
        public void TestNegativeStock()
        {
            product.Name = "Product8";
            product.Price = "5";
            product.Stock = "-1";
            Assert.False(ValidateModel(product));
            Assert.Equal("StockNotGreaterThanZero", GetFirstErrorMessage(product));
        }

        [Fact]
        public void TestStockWithDecimals()
        {
            product.Name = "Product9";
            product.Price = "5";
            product.Stock = "5.5";
            Assert.False(ValidateModel(product));
            Assert.Equal("StockNotAnInteger", GetFirstErrorMessage(product));
        }

        [Fact]
        public void TestMissingStock()
        {
            product.Name = "Product10";
            product.Price = "5";
            product.Stock = string.Empty;
            Assert.False(ValidateModel(product));
            Assert.Equal("MissingStock", GetFirstErrorMessage(product));
        }

        // Ajoutez ici d'autres méthodes pour tester les autres cas...

        private bool ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            return Validator.TryValidateObject(model, ctx, validationResults, true);
        }

        private string GetFirstErrorMessage(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults[0].ErrorMessage;
        }
    }
}