﻿#region licence
// =====================================================
// EfSchemeCompare Project - project to compare EF schema to SQL schema
// Filename: Test32EfTableInfoComplexTypes.cs
// Date Created: 2016/04/06
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// =====================================================
#endregion

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CompareCore.EFInfo;
using Ef6SchemaCompare.InternalEf6;
using Ef6TestDbContext;
using EfPocoClasses.ComplexTypes;
using EfPocoClasses.Relationships;
using NUnit.Framework;
using Tests.Helpers;

namespace Tests.UnitTests
{
    public class Test32EfTableInfoComplexTypes
    {
        private ICollection<EfTableInfo> _efInfos;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            using (var db = new TestEf6SchemaCompareDb())
            {
                var decoder = new Ef6MetadataDecoder(Assembly.GetAssembly(typeof(DataTop)));
                _efInfos = decoder.GetAllEfTablesWithColInfo(db);
            }
        }

        [Test]
        public void Test70DataComplexColsOk()
        {
            //SETUP

            //EXECUTE
            var efInfo = _efInfos.SingleOrDefault(x => x.ClrClassType == typeof(DataComplex));

            //VERIFY
            efInfo.ShouldNotEqualNull();
            efInfo.TableName.ShouldEqual("DataComplex");
            efInfo.NormalCols.Count.ShouldEqual(7);
            //foreach (var col in efInfo.NormalCols)
            //{
            //    Console.WriteLine("list[i++].ToString().ShouldEqual(\"{0}\");", col);
            //}
            var list = efInfo.NormalCols.ToList();
            var i = 0;
            list[i++].ToString().ShouldEqual("SqlColumnName: DataComplexId, SqlTypeName: int, ClrColumName: DataComplexId, ClrColumnType: System.Int32, IsPrimaryKey: True, PrimaryKeyOrder: 1, IsNullable: False, MaxLength: 4");
            list[i++].ToString().ShouldEqual("SqlColumnName: ComplexData_ComplexInt, SqlTypeName: int, ClrColumName: ComplexInt, ClrColumnType: System.Int32, IsPrimaryKey: False, PrimaryKeyOrder: 0, IsNullable: False, MaxLength: 4");
            list[i++].ToString().ShouldEqual("SqlColumnName: ComplexData_ComplexString, SqlTypeName: nvarchar, ClrColumName: ComplexString, ClrColumnType: System.String, IsPrimaryKey: False, PrimaryKeyOrder: 0, IsNullable: True, MaxLength: 50");
            list[i++].ToString().ShouldEqual("SqlColumnName: ComplexComplexData_ComplexDateTime, SqlTypeName: datetime, ClrColumName: ComplexDateTime, ClrColumnType: System.DateTime, IsPrimaryKey: False, PrimaryKeyOrder: 0, IsNullable: False, MaxLength: 8");
            list[i++].ToString().ShouldEqual("SqlColumnName: ComplexComplexData_ComplexGuid, SqlTypeName: uniqueidentifier, ClrColumName: ComplexGuid, ClrColumnType: System.Guid, IsPrimaryKey: False, PrimaryKeyOrder: 0, IsNullable: False, MaxLength: 16");
            list[i++].ToString().ShouldEqual("SqlColumnName: ComplexComplexData_ComplexData_ComplexInt, SqlTypeName: int, ClrColumName: ComplexInt, ClrColumnType: System.Int32, IsPrimaryKey: False, PrimaryKeyOrder: 0, IsNullable: False, MaxLength: 4");
            list[i++].ToString().ShouldEqual("SqlColumnName: ComplexComplexData_ComplexData_ComplexString, SqlTypeName: nvarchar, ClrColumName: ComplexString, ClrColumnType: System.String, IsPrimaryKey: False, PrimaryKeyOrder: 0, IsNullable: True, MaxLength: 50");
        }

        [Test]
        public void Test71DataComplexRelationshipsOk()
        {
            //SETUP

            //EXECUTE
            var efInfo = _efInfos.SingleOrDefault(x => x.ClrClassType == typeof(DataComplex));

            //VERIFY
            efInfo.ShouldNotEqualNull();
            efInfo.RelationshipCols.Count().ShouldEqual(0);
        }
    }
}