﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using VistaDB.EntityFrameworkCore.Utilities;

namespace VistaDB.EntityFrameworkCore.Internal
{
    public class VistaDBModelValidator : RelationalModelValidator
    {
        public VistaDBModelValidator(
            [NotNull] ModelValidatorDependencies dependencies,
            [NotNull] RelationalModelValidatorDependencies relationalDependencies)
            : base(dependencies, relationalDependencies)
        {
        }

        public override void Validate(IModel model)
        {
            base.Validate(model);

            ValidateNoSchemas(model);
            ValidateNoSequences(model);
        }

        protected virtual void ValidateNoSchemas([NotNull] IModel model)
        {
            foreach (var entityType in model.GetEntityTypes().Where(e => e.Relational().Schema != null))
            {
                Dependencies.Logger.SchemaConfiguredWarning(entityType, entityType.Relational().Schema);
            }
        }

        protected virtual void ValidateNoSequences([NotNull] IModel model)
        {
            foreach (var sequence in model.Relational().Sequences)
            {
                Dependencies.Logger.SequenceConfiguredWarning(sequence);
            }
        }
    }
}