namespace MobyDick.TestKit.Data.Templates
{
  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Globalization;
  using MobyDick.TestKit.Data.Extensions;

  /// <summary>
  /// The template manager.
  /// </summary>
  internal class TemplateManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The create template.
    /// </summary>
    /// <param name="tree">
    /// The tree.
    /// </param>
    /// <param name="template">
    /// The template.
    /// </param>
    /// <param name="parentId">
    /// The parent id.
    /// </param>
    public static void CreateTemplate(TTree tree, TTemplate template, ID parentId)
    {
      Assert.ArgumentNotNull(tree, "database");
      Assert.ArgumentNotNull(template, "template");
      Assert.ArgumentNotNull(parentId, "parentId");

      Item item = tree.Database.GetItem(template.ID);

      if (item == null)
      {
        tree.Database.CreateItem(template.ID, template.Name, new TemplateID(TemplateIDs.Template), parentId);
      }

      CreateSections(tree, template);

      tree.Database.Engines.TemplateEngine.Reset();
    }

    #endregion

    #region Methods

    /// <summary>
    /// The create fields.
    /// </summary>
    /// <param name="tree">
    /// The tree.
    /// </param>
    /// <param name="section">
    /// The section.
    /// </param>
    private static void CreateFields(TTree tree, TSection section)
    {
      foreach (TField field in section)
      {
        Item item = tree.Database.GetItem(field.ID);

        if (item == null)
        {
          tree.Database.CreateItem(field.ID, field.Name, new TemplateID(TemplateIDs.TemplateField), section.ID);
        }

        item = tree.Database.GetItem(field.ID, Language.Invariant, Version.Latest);

        item.Edit(i => i.Fields[TemplateFieldIDs.Type].Value = field.Type);
        item.Edit(i => i.Fields[TemplateFieldIDs.Shared].Value = field.Shared ? "1" : "0");
        item.Edit(i => i.Fields[TemplateFieldIDs.Unversioned].Value = field.Unversioned ? "1" : "0");
      }
    }

    /// <summary>
    /// The create sections.
    /// </summary>
    /// <param name="tree">
    /// The tree.
    /// </param>
    /// <param name="template">
    /// The template.
    /// </param>
    private static void CreateSections(TTree tree, TTemplate template)
    {
      foreach (TSection section in template)
      {
        Item item = tree.Database.GetItem(section.ID);

        if (item == null)
        {
          tree.Database.CreateItem(section.ID, section.Name, new TemplateID(TemplateIDs.TemplateSection), template.ID);
        }

        CreateFields(tree, section);
      }
    }

    #endregion
  }
}