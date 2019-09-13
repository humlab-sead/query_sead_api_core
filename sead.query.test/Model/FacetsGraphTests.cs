using Moq;
using SeadQueryCore;
using System;
using System.Collections.Generic;
using Xunit;
using Autofac;
using System.Linq;
using SeadQueryTest.Infrastructure;

namespace SeadQueryTest2.Model
{
    public class FacetsGraphTests : IDisposable
    {
        // private MockRepository mockRepository;
        // private Mock<Dictionary> mockDictionary;
        private Mock<List<GraphEdge>> mockListGraphEdge;
        private Mock<List<Facet>> mockListFacet;

        public FacetsGraphTests()
        {
            // this.mockRepository = new MockRepository(MockBehavior.Strict);

            // this.mockDictionary = this.mockRepository.Create<Dictionary>();
            this.mockListGraphEdge = this.mockRepository.Create<List<GraphEdge>>();
            this.mockListFacet = this.mockRepository.Create<List<Facet>>();
        }

        public void Dispose()
        {
            //this.mockRepository.VerifyAll();
        }

        private FacetsGraph CreateFacetsGraph()
        {
            return new FacetsGraph(
                this.mockDictionary.Object,
                this.mockListGraphEdge.Object,
                this.mockListFacet.Object);
        }

        [Fact]
        public void GetEdge_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var facetsGraph = this.CreateFacetsGraph();
            string source = null;
            string target = null;

            // Act
            var result = facetsGraph.GetEdge(
                source,
                target);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void GetEdge_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var facetsGraph = this.CreateFacetsGraph();
            int sourceId = 0;
            int targetId = 0;

            // Act
            var result = facetsGraph.GetEdge(
                sourceId,
                targetId);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void IsAlias_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var facetsGraph = this.CreateFacetsGraph();
            string name = null;

            // Act
            var result = facetsGraph.IsAlias(
                name);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ResolveTargetName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var facetsGraph = this.CreateFacetsGraph();
            string aliasOrTable = null;

            // Act
            var result = facetsGraph.ResolveTargetName(
                aliasOrTable);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ResolveAliasName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var facetsGraph = this.CreateFacetsGraph();
            string aliasOrTable = null;

            // Act
            var result = facetsGraph.ResolveAliasName(
                aliasOrTable);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void Find_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var facetsGraph = this.CreateFacetsGraph();
            string start_table = null;
            List<string> destination_tables = null;

            // Act
            var result = facetsGraph.Find(start_table,destination_tables);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void Find_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var facetsGraph = this.CreateFacetsGraph();
            string startTable = null;
            string destinationTable = null;

            // Act
            var result = facetsGraph.Find(
                startTable,
                destinationTable);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void Find_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var facetsGraph = this.CreateFacetsGraph();
            int startTableId = 0;
            int destinationTableId = 0;

            // Act
            var result = facetsGraph.Find(
                startTableId,
                destinationTableId);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ToCSV_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var facetsGraph = this.CreateFacetsGraph();

            // Act
            var result = facetsGraph.ToCSV();

            // Assert
            Assert.True(false);
        }


        private dynamic expectedEdges = new dynamic[] {
            new {  SourceName = "countries", TargetName = "tbl_location_types", Weight = 20},
            new {  SourceName = "countries", TargetName = "tbl_rdb", Weight = 150},
            new {  SourceName = "countries", TargetName = "tbl_rdb_systems", Weight = 150},
            new {  SourceName = "countries", TargetName = "tbl_relative_ages", Weight = 70},
            new {  SourceName = "countries", TargetName = "tbl_site_locations", Weight = 5},
            new {  SourceName = "countries", TargetName = "tbl_taxa_seasonality", Weight = 60},
            new {  SourceName = "countries", TargetName = "view_places_relations", Weight = 20},
            new {  SourceName = "metainformation.tbl_denormalized_measured_values", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "metainformation.view_abundance", TargetName = "tbl_analysis_entities", Weight = 2},
            new {  SourceName = "metainformation.view_abundance", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "metainformation.view_abundances_by_taxon_analysis_entity", TargetName = "tbl_analysis_entities", Weight = 20},
            new {  SourceName = "metainformation.view_abundances_by_taxon_analysis_entity", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "metainformation.view_sample_group_references", TargetName = "tbl_biblio", Weight = 80},
            new {  SourceName = "metainformation.view_sample_group_references", TargetName = "tbl_sample_groups", Weight = 15},
            new {  SourceName = "metainformation.view_site_references", TargetName = "tbl_biblio", Weight = 80},
            new {  SourceName = "metainformation.view_site_references", TargetName = "tbl_sites", Weight = 15},
            new {  SourceName = "metainformation.view_taxa_biblio", TargetName = "tbl_biblio", Weight = 10},
            new {  SourceName = "metainformation.view_taxa_biblio", TargetName = "tbl_taxa_tree_master", Weight = 2},
            new {  SourceName = "tbl_abundance_elements", TargetName = "tbl_abundances", Weight = 20},
            new {  SourceName = "tbl_abundance_elements", TargetName = "tbl_dating_material", Weight = 20},
            new {  SourceName = "tbl_abundance_elements", TargetName = "tbl_record_types", Weight = 20},
            new {  SourceName = "tbl_abundance_ident_levels", TargetName = "tbl_abundances", Weight = 20},
            new {  SourceName = "tbl_abundance_ident_levels", TargetName = "tbl_identification_levels", Weight = 20},
            new {  SourceName = "tbl_abundance_modifications", TargetName = "tbl_abundances", Weight = 20},
            new {  SourceName = "tbl_abundance_modifications", TargetName = "tbl_modification_types", Weight = 20},
            new {  SourceName = "tbl_abundances", TargetName = "tbl_abundance_elements", Weight = 20},
            new {  SourceName = "tbl_abundances", TargetName = "tbl_abundance_ident_levels", Weight = 20},
            new {  SourceName = "tbl_abundances", TargetName = "tbl_abundance_modifications", Weight = 20},
            new {  SourceName = "tbl_abundances", TargetName = "tbl_analysis_entities", Weight = 1},
            new {  SourceName = "tbl_abundances", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_activity_types", TargetName = "tbl_taxa_seasonality", Weight = 20},
            new {  SourceName = "tbl_aggregate_datasets", TargetName = "tbl_aggregate_order_types", Weight = 20},
            new {  SourceName = "tbl_aggregate_datasets", TargetName = "tbl_aggregate_sample_ages", Weight = 20},
            new {  SourceName = "tbl_aggregate_datasets", TargetName = "tbl_aggregate_samples", Weight = 20},
            new {  SourceName = "tbl_aggregate_datasets", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_aggregate_order_types", TargetName = "tbl_aggregate_datasets", Weight = 20},
            new {  SourceName = "tbl_aggregate_sample_ages", TargetName = "tbl_aggregate_datasets", Weight = 20},
            new {  SourceName = "tbl_aggregate_sample_ages", TargetName = "tbl_analysis_entity_ages", Weight = 20},
            new {  SourceName = "tbl_aggregate_samples", TargetName = "tbl_aggregate_datasets", Weight = 20},
            new {  SourceName = "tbl_aggregate_samples", TargetName = "tbl_analysis_entities", Weight = 20},
            new {  SourceName = "tbl_alt_ref_types", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_alt_ref_types", TargetName = "tbl_sample_alt_refs", Weight = 20},
            new {  SourceName = "tbl_analysis_entities", TargetName = "metainformation.view_abundance", Weight = 2},
            new {  SourceName = "tbl_analysis_entities", TargetName = "metainformation.view_abundances_by_taxon_analysis_entity", Weight = 20},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_abundances", Weight = 1},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_aggregate_samples", Weight = 20},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_analysis_entity_ages", Weight = 20},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_analysis_entity_dimensions", Weight = 20},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_analysis_entity_prep_methods", Weight = 20},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_ceramics", Weight = 20},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_datasets", Weight = 1},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_dendro", Weight = 20},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_dendro_dates", Weight = 20},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_geochronology", Weight = 20},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_measured_values", Weight = 20},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_physical_samples", Weight = 1},
            new {  SourceName = "tbl_analysis_entities", TargetName = "tbl_tephra_dates", Weight = 20},
            new {  SourceName = "tbl_analysis_entity_ages", TargetName = "tbl_aggregate_sample_ages", Weight = 20},
            new {  SourceName = "tbl_analysis_entity_ages", TargetName = "tbl_analysis_entities", Weight = 20},
            new {  SourceName = "tbl_analysis_entity_ages", TargetName = "tbl_chronologies", Weight = 20},
            new {  SourceName = "tbl_analysis_entity_dimensions", TargetName = "tbl_analysis_entities", Weight = 20},
            new {  SourceName = "tbl_analysis_entity_dimensions", TargetName = "tbl_dimensions", Weight = 20},
            new {  SourceName = "tbl_analysis_entity_prep_methods", TargetName = "tbl_analysis_entities", Weight = 20},
            new {  SourceName = "tbl_analysis_entity_prep_methods", TargetName = "tbl_methods", Weight = 20},
            new {  SourceName = "tbl_biblio", TargetName = "metainformation.view_sample_group_references", Weight = 80},
            new {  SourceName = "tbl_biblio", TargetName = "metainformation.view_site_references", Weight = 80},
            new {  SourceName = "tbl_biblio", TargetName = "metainformation.view_taxa_biblio", Weight = 10},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_aggregate_datasets", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_biblio_keywords", Weight = 20},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_dataset_masters", Weight = 200},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_datasets", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_ecocode_systems", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_geochron_refs", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_methods", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_rdb_systems", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_relative_age_refs", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_sample_group_references", Weight = 90},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_site_other_records", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_site_references", Weight = 90},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_species_associations", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_taxa_synonyms", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_taxonomic_order_biblio", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_taxonomy_notes", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_tephra_refs", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_text_biology", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_text_distribution", Weight = 150},
            new {  SourceName = "tbl_biblio", TargetName = "tbl_text_identification_keys", Weight = 150},
            new {  SourceName = "tbl_biblio_keywords", TargetName = "tbl_biblio", Weight = 20},
            new {  SourceName = "tbl_biblio_keywords", TargetName = "tbl_keywords", Weight = 20},
            new {  SourceName = "tbl_ceramics", TargetName = "tbl_analysis_entities", Weight = 20},
            new {  SourceName = "tbl_ceramics", TargetName = "tbl_ceramics_measurements", Weight = 20},
            new {  SourceName = "tbl_ceramics_measurement_lookup", TargetName = "tbl_ceramics_measurements", Weight = 20},
            new {  SourceName = "tbl_ceramics_measurements", TargetName = "tbl_ceramics", Weight = 20},
            new {  SourceName = "tbl_ceramics_measurements", TargetName = "tbl_ceramics_measurement_lookup", Weight = 20},
            new {  SourceName = "tbl_ceramics_measurements", TargetName = "tbl_methods", Weight = 20},
            new {  SourceName = "tbl_chron_control_types", TargetName = "tbl_chron_controls", Weight = 20},
            new {  SourceName = "tbl_chron_controls", TargetName = "tbl_chron_control_types", Weight = 20},
            new {  SourceName = "tbl_chron_controls", TargetName = "tbl_chronologies", Weight = 20},
            new {  SourceName = "tbl_chronologies", TargetName = "tbl_analysis_entity_ages", Weight = 20},
            new {  SourceName = "tbl_chronologies", TargetName = "tbl_chron_controls", Weight = 20},
            new {  SourceName = "tbl_chronologies", TargetName = "tbl_contacts", Weight = 20},
            new {  SourceName = "tbl_chronologies", TargetName = "tbl_sample_groups", Weight = 20},
            new {  SourceName = "tbl_collections_or_journals", TargetName = "tbl_publishers", Weight = 20},
            new {  SourceName = "tbl_colours", TargetName = "tbl_methods", Weight = 20},
            new {  SourceName = "tbl_colours", TargetName = "tbl_sample_colours", Weight = 20},
            new {  SourceName = "tbl_contact_types", TargetName = "tbl_dataset_contacts", Weight = 20},
            new {  SourceName = "tbl_contacts", TargetName = "tbl_chronologies", Weight = 20},
            new {  SourceName = "tbl_contacts", TargetName = "tbl_dataset_contacts", Weight = 20},
            new {  SourceName = "tbl_contacts", TargetName = "tbl_dataset_masters", Weight = 20},
            new {  SourceName = "tbl_contacts", TargetName = "tbl_dataset_submissions", Weight = 20},
            new {  SourceName = "tbl_contacts", TargetName = "tbl_dating_labs", Weight = 20},
            new {  SourceName = "tbl_contacts", TargetName = "tbl_site_images", Weight = 20},
            new {  SourceName = "tbl_contacts", TargetName = "tbl_taxa_reference_specimens", Weight = 20},
            new {  SourceName = "tbl_coordinate_method_dimensions", TargetName = "tbl_dimensions", Weight = 20},
            new {  SourceName = "tbl_coordinate_method_dimensions", TargetName = "tbl_methods", Weight = 20},
            new {  SourceName = "tbl_coordinate_method_dimensions", TargetName = "tbl_sample_coordinates", Weight = 20},
            new {  SourceName = "tbl_coordinate_method_dimensions", TargetName = "tbl_sample_group_coordinates", Weight = 20},
            new {  SourceName = "tbl_data_type_groups", TargetName = "tbl_data_types", Weight = 20},
            new {  SourceName = "tbl_data_types", TargetName = "tbl_data_type_groups", Weight = 20},
            new {  SourceName = "tbl_data_types", TargetName = "tbl_datasets", Weight = 20},
            new {  SourceName = "tbl_dataset_contacts", TargetName = "tbl_contact_types", Weight = 20},
            new {  SourceName = "tbl_dataset_contacts", TargetName = "tbl_contacts", Weight = 20},
            new {  SourceName = "tbl_dataset_contacts", TargetName = "tbl_datasets", Weight = 20},
            new {  SourceName = "tbl_dataset_masters", TargetName = "tbl_biblio", Weight = 200},
            new {  SourceName = "tbl_dataset_masters", TargetName = "tbl_contacts", Weight = 20},
            new {  SourceName = "tbl_dataset_masters", TargetName = "tbl_datasets", Weight = 20},
            new {  SourceName = "tbl_dataset_submission_types", TargetName = "tbl_dataset_submissions", Weight = 20},
            new {  SourceName = "tbl_dataset_submissions", TargetName = "tbl_contacts", Weight = 20},
            new {  SourceName = "tbl_dataset_submissions", TargetName = "tbl_dataset_submission_types", Weight = 20},
            new {  SourceName = "tbl_dataset_submissions", TargetName = "tbl_datasets", Weight = 20},
            new {  SourceName = "tbl_datasets", TargetName = "tbl_analysis_entities", Weight = 1},
            new {  SourceName = "tbl_datasets", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_datasets", TargetName = "tbl_data_types", Weight = 20},
            new {  SourceName = "tbl_datasets", TargetName = "tbl_dataset_contacts", Weight = 20},
            new {  SourceName = "tbl_datasets", TargetName = "tbl_dataset_masters", Weight = 20},
            new {  SourceName = "tbl_datasets", TargetName = "tbl_dataset_submissions", Weight = 20},
            new {  SourceName = "tbl_datasets", TargetName = "tbl_datasets", Weight = 20},
            new {  SourceName = "tbl_datasets", TargetName = "tbl_methods", Weight = 1},
            new {  SourceName = "tbl_datasets", TargetName = "tbl_projects", Weight = 20},
            new {  SourceName = "tbl_dating_labs", TargetName = "tbl_contacts", Weight = 20},
            new {  SourceName = "tbl_dating_labs", TargetName = "tbl_geochronology", Weight = 20},
            new {  SourceName = "tbl_dating_material", TargetName = "tbl_abundance_elements", Weight = 20},
            new {  SourceName = "tbl_dating_material", TargetName = "tbl_geochronology", Weight = 20},
            new {  SourceName = "tbl_dating_material", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_dating_uncertainty", TargetName = "tbl_dendro_dates", Weight = 20},
            new {  SourceName = "tbl_dating_uncertainty", TargetName = "tbl_geochronology", Weight = 20},
            new {  SourceName = "tbl_dating_uncertainty", TargetName = "tbl_relative_dates", Weight = 20},
            new {  SourceName = "tbl_dating_uncertainty", TargetName = "tbl_tephra_dates", Weight = 20},
            new {  SourceName = "tbl_dendro", TargetName = "tbl_analysis_entities", Weight = 20},
            new {  SourceName = "tbl_dendro", TargetName = "tbl_dendro_measurements", Weight = 20},
            new {  SourceName = "tbl_dendro_date_notes", TargetName = "tbl_dendro_dates", Weight = 20},
            new {  SourceName = "tbl_dendro_dates", TargetName = "tbl_analysis_entities", Weight = 20},
            new {  SourceName = "tbl_dendro_dates", TargetName = "tbl_dating_uncertainty", Weight = 20},
            new {  SourceName = "tbl_dendro_dates", TargetName = "tbl_dendro_date_notes", Weight = 20},
            new {  SourceName = "tbl_dendro_dates", TargetName = "tbl_years_types", Weight = 20},
            new {  SourceName = "tbl_dendro_measurement_lookup", TargetName = "tbl_dendro_measurements", Weight = 20},
            new {  SourceName = "tbl_dendro_measurements", TargetName = "tbl_dendro", Weight = 20},
            new {  SourceName = "tbl_dendro_measurements", TargetName = "tbl_dendro_measurement_lookup", Weight = 20},
            new {  SourceName = "tbl_dendro_measurements", TargetName = "tbl_methods", Weight = 20},
            new {  SourceName = "tbl_dimensions", TargetName = "tbl_analysis_entity_dimensions", Weight = 20},
            new {  SourceName = "tbl_dimensions", TargetName = "tbl_coordinate_method_dimensions", Weight = 20},
            new {  SourceName = "tbl_dimensions", TargetName = "tbl_measured_value_dimensions", Weight = 20},
            new {  SourceName = "tbl_dimensions", TargetName = "tbl_method_groups", Weight = 20},
            new {  SourceName = "tbl_dimensions", TargetName = "tbl_sample_dimensions", Weight = 20},
            new {  SourceName = "tbl_dimensions", TargetName = "tbl_sample_group_dimensions", Weight = 20},
            new {  SourceName = "tbl_dimensions", TargetName = "tbl_units", Weight = 20},
            new {  SourceName = "tbl_ecocode_definitions", TargetName = "tbl_ecocode_groups", Weight = 20},
            new {  SourceName = "tbl_ecocode_definitions", TargetName = "tbl_ecocodes", Weight = 20},
            new {  SourceName = "tbl_ecocode_groups", TargetName = "tbl_ecocode_definitions", Weight = 20},
            new {  SourceName = "tbl_ecocode_groups", TargetName = "tbl_ecocode_systems", Weight = 20},
            new {  SourceName = "tbl_ecocode_systems", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_ecocode_systems", TargetName = "tbl_ecocode_groups", Weight = 20},
            new {  SourceName = "tbl_ecocodes", TargetName = "tbl_ecocode_definitions", Weight = 20},
            new {  SourceName = "tbl_ecocodes", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_feature_types", TargetName = "tbl_features", Weight = 20},
            new {  SourceName = "tbl_features", TargetName = "tbl_feature_types", Weight = 20},
            new {  SourceName = "tbl_features", TargetName = "tbl_physical_sample_features", Weight = 20},
            new {  SourceName = "tbl_geochron_refs", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_geochron_refs", TargetName = "tbl_geochronology", Weight = 20},
            new {  SourceName = "tbl_geochronology", TargetName = "tbl_analysis_entities", Weight = 20},
            new {  SourceName = "tbl_geochronology", TargetName = "tbl_dating_labs", Weight = 20},
            new {  SourceName = "tbl_geochronology", TargetName = "tbl_dating_material", Weight = 20},
            new {  SourceName = "tbl_geochronology", TargetName = "tbl_dating_uncertainty", Weight = 20},
            new {  SourceName = "tbl_geochronology", TargetName = "tbl_geochron_refs", Weight = 20},
            new {  SourceName = "tbl_horizons", TargetName = "tbl_methods", Weight = 70},
            new {  SourceName = "tbl_horizons", TargetName = "tbl_sample_horizons", Weight = 20},
            new {  SourceName = "tbl_identification_levels", TargetName = "tbl_abundance_ident_levels", Weight = 20},
            new {  SourceName = "tbl_image_types", TargetName = "tbl_sample_group_images", Weight = 20},
            new {  SourceName = "tbl_image_types", TargetName = "tbl_sample_images", Weight = 20},
            new {  SourceName = "tbl_image_types", TargetName = "tbl_site_images", Weight = 20},
            new {  SourceName = "tbl_image_types", TargetName = "tbl_taxa_images", Weight = 20},
            new {  SourceName = "tbl_imported_taxa_replacements", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_keywords", TargetName = "tbl_biblio_keywords", Weight = 20},
            new {  SourceName = "tbl_languages", TargetName = "tbl_taxa_common_names", Weight = 20},
            new {  SourceName = "tbl_lithology", TargetName = "tbl_sample_groups", Weight = 20},
            new {  SourceName = "tbl_location_types", TargetName = "countries", Weight = 20},
            new {  SourceName = "tbl_location_types", TargetName = "tbl_locations", Weight = 20},
            new {  SourceName = "tbl_locations", TargetName = "tbl_location_types", Weight = 20},
            new {  SourceName = "tbl_locations", TargetName = "tbl_rdb", Weight = 150},
            new {  SourceName = "tbl_locations", TargetName = "tbl_rdb_systems", Weight = 150},
            new {  SourceName = "tbl_locations", TargetName = "tbl_relative_ages", Weight = 70},
            new {  SourceName = "tbl_locations", TargetName = "tbl_site_locations", Weight = 5},
            new {  SourceName = "tbl_locations", TargetName = "tbl_taxa_seasonality", Weight = 60},
            new {  SourceName = "tbl_locations", TargetName = "view_places_relations", Weight = 1},
            new {  SourceName = "tbl_mcr_names", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_mcr_summary_data", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_mcrdata_birmbeetledat", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_measured_value_dimensions", TargetName = "tbl_dimensions", Weight = 20},
            new {  SourceName = "tbl_measured_value_dimensions", TargetName = "tbl_measured_values", Weight = 20},
            new {  SourceName = "tbl_measured_values", TargetName = "tbl_analysis_entities", Weight = 20},
            new {  SourceName = "tbl_measured_values", TargetName = "tbl_measured_value_dimensions", Weight = 20},
            new {  SourceName = "tbl_method_groups", TargetName = "tbl_dimensions", Weight = 20},
            new {  SourceName = "tbl_method_groups", TargetName = "tbl_methods", Weight = 20},
            new {  SourceName = "tbl_methods", TargetName = "tbl_analysis_entity_prep_methods", Weight = 20},
            new {  SourceName = "tbl_methods", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_methods", TargetName = "tbl_ceramics_measurements", Weight = 20},
            new {  SourceName = "tbl_methods", TargetName = "tbl_colours", Weight = 20},
            new {  SourceName = "tbl_methods", TargetName = "tbl_coordinate_method_dimensions", Weight = 20},
            new {  SourceName = "tbl_methods", TargetName = "tbl_datasets", Weight = 1},
            new {  SourceName = "tbl_methods", TargetName = "tbl_dendro_measurements", Weight = 20},
            new {  SourceName = "tbl_methods", TargetName = "tbl_horizons", Weight = 70},
            new {  SourceName = "tbl_methods", TargetName = "tbl_method_groups", Weight = 20},
            new {  SourceName = "tbl_methods", TargetName = "tbl_record_types", Weight = 20},
            new {  SourceName = "tbl_methods", TargetName = "tbl_relative_dates", Weight = 70},
            new {  SourceName = "tbl_methods", TargetName = "tbl_sample_dimensions", Weight = 150},
            new {  SourceName = "tbl_methods", TargetName = "tbl_sample_groups", Weight = 150},
            new {  SourceName = "tbl_methods", TargetName = "tbl_site_natgridrefs", Weight = 20},
            new {  SourceName = "tbl_methods", TargetName = "tbl_units", Weight = 150},
            new {  SourceName = "tbl_modification_types", TargetName = "tbl_abundance_modifications", Weight = 20},
            new {  SourceName = "tbl_physical_sample_features", TargetName = "tbl_features", Weight = 20},
            new {  SourceName = "tbl_physical_sample_features", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "metainformation.tbl_denormalized_measured_values", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_alt_ref_types", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_analysis_entities", Weight = 1},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_physical_sample_features", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_relative_dates", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_sample_alt_refs", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_sample_colours", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_sample_coordinates", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_sample_descriptions", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_sample_dimensions", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_sample_groups", Weight = 1},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_sample_horizons", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_sample_images", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_sample_locations", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_sample_notes", Weight = 20},
            new {  SourceName = "tbl_physical_samples", TargetName = "tbl_sample_types", Weight = 20},
            new {  SourceName = "tbl_project_stages", TargetName = "tbl_projects", Weight = 20},
            new {  SourceName = "tbl_project_types", TargetName = "tbl_projects", Weight = 20},
            new {  SourceName = "tbl_projects", TargetName = "tbl_datasets", Weight = 20},
            new {  SourceName = "tbl_projects", TargetName = "tbl_project_stages", Weight = 20},
            new {  SourceName = "tbl_projects", TargetName = "tbl_project_types", Weight = 20},
            new {  SourceName = "tbl_publishers", TargetName = "tbl_collections_or_journals", Weight = 20},
            new {  SourceName = "tbl_rdb", TargetName = "countries", Weight = 150},
            new {  SourceName = "tbl_rdb", TargetName = "tbl_locations", Weight = 150},
            new {  SourceName = "tbl_rdb", TargetName = "tbl_rdb_codes", Weight = 20},
            new {  SourceName = "tbl_rdb", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_rdb_codes", TargetName = "tbl_rdb", Weight = 20},
            new {  SourceName = "tbl_rdb_codes", TargetName = "tbl_rdb_systems", Weight = 20},
            new {  SourceName = "tbl_rdb_systems", TargetName = "countries", Weight = 150},
            new {  SourceName = "tbl_rdb_systems", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_rdb_systems", TargetName = "tbl_locations", Weight = 150},
            new {  SourceName = "tbl_rdb_systems", TargetName = "tbl_rdb_codes", Weight = 20},
            new {  SourceName = "tbl_record_types", TargetName = "tbl_abundance_elements", Weight = 20},
            new {  SourceName = "tbl_record_types", TargetName = "tbl_methods", Weight = 20},
            new {  SourceName = "tbl_record_types", TargetName = "tbl_site_other_records", Weight = 150},
            new {  SourceName = "tbl_record_types", TargetName = "tbl_taxa_tree_orders", Weight = 1},
            new {  SourceName = "tbl_relative_age_refs", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_relative_age_refs", TargetName = "tbl_relative_ages", Weight = 20},
            new {  SourceName = "tbl_relative_age_types", TargetName = "tbl_relative_ages", Weight = 20},
            new {  SourceName = "tbl_relative_ages", TargetName = "countries", Weight = 70},
            new {  SourceName = "tbl_relative_ages", TargetName = "tbl_locations", Weight = 70},
            new {  SourceName = "tbl_relative_ages", TargetName = "tbl_relative_age_refs", Weight = 20},
            new {  SourceName = "tbl_relative_ages", TargetName = "tbl_relative_age_types", Weight = 20},
            new {  SourceName = "tbl_relative_ages", TargetName = "tbl_relative_dates", Weight = 20},
            new {  SourceName = "tbl_relative_dates", TargetName = "tbl_dating_uncertainty", Weight = 20},
            new {  SourceName = "tbl_relative_dates", TargetName = "tbl_methods", Weight = 70},
            new {  SourceName = "tbl_relative_dates", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_relative_dates", TargetName = "tbl_relative_ages", Weight = 20},
            new {  SourceName = "tbl_sample_alt_refs", TargetName = "tbl_alt_ref_types", Weight = 20},
            new {  SourceName = "tbl_sample_alt_refs", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_sample_colours", TargetName = "tbl_colours", Weight = 20},
            new {  SourceName = "tbl_sample_colours", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_sample_coordinates", TargetName = "tbl_coordinate_method_dimensions", Weight = 20},
            new {  SourceName = "tbl_sample_coordinates", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_sample_description_sample_group_contexts", TargetName = "tbl_sample_description_types", Weight = 20},
            new {  SourceName = "tbl_sample_description_sample_group_contexts", TargetName = "tbl_sample_group_sampling_contexts", Weight = 20},
            new {  SourceName = "tbl_sample_description_types", TargetName = "tbl_sample_description_sample_group_contexts", Weight = 20},
            new {  SourceName = "tbl_sample_description_types", TargetName = "tbl_sample_descriptions", Weight = 20},
            new {  SourceName = "tbl_sample_descriptions", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_sample_descriptions", TargetName = "tbl_sample_description_types", Weight = 20},
            new {  SourceName = "tbl_sample_dimensions", TargetName = "tbl_dimensions", Weight = 20},
            new {  SourceName = "tbl_sample_dimensions", TargetName = "tbl_methods", Weight = 150},
            new {  SourceName = "tbl_sample_dimensions", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_sample_group_coordinates", TargetName = "tbl_coordinate_method_dimensions", Weight = 20},
            new {  SourceName = "tbl_sample_group_coordinates", TargetName = "tbl_sample_groups", Weight = 20},
            new {  SourceName = "tbl_sample_group_description_type_sampling_contexts", TargetName = "tbl_sample_group_description_types", Weight = 20},
            new {  SourceName = "tbl_sample_group_description_type_sampling_contexts", TargetName = "tbl_sample_group_sampling_contexts", Weight = 20},
            new {  SourceName = "tbl_sample_group_description_types", TargetName = "tbl_sample_group_description_type_sampling_contexts", Weight = 20},
            new {  SourceName = "tbl_sample_group_description_types", TargetName = "tbl_sample_group_descriptions", Weight = 20},
            new {  SourceName = "tbl_sample_group_descriptions", TargetName = "tbl_sample_group_description_types", Weight = 20},
            new {  SourceName = "tbl_sample_group_descriptions", TargetName = "tbl_sample_groups", Weight = 20},
            new {  SourceName = "tbl_sample_group_dimensions", TargetName = "tbl_dimensions", Weight = 20},
            new {  SourceName = "tbl_sample_group_dimensions", TargetName = "tbl_sample_groups", Weight = 20},
            new {  SourceName = "tbl_sample_group_images", TargetName = "tbl_image_types", Weight = 20},
            new {  SourceName = "tbl_sample_group_images", TargetName = "tbl_sample_groups", Weight = 20},
            new {  SourceName = "tbl_sample_group_notes", TargetName = "tbl_sample_groups", Weight = 20},
            new {  SourceName = "tbl_sample_group_references", TargetName = "tbl_biblio", Weight = 90},
            new {  SourceName = "tbl_sample_group_references", TargetName = "tbl_sample_groups", Weight = 20},
            new {  SourceName = "tbl_sample_group_sampling_contexts", TargetName = "tbl_sample_description_sample_group_contexts", Weight = 20},
            new {  SourceName = "tbl_sample_group_sampling_contexts", TargetName = "tbl_sample_group_description_type_sampling_contexts", Weight = 20},
            new {  SourceName = "tbl_sample_group_sampling_contexts", TargetName = "tbl_sample_groups", Weight = 20},
            new {  SourceName = "tbl_sample_group_sampling_contexts", TargetName = "tbl_sample_location_type_sampling_contexts", Weight = 20},
            new {  SourceName = "tbl_sample_groups", TargetName = "metainformation.view_sample_group_references", Weight = 15},
            new {  SourceName = "tbl_sample_groups", TargetName = "tbl_chronologies", Weight = 20},
            new {  SourceName = "tbl_sample_groups", TargetName = "tbl_lithology", Weight = 20},
            new {  SourceName = "tbl_sample_groups", TargetName = "tbl_methods", Weight = 150},
            new {  SourceName = "tbl_sample_groups", TargetName = "tbl_physical_samples", Weight = 1},
            new {  SourceName = "tbl_sample_groups", TargetName = "tbl_sample_group_coordinates", Weight = 20},
            new {  SourceName = "tbl_sample_groups", TargetName = "tbl_sample_group_descriptions", Weight = 20},
            new {  SourceName = "tbl_sample_groups", TargetName = "tbl_sample_group_dimensions", Weight = 20},
            new {  SourceName = "tbl_sample_groups", TargetName = "tbl_sample_group_images", Weight = 20},
            new {  SourceName = "tbl_sample_groups", TargetName = "tbl_sample_group_notes", Weight = 20},
            new {  SourceName = "tbl_sample_groups", TargetName = "tbl_sample_group_references", Weight = 20},
            new {  SourceName = "tbl_sample_groups", TargetName = "tbl_sample_group_sampling_contexts", Weight = 20},
            new {  SourceName = "tbl_sample_groups", TargetName = "tbl_sites", Weight = 1},
            new {  SourceName = "tbl_sample_horizons", TargetName = "tbl_horizons", Weight = 20},
            new {  SourceName = "tbl_sample_horizons", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_sample_images", TargetName = "tbl_image_types", Weight = 20},
            new {  SourceName = "tbl_sample_images", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_sample_location_type_sampling_contexts", TargetName = "tbl_sample_group_sampling_contexts", Weight = 20},
            new {  SourceName = "tbl_sample_location_type_sampling_contexts", TargetName = "tbl_sample_location_types", Weight = 20},
            new {  SourceName = "tbl_sample_location_types", TargetName = "tbl_sample_location_type_sampling_contexts", Weight = 20},
            new {  SourceName = "tbl_sample_location_types", TargetName = "tbl_sample_locations", Weight = 20},
            new {  SourceName = "tbl_sample_locations", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_sample_locations", TargetName = "tbl_sample_location_types", Weight = 20},
            new {  SourceName = "tbl_sample_notes", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_sample_types", TargetName = "tbl_physical_samples", Weight = 20},
            new {  SourceName = "tbl_season_types", TargetName = "tbl_seasons", Weight = 20},
            new {  SourceName = "tbl_seasons", TargetName = "tbl_season_types", Weight = 20},
            new {  SourceName = "tbl_seasons", TargetName = "tbl_taxa_seasonality", Weight = 20},
            new {  SourceName = "tbl_site_images", TargetName = "tbl_contacts", Weight = 20},
            new {  SourceName = "tbl_site_images", TargetName = "tbl_image_types", Weight = 20},
            new {  SourceName = "tbl_site_images", TargetName = "tbl_sites", Weight = 20},
            new {  SourceName = "tbl_site_locations", TargetName = "countries", Weight = 5},
            new {  SourceName = "tbl_site_locations", TargetName = "tbl_locations", Weight = 5},
            new {  SourceName = "tbl_site_locations", TargetName = "tbl_sites", Weight = 20},
            new {  SourceName = "tbl_site_natgridrefs", TargetName = "tbl_methods", Weight = 20},
            new {  SourceName = "tbl_site_natgridrefs", TargetName = "tbl_sites", Weight = 20},
            new {  SourceName = "tbl_site_other_records", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_site_other_records", TargetName = "tbl_record_types", Weight = 150},
            new {  SourceName = "tbl_site_other_records", TargetName = "tbl_sites", Weight = 150},
            new {  SourceName = "tbl_site_preservation_status", TargetName = "tbl_sites", Weight = 20},
            new {  SourceName = "tbl_site_references", TargetName = "tbl_biblio", Weight = 90},
            new {  SourceName = "tbl_site_references", TargetName = "tbl_sites", Weight = 20},
            new {  SourceName = "tbl_sites", TargetName = "metainformation.view_site_references", Weight = 15},
            new {  SourceName = "tbl_sites", TargetName = "tbl_sample_groups", Weight = 1},
            new {  SourceName = "tbl_sites", TargetName = "tbl_site_images", Weight = 20},
            new {  SourceName = "tbl_sites", TargetName = "tbl_site_locations", Weight = 20},
            new {  SourceName = "tbl_sites", TargetName = "tbl_site_natgridrefs", Weight = 20},
            new {  SourceName = "tbl_sites", TargetName = "tbl_site_other_records", Weight = 150},
            new {  SourceName = "tbl_sites", TargetName = "tbl_site_preservation_status", Weight = 20},
            new {  SourceName = "tbl_sites", TargetName = "tbl_site_references", Weight = 20},
            new {  SourceName = "tbl_species_association_types", TargetName = "tbl_species_associations", Weight = 20},
            new {  SourceName = "tbl_species_associations", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_species_associations", TargetName = "tbl_species_association_types", Weight = 20},
            new {  SourceName = "tbl_species_associations", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_taxa_common_names", TargetName = "tbl_languages", Weight = 20},
            new {  SourceName = "tbl_taxa_common_names", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_taxa_images", TargetName = "tbl_image_types", Weight = 20},
            new {  SourceName = "tbl_taxa_images", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_taxa_measured_attributes", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_taxa_reference_specimens", TargetName = "tbl_contacts", Weight = 20},
            new {  SourceName = "tbl_taxa_reference_specimens", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_taxa_seasonality", TargetName = "countries", Weight = 60},
            new {  SourceName = "tbl_taxa_seasonality", TargetName = "tbl_activity_types", Weight = 20},
            new {  SourceName = "tbl_taxa_seasonality", TargetName = "tbl_locations", Weight = 60},
            new {  SourceName = "tbl_taxa_seasonality", TargetName = "tbl_seasons", Weight = 20},
            new {  SourceName = "tbl_taxa_seasonality", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_taxa_synonyms", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_taxa_synonyms", TargetName = "tbl_taxa_tree_authors", Weight = 150},
            new {  SourceName = "tbl_taxa_synonyms", TargetName = "tbl_taxa_tree_families", Weight = 150},
            new {  SourceName = "tbl_taxa_synonyms", TargetName = "tbl_taxa_tree_genera", Weight = 150},
            new {  SourceName = "tbl_taxa_synonyms", TargetName = "tbl_taxa_tree_master", Weight = 150},
            new {  SourceName = "tbl_taxa_tree_authors", TargetName = "tbl_taxa_synonyms", Weight = 150},
            new {  SourceName = "tbl_taxa_tree_authors", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_families", TargetName = "tbl_taxa_synonyms", Weight = 150},
            new {  SourceName = "tbl_taxa_tree_families", TargetName = "tbl_taxa_tree_genera", Weight = 1},
            new {  SourceName = "tbl_taxa_tree_families", TargetName = "tbl_taxa_tree_orders", Weight = 1},
            new {  SourceName = "tbl_taxa_tree_genera", TargetName = "tbl_taxa_synonyms", Weight = 150},
            new {  SourceName = "tbl_taxa_tree_genera", TargetName = "tbl_taxa_tree_families", Weight = 1},
            new {  SourceName = "tbl_taxa_tree_genera", TargetName = "tbl_taxa_tree_master", Weight = 1},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "metainformation.view_abundance", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "metainformation.view_abundances_by_taxon_analysis_entity", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "metainformation.view_taxa_biblio", Weight = 2},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_abundances", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_dating_material", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_ecocodes", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_imported_taxa_replacements", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_mcr_names", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_mcr_summary_data", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_mcrdata_birmbeetledat", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_rdb", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_species_associations", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_taxa_common_names", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_taxa_images", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_taxa_measured_attributes", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_taxa_reference_specimens", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_taxa_seasonality", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_taxa_synonyms", Weight = 150},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_taxa_tree_authors", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_taxa_tree_genera", Weight = 1},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_taxonomic_order", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_taxonomy_notes", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_text_biology", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_text_distribution", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_master", TargetName = "tbl_text_identification_keys", Weight = 20},
            new {  SourceName = "tbl_taxa_tree_orders", TargetName = "tbl_record_types", Weight = 1},
            new {  SourceName = "tbl_taxa_tree_orders", TargetName = "tbl_taxa_tree_families", Weight = 1},
            new {  SourceName = "tbl_taxonomic_order", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_taxonomic_order", TargetName = "tbl_taxonomic_order_systems", Weight = 20},
            new {  SourceName = "tbl_taxonomic_order_biblio", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_taxonomic_order_biblio", TargetName = "tbl_taxonomic_order_systems", Weight = 20},
            new {  SourceName = "tbl_taxonomic_order_systems", TargetName = "tbl_taxonomic_order", Weight = 20},
            new {  SourceName = "tbl_taxonomic_order_systems", TargetName = "tbl_taxonomic_order_biblio", Weight = 20},
            new {  SourceName = "tbl_taxonomy_notes", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_taxonomy_notes", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_tephra_dates", TargetName = "tbl_analysis_entities", Weight = 20},
            new {  SourceName = "tbl_tephra_dates", TargetName = "tbl_dating_uncertainty", Weight = 20},
            new {  SourceName = "tbl_tephra_dates", TargetName = "tbl_tephras", Weight = 20},
            new {  SourceName = "tbl_tephra_refs", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_tephra_refs", TargetName = "tbl_tephras", Weight = 20},
            new {  SourceName = "tbl_tephras", TargetName = "tbl_tephra_dates", Weight = 20},
            new {  SourceName = "tbl_tephras", TargetName = "tbl_tephra_refs", Weight = 20},
            new {  SourceName = "tbl_text_biology", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_text_biology", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_text_distribution", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_text_distribution", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_text_identification_keys", TargetName = "tbl_biblio", Weight = 150},
            new {  SourceName = "tbl_text_identification_keys", TargetName = "tbl_taxa_tree_master", Weight = 20},
            new {  SourceName = "tbl_units", TargetName = "tbl_dimensions", Weight = 20},
            new {  SourceName = "tbl_units", TargetName = "tbl_methods", Weight = 150},
            new {  SourceName = "tbl_years_types", TargetName = "tbl_dendro_dates", Weight = 20},
            new {  SourceName = "view_places_relations", TargetName = "countries", Weight = 20},
            new {  SourceName = "view_places_relations", TargetName = "tbl_locations", Weight = 1}

        };


        [Fact]
        public void CanCreateExpectedFacetGraph()
        {
            var container = new TestDependencyService().Register();
            using (var scope = container.BeginLifetimeScope()) {
                var service = scope.Resolve<IFacetsGraph>();
                Assert.Equal(expectedEdges.Length, service.Edges.ToList().Count);
                foreach (var expected in expectedEdges) {
                    var edge = service.GetEdge(expected.SourceName, expected.TargetName);
                    Assert.NotNull(edge);
                    Assert.Equal(expected.Weight, edge.Weight, "Weight mismatch: " + expected.SourceName + " " + expected.TargetName);
                }
            }
        }

        [Fact]
        public void AdjecentCloseNodesShouldHaveSingleEdgeInPath()
        {
            var container = new TestDependencyService().Register();
            using (var scope = container.BeginLifetimeScope()) {
                var graph = scope.Resolve<IFacetsGraph>();

                GraphRoute route = graph.Find("tbl_locations", "tbl_site_locations");

                Assert.NotNull(route);
                Assert.Single(route.Items);
                Assert.Equal("tbl_locations", route.Items[0].SourceName);
                Assert.Equal("tbl_site_locations", route.Items[0].TargetName);
            }
        }

        [Fact]
        public void StartSameAsStopShouldBeEmpty()
        {
            var container = new TestDependencyService().Register();
            using (var scope = container.BeginLifetimeScope()) {
                var graph = scope.Resolve<IFacetsGraph>();

                GraphRoute route = graph.Find("tbl_locations", "tbl_locations");

                Assert.NotNull(route);
                Assert.Empty(route.Items);
            }
        }
    }
}
