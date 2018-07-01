using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.TestUtilities;
using VistaDB.EFCore.Storage.Internal;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.Storage
{
    public class VistaDBTypeMappingTest : RelationalTypeMappingTest
    {
        [Theory]
        [InlineData(nameof(ChangeTracker.DetectChanges), false)]
        [InlineData(nameof(PropertyEntry.CurrentValue), false)]
        [InlineData(nameof(PropertyEntry.OriginalValue), false)]
        [InlineData(nameof(ChangeTracker.DetectChanges), true)]
        [InlineData(nameof(PropertyEntry.CurrentValue), true)]
        [InlineData(nameof(PropertyEntry.OriginalValue), true)]
        public void Row_version_is_marked_as_modified_only_if_it_really_changed(string mode, bool changeValue)
        {
            using (var context = new OptimisticContext())
            {
                var token = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                var newToken = changeValue ? new byte[] { 1, 2, 3, 4, 0, 6, 7, 8 } : token;

                var entity = context.Attach(
                    new WithRowVersion
                    {
                        Id = 789,
                        Version = token.ToArray()
                    }).Entity;

                var propertyEntry = context.Entry(entity).Property(e => e.Version);

                Assert.Equal(token, propertyEntry.CurrentValue);
                Assert.Equal(token, propertyEntry.OriginalValue);
                Assert.False(propertyEntry.IsModified);
                Assert.Equal(EntityState.Unchanged, context.Entry(entity).State);

                switch (mode)
                {
                    case nameof(ChangeTracker.DetectChanges):
                        entity.Version = newToken.ToArray();
                        context.ChangeTracker.DetectChanges();
                        break;
                    case nameof(PropertyEntry.CurrentValue):
                        propertyEntry.CurrentValue = newToken.ToArray();
                        break;
                    case nameof(PropertyEntry.OriginalValue):
                        propertyEntry.OriginalValue = newToken.ToArray();
                        break;
                    default:
                        throw new NotImplementedException("Unexpected test mode.");
                }

                Assert.Equal(changeValue, propertyEntry.IsModified);
                Assert.Equal(changeValue ? EntityState.Modified : EntityState.Unchanged, context.Entry(entity).State);
            }
        }

        private class WithRowVersion
        {
            public int Id { get; set; }
            public byte[] Version { get; set; }
        }

        private class OptimisticContext : DbContext
        {
            public DbSet<WithRowVersion> _ { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseVistaDB("Data Source=Branston");

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<WithRowVersion>().Property(e => e.Version).IsRowVersion();
            }
        }

        protected override DbCommand CreateTestCommand()
            => new SqlCommand();

        protected override DbType DefaultParameterType
            => DbType.Int32;

        [InlineData(typeof(VistaDBDateTimeOffsetTypeMapping), typeof(DateTimeOffset))]
        [InlineData(typeof(VistaDBDateTimeTypeMapping), typeof(DateTime))]
        [InlineData(typeof(VistaDBDoubleTypeMapping), typeof(double))]
        [InlineData(typeof(VistaDBFloatTypeMapping), typeof(float))]
        [InlineData(typeof(VistaDBTimeSpanTypeMapping), typeof(TimeSpan))]
        public override void Create_and_clone_with_converter(Type mappingType, Type clrType)
        {
            base.Create_and_clone_with_converter(mappingType, clrType);
        }

        [InlineData(typeof(VistaDBByteArrayTypeMapping), typeof(byte[]))]
        public override void Create_and_clone_sized_mappings_with_converter(Type mappingType, Type clrType)
        {
            base.Create_and_clone_sized_mappings_with_converter(mappingType, clrType);
        }

        [InlineData(typeof(VistaDBStringTypeMapping), typeof(string))]
        public override void Create_and_clone_unicode_sized_mappings_with_converter(Type mappingType, Type clrType)
        {
            base.Create_and_clone_unicode_sized_mappings_with_converter(mappingType, clrType);
        }

        public static RelationalTypeMapping GetMapping(Type type)
            => (RelationalTypeMapping)new VistaDBTypeMappingSource(
                    TestServiceFactory.Instance.Create<TypeMappingSourceDependencies>(),
                    TestServiceFactory.Instance.Create<RelationalTypeMappingSourceDependencies>())
                .FindMapping(type);

        public override void GenerateSqlLiteral_returns_ByteArray_literal()
        {
            var value = new byte[] { 0xDA, 0x7A };
            var literal = GetMapping(typeof(byte[])).GenerateSqlLiteral(value);
            Assert.Equal("0xDA7A", literal);
        }

        public override void GenerateSqlLiteral_returns_DateTime_literal()
        {
            var value = new DateTime(2015, 3, 12, 13, 36, 37, 371);
            var literal = GetMapping(typeof(DateTime)).GenerateSqlLiteral(value);

            Assert.Equal("'2015-03-12T13:36:37.3710000'", literal);
        }

        public override void GenerateSqlLiteral_returns_DateTimeOffset_literal()
        {
            var value = new DateTimeOffset(2015, 3, 12, 13, 36, 37, 371, new TimeSpan(-7, 0, 0));
            var literal = GetMapping(typeof(DateTimeOffset)).GenerateSqlLiteral(value);

            Assert.Equal("'2015-03-12T13:36:37.371-07:00'", literal);
        }

        public static RelationalTypeMapping GetMapping(string type)
            => new VistaDBTypeMappingSource(
                TestServiceFactory.Instance.Create<TypeMappingSourceDependencies>(),
                TestServiceFactory.Instance.Create<RelationalTypeMappingSourceDependencies>())
                .FindMapping(type);

        [Fact]
        public virtual void GenerateSqlLiteralValue_returns_Unicode_String_literal()
        {
            var mapping = GetMapping("nvarchar(max)");

            var literal = mapping.GenerateSqlLiteral("A Unicode String");

            Assert.Equal("N'A Unicode String'", literal);
        }

        [Fact]
        public virtual void GenerateSqlLiteralValue_returns_NonUnicode_String_literal()
        {
            var mapping = GetMapping("varchar(max)");

            var literal = mapping.GenerateSqlLiteral("A Non-Unicode String");
            Assert.Equal("'A Non-Unicode String'", literal);
        }

        private class FakeType : Type
        {
            public FakeType(string fullName)
            {
                FullName = fullName;
            }

            public override object[] GetCustomAttributes(bool inherit) => throw new NotImplementedException();
            public override bool IsDefined(Type attributeType, bool inherit) => throw new NotImplementedException();
            public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr) => throw new NotImplementedException();
            public override Type GetInterface(string name, bool ignoreCase) => throw new NotImplementedException();
            public override Type[] GetInterfaces() => throw new NotImplementedException();
            public override EventInfo GetEvent(string name, BindingFlags bindingAttr) => throw new NotImplementedException();
            public override EventInfo[] GetEvents(BindingFlags bindingAttr) => throw new NotImplementedException();
            public override Type[] GetNestedTypes(BindingFlags bindingAttr) => throw new NotImplementedException();
            public override Type GetNestedType(string name, BindingFlags bindingAttr) => throw new NotImplementedException();
            public override Type GetElementType() => throw new NotImplementedException();
            protected override bool HasElementTypeImpl() => throw new NotImplementedException();
            protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers) => throw new NotImplementedException();
            public override PropertyInfo[] GetProperties(BindingFlags bindingAttr) => throw new NotImplementedException();
            protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers) => throw new NotImplementedException();
            public override MethodInfo[] GetMethods(BindingFlags bindingAttr) => throw new NotImplementedException();
            public override FieldInfo GetField(string name, BindingFlags bindingAttr) => throw new NotImplementedException();
            public override FieldInfo[] GetFields(BindingFlags bindingAttr) => throw new NotImplementedException();
            public override MemberInfo[] GetMembers(BindingFlags bindingAttr) => throw new NotImplementedException();
            protected override TypeAttributes GetAttributeFlagsImpl() => throw new NotImplementedException();
            protected override bool IsArrayImpl() => throw new NotImplementedException();
            protected override bool IsByRefImpl() => throw new NotImplementedException();
            protected override bool IsPointerImpl() => throw new NotImplementedException();
            protected override bool IsPrimitiveImpl() => throw new NotImplementedException();
            protected override bool IsCOMObjectImpl() => throw new NotImplementedException();
            public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters) => throw new NotImplementedException();
            public override Type UnderlyingSystemType { get; }
            protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers) => throw new NotImplementedException();
            public override string Name => throw new NotImplementedException();
            public override Guid GUID => throw new NotImplementedException();
            public override Module Module => throw new NotImplementedException();
            public override Assembly Assembly => throw new NotImplementedException();
            public override string Namespace => throw new NotImplementedException();
            public override string AssemblyQualifiedName => throw new NotImplementedException();
            public override Type BaseType => throw new NotImplementedException();
            public override object[] GetCustomAttributes(Type attributeType, bool inherit) => throw new NotImplementedException();

            public override string FullName { get; }

            public override int GetHashCode() => FullName.GetHashCode();

            public override bool Equals(object o) => ReferenceEquals(this, o);
        }

        protected override DbContextOptions ContextOptions { get; }
            = new DbContextOptionsBuilder().UseVistaDB("Server=Dummy").Options;
    }
}
