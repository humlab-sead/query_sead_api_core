﻿using Microsoft.EntityFrameworkCore;
using System;

namespace SeadQueryCore
{
    public interface IFacetContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbSet<GraphTableRelation> Edges { get; set; }
        DbSet<Facet> FacetDefinitions { get; set; }
        DbSet<FacetGroup> FacetGroups { get; set; }
        DbSet<FacetType> FacetTypes { get; set; }
        DbSet<GraphTable> Nodes { get; set; }
        DbSet<ResultAggregate> ResultDefinitions { get; set; }
        DbSet<ResultField> ResultFields { get; set; }
        DbSet<ResultFieldType> ResultFieldTypes { get; set; }
        StoreSetting Settings { get; set; }
        DbSet<ViewState> ViewStates { get; set; }
        DbSet<ResultViewType> ViewTypes { get; set; }

        int SaveChanges();
    }
}